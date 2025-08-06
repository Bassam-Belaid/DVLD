using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLDBusinessLayer
{
    public class clsLicense
    {
        private enum enIssueReasons { eNew = 1, eLostReplacement = 2, eDamagedReplacement = 3, eRenewal = 4 }

        private enum enMode { eAddNew = 0, eUpdate = 1 }

        private enMode _Mode;

        private int _LicenseID;

        public int GetLicenseID() { return _LicenseID; }

        public int ApplicationID { set; get; }

        public int DriverID { set; get; }

        public int LicenseClassID { set; get; }

        public DateTime IssueDate { set; get; }

        public DateTime ExpirationDate { set; get; }

        public string Notes { set; get; }

        public decimal PaidFees { set; get; }

        public bool IsActive { set; get; }

        private enIssueReasons IssueReason { set; get; }

        public int CreatedByUserID { set; get; }

        public clsLicense() 
        {
            this._Mode = enMode.eAddNew;
            this._LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReasons.eNew;
            this.CreatedByUserID = -1;
        }

        private bool _IssueNewsLicense() 
        {
            this._LicenseID = clsLicenseData.IssueNewsLicense(this.ApplicationID, this.LicenseClassID, this.Notes, this.CreatedByUserID);

            return (this._LicenseID != -1);
        }

        public bool Save() 
        {
            switch (this._Mode) 
            {
                case enMode.eAddNew:
                    if(_IssueNewsLicense()) 
                    {
                        this._Mode = enMode.eUpdate;
                        return true;
                    }

                    else
                        return false;
            }

            return false;
        }

    }
}
