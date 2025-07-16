using System;
using System.Collections.Generic;

namespace DVLDBusinessLayer
{
    public static class clsUserPermission
    {
        public enum enPermissions { eShowPeopleList = 1, eShowPersonDetails = 2, eAddNewPerson = 4, eEditPerson = 8, eDeletePerson = 16, eShowUsersList = 32, eShowUserDetails = 64, eAddNewUser = 128, 
            eEditUser = 256, eDeleteUser = 512, eChangeUserPassword = 1024, eShowApplicationTypesList = 2048, eEditApplicationType = 4096, eShowTestTypesList = 8192, eEditTestType = 16384,
            eAddNewLocalDrivingLicenseApplication = 32768, eShowLocalDrivingLicenseApplicationsList = 65536, eCancelLocalDrivingLicenseApplication = 131072, eDeleteLocalDrivingLicenseApplication = 262144,

        };

        public static readonly Dictionary<enPermissions, string> PermissionDescriptions = new Dictionary<enPermissions, string>
        {
            { enPermissions.eShowPeopleList, "Show People List" },
            { enPermissions.eShowPersonDetails, "Show Person Details" },
            { enPermissions.eAddNewPerson, "Add New Person" },
            { enPermissions.eEditPerson, "Edit Person" },
            { enPermissions.eDeletePerson, "Delete Person" },
            { enPermissions.eShowUsersList, "Show Users List" },
            { enPermissions.eShowUserDetails, "Show User Details" },
            { enPermissions.eAddNewUser, "Add New User" },
            { enPermissions.eEditUser, "Edit User" },
            { enPermissions.eDeleteUser, "Delete User" },
            { enPermissions.eChangeUserPassword, "Change User Password" },
            { enPermissions.eShowApplicationTypesList, "Show Application Types List" },
            { enPermissions.eEditApplicationType, "Edit Application Type" },
            { enPermissions.eShowTestTypesList, "Show Test Types List" },
            { enPermissions.eEditTestType, "Edit Test Type" },
            { enPermissions.eAddNewLocalDrivingLicenseApplication, "Add New Local Driving License Application" },
            { enPermissions.eShowLocalDrivingLicenseApplicationsList, "Show Local Driving License Applications List" },
            { enPermissions.eCancelLocalDrivingLicenseApplication, "Cancel Local Driving License Application" },
            { enPermissions.eDeleteLocalDrivingLicenseApplication, "Delete Local Driving License Application" },

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
