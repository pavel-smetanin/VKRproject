using MySql.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
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
        public static void OpenDbConnection()
        {
            try
            {
                Connection.Open();
            }
            catch(Exception ex) 
            {
                throw new Exception("Cannot to open Db connection " + ex.Message);
            }
        }
        public static void CloseDbConnection() 
        {
            try
            {
                Connection.Close();
            }
            catch(Exception ex)
            {
                throw new Exception("Cannot to close Db connection " + ex.Message);
            }
        }
        public static void ExcecuteQueryNonResult(string sqlStr)
        {
            OpenDbConnection();
            try
            {
                MySqlCommand command = new MySqlCommand(sqlStr, Connection);
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                CloseDbConnection();
                throw new Exception("Query to DataBase is failed! " + ex.Message);
            }
            CloseDbConnection();
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
        public static object ExcecuteQueryWithScalar(string sqlStr) 
        {
            OpenDbConnection();
            try
            {
                MySqlCommand command = new MySqlCommand(sqlStr, Connection);
                object result = command.ExecuteScalar();
                CloseDbConnection();
                return result;
            }
            catch(Exception ex) 
            {
                CloseDbConnection();
                throw new Exception("Query to DataBase is failed! " + ex.Message);
            }
        }
    }
}
