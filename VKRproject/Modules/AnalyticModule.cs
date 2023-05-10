using VKRproject.Models;
using VKRproject.Tools;

namespace VKRproject.Modules
{
    public class AnalyticModule
    {
        public static DateTime DateDataUpdate { get; set; }
        public int GeneralToursCount()
        {
            string sql = "SELECT count(ID) FROM tours_data;";
            var result = Int32.Parse(DbTool.ExcecuteQueryWithScalar(sql).ToString());
            return result;
        }
        public float GeneralAvgPrice()
        {
            string sql = "SELECT avg(price) FROM tours_data;";
            var result = float.Parse(DbTool.ExcecuteQueryWithScalar(sql).ToString());
            return result;
        }

        public int GeneralAvgNights()
        {
            string sql = "SELECT round(avg(nights_count), 0) FROM tours_data;";
            var result = Int32.Parse(DbTool.ExcecuteQueryWithScalar(sql).ToString());
            return result;
        }
        public Dictionary<string, int> ToursCountsByCountry()
        {
            string sql = "SELECT c.name, count(t.ID) FROM tours_data t JOIN countries c ON t.country_id = c.ID GROUP BY t.country_id;";
            var result = GetDictionaryFromDb(sql);
            return result;
        }
        public Dictionary<string, int> ToursCountsByOperator() 
        {
            string sql = "SELECT o.name, count(t.ID) FROM tours_data t JOIN tour_operators o ON t.op_id = o.ID GROUP BY t.op_id;";
            var result = GetDictionaryFromDb(sql);
            return result;
        }
        public KeyValuePair<string, int> MinNightsCount()
        {
            string sql = "SELECT ID, min(nights_count) FROM tours_data;";
            var result = GetDictionaryFromDb(sql);
            return result.First();
        }
        public KeyValuePair<string, int> MaxNightsCount()
        {
            string sql = "SELECT ID, max(nights_count) FROM tours_data;";
            var result = GetDictionaryFromDb(sql);
            return result.First();
        }
        public KeyValuePair<string, int> MinPrice()
        {
            string sql = "SELECT ID, min(price) FROM tours_data;";
            var result = GetDictionaryFromDb(sql);
            return result.First();
          }
        public KeyValuePair<string, int> MaxPrice()
        {
            string sql = "SELECT ID, max(price) FROM tours_data;";
            var result = GetDictionaryFromDb(sql);
            return result.First();
        }
        public KeyValuePair<string, int> CountryWithMaxTours()
        {
            var dict = ToursCountsByCountry();
            var result = MaxKeyValueInDictionary(dict);
            return result;
        }
        public KeyValuePair<string, int> CityWithMaxTours()
        {
            string sql = "SELECT  c.name, count(t.ID) FROM tours_data t JOIN cities_data c ON t.city_id = c.ID GROUP BY city_id;";
            var dict = GetDictionaryFromDb(sql);
            var result = MaxKeyValueInDictionary(dict);
            return result;
        }
        public KeyValuePair<string, int> HotelWithMaxTours()
        {
            string sql = "SELECT h.name, count(t.ID) FROM tours_data t JOIN hotels_data h ON t.hotel_id = h.ID GROUP BY t.hotel_id;";
            var dict = GetDictionaryFromDb(sql);
            var result = MaxKeyValueInDictionary(dict);
            return result;
        }
        private KeyValuePair<string, int> MaxKeyValueInDictionary(Dictionary<string, int> dictionary)
        {
            int maxValue = 0;
            var max = new KeyValuePair<string, int>();
            foreach(var d in dictionary)
            {
                if (d.Value > maxValue)
                {
                    maxValue = d.Value;
                    max = d;
                }
            }
            return max;
        }
        private Dictionary<string, int> GetDictionaryFromDb(string sqlStr)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sqlStr);
            if(reader.HasRows)
            {
                while(reader.Read()) 
                {
                    result.Add(reader[0].ToString(), Int32.Parse(reader[1].ToString()));
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return result;
        }
        
    }
}
