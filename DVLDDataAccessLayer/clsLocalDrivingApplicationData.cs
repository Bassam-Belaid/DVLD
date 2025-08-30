using System.Data.SqlClient;
using System;
using System.Data;

namespace DVLDDataAccessLayer
{
    public static class clsLocalDrivingApplicationData
    {
        public static int AddNewLocalDrivingApplication(int ApplicantPersonID, int ApplicationTypeID, decimal PaidFees, int LicenseClassID, int CreatedByUserID)
        {
            int LocalDrivingApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRANSACTION;

                                DECLARE @NewApplicationID INT;

                                INSERT INTO Applications 
                                    (ApplicantPersonID, ApplicationTypeID, PaidFees, CreatedByUserID)
                                VALUES 
                                    (@ApplicantPersonID, @ApplicationTypeID, @PaidFees, @CreatedByUserID);

                                SET @NewApplicationID = SCOPE_IDENTITY();

                                INSERT INTO LocalDrivingLicenseApplications
                                    (ApplicationID, LicenseClassID)
                                VALUES
                                    (@NewApplicationID, @LicenseClassID);

                                DECLARE @NewLocalDrivingLicenseApplicationID INT;

                                SET @NewLocalDrivingLicenseApplicationID = SCOPE_IDENTITY();

                                COMMIT TRANSACTION;

                                SELECT @NewLocalDrivingLicenseApplicationID AS NewLocalDrivingLicenseApplicationID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
           
            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    LocalDrivingApplicationID = InsertedID;
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

            return LocalDrivingApplicationID;
        }

        public static bool UpdateLocalDrivingApplication(int LDLAppID, int LicenseClassID, int CreatedByUserID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRANSACTION;

                                UPDATE LocalDrivingLicenseApplications
                                SET LicenseClassID = @LicenseClassID
                                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;

                                DECLARE @ApplicationID INT;

                                SET @ApplicationID = (SELECT ApplicationID FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID);

                                UPDATE Applications
                                SET CreatedByUserID = @CreatedByUserID
                                WHERE ApplicationID = @ApplicationID;

                            COMMIT TRANSACTION;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vLocalDrivingLicenseApplicationsView;";

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
        
        public static DataTable GetLocalDrivingLicenseApplicationsFilteredByLDLAppID(int LDLAppID)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM vLocalDrivingLicenseApplicationsView 
                             WHERE vLocalDrivingLicenseApplicationsView.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);

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

        public static DataTable GetLocalDrivingLicenseApplicationsFilteredByNationalNo(string NationalNo)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM vLocalDrivingLicenseApplicationsView 
                             WHERE vLocalDrivingLicenseApplicationsView.NationalNo LIKE @NationalNo;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static DataTable GetLocalDrivingLicenseApplicationsFilteredByFullName(string FullName)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM vLocalDrivingLicenseApplicationsView 
                             WHERE vLocalDrivingLicenseApplicationsView.FullName LIKE @FullName;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@FullName", FullName);

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

        public static DataTable GetLocalDrivingLicenseApplicationsFilteredByStatus(string Status)
        {
            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM vLocalDrivingLicenseApplicationsView 
                             WHERE vLocalDrivingLicenseApplicationsView.Status LIKE @Status;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Status", Status);

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

        public static int IsApplicantHasAnActiveLocalDrivingLicenseApplicationWithSameLicenseClass(int ApplicantPersonID, string ClassName) 
        {
            int LocalDrivingApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID FROM Applications 
                             INNER JOIN LocalDrivingLicenseApplications 
                             ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID 
                             INNER JOIN LicenseClasses 
                             ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
                             WHERE Applications.ApplicantPersonID = @ApplicantPersonID
                             AND LicenseClasses.ClassName = @ClassName 
                             AND Applications.ApplicationStatus = 1;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int SelectedID))
                {
                    LocalDrivingApplicationID = SelectedID;
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

            return LocalDrivingApplicationID;
        }

        public static int IsApplicantHasAnActiveLicenseWithSameLicenseClass(int ApplicantPersonID, string ClassName)
        {
            int LicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Licenses.LicenseID
                                FROM People INNER JOIN Drivers 
                                ON People.PersonID = Drivers.PersonID INNER JOIN Licenses 
                                ON Drivers.DriverID = Licenses.DriverID INNER JOIN LicenseClasses 
                                ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                                WHERE Drivers.PersonID = @ApplicantPersonID AND LicenseClasses.ClassName = @ClassName;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int SelectedID))
                {
                    LicenseID = SelectedID;
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

            return LicenseID;
        }

        public static bool CancelLocalDrivingLicenseApplication(int LDLAppID) 
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"UPDATE Applications
                             SET ApplicationStatus = 2,
                             LastStatusDate = GETDATE()
                             WHERE ApplicationID =  
                             (SELECT LocalDrivingLicenseApplications.ApplicationID 
                                 From LocalDrivingLicenseApplications 
                                 WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID);";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
          
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

        public static bool DeleteLocalDrivingLicenseApplication(int LDLAppID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRANSACTION;

                                DECLARE @ApplicationID INT;

                                SET @ApplicationID = (SELECT LocalDrivingLicenseApplications.ApplicationID FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID);

                                DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;

                                DELETE FROM Applications WHERE Applications.ApplicationID = @ApplicationID;

                                COMMIT TRANSACTION;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);

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

        public static byte NumberOfTestsThatTakenByApplicantForLocalDrivingLicenseApplication(int LDLAppID)
        {
            byte NumberOfTests = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT COUNT(TestAppointments.TestTypeID) AS PassedTests
                                FROM TestAppointments INNER JOIN Tests 
                                ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                                WHERE (TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) AND (Tests.TestResult = 1)";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);

            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null && byte.TryParse(Result.ToString(), out byte PassedTests))
                {
                    NumberOfTests = PassedTests;
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

            return NumberOfTests;
        }

        public static bool IsLocalDrivingLicenseApplicationCanceled(int LDLAppID)
        {
            bool IsCanceled = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRANSACTION;

                            DECLARE @ApplicationID INT;

                            SET @ApplicationID = (SELECT LocalDrivingLicenseApplications.ApplicationID
				                                 FROM LocalDrivingLicenseApplications
					                             WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)

                            COMMIT TRANSACTION;

                            SELECT IsCanceled = 1
                            FROM Applications 
                            WHERE Applications.ApplicationID = @ApplicationID AND Applications.ApplicationStatus = 2";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                IsCanceled = Reader.HasRows;

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

            return IsCanceled;
        }

        public static bool IsLocalDrivingLicenseApplicationCompleted(int LDLAppID)
        {
            bool IsCanceled = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRANSACTION;

                            DECLARE @ApplicationID INT;

                            SET @ApplicationID = (SELECT LocalDrivingLicenseApplications.ApplicationID
				                                 FROM LocalDrivingLicenseApplications
					                             WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)

                            COMMIT TRANSACTION;

                            SELECT IsCanceled = 1
                            FROM Applications 
                            WHERE Applications.ApplicationID = @ApplicationID AND Applications.ApplicationStatus = 3";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                IsCanceled = Reader.HasRows;

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

            return IsCanceled;
        }

        public static bool GetLocalDrivingLicenseApplicationInfoByLDLAppID(int LDLAppID, ref int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, 
            ref byte ApplicationStatus, ref DateTime LastStatusDate, ref decimal PaidFees, ref int CreatedByUserID, ref int LicenseClassID)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Applications.ApplicationID, Applications.ApplicantPersonID, Applications.ApplicationDate, Applications.ApplicationStatus,
                                Applications.PaidFees, Applications.LastStatusDate, Applications.CreatedByUserID, LocalDrivingLicenseApplications.LicenseClassID
                                FROM Applications INNER JOIN LocalDrivingLicenseApplications
                                ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                                WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLAppID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    ApplicationID = (int)Reader["ApplicationID"];
                    ApplicantPersonID = (int)Reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)Reader["ApplicationDate"];
                    ApplicationStatus = Convert.ToByte(Reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)Reader["LastStatusDate"];
                    PaidFees = (decimal)Reader["PaidFees"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    LicenseClassID = (int)Reader["LicenseClassID"];
                }
                else
                {
                    IsFound = false;
                }

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

        public static int GetLicenseIDByLDLAppID(int LDLAppID)
        {
            int LicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Licenses.LicenseID
                            FROM Applications INNER JOIN LocalDrivingLicenseApplications 
                            ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN Licenses 
                            ON Applications.ApplicationID = Licenses.ApplicationID
                            WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);

            try
            {

                Connection.Open();

                object Result = Command.ExecuteScalar();


                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    LicenseID = ID;
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

            return LicenseID;
        }


    }
}
