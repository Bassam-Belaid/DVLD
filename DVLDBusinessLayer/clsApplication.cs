using System;

namespace DVLDBusinessLayer
{
    public abstract class clsApplication
    {
        public int ApplicantPersonID { get; set; } 
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
    }
}
