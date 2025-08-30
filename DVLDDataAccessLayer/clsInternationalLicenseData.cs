using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public static class clsInternationalLicenseData
    {
        public static bool AddNewInternationalLicense(ref int ApplicationID, ref int InternationalLicenseID, int ApplicantPersonID, int ApplicationTypeID, decimal PaidFees, int CreatedByUserID, int DriverID, int IssuedUsingLocalLicenseID)
        {
            bool IsAdded = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"BEGIN TRANSACTION;

                                DECLARE @NewApplicationID INT;

                                INSERT INTO Applications 
                                    (ApplicantPersonID, ApplicationTypeID, PaidFees, CreatedByUserID)
                                VALUES 
                                    (@ApplicantPersonID, @ApplicationTypeID, @PaidFees, @CreatedByUserID);

                                SET @NewApplicationID = SCOPE_IDENTITY();

                                INSERT INTO InternationalLicenses
                                    (ApplicationID, DriverID, IssuedUsingLocalLicenseID, CreatedByUserID)
                                VALUES
                                    (@NewApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @CreatedByUserID);

                                DECLARE @NewInternationalDrivingLicenseApplicationID INT;

                                SET @NewInternationalDrivingLicenseApplicationID = SCOPE_IDENTITY();

                                COMMIT TRANSACTION;

                                SELECT @NewInternationalDrivingLicenseApplicationID AS NewInternationalDrivingLicenseApplicationID, @NewApplicationID AS NewApplicationID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsAdded = true;

                    ApplicationID = (int)Reader["NewApplicationID"];
                    InternationalLicenseID = (int)Reader["NewInternationalDrivingLicenseApplicationID"];
   
                }
                else
                {
                    IsAdded = false;
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

            return IsAdded;
        }

        public static int IsApplicantHasAnActiveInternationalLicense(int IssuedUsingLocalLicenseID)
        {
            int InternationalLicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT InternationalLicenseID FROM InternationalLicenses WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int SelectedID))
                {
                    InternationalLicenseID = SelectedID;
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

            return InternationalLicenseID;
        }

        public static bool GetInternationalLicenseByInternationalLicenseID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, 
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * From InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsFound = true;

                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)Reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    IsActive = (bool)Reader["IsActive"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

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

        public static DataTable GetAllInternationalLicensesForApplicant(int ApplicantPersonID)
        {
            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT InternationalLicenses.InternationalLicenseID, InternationalLicenses.ApplicationID, InternationalLicenses.IssuedUsingLocalLicenseID, 
                                InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive
                                FROM Drivers INNER JOIN InternationalLicenses 
                                ON Drivers.DriverID = InternationalLicenses.DriverID
                                WHERE Drivers.PersonID = @ApplicantPersonID;";

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

        public static DataTable GetAllInternationalLicenseApllications()
        {
            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive
                                FROM InternationalLicenses;";

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
    }
}
