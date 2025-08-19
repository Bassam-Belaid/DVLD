using System;
using System.Collections.Generic;

namespace DVLDBusinessLayer
{
    public static class clsUserPermission
    {
        public enum enPermissions { eManagePeople = 1, eManageUsers = 2, eManageApplicationTypes = 4, eManageTestTypes = 8,
            eAddNewLocalDrivingLicenseApplication = 16, eManageLocalDrivingLicenseApplications = 32
        };

        public static readonly Dictionary<enPermissions, string> PermissionDescriptions = new Dictionary<enPermissions, string>
        {
            { enPermissions.eManagePeople, "Manage People" },
            { enPermissions.eManageUsers, "Manage Users" },
            { enPermissions.eManageApplicationTypes, "Manage Application" },
            { enPermissions.eManageTestTypes, "Manage Test Types" },
            { enPermissions.eAddNewLocalDrivingLicenseApplication, "Add New Local Driving License Application" },
            { enPermissions.eManageLocalDrivingLicenseApplications, "Manage Local Driving License Applications" },
        };

        public static int GetPermissionValueByPermissionDescription(string PermissionDescription)
        {
           foreach (var Permission in PermissionDescriptions)
           {
               if (Permission.Value.Equals(PermissionDescription, StringComparison.OrdinalIgnoreCase))
               {
                   return _GetPermissionValue(Permission.Key);
               }
           }

           return -1;
        }

        private static int _GetPermissionValue(enPermissions Permission)
        {
            int PermissionValue = (int)Permission;
            
            return PermissionValue;
        }

        private static bool _IsAdmin() 
        {
            return (clsGlobal.CurrentUser.Permissions == -1);
        }

        public static bool CheckUserPermissions(enPermissions Permission) 
        {
            if(_IsAdmin())
                return true;

            int PermissionValue = _GetPermissionValue(Permission);

            return ((clsGlobal.CurrentUser.Permissions & PermissionValue) == PermissionValue);
        }
    }
}
