using System;
using System.Runtime.InteropServices;

namespace GeneralToolkitLib.WindowsApi.UserAccountControl
{
    internal static class NativeMethods
    {
        // Token Specific Access Rights
        private const uint StandardRightsRequired = 0x000F0000;
        private const uint StandardRightsRead = 0x00020000;
        private const uint TokenAssignPrimary = 0x0001;
        public const uint TokenDuplicate = 0x0002;
        private const uint TokenImpersonate = 0x0004;
        public const uint TokenQuery = 0x0008;
        private const uint TokenQuerySource = 0x0010;
        private const uint TokenAdjustPrivileges = 0x0020;
        private const uint TokenAdjustGroups = 0x0040;
        private const uint TokenAdjustDefault = 0x0080;
        private const uint TokenAdjustSessionId = 0x0100;
        private const uint TokenRead = StandardRightsRead | TokenQuery;

        public const uint TokenAllAccess = StandardRightsRequired |
                                           TokenAssignPrimary | TokenDuplicate | TokenImpersonate |
                                           TokenQuery | TokenQuerySource | TokenAdjustPrivileges |
                                           TokenAdjustGroups | TokenAdjustDefault | TokenAdjustSessionId;

        public const int ErrorInsufficientBuffer = 122;

        // Integrity Levels
        public const int SecurityMandatoryUntrustedRid = 0x00000000;
        public const int SecurityMandatoryLowRid = 0x00001000;
        public const int SecurityMandatoryMediumRid = 0x00002000;
        public const int SecurityMandatoryHighRid = 0x00003000;
        public const int SecurityMandatorySystemRid = 0x00004000;

        /// <summary>
        ///     Sets the elevation required state for a specified button or
        ///     command link to display an elevated icon.
        /// </summary>
        public const uint BcmSetShield = 0x160C;

        /// <summary>
        ///     The function opens the access token associated with a process.
        /// </summary>
        /// <param name="hProcess">
        ///     A handle to the process whose access token is opened.
        /// </param>
        /// <param name="desiredAccess">
        ///     Specifies an access mask that specifies the requested types of
        ///     access to the access token.
        /// </param>
        /// <param name="hToken">
        ///     Outputs a handle that identifies the newly opened access token
        ///     when the function returns.
        /// </param>
        /// <returns></returns>
        [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr hProcess, uint desiredAccess, out SafeTokenHandle hToken);

        /// <summary>
        ///     The function creates a new access token that duplicates one
        ///     already in existence.
        /// </summary>
        /// <param name="existingTokenHandle">
        ///     A handle to an access token opened with TOKEN_DUPLICATE access.
        /// </param>
        /// <param name="impersonationLevel">
        ///     Specifies a SECURITY_IMPERSONATION_LEVEL enumerated type that
        ///     supplies the impersonation level of the new token.
        /// </param>
        /// <param name="duplicateTokenHandle">
        ///     Outputs a handle to the duplicate token.
        /// </param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DuplicateToken(
            SafeTokenHandle            existingTokenHandle,
            SecurityImpersonationLevel impersonationLevel,
            out SafeTokenHandle        duplicateTokenHandle);

        /// <summary>
        ///     The function retrieves a specified type of information about an
        ///     access token. The calling process must have appropriate access
        ///     rights to obtain the information.
        /// </summary>
        /// <param name="hToken">
        ///     A handle to an access token from which information is retrieved.
        /// </param>
        /// <param name="tokenInfoClass">
        ///     Specifies a value from the TOKEN_INFORMATION_CLASS enumerated
        ///     type to identify the type of information the function retrieves.
        /// </param>
        /// <param name="pTokenInfo">
        ///     A pointer to a buffer the function fills with the requested
        ///     information.
        /// </param>
        /// <param name="tokenInfoLength">
        ///     Specifies the size, in bytes, of the buffer pointed to by the
        ///     TokenInformation parameter.
        /// </param>
        /// <param name="returnLength">
        ///     A pointer to a variable that receives the number of bytes needed
        ///     for the buffer pointed to by the TokenInformation parameter.
        /// </param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTokenInformation(
            SafeTokenHandle       hToken,
            TokenInformationClass tokenInfoClass,
            IntPtr                pTokenInfo,
            int                   tokenInfoLength,
            out int               returnLength);

        /// <summary>
        ///     Sends the specified message to a window or windows. The function
        ///     calls the window procedure for the specified window and does not
        ///     return until the window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window whose window procedure will receive the
        ///     message.
        /// </param>
        /// <param name="msg">Specifies the message to be sent.</param>
        /// <param name="wParam">
        ///     Specifies additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Specifies additional message-specific information.
        /// </param>
        /// <returns></returns>
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);

        /// <summary>
        ///     The function returns a pointer to a specified subauthority in a
        ///     security identifier (SID). The subauthority value is a relative
        ///     identifier (RID).
        /// </summary>
        /// <param name="pSid">
        ///     A pointer to the SID structure from which a pointer to a
        ///     subauthority is to be returned.
        /// </param>
        /// <param name="nSubAuthority">
        ///     Specifies an index value identifying the subauthority array
        ///     element whose address the function will return.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a pointer to the
        ///     specified SID subauthority. To get extended error information,
        ///     call GetLastError. If the function fails, the return value is
        ///     undefined. The function fails if the specified SID structure is
        ///     not valid or if the index value specified by the nSubAuthority
        ///     parameter is out of bounds.
        /// </returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetSidSubAuthority(IntPtr pSid, uint nSubAuthority);
    }
}