using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace VKRproject.Tools
{
    public static class DbTool
    {
        private static MySqlConnection Connection;
        static DbTool()
        {
            Connection = new MySqlConnection("Server = localhost; Database = is_travel_agency; port = 3306; User Id = root; password = q1234as");
        }
        public static void InitConnection(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
        
        public static void ExcecuteQueryNonResult(string sqlStr)
        {
            try
            {
                Connection.Open();
                
                MySqlCommand command = new MySqlCommand(sqlStr, Connection);
                command.ExecuteNonQuery();
                Connection.Close();
            }
            catch(Exception ex)
            {
                throw new Exception("Query to DataBase is failed! " + ex.Message);
            }
        }
        public static MySqlDataReader ExcecuteQueryWithResult(string sqlStr)
        {
            try
            {
                //Connection.Open();
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
