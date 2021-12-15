using System;
using System.Data;
using System.Data.SqlClient;

namespace Address_Book_System_ADO.NET
{
    public class AddressBookRepo
    {
        public static SqlConnection con { get; set; }
        public void DatabaseConnection()
        {
            DataBaseConnection db = new DataBaseConnection();
            con = db.GetConnection();
            using (con)
            {
                con.Open();
                Console.WriteLine("The Connection is Created \n");
            }
            con.Close();
        }

        public bool AddContact(AddressBookModel model)
        {
            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand("AddressProcedure", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@State", model.State);
                    command.Parameters.AddWithValue("@Zipcode", model.Zipcode);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@AddressBookName", model.AddressBookName);
                    command.Parameters.AddWithValue("@AddressbookType", model.AddressBookType);
                    con.Open();
                    var result = command.ExecuteNonQuery();
                    con.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void EditContactUsingFirstName(AddressBookModel model)
        {
            try
            {
                using (con)
                {
                    string updateQuery = @"UPDATE AddressBooks SET LastName = @LastName, City = @City, State = @State, Email = @Email, AddressBookName = @AddressBookName, AddressBookType = @AddressBookType WHERE FirstName = @FirstName;";
                    SqlCommand command = new SqlCommand(updateQuery, con);
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@State", model.State);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@AddressBookName", model.AddressBookName);
                    command.Parameters.AddWithValue("@AddressBookType", model.AddressBookType);

                    con.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Updated successfully...");
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void DeleteContactUsingName(AddressBookModel model)
        {
            try
            {
                using (con)
                {

                    string deleteQuery = @"Delete from AddressBooks WHERE FirstName = @FirstName;";
                    SqlCommand command = new SqlCommand(deleteQuery, con);
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    con.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Deleted successfully...");
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void RetrieveContactFromPerticularCityOrState()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (con)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM AddressBooks WHERE City = 'CCCCC' OR State = 'MMMMM';", con))

                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.FirstName = reader.GetString(0);
                                model.LastName = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zipcode = Convert.ToInt32(reader.GetString(5));
                                model.PhoneNumber = Convert.ToInt64(reader.GetString(6));
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.FirstName, model.LastName, model.Address, model.City,
                                    model.State, model.Zipcode, model.PhoneNumber, model.Email);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    model.FirstName = reader.GetString(0);
                                    model.LastName = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zipcode = Convert.ToInt32(reader.GetString(5));
                                    model.PhoneNumber = Convert.ToInt64(reader.GetString(6));
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.FirstName, model.LastName, model.Address, model.City,
                                        model.State, model.Zipcode, model.PhoneNumber, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void AddressBookSizeByCityANDState()
        {
            try
            {
                using (con)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"select count(FirstName) from AddressBook WHERE City = 'CCCCC' AND State = 'MMMMM';", con))
                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int count = reader.GetInt32(0);
                                Console.WriteLine("Total Contacts From City And State : {0}", +count);
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    int count = reader.GetInt32(0);
                                    Console.WriteLine("Total Contacts From City And State : {0}", +count);
                                }
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void SortPersonNameByCity()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (con)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM AddressBook WHERE City = 'CCCCCC' order by FirstName; 
                        SELECT * FROM AddressBook WHERE City = 'CCCCC' order by FirstName, LastName;", con))
                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Sorted Contact Using first name from city");
                            Console.WriteLine("===========================================");
                            while (reader.Read())
                            {
                                model.FirstName = reader.GetString(0);
                                model.LastName = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zipcode = Convert.ToInt32(reader.GetString(5));
                                model.PhoneNumber = Convert.ToInt64(reader.GetString(6));
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.FirstName, model.LastName, model.Address, model.City,
                                        model.State, model.Zipcode, model.PhoneNumber, model.Email);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                Console.WriteLine("Sorted Contact Using FirstName from city");
                                Console.WriteLine("===========================================");
                                while (reader.Read())
                                {
                                    model.FirstName = reader.GetString(0);
                                    model.LastName = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zipcode = Convert.ToInt32(reader.GetString(5));
                                    model.PhoneNumber = Convert.ToInt64(reader.GetString(6));
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.FirstName, model.LastName, model.Address, model.City,
                                        model.State, model.Zipcode, model.PhoneNumber, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void identifyEachAddressbooktype()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (con)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM AddressBook WHERE AddressBookType = 'family'; 
                        SELECT * FROM AddressBook WHERE AddressBookType = 'friend';", con))
                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Identify each address book by person name");
                            Console.WriteLine("===========================================");
                            while (reader.Read())
                            {
                                model.FirstName = reader.GetString(0);
                                model.LastName = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zipcode = Convert.ToInt32(reader.GetString(5));
                                model.PhoneNumber = Convert.ToInt64(reader.GetString(6));
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.FirstName, model.LastName, model.Address, model.City,
                                        model.State, model.Zipcode, model.PhoneNumber, model.Email);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                Console.WriteLine("Identify each address book by person name");
                                Console.WriteLine("===========================================");
                                while (reader.Read())
                                {
                                    model.FirstName = reader.GetString(0);
                                    model.LastName = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zipcode = Convert.ToInt32(reader.GetString(5));
                                    model.PhoneNumber = Convert.ToInt64(reader.GetString(6));
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.FirstName, model.LastName, model.Address, model.City,
                                        model.State, model.Zipcode, model.PhoneNumber, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                            if (reader.NextResult())
                            {
                                Console.WriteLine("Identify each address book by person name");
                                Console.WriteLine("===========================================");
                                while (reader.Read())
                                {
                                    model.FirstName = reader.GetString(0);
                                    model.LastName = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zipcode = Convert.ToInt32(reader.GetString(5));
                                    model.PhoneNumber = Convert.ToInt64(reader.GetString(6));
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.FirstName, model.LastName, model.Address, model.City,
                                        model.State, model.Zipcode, model.PhoneNumber, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }

                        }
                        con.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void GetNumberOfContactsCountByBookType()
        {
            try
            {
                using (con)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT COUNT(FirstName) FROM AddressBook WHERE addressBooktype = 'family'; 
                        SELECT COUNT(FirstName) FROM AddressBook WHERE addressBooktype = 'friend';", con))
                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int count = reader.GetInt32(0);
                                Console.WriteLine("Number of Contacts From Addressbook_Type_Family: {0} ", +count);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    var count = reader.GetInt32(0);
                                    Console.WriteLine("Number of Contacts From Addressbook_Type_Friend: {0} ", +count);
                                    Console.WriteLine("\n");
                                }
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    var count = reader.GetInt32(0);
                                    Console.WriteLine("Number of Contacts From Addressbook_Type_Office: {0} ", +count);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
