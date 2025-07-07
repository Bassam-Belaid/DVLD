using DVLDDataAccessLayer;
using System.Data;

namespace DVLDBusinessLayer
{
    public class clsCountry
    {
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
        public static string GetCountryNameByCountryID(int CountryID)
        {
            return clsCountryData.GetCountryNameByCountryID(CountryID);
        }
        public static int GetCountryIDByCountryName(string CountryName)
        {
            return clsCountryData.GetCountryIDByCountryName(CountryName);
        }
    }
}
