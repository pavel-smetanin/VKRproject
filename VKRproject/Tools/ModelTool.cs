using VKRproject.Models;

namespace VKRproject.Tools
{
    public static class ModelTool
    {
        public static List<Country> GetCountriesFromDb()
        {
            try
            {
                List<Country> countrieslist = new List<Country>();
                string sql = "SELECT * FROM countries;";
                DbTool.OpenDbConnection();
                var reader = DbTool.ExcecuteQueryWithResult(sql);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Country item = new Country();
                        item.ID = Int32.Parse(reader[0].ToString());
                        item.Name = reader[1].ToString();
                        item.Alias = reader[2].ToString();
                        item.Visa = Convert.ToBoolean(Int32.Parse(reader[3].ToString()));
                        countrieslist.Add(item);
                    }
                }
                reader.Close();
                DbTool.CloseDbConnection();
                return countrieslist;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in method GetCountriesFromDb " + ex.Message);
            }
        }
    }
}
