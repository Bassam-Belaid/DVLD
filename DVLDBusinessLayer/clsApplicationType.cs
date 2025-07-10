using DVLDDataAccessLayer;
using System.Data;

namespace DVLDBusinessLayer
{
    public class clsApplicationType
    {
        private int _ApplicationTypeID;
        private string _ApplicationTypeTitle;
        private decimal _ApplicationFees;

        public clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees) 
        {
            _ApplicationTypeID = ApplicationTypeID;
            _ApplicationTypeTitle = ApplicationTypeTitle;
            _ApplicationFees = ApplicationFees;
        }

        public int GetApplicationTypeID() { return _ApplicationTypeID; }
        public string GetApplicationTypeTitle() { return _ApplicationTypeTitle; }
        public decimal GetApplicationFees() { return _ApplicationFees; }

        public void SetApplicationTypeTitle(string ApplicationTypeTitle) { _ApplicationTypeTitle = ApplicationTypeTitle.Trim(); }
        public void SetApplicationFees(decimal ApplicationFees) {  _ApplicationFees = ApplicationFees; }

        public static DataTable GetAllApplicationTypes() 
        { 
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
        public static clsApplicationType GetApplicationTypeByApplicationTypeID(int ApplicationTypeID) 
        {
            string ApplicationTypeTitle = null;
            decimal ApplicationFees = 0;

            if(clsApplicationTypeData.GetApplicationTypeByApplicationTypeID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);

            else
                return null;
        }
        public static decimal GetApplicationTypeFeesByApplicationTypeTitle(string ApplicationTypeTitle) 
        {
            return clsApplicationTypeData.GetApplicationTypeFeesByApplicationTypeTitle(ApplicationTypeTitle);
        }
        public static int GetApplicationTypeIDByApplicationTypeTitle(string ApplicationTypeTitle) 
        {
            return clsApplicationTypeData.GetApplicationTypeIDByApplicationTypeTitle(ApplicationTypeTitle);
        }
        public static bool IsApplicationTypeExistsByApplicationTypeTitle(string ApplicationTypeTitle) 
        {
            return clsApplicationTypeData.IsApplicationTypeExistsByApplicationTypeTitle(ApplicationTypeTitle);
        }
        public bool Save()
        {
            return clsApplicationTypeData.UpdateApplicationType(_ApplicationTypeID, _ApplicationTypeTitle, _ApplicationFees);
        }

        public static string GetApplicationTypeTitleByApplicationTypeID(int ApplicationTypeID) 
        {
            return clsApplicationTypeData.GetApplicationTypeTitleByApplicationTypeID(ApplicationTypeID);
        }
    }
}
