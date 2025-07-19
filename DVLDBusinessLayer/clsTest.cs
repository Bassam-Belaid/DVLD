using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        public static byte CountNumberOfTestTrials(int LDLAppID, clsTestType.enTestTypes TestType) 
        {
            return clsTestData.CountNumberOfTestTrials(LDLAppID, (int)TestType);
        }
    }
}
