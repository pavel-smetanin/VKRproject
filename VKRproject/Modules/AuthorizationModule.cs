using VKRproject.Models;
using VKRproject.Tools;

namespace VKRproject.Modules
{
    public class AuthorizationModule
    {
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
        
    }
}
