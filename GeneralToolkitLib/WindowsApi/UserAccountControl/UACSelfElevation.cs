﻿#region Using directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using Serilog;

#endregion

namespace GeneralToolkitLib.WindowsApi.UserAccountControl
{
    public class CSUACSelfElevation
    {
        private bool _inAdminGroup;
        private string _integrityLevel;
        private bool _isElevated;
        private bool _isRunAsAdmin;

        public bool IsApplicationRunningWithAdminAccess => _isRunAsAdmin;

        public bool IsElevated => _isElevated;

        public string IntegrityLevel => _integrityLevel;

        public bool CurrentUserInAdminGroup => _inAdminGroup;

        public bool DisplayMessageBoxes { get; set; }

        public Button UacButton { get; set; }


        public void Initialize()
        {
            // Get and display whether the primary access token of the process belongs 
            // to user account that is a member of the local Administrators group even 
            // if it currently is not elevated (IsUserInAdminGroup)

            try
            {
                _inAdminGroup = IsUserInAdminGroup();
            }
            catch (Exception ex)
            {
                if (DisplayMessageBoxes)
                    MessageBox.Show(ex.Message, @"An error occurred in IsUserInAdminGroup", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log.Error(ex, "Error when calling IsUserInAdminGroup");
            }

            try
            {
                _isRunAsAdmin = IsRunAsAdmin();
            }
            catch (Exception ex)
            {
                if (DisplayMessageBoxes)
                    MessageBox.Show(ex.Message, @"An error occurred in IsRunAsAdmin", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log.Error(ex,"Error when calling IsRunAsAdmin");
            }


            // Get and display the process elevation information (IsProcessElevated) 
            // and integrity level (GetProcessIntegrityLevel). The information is not 
            // available on operating systems prior to Windows Vista.
            if (Environment.OSVersion.Version.Major >= 6)
            {
                // Running Windows Vista or later (major version >= 6). 

                try
                {
                    // Get and display the process elevation information.
                    _isElevated = IsProcessElevated();

                    // Update the Self-elevate button to show the UAC shield icon on 
                    // the UI if the process is not elevated.
                    if (UacButton != null)
                        NativeMethods.SendMessage(UacButton.Handle, NativeMethods.BCM_SETSHIELD, 0, _isElevated ? IntPtr.Zero : (IntPtr) 1);
                }
                catch (Exception ex)
                {
                    if (DisplayMessageBoxes)
                        MessageBox.Show(ex.Message, @"An error occurred in IsProcessElevated", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Log.Error(ex,"Error when calling NativeMethods.SendMessage");
                }

                try
                {
                    // Get and display the process integrity level.
                    int IL = GetProcessIntegrityLevel();
                    switch (IL)
                    {
                        case NativeMethods.SECURITY_MANDATORY_UNTRUSTED_RID:
                            _integrityLevel = "Untrusted";
                            break;
                        case NativeMethods.SECURITY_MANDATORY_LOW_RID:
                            _integrityLevel = "Low";
                            break;
                        case NativeMethods.SECURITY_MANDATORY_MEDIUM_RID:
                            _integrityLevel = "Medium";
                            break;
                        case NativeMethods.SECURITY_MANDATORY_HIGH_RID:
                            _integrityLevel = "High";
                            break;
                        case NativeMethods.SECURITY_MANDATORY_SYSTEM_RID:
                            _integrityLevel = "System";
                            break;
                        default:
                            _integrityLevel = "Unknown";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (DisplayMessageBoxes)
                        MessageBox.Show(ex.Message, @"An error occurred in GetProcessIntegrityLevel", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Log.Error(ex,"Error when calling GetProcessIntegrityLevel()");
                }
            }
        }


        public void ElevateProcessToRunAsAdmin()
        {
            // Elevate the process if it is not run as administrator.
            if (!IsRunAsAdmin())
            {
                // Launch itself as administrator
                var proc = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas"
                };

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    // The user refused the elevation.
                    // Do nothing and return directly ...
                    return;
                }

                Application.Exit(); // Quit itself
                return;
            }
            if (DisplayMessageBoxes)
                MessageBox.Show("The process is running as administrator", "UAC");
            Log.Information("The process is running as administrator");
        }

        #region Helper Functions for Admin Privileges and Elevation Status


        public CSUACSelfElevation(bool displayMessagBoxes)
        {
            DisplayMessageBoxes = displayMessagBoxes;
        }

        /// <summary>
        ///     The function checks whether the primary access token of the process belongs
        ///     to user account that is a member of the local Administrators group, even if
        ///     it currently is not elevated.
        /// </summary>
        /// <returns>
        ///     Returns true if the primary access token of the process belongs to user
        ///     account that is a member of the local Administrators group. Returns false
        ///     if the token does not.
        /// </returns>
        /// <exception cref="System.ComponentModel.Win32Exception">
        ///     When any native Windows API call fails, the function throws a Win32Exception
        ///     with the last error code.
        /// </exception>
        private bool IsUserInAdminGroup()
        {
            bool fInAdminGroup = false;
            SafeTokenHandle hToken = null;
            SafeTokenHandle hTokenToCheck = null;
            IntPtr pElevationType = IntPtr.Zero;
            IntPtr pLinkedToken = IntPtr.Zero;
            int cbSize = 0;

            try
            {
                // Open the access token of the current process for query and duplicate.
                if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle,
                    NativeMethods.TOKEN_QUERY | NativeMethods.TOKEN_DUPLICATE, out hToken))
                    throw new Win32Exception();

                // Determine whether system is running Windows Vista or later operating 
                // systems (major version >= 6) because they support linked tokens, but 
                // previous versions (major version < 6) do not.
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    // Running Windows Vista or later (major version >= 6). 
                    // Determine token type: limited, elevated, or default. 

                    // Allocate a buffer for the elevation type information.
                    cbSize = sizeof (TOKEN_ELEVATION_TYPE);
                    pElevationType = Marshal.AllocHGlobal(cbSize);
                    if (pElevationType == IntPtr.Zero)
                        throw new Win32Exception();

                    // Retrieve token elevation type information.
                    if (!NativeMethods.GetTokenInformation(hToken,
                        TOKEN_INFORMATION_CLASS.TokenElevationType, pElevationType,
                        cbSize, out cbSize))
                        throw new Win32Exception();

                    // Marshal the TOKEN_ELEVATION_TYPE enum from native to .NET.
                    var elevType = (TOKEN_ELEVATION_TYPE)
                                   Marshal.ReadInt32(pElevationType);

                    // If limited, get the linked elevated token for further check.
                    if (elevType == TOKEN_ELEVATION_TYPE.TokenElevationTypeLimited)
                    {
                        // Allocate a buffer for the linked token.
                        cbSize = IntPtr.Size;
                        pLinkedToken = Marshal.AllocHGlobal(cbSize);
                        if (pLinkedToken == IntPtr.Zero)
                            throw new Win32Exception();

                        // Get the linked token.
                        if (!NativeMethods.GetTokenInformation(hToken,
                            TOKEN_INFORMATION_CLASS.TokenLinkedToken, pLinkedToken,
                            cbSize, out cbSize))
                            throw new Win32Exception();

                        // Marshal the linked token value from native to .NET.
                        IntPtr hLinkedToken = Marshal.ReadIntPtr(pLinkedToken);
                        hTokenToCheck = new SafeTokenHandle(hLinkedToken);
                    }
                }

                // CheckTokenMembership requires an impersonation token. If we just got 
                // a linked token, it already is an impersonation token.  If we did not 
                // get a linked token, duplicate the original into an impersonation 
                // token for CheckTokenMembership.
                if (hTokenToCheck == null)
                    if (!NativeMethods.DuplicateToken(hToken,
                        SECURITY_IMPERSONATION_LEVEL.SecurityIdentification,
                        out hTokenToCheck))
                        throw new Win32Exception();

                // Check if the token to be checked contains admin SID.
                var id = new WindowsIdentity(hTokenToCheck.DangerousGetHandle());
                var principal = new WindowsPrincipal(id);
                fInAdminGroup = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            finally
            {
                // Centralized cleanup for all allocated resources. 
                if (hToken != null)
                {
                    hToken.Close();
                    hToken = null;
                }
                if (hTokenToCheck != null)
                {
                    hTokenToCheck.Close();
                    hTokenToCheck = null;
                }
                if (pElevationType != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pElevationType);
                    pElevationType = IntPtr.Zero;
                }
                if (pLinkedToken != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pLinkedToken);
                    pLinkedToken = IntPtr.Zero;
                }
            }

            return fInAdminGroup;
        }


        /// <summary>
        ///     The function checks whether the current process is run as administrator.
        ///     In other words, it dictates whether the primary access token of the
        ///     process belongs to user account that is a member of the local
        ///     Administrators group and it is elevated.
        /// </summary>
        /// <returns>
        ///     Returns true if the primary access token of the process belongs to user
        ///     account that is a member of the local Administrators group and it is
        ///     elevated. Returns false if the token does not.
        /// </returns>
        internal bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }


        /// <summary>
        ///     The function gets the elevation information of the current process. It
        ///     dictates whether the process is elevated or not. Token elevation is only
        ///     available on Windows Vista and newer operating systems, thus
        ///     IsProcessElevated throws a C++ exception if it is called on systems prior
        ///     to Windows Vista. It is not appropriate to use this function to determine
        ///     whether a process is run as administartor.
        /// </summary>
        /// <returns>
        ///     Returns true if the process is elevated. Returns false if it is not.
        /// </returns>
        /// <exception cref="System.ComponentModel.Win32Exception">
        ///     When any native Windows API call fails, the function throws a Win32Exception
        ///     with the last error code.
        /// </exception>
        /// <remarks>
        ///     TOKEN_INFORMATION_CLASS provides TokenElevationType to check the elevation
        ///     type (TokenElevationTypeDefault / TokenElevationTypeLimited /
        ///     TokenElevationTypeFull) of the process. It is different from TokenElevation
        ///     in that, when UAC is turned off, elevation type always returns
        ///     TokenElevationTypeDefault even though the process is elevated (Integrity
        ///     Level == High). In other words, it is not safe to say if the process is
        ///     elevated based on elevation type. Instead, we should use TokenElevation.
        /// </remarks>
        internal bool IsProcessElevated()
        {
            bool fIsElevated;
            SafeTokenHandle hToken = null;
            int cbTokenElevation;
            IntPtr pTokenElevation = IntPtr.Zero;

            try
            {
                // Open the access token of the current process with TOKEN_QUERY.
                if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle,
                    NativeMethods.TOKEN_QUERY, out hToken))
                    throw new Win32Exception();

                // Allocate a buffer for the elevation information.
                cbTokenElevation = Marshal.SizeOf(typeof (TOKEN_ELEVATION));
                pTokenElevation = Marshal.AllocHGlobal(cbTokenElevation);
                if (pTokenElevation == IntPtr.Zero)
                    throw new Win32Exception();

                // Retrieve token elevation information.
                if (!NativeMethods.GetTokenInformation(hToken,
                    TOKEN_INFORMATION_CLASS.TokenElevation, pTokenElevation,
                    cbTokenElevation, out cbTokenElevation))
                    // When the process is run on operating systems prior to Windows 
                    // Vista, GetTokenInformation returns false with the error code 
                    // ERROR_INVALID_PARAMETER because TokenElevation is not supported 
                    // on those operating systems.
                    throw new Win32Exception();

                // Marshal the TOKEN_ELEVATION struct from native to .NET object.
                var elevation = (TOKEN_ELEVATION) Marshal.PtrToStructure(
                    pTokenElevation, typeof (TOKEN_ELEVATION));

                // TOKEN_ELEVATION.TokenIsElevated is a non-zero value if the token 
                // has elevated privileges; otherwise, a zero value.
                fIsElevated = (elevation.TokenIsElevated != 0);
            }
            finally
            {
                // Centralized cleanup for all allocated resources. 
                if (hToken != null)
                {
                    hToken.Close();
                    hToken = null;
                }
                if (pTokenElevation != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pTokenElevation);
                    pTokenElevation = IntPtr.Zero;
                    cbTokenElevation = 0;
                }
            }

            return fIsElevated;
        }


        /// <summary>
        ///     The function gets the integrity level of the current process. Integrity
        ///     level is only available on Windows Vista and newer operating systems, thus
        ///     GetProcessIntegrityLevel throws a C++ exception if it is called on systems
        ///     prior to Windows Vista.
        /// </summary>
        /// <returns>
        ///     Returns the integrity level of the current process. It is usually one of
        ///     these values:
        ///     SECURITY_MANDATORY_UNTRUSTED_RID - means untrusted level. It is used
        ///     by processes started by the Anonymous group. Blocks most write access.
        ///     (SID: S-1-16-0x0)
        ///     SECURITY_MANDATORY_LOW_RID - means low integrity level. It is used by
        ///     Protected Mode Internet Explorer. Blocks write acess to most objects
        ///     (such as files and registry keys) on the system. (SID: S-1-16-0x1000)
        ///     SECURITY_MANDATORY_MEDIUM_RID - means medium integrity level. It is
        ///     used by normal applications being launched while UAC is enabled.
        ///     (SID: S-1-16-0x2000)
        ///     SECURITY_MANDATORY_HIGH_RID - means high integrity level. It is used
        ///     by administrative applications launched through elevation when UAC is
        ///     enabled, or normal applications if UAC is disabled and the user is an
        ///     administrator. (SID: S-1-16-0x3000)
        ///     SECURITY_MANDATORY_SYSTEM_RID - means system integrity level. It is
        ///     used by services and other system-level applications (such as Wininit,
        ///     Winlogon, Smss, etc.)  (SID: S-1-16-0x4000)
        /// </returns>
        /// <exception cref="System.ComponentModel.Win32Exception">
        ///     When any native Windows API call fails, the function throws a Win32Exception
        ///     with the last error code.
        /// </exception>
        private int GetProcessIntegrityLevel()
        {
            int IL = -1;
            SafeTokenHandle hToken = null;
            int cbTokenIL = 0;
            IntPtr pTokenIL = IntPtr.Zero;

            try
            {
                // Open the access token of the current process with TOKEN_QUERY.
                if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle,
                    NativeMethods.TOKEN_QUERY, out hToken))
                    throw new Win32Exception();

                // Then we must query the size of the integrity level information 
                // associated with the token. Note that we expect GetTokenInformation 
                // to return false with the ERROR_INSUFFICIENT_BUFFER error code 
                // because we've given it a null buffer. On exit cbTokenIL will tell 
                // the size of the group information.
                if (!NativeMethods.GetTokenInformation(hToken,
                    TOKEN_INFORMATION_CLASS.TokenIntegrityLevel, IntPtr.Zero, 0,
                    out cbTokenIL))
                {
                    int error = Marshal.GetLastWin32Error();
                    if (error != NativeMethods.ERROR_INSUFFICIENT_BUFFER)
                        // When the process is run on operating systems prior to 
                        // Windows Vista, GetTokenInformation returns false with the 
                        // ERROR_INVALID_PARAMETER error code because 
                        // TokenIntegrityLevel is not supported on those OS's.
                        throw new Win32Exception(error);
                }

                // Now we allocate a buffer for the integrity level information.
                pTokenIL = Marshal.AllocHGlobal(cbTokenIL);
                if (pTokenIL == IntPtr.Zero)
                    throw new Win32Exception();

                // Now we ask for the integrity level information again. This may fail 
                // if an administrator has added this account to an additional group 
                // between our first call to GetTokenInformation and this one.
                if (!NativeMethods.GetTokenInformation(hToken,
                    TOKEN_INFORMATION_CLASS.TokenIntegrityLevel, pTokenIL, cbTokenIL,
                    out cbTokenIL))
                    throw new Win32Exception();

                // Marshal the TOKEN_MANDATORY_LABEL struct from native to .NET object.
                var tokenIL = (TOKEN_MANDATORY_LABEL)
                              Marshal.PtrToStructure(pTokenIL, typeof (TOKEN_MANDATORY_LABEL));

                // Integrity Level SIDs are in the form of S-1-16-0xXXXX. (e.g. 
                // S-1-16-0x1000 stands for low integrity level SID). There is one 
                // and only one subauthority.
                IntPtr pIL = NativeMethods.GetSidSubAuthority(tokenIL.Label.Sid, 0);
                IL = Marshal.ReadInt32(pIL);
            }
            finally
            {
                // Centralized cleanup for all allocated resources. 
                if (hToken != null)
                {
                    hToken.Close();
                    hToken = null;
                }
                if (pTokenIL != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pTokenIL);
                    pTokenIL = IntPtr.Zero;
                    cbTokenIL = 0;
                }
            }

            return IL;
        }

        #endregion
    }
}