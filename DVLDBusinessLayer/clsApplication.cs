using System;

namespace DVLDBusinessLayer
{
    public abstract class clsApplication
    {
        protected enum enApplicationStatus {eNew = 1, eCanceled = 2, eCompleted = 3}

        protected int ApplicationID;
        public int ApplicantPersonID { get; set; } 
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        protected byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
    }
}
