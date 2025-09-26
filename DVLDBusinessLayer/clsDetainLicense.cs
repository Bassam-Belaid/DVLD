using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsDetainLicense
    {
        private enum enMode { eAddNew = 0, eUpdate = 1 }

        private enMode _Mode;

        private int _DetainID;
        public int GetDetainID() { return _DetainID; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

        public clsDetainLicense() 
        {
            this._Mode = enMode.eAddNew;

            this._DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
        }

        public clsDetainLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID,
             bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this._Mode = enMode.eUpdate;

            this._DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
        }

        private bool _AddNewDetain() 
        {
            this._DetainID = clsDetainLicenseData.AddNewDetain(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return (this._DetainID != -1);
        }

        public bool ReleaseDetain()
        {
            this.ReleaseApplicationID = clsDetainLicenseData.ReleaseDetain(this._DetainID, this.LicenseID, this.ReleaseDate, this.CreatedByUserID, this.ReleasedByUserID);

            return (this.ReleaseApplicationID != -1);
        }

        public static clsDetainLicense GetDetainedLicenseByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.MinValue, ReleaseDate = DateTime.MinValue;
            decimal FineFees = 0;
            bool IsReleased = false;

            if(clsDetainLicenseData.GetDetainedLicenseByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, 
                ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);

            else 
                return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAddNew:
                    if (_AddNewDetain())
                    {
                        _Mode = enMode.eUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    } 
            }

            return false;
        }

        public static DataTable GetAllDetainLicenses() 
        {
            return clsDetainLicenseData.GetAllDetainLicenses();
        }

        public static DataTable GetAllDetainLicensesFilteredByDetainID(int DetainID) 
        {
            return clsDetainLicenseData.GetAllDetainLicensesFilteredByDetainID(DetainID);
        }

        public static DataTable GetAllDetainLicensesFilteredByReleaseStatus(bool ReleaseStatus) 
        {
            return clsDetainLicenseData.GetAllDetainLicensesFilteredByReleaseStatus(ReleaseStatus);
        }

        public static DataTable GetAllDetainLicensesFilteredByFullName(string FullName)
        {
            FullName += "%";
            return clsDetainLicenseData.GetAllDetainLicensesFilteredByFullName(FullName);
        }

        public static DataTable GetAllDetainLicensesFilteredByNationalNo(string NationalNo)
        {
            NationalNo += "%";
            return clsDetainLicenseData.GetAllDetainLicensesFilteredByNationalNo(NationalNo);
        }

        public static DataTable GetAllDetainLicensesFilteredByReleaseApplicationID(int ReleaseApplicationID)
        {
            return clsDetainLicenseData.GetAllDetainLicensesFilteredByReleaseApplicationID(ReleaseApplicationID);
        }

    }
}
