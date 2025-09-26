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
    public static class clsLicenseData
    {
        public static bool RenewLicense(int OldLicenseID, ref int ApplicationID, ref int LicenseID, int DriverID, int LicenseClassID, DateTime ExpirationDate, string Notes, int IssueReason, int CreatedByUserID)
        {
            bool IsRenewed = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRY
                                BEGIN TRANSACTION;

                                DECLARE @ApplicantPersonID INT;
                                SET @ApplicantPersonID = (SELECT PersonID FROM Drivers WHERE DriverID = @DriverID);

                                DECLARE @ApplicationFees SMALLMONEY;
                                SET @ApplicationFees = (SELECT ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeID = 2);

                                DECLARE @NewApplicationID INT;

                                INSERT INTO Applications 
                                    (ApplicantPersonID, ApplicationTypeID, PaidFees, CreatedByUserID)
                                VALUES 
                                    (@ApplicantPersonID, 2, @ApplicationFees, @CreatedByUserID);

                                SET @NewApplicationID = SCOPE_IDENTITY();

                                DECLARE @PaidFees SMALLMONEY;
                                SET @PaidFees = (SELECT ClassFees FROM LicenseClasses WHERE LicenseClassID = @LicenseClass);

                                INSERT INTO Licenses
                                    (ApplicationID, DriverID, LicenseClass, ExpirationDate, Notes, PaidFees, IssueReason, CreatedByUserID)
                                VALUES
                                    (@NewApplicationID, @DriverID, @LicenseClass, @ExpirationDate, @Notes, @PaidFees, @IssueReason, @CreatedByUserID);

                                DECLARE @NewLicenseID INT;
                                SET @NewLicenseID = SCOPE_IDENTITY();

                                UPDATE Licenses
                                SET IsActive = 0
                                WHERE LicenseID = @OldLicenseID;

                                COMMIT TRANSACTION;

                                SELECT @NewApplicationID AS NewApplicationID, @NewLicenseID AS NewLicenseID;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRANSACTION;

                                SELECT -1 AS NewApplicationID, -1 AS NewLicenseID;
                            END CATCH;";

            SqlCommand Command = new SqlCommand(Query, Connection);
            
            Command.Parameters.AddWithValue("@OldLicenseID", OldLicenseID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (!string.IsNullOrEmpty(Notes))
                Command.Parameters.AddWithValue("@Notes", Notes);
            else
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);

            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsRenewed = true;

                    ApplicationID = (int)Reader["NewApplicationID"];
                    LicenseID = (int)Reader["NewLicenseID"];

                }
                else
                {
                    IsRenewed = false;
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

            return IsRenewed;
        }

        public static bool ReplacementForDamagedOrLostLicense(int OldLicenseID, ref int ApplicationID, ref int LicenseID, int DriverID, int LicenseClassID, DateTime ExpirationDate, string Notes, int IssueReason, int ApplicationTypeID, decimal ApplicationFees, int CreatedByUserID)
        { 
                bool IsReplacement = false;

                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string Query = @"BEGIN TRY
                                BEGIN TRANSACTION;

                                DECLARE @ApplicantPersonID INT;
                                SET @ApplicantPersonID = (SELECT PersonID FROM Drivers WHERE DriverID = @DriverID);

                                DECLARE @NewApplicationID INT;

                                INSERT INTO Applications 
                                    (ApplicantPersonID, ApplicationTypeID, PaidFees, CreatedByUserID)
                                VALUES 
                                    (@ApplicantPersonID, @ApplicationTypeID, @ApplicationFees, @CreatedByUserID);

                                SET @NewApplicationID = SCOPE_IDENTITY();

                                DECLARE @PaidFees SMALLMONEY;
                                SET @PaidFees = (SELECT ClassFees FROM LicenseClasses WHERE LicenseClassID = @LicenseClass);

                                INSERT INTO Licenses
                                    (ApplicationID, DriverID, LicenseClass, ExpirationDate, Notes, PaidFees, IssueReason, CreatedByUserID)
                                VALUES
                                    (@NewApplicationID, @DriverID, @LicenseClass, @ExpirationDate, @Notes, @PaidFees, @IssueReason, @CreatedByUserID);

                                DECLARE @NewLicenseID INT;
                                SET @NewLicenseID = SCOPE_IDENTITY();

                                UPDATE Licenses
                                SET IsActive = 0
                                WHERE LicenseID = @OldLicenseID;

                                COMMIT TRANSACTION;

                                SELECT @NewApplicationID AS NewApplicationID, @NewLicenseID AS NewLicenseID;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRANSACTION;

                                SELECT -1 AS NewApplicationID, -1 AS NewLicenseID;
                            END CATCH;";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue("@OldLicenseID", OldLicenseID);
                Command.Parameters.AddWithValue("@DriverID", DriverID);
                Command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
                Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

                if (!string.IsNullOrEmpty(Notes))
                    Command.Parameters.AddWithValue("@Notes", Notes);
                else
                    Command.Parameters.AddWithValue("@Notes", DBNull.Value);

                Command.Parameters.AddWithValue("@IssueReason", IssueReason);
                Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                Command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
                Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    Connection.Open();
                    SqlDataReader Reader = Command.ExecuteReader();

                    if (Reader.Read())
                    {

                        IsReplacement = true;

                        ApplicationID = (int)Reader["NewApplicationID"];
                        LicenseID = (int)Reader["NewLicenseID"];

                    }
                    else
                    {
                        IsReplacement = false;
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

                return IsReplacement;
        }

        public static int IssueNewsLicense(int ApplicationID, int LicenseClassID, string Notes, int CreatedByUserID)
        {
            int License = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRY
                                BEGIN TRANSACTION;

                                DECLARE @PersonID INT;
                                SET @PersonID = (SELECT ApplicantPersonID FROM Applications WHERE ApplicationID = @ApplicationID);

                                INSERT INTO Drivers (PersonID, CreatedByUserID)
                                VALUES (@PersonID, @CreatedByUserID);

                                DECLARE @DriverID INT;
                                SET @DriverID = SCOPE_IDENTITY();

                                DECLARE @DefaultValidityLength TINYINT;
                                SET @DefaultValidityLength = (SELECT DefaultValidityLength FROM LicenseClasses WHERE LicenseClassID = @LicenseClass);

                                DECLARE @ExpirationDate DATETIME;
                                SET @ExpirationDate = DATEADD(YEAR, @DefaultValidityLength, GETDATE());

                                DECLARE @PaidFees SMALLMONEY;
                                SET @PaidFees = (SELECT ClassFees FROM LicenseClasses WHERE LicenseClassID = @LicenseClass);

                                INSERT INTO Licenses
                                    (ApplicationID, DriverID, LicenseClass, ExpirationDate, Notes, PaidFees, CreatedByUserID)
                                VALUES
                                    (@ApplicationID, @DriverID, @LicenseClass, @ExpirationDate, @Notes, @PaidFees, @CreatedByUserID);

                                DECLARE @NewLicenseID INT;
                                SET @NewLicenseID = SCOPE_IDENTITY();

                                UPDATE Applications
                                SET ApplicationStatus = 3, LastStatusDate = GETDATE()
                                WHERE ApplicationID = @ApplicationID;

                                COMMIT TRANSACTION;

                                SELECT @NewLicenseID AS NewLicenseID;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRANSACTION;

                                SELECT -1 AS NewLicenseID;
                            END CATCH;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

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
                    License = InsertedID;
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

            return License;
        }


        public static bool GetLicenseByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, ref int LicenseID, ref int ApplicationID, ref int DriverID,
            ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, 
            ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID) 
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Licenses.*
                                FROM Applications INNER JOIN LocalDrivingLicenseApplications 
                                ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN Licenses 
                                ON Applications.ApplicationID = Licenses.ApplicationID
                                WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsFound = true;

                    LicenseID = (int)Reader["LicenseID"];
                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    PaidFees = (decimal)Reader["PaidFees"];
                    IsActive = (bool)Reader["IsActive"];
                    IssueReason = Convert.ToByte((byte)Reader["IssueReason"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    if (Reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)Reader["Notes"];
                    }
                    else
                    {
                        Notes = null;
                    }
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

        public static DataTable GetAllLocalLicensesForApplicant(int ApplicantPersonID) 
        {
            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
                                FROM Applications INNER JOIN Licenses 
                                ON Applications.ApplicationID = Licenses.ApplicationID INNER JOIN LicenseClasses 
                                ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                                WHERE Applications.ApplicantPersonID = @ApplicantPersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

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

        public static bool GetLicenseByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID,
           ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
           ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * From Licenses WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsFound = true;

                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    PaidFees = (decimal)Reader["PaidFees"];
                    IsActive = (bool)Reader["IsActive"];
                    IssueReason = Convert.ToByte((byte)Reader["IssueReason"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    if (Reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)Reader["Notes"];
                    }
                    else
                    {
                        Notes = null;
                    }
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

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT IsFound = 1 FROM DetainedLicenses
                                WHERE DetainedLicenses.LicenseID = @LicenseID AND DetainedLicenses.IsReleased = 0;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                IsDetained = Reader.HasRows;

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

            return IsDetained;
        }


    }
}
