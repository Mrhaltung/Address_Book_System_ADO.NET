using System;
using System.Data.SqlClient;

namespace Address_Book_System_ADO.NET
{
    public class DataBaseConnection
    {
        public SqlConnection GetConnection()
        {
            string ConnectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Address_Book_System_ADO_NET;Integrated Security=True";
            SqlConnection connection = new SqlConnection(ConnectionString);
            return connection;
        }
    }
}