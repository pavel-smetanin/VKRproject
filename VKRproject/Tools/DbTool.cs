﻿using MySql.Data.MySqlClient;

namespace VKRproject.Tools
{
    public static class DbTool
    {
        private static MySqlConnection Connection;
        static DbTool()
        {
            string server = ConfigProvider.PrivateConfig["MySQL:Server"];
            string database = ConfigProvider.PrivateConfig["MySQL:Database"];
            string port = ConfigProvider.PrivateConfig["MySQL:Port"];
            string user = ConfigProvider.PrivateConfig["MySQL:UserId"];
            string password = ConfigProvider.PrivateConfig["MySQL:Password"];
            Connection = new MySqlConnection($"Server = {server}; Database = {database}; port = {port}; User Id = {user}; password = {password}");
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
                MySqlCommand command = new MySqlCommand(sqlStr, Connection);
                var result = command.ExecuteReader();
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("Query to Data Base with reader result is failed! Maybe you need to open connection " + ex.Message);
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
        public static void ExcecuteQueryNonResultAndOpen(string sqlStr)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(sqlStr, Connection);
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception("Erorr in sql query: " + ex.Message);
            }
        }
    }
}
