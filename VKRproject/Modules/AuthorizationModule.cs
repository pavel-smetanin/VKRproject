using VKRproject.Models;
using VKRproject.Tools;

namespace VKRproject.Modules
{
    public class AuthorizationModule
    {
        public static User AuthUser { get; private set; }
        public bool CheckAuth(string login, string password)
        {
            string sqlStr = $"SELECT count(*) FROM auth_data WHERE login = '{login}' AND password = '{password}';";
            object check = DbTool.ExcecuteQueryWithScalar(sqlStr);
            if (check != null)
            {
                if (Int32.Parse(check.ToString()) > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public void InitUser(string login, string password)
        {
            string sqlStr = $"SELECT a.empl_id, a.role, e.last_name, e.first_name, e.patr_name, e.position " +
                $"FROM auth_data a JOIN employees e ON a.empl_id = e.ID " +
                $"WHERE a.login = '{login}' AND a.password = '{password}'";
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sqlStr);
            User user = new User();
            if (reader.HasRows) 
            {
                while(reader.Read())
                {
                    user.Employee.ID = Int32.Parse(reader[0].ToString());
                    user.Role = reader[1].ToString();
                    user.Employee.LastName = reader[2].ToString();
                    user.Employee.FirstName = reader[3].ToString();
                    user.Employee.PatrName = reader[4].ToString();
                    user.Employee.Position = reader[5].ToString();
                }
                reader.Close();
                DbTool.CloseDbConnection();
            }
            AuthUser = user;
        }
    }
}
