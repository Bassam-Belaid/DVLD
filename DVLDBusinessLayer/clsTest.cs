using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        private int _TestID;
        public int GetTestID() { return _TestID; }

        public int TestAppointmentID { set; get; }   

        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsTest() 
        {
            _TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = null;
            this.CreatedByUserID = -1;
        }

        private bool _AddNewTest() 
        {
            _TestID = clsTestData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return (_TestID != -1);
        }

        public bool Save() 
        {
            return _AddNewTest();
        }

        public static byte CountNumberOfTestTrials(int LDLAppID, clsTestType.enTestTypes TestType) 
        {
            return clsTestData.CountNumberOfTestTrials(LDLAppID, (int)TestType);
        }
    }
}
