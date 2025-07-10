using DVLDDataAccessLayer;
using System.Data;

namespace DVLDBusinessLayer
{
    public class clsTestType
    {
        private int _TestTypeID;
        private string _TestTypeTitle;
        private string _TestTypeDescription;
        private decimal _TestTypeFees;

        public static int NumberOfTestTypes = 3;

        public clsTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            _TestTypeID = TestTypeID;
            _TestTypeTitle = TestTypeTitle;
            _TestTypeDescription = TestTypeDescription;
            _TestTypeFees = TestTypeFees;
        }

        public int GetTestTypeID() { return _TestTypeID; }
        public string GetTestTypeTitle() { return _TestTypeTitle; }
        public string GetTestTypeDescription() { return _TestTypeDescription; }
        public decimal GetTestTypeFees() { return _TestTypeFees; }

        public void SetTestTypeTitle(string TestTypeTitle) { _TestTypeTitle = TestTypeTitle.Trim(); }
        public void SetTestTypeDescription(string TestTypeDescription) { _TestTypeDescription = TestTypeDescription.Trim(); }
        public void SetTestTypeFees(decimal TestTypeFees) { _TestTypeFees = TestTypeFees; }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }
        public static clsTestType GetTestTypeByTestTypeID(int TestTypeID)
        {
            string TestTypeTitle = null;
            string TestTypeDescription = null;
            decimal TestTypeFees = 0;

            if (clsTestTypeData.GetTestTypeByTestTypeID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);

            else
                return null;
        }
        public static bool IsTestTypeExistsByTestTypeTitle(string TestTypeTitle)
        {
            return clsTestTypeData.IsTestTypeExistsByTestTypeTitle(TestTypeTitle);
        }
        public bool Save()
        {
            return clsTestTypeData.UpdateTestType(_TestTypeID, _TestTypeTitle, _TestTypeDescription, _TestTypeFees);
        }
    }
}
