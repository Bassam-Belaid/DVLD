using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public static class clsLicenseClassData
    {
        public static DataTable GetAllLicenseClasses() 
        {
            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT ClassName FROM LicenseClasses;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)

                {
                    DT = new DataTable();
                    DT.Load(Reader);
                }

                Reader.Close();
            }

            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            finally
            {
                Connection.Close();
            }

            return DT;
        }

        public static int GetLicenseClassIDByLicenseClassName(string LicenseClassName)
        {
            int LicenseClassID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT LicenseClassID FROM LicenseClasses WHERE ClassName = @LicenseClassName;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassName", LicenseClassName);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (int.TryParse(Result.ToString(), out int ID))
                {
                    LicenseClassID = ID;
                }
            }

            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return LicenseClassID;
        }

        public static string GetLicenseClassNameByLicenseClassID(int LicenseClassID)
        {
            string LicenseClassName = "";

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT ClassName FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                LicenseClassName = Result.ToString();
            }

            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return LicenseClassName;
        }

    }
}
