using System;
using System.Collections.Generic;

namespace DVLDBusinessLayer
{
    public static class clsUserPermission
    {
        public enum Permissions { eShowPeopleList = 1, eShowPersonDetails = 2, eAddNewPerson = 4, eEditPerson = 8, eDeletePerson = 16, eShowUsersList = 32, eShowUserDetails = 64, eAddNewUser = 128, 
            eEditUser = 256, eDeleteUser = 512, eChangeUserPassword = 1024, eShowApplicationTypesList = 2048, eEditApplicationType = 4096, eShowTestTypesList = 8192, eEditTestType = 16384,
            eAddNewLocalDrivingLicenseApplication = 32768, eShowLocalDrivingLicenseApplicationsList = 65536, eCancelLocalDrivingLicenseApplication = 131072, eDeleteLocalDrivingLicenseApplication = 262144,

        };

        public static readonly Dictionary<Permissions, string> PermissionDescriptions = new Dictionary<Permissions, string>
        {
            { Permissions.eShowPeopleList, "Show People List" },
            { Permissions.eShowPersonDetails, "Show Person Details" },
            { Permissions.eAddNewPerson, "Add New Person" },
            { Permissions.eEditPerson, "Edit Person" },
            { Permissions.eDeletePerson, "Delete Person" },
            { Permissions.eShowUsersList, "Show Users List" },
            { Permissions.eShowUserDetails, "Show User Details" },
            { Permissions.eAddNewUser, "Add New User" },
            { Permissions.eEditUser, "Edit User" },
            { Permissions.eDeleteUser, "Delete User" },
            { Permissions.eChangeUserPassword, "Change User Password" },
            { Permissions.eShowApplicationTypesList, "Show Application Types List" },
            { Permissions.eEditApplicationType, "Edit Application Type" },
            { Permissions.eShowTestTypesList, "Show Test Types List" },
            { Permissions.eEditTestType, "Edit Test Type" },
            { Permissions.eAddNewLocalDrivingLicenseApplication, "Add New Local Driving License Application" },
            { Permissions.eShowLocalDrivingLicenseApplicationsList, "Show Local Driving License Applications List" },
            { Permissions.eCancelLocalDrivingLicenseApplication, "Cancel Local Driving License Application" },
            { Permissions.eDeleteLocalDrivingLicenseApplication, "Delete Local Driving License Application" },

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

        private static int _GetPermissionValue(Permissions Permission)
        {
            int PermissionValue = (int)Permission;
            
            return PermissionValue;
        }

        private static bool _IsAdmin() 
        {
            return (clsGlobal.CurrentUser.Permissions == -1);
        }

        public static bool CheckUserPermissions(Permissions Permission) 
        {
            if(_IsAdmin())
                return true;

            int PermissionValue = _GetPermissionValue(Permission);

            return ((clsGlobal.CurrentUser.Permissions & PermissionValue) == PermissionValue);
        }
    }
}
