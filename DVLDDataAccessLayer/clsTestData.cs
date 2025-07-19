using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestData
    {
        public static byte CountNumberOfTestTrials(int LDLAppID, int TestTypeID)
        {
            byte NumberOfTestTypes = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT COUNT(Tests.TestID) AS NumberOfTrials
                                FROM TestAppointments INNER JOIN Tests 
                                ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                                WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID = @TestTypeID AND Tests.TestResult = 0;";

            SqlCommand Command = new SqlCommand(Query, Connection);
            
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();


                if (Result != null && byte.TryParse(Result.ToString(), out byte Count))
                {
                    NumberOfTestTypes = Count;
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

            return NumberOfTestTypes;
        }

    }
}
