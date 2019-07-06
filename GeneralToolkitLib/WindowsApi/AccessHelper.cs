using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Serilog;


namespace GeneralToolkitLib.WindowsApi
{
    public class AccessHelper
    {
        private readonly WindowsIdentity _winId;

        public AccessHelper()
        {
            _winId = WindowsIdentity.GetCurrent();
        }

        public bool UserHasReadAccessToDirectory(DirectoryInfo directoryInfo)
        {
            try
            {
                DirectorySecurity dSecurity = directoryInfo.GetAccessControl();
                AuthorizationRuleCollection authorizarionRuleCollecion = dSecurity.GetAccessRules(true, true, typeof (SecurityIdentifier));

                foreach (FileSystemAccessRule fsAccessRules in authorizarionRuleCollecion)
                {
                    if (_winId.UserClaims.Any(c => c.Value == fsAccessRules.IdentityReference.Value) &&
                        fsAccessRules.FileSystemRights.HasFlag(FileSystemRights.ReadData) && fsAccessRules.AccessControlType == AccessControlType.Allow)
                        return true;
                }
                return false;
            }
            catch (UnauthorizedAccessException accessException)
            {
                Log.Information(accessException.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Error in DirectoryData.UserHasReadAccessToDirectory - ");
            }
            return false;
        }
    }
}