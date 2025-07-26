using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public static class clsTestData
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

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID) 
        {
            int TestID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRANSACTION;

                                 DECLARE @NewTestID INT;
							     
                                 INSERT INTO Tests 
                                 (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                                 VALUES 
                                 (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);

                                 SET @NewTestID = SCOPE_IDENTITY();

								 Update TestAppointments
								 SET IsLocked = 1
								 WHERE TestAppointmentID = @TestAppointmentID;

                             COMMIT TRANSACTION;

                                 SELECT @NewTestID AS NewTestID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            
            if (!string.IsNullOrEmpty(Notes))
                Command.Parameters.AddWithValue("@Notes", Notes);
            else
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);

            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    TestID = InsertedID;
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

            return TestID;
        }
    }
}
