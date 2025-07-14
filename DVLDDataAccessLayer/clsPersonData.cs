using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public static class clsPersonData
    {

        public static DataTable GetAllPeople()
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView;";

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

        public static DataTable GetPeopleFilteredByPersonID(int PersonID)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetPeopleFilteredByNationalNo(string NationalNo)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.NationalNo LIKE @NationalNo;";

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

        public static DataTable GetPeopleFilteredByFirstName(string FirstName)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.FirstName LIKE @FirstName;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@FirstName", FirstName);

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

        public static DataTable GetPeopleFilteredBySecondName(string SecondName)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.SecondName LIKE @SecondName;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@SecondName", SecondName);

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

        public static DataTable GetPeopleFilteredByThirdName(string ThirdName)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.ThirdName LIKE @ThirdName;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ThirdName", ThirdName);

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

        public static DataTable GetPeopleFilteredByLastName(string LastName)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.LastName LIKE @LastName;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LastName", LastName);

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

        public static DataTable GetPeopleFilteredByNationality(string Nationality)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.CountryNationality LIKE @Nationality;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Nationality", Nationality);

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

        public static DataTable GetPeopleFilteredByGender(string Gender)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.Gender LIKE @Gender;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Gender", Gender);

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

        public static DataTable GetPeopleFilteredByPhone(string Phone)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.Phone LIKE @Phone;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Phone", Phone);

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

        public static DataTable GetPeopleFilteredByEmail(string Email)
        {

            DataTable DT = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM vPeopleView WHERE vPeopleView.Email LIKE @Email;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Email", Email);

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

        public static bool GetPersonInfoByPersonID(int PersonID, ref int NationalityCountryID, ref string NationalNo,
            ref string FirstName, ref string SecondName, ref string ThirdName, 
            ref string LastName, ref DateTime DateOfBirth, ref bool Gender,
            ref string Address, ref string Phone, ref string Email,
            ref string ImagePath)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM People WHERE PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsFound = true;

                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    NationalNo = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gender = (bool)Reader["Gender"];
                    Address = (string)Reader["Address"];
                    Phone = (string)Reader["Phone"];

                    if (Reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)Reader["ThirdName"];

                    }
                    else
                    {
                        ThirdName = null;
                    }

                    if (Reader["Email"] != DBNull.Value)
                    {
                        Email = (string)Reader["Email"];

                    }
                    else
                    {
                        Email = null;
                    }

                    if (Reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)Reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = null;
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

        public static bool GetPersonInfoByNationalNo(ref int PersonID, ref int NationalityCountryID, string NationalNo,
           ref string FirstName, ref string SecondName, ref string ThirdName,
           ref string LastName, ref DateTime DateOfBirth, ref bool Gender,
           ref string Address, ref string Phone, ref string Email,
           ref string ImagePath)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM People WHERE NationalNo = @NationalNo;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsFound = true;

                    PersonID = (int)Reader["PersonID"];
                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gender = (bool)Reader["Gender"];
                    Address = (string)Reader["Address"];
                    Phone = (string)Reader["Phone"];

                    if (Reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)Reader["ThirdName"];

                    }
                    else
                    {
                        ThirdName = null;
                    }

                    if (Reader["Email"] != DBNull.Value)
                    {
                        Email = (string)Reader["Email"];

                    }
                    else
                    {
                        Email = null;
                    }

                    if (Reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)Reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = null;
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

        public static bool GetPersonInfoByEmail(ref int PersonID, ref int NationalityCountryID, ref string NationalNo,
           ref string FirstName, ref string SecondName, ref string ThirdName,
           ref string LastName, ref DateTime DateOfBirth, ref bool Gender,
           ref string Address, ref string Phone, string Email,
           ref string ImagePath)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM People WHERE Email = @Email;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Email", Email);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsFound = true;

                    PersonID = (int)Reader["PersonID"];
                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    NationalNo = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gender = (bool)Reader["Gender"];
                    Address = (string)Reader["Address"];
                    Phone = (string)Reader["Phone"];

                    if (Reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)Reader["ThirdName"];

                    }
                    else
                    {
                        ThirdName = null;
                    }

                    if (Reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)Reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = null;
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

        public static bool GetPersonInfoByPhone(ref int PersonID, ref int NationalityCountryID, ref string NationalNo,
           ref string FirstName, ref string SecondName, ref string ThirdName,
           ref string LastName, ref DateTime DateOfBirth, ref bool Gender,
           ref string Address, string Phone, ref string Email,
           ref string ImagePath)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM People WHERE Phone = @Phone;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Phone", Phone);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsFound = true;

                    PersonID = (int)Reader["PersonID"];
                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    NationalNo = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gender = (bool)Reader["Gender"];
                    Address = (string)Reader["Address"];

                    if (Reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)Reader["ThirdName"];

                    }
                    else
                    {
                        ThirdName = null;
                    }

                    if (Reader["Email"] != DBNull.Value)
                    {
                        Email = (string)Reader["Email"];

                    }
                    else
                    {
                        Email = null;
                    }

                    if (Reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)Reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = null;
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

        public static int AddNewPerson(int NationalityCountryID, string NationalNo,
            string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email,
            string ImagePath)
        {
            int PersonID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
                string Query = @"INSERT INTO People 
                                 (NationalityCountryID, NationalNo, FirstName, SecondName, 
                                  ThirdName, LastName, DateOfBirth, Gender, Address, Phone, 
                                  Email, ImagePath)
                                 VALUES 
                                 (@NationalityCountryID, @NationalNo, @FirstName, @SecondName, 
                                  @ThirdName, @LastName, @DateOfBirth, @Gender, @Address, 
                                  @Phone, @Email, @ImagePath);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand Command = new SqlCommand(Query, Connection);
                
                    Command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                    Command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    Command.Parameters.AddWithValue("@FirstName", FirstName);
                    Command.Parameters.AddWithValue("@SecondName", SecondName);
                    Command.Parameters.AddWithValue("@LastName", LastName);
                    Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    Command.Parameters.AddWithValue("@Gender", Gender);
                    Command.Parameters.AddWithValue("@Address", Address);
                    Command.Parameters.AddWithValue("@Phone", Phone);

                    if (!string.IsNullOrEmpty(ThirdName))
                        Command.Parameters.AddWithValue("@ThirdName", ThirdName);
                    else
                        Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

                    if (!string.IsNullOrEmpty(Email))
                        Command.Parameters.AddWithValue("@Email", Email);
                    else
                        Command.Parameters.AddWithValue("@Email", DBNull.Value);

                    if (!string.IsNullOrEmpty(ImagePath))
                        Command.Parameters.AddWithValue("@ImagePath", ImagePath);
                    else
                        Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

                    try
                    {
                        Connection.Open();
                        object Result = Command.ExecuteScalar();

                        if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                        {
                            PersonID = InsertedID;
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

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, int NationalityCountryID, string NationalNo,
            string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email,
            string ImagePath)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Update People 
                             SET NationalityCountryID = @NationalityCountryID,
                             NationalNo  = @NationalNo,
                             FirstName   = @FirstName,
                             SecondName  = @SecondName,
                             ThirdName   = @ThirdName,
                             LastName    = @LastName,
                             DateOfBirth = @DateOfBirth,
                             Gender      = @Gender,
                             Address     = @Address,
                             Phone       = @Phone,
                             Email       = @Email,
                             ImagePath   = @ImagePath
                             WHERE PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@Phone", Phone);

            if (!string.IsNullOrEmpty(ThirdName))
                Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            if (!string.IsNullOrEmpty(Email))
                Command.Parameters.AddWithValue("@Email", Email);
            else
                Command.Parameters.AddWithValue("@Email", DBNull.Value);

            if (!string.IsNullOrEmpty(ImagePath))
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

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

        public static bool DeletePerson(int PersonID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "DELETE FROM People WHERE PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExistsByPersonID(int PersonID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExistsByNationalNo(string NationalNo)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static bool IsPersonExistsByEmail(string Email)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT Found=1 FROM People WHERE Email = @Email;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Email", Email);

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

        public static bool IsPersonExistsByPhone(string Phone)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT Found=1 FROM People WHERE Phone = @Phone;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Phone", Phone);

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

        public static string GetImagePathByPersonID(int PersonID)
        {

            string ImagePath = null;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT ImagePath FROM People WHERE PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {

                Connection.Open();

                object Result = Command.ExecuteScalar();


                if (Result != null)
                {
                    ImagePath = Result.ToString();
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

            return ImagePath;
        }

        public static string GetPersonFullNameByPersonID(int PersonID)
        {

            string FullName = null;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT (People.FirstName + ' ' + 
                             People.SecondName + ' ' + 
                             CASE WHEN People.ThirdName IS NOT NULL THEN People.ThirdName + ' ' ELSE '' END + 
                             People.LastName) AS FullName 
	                         From People 
                             WHERE PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {

                Connection.Open();

                object Result = Command.ExecuteScalar();


                if (Result != null)
                {
                    FullName = Result.ToString();
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

            return FullName;
        }

        public static bool GetPersonGenderByPersonID(int PersonID)
        {

            bool PersonGender = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT Gender From People WHERE PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {

                Connection.Open();

                object Result = Command.ExecuteScalar();


                if (Result != null && Boolean.TryParse(Result.ToString(), out bool Gender))
                {
                    PersonGender = Gender;
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

            return PersonGender;
        }
    }
}
