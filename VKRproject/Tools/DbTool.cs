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
            Connection = new MySqlConnection($"Server=;Database=;port=;User Id=;password=");
        }
        public static void ExcecuteQueryNonResult(string sqlStr)
        {
            Connection.Open();

        }
        public static MySqlDataReader ExcecuteQueryWithResult(string sqlStr)
        {

        }
    }
}
