using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public static class clsTestAppointmentData
    {


        public static DataTable GetAllTestAppointmentsForLocalDrivingLicenseApplication(int TestTypeID, int LDLAppID)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT TestAppointments.TestAppointmentID, TestAppointments.AppointmentDate, TestAppointments.PaidFees, TestAppointments.IsLocked
                                FROM TestAppointments INNER JOIN TestTypes 
                                ON TestAppointments.TestTypeID = TestTypes.TestTypeID
                                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypes.TestTypeID = @TestTypeID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static bool IsHasATestAppointmentForLocalDrivingLicenseApplication(int TestTypeID, int LDLAppID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT IsFound = 1
                                FROM TestAppointments 
                                WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                                AND TestAppointments.TestTypeID = @TestTypeID 
                                AND TestAppointments.IsLocked = 0;";
            
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                IsFound = Reader.HasRows;

                Reader.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                IsFound = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
        
        public static bool IsHasPassedTestForLocalDrivingLicenseApplication(int TestTypeID, int LDLAppID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT IsPassed = 1        
                                FROM TestAppointments INNER JOIN Tests 
                                ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                                WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                                AND TestAppointments.TestTypeID = @TestTypeID 
                                AND TestS.TestResult = 1;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                IsFound = Reader.HasRows;

                Reader.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                IsFound = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool IsTestAppointmentForLocalDrivingLicenseApplicationIsActive(int TestAppointmentID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT IsActive = 1
                                FROM TestAppointments 
                                WHERE TestAppointments.TestAppointmentID = @TestAppointmentID AND TestAppointments.IsLocked = 0;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                IsFound = Reader.HasRows;

                Reader.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                IsFound = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool AddNewTestAppointmentForLocalDrivingLicenseApplication(int TestTypeID, int LDLAppID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID)
        {
            bool IsAdded = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRANSACTION;
	
	        DECLARE @NumberOfTrials INT;

	        SET @NumberOfTrials = (SELECT COUNT(Tests.TestID) AS NumberOfTrials
                                        FROM TestAppointments INNER JOIN Tests 
                                        ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                                        WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID = @TestTypeID AND Tests.TestResult = 0);

								IF @NumberOfTrials > 0
								BEGIN

								DECLARE @ApplicantPersonID INT;
								DECLARE @ApplicationTypeID INT;
								DECLARE @ApplicationPaidFees smallmoney;

								SET @ApplicantPersonID = (SELECT Applications.ApplicantPersonID
									 FROM Applications INNER JOIN LocalDrivingLicenseApplications 
									 ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
									 WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID);

								SET @ApplicationTypeID = 7;
								
								SET @ApplicationPaidFees = (SELECT ApplicationTypes.ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID);			

                            	DECLARE @NewApplicationID INT;

									 INSERT INTO Applications 
										 (ApplicantPersonID, ApplicationTypeID, PaidFees, CreatedByUserID)
									 VALUES 
										 (@ApplicantPersonID, @ApplicationTypeID, @ApplicationPaidFees, @CreatedByUserID);

								SET @NewApplicationID = SCOPE_IDENTITY();                        
                                
                                UPDATE LocalDrivingLicenseApplications
                                SET ApplicationID = @NewApplicationID 
                                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID

								END
	
								DECLARE @NewTestAppointmentID INT;
	
								INSERT INTO TestAppointments
														  (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID)
														 VALUES 
														  (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID);
								
								SET @NewTestAppointmentID = SCOPE_IDENTITY();                        
                                
                                COMMIT TRANSACTION;

                                SELECT  @NewTestAppointmentID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null)
                {
                    IsAdded = true;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                IsAdded = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsAdded = false;
            }
            finally
            {
                Connection.Close();
            }

            return IsAdded;
        }

        public static DateTime GetTestAppointmentDateForLocalDrivingLicenseApplication(int TestTypeID, int LDLAppID)
        {
            DateTime AppointmentDate = DateTime.Now;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT AppointmentDate
                                FROM TestAppointments
                                WHERE TestTypeID = @TestTypeID AND LocalDrivingLicenseApplicationID = @LDLAppID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
    
            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null && DateTime.TryParse(Result.ToString(), out DateTime CurrentAppointmentDate))
                {
                    AppointmentDate = CurrentAppointmentDate;
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

            return AppointmentDate;
        }

        public static bool UpdateTestAppointmentForLocalDrivingLicenseApplication(int TestTypeID, int LDLAppID, DateTime NewAppointmentDate)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Update TestAppointments 
                               SET AppointmentDate = @NewAppointmentDate
                               WHERE TestTypeID = @TestTypeID AND LocalDrivingLicenseApplicationID = @LDLAppID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@NewAppointmentDate", NewAppointmentDate);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
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

            return (RowsAffected > 0);
        }
    }
}
