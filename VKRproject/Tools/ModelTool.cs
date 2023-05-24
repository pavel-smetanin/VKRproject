using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using VKRproject.Models;
using VKRproject.Modules;

namespace VKRproject.Tools
{
    public static class ModelTool
    {
        public static Dictionary<int, string> GetCountriesDict()
        {
            Dictionary<int, string> countriesDict = new Dictionary<int, string>();
            string sql = "SELECT ID, name FROM countries;";
            countriesDict = GetDictionaryFromDb(sql);
            return countriesDict;
        }
        public static Dictionary<int, string> GetOperatorsDict()
        {
            Dictionary<int, string> operatorsDict = new Dictionary<int, string>();
            string sql = "SELECT ID, name FROM tour_operators;";
            operatorsDict = GetDictionaryFromDb(sql);
            return operatorsDict;
        }
        public static Dictionary<int, string> GetDepCitiesDict()
        {
            Dictionary<int, string> depCitiesDict = new Dictionary<int, string>();
            string sql = "SELECT ID, name FROM dep_cities;";
            depCitiesDict = GetDictionaryFromDb(sql);
            return depCitiesDict;
        }
        private static Dictionary<int, string> GetDictionaryFromDb(string sql)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(Int32.Parse(reader[0].ToString()), reader[1].ToString());
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return result;
        }
        public static List<ModelType> GetMealsType()
        {
            List<ModelType> mealsType = new List<ModelType>();
            string sql = "SELECT code, description FROM meals_type;";
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ModelType type = new ModelType();
                    type.Code = reader[0].ToString();
                    type.Description = reader[1].ToString();
                    mealsType.Add(type);
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return mealsType;
        }
        
        public static Tour GetTourFromDb(string tourId)
        {
            Tour tour = new Tour();
            DbTool.OpenDbConnection();
            string sql = SqlConstructor.ForToursTabJoin("tours_data") + $" WHERE t.ID = '{tourId}';";
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tour.ID = reader[0].ToString();
                    tour.TourOperator.ID = Int32.Parse(reader[1].ToString());
                    tour.TourOperator.Name = reader[2].ToString();
                    tour.Hotel.ID = Int32.Parse(reader[3].ToString());
                    tour.Hotel.Name = reader[4].ToString();
                    tour.Hotel.Category = reader[5].ToString();
                    tour.City.ID = Int32.Parse(reader[6].ToString());
                    tour.City.Name = reader[7].ToString();
                    tour.Country.ID = Int32.Parse(reader[8].ToString());
                    tour.Country.Name = reader[9].ToString();
                    tour.Country.Visa = Convert.ToBoolean(Int32.Parse(reader[10].ToString()));
                    tour.Name = reader[11].ToString();
                    tour.Room = reader[12].ToString();
                    tour.MealType.Code = reader[13].ToString();
                    tour.MealType.Description = reader[14].ToString();
                    tour.AccomType.Code = reader[15].ToString();
                    tour.AccomType.Description = reader[16].ToString();
                    tour.DateStart = DateOnly.FromDateTime(DateTime.Parse(reader[17].ToString()));
                    tour.DateFinish = DateOnly.FromDateTime(DateTime.Parse(reader[18].ToString()));
                    tour.NightsCount = Int32.Parse(reader[19].ToString());
                    tour.AdultsCount = Int32.Parse(reader[20].ToString());
                    tour.ChildCount = Int32.Parse(reader[21].ToString());
                    tour.OpLinks = JArray.Parse(reader[22].ToString());
                    tour.ImgLink = reader[23].ToString();
                    tour.DepCity.ID = Int32.Parse(reader[24].ToString());
                    tour.DepCity.Name = reader[25].ToString();
                    tour.Price = Int32.Parse(reader[26].ToString());
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return tour;
        }
        public static Country GetCountryFromDb(int id)
        {
            Country country = new Country();
            string sql = $"SELECT * FROM countries WHERE ID = {id};";
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    country.ID = Int32.Parse(reader[0].ToString());
                    country.Name = reader[1].ToString();
                    country.Alias = reader[2].ToString();
                    country.Visa = Convert.ToBoolean(Int32.Parse(reader[3].ToString()));
                    country.CurrencyInfo = JObject.Parse(reader[5].ToString());
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return country;
        }
        public static City GetCityFromDb(int id)
        {
            City city = new City();
            string sql = $"SELECT c_d.ID, c_d.name, c_d.country_id, c.name, c_d.descr_url, c_d.popular " +
                $"FROM cities_data c_d JOIN countries c ON c_d.country_id = c.ID " +
                $"WHERE c_d.ID = {id};";
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    city.ID = Int32.Parse(reader[0].ToString());
                    city.Name = reader[1].ToString();
                    city.Country.ID = Int32.Parse(reader[2].ToString());
                    city.Country.Name = reader[3].ToString();
                    city.DescrUrl = reader[4].ToString();
                    city.Popular = Convert.ToBoolean(Int32.Parse(reader[5].ToString()));
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return city;
        }
        public static Hotel GetHotelFromDb(int id)
        {
            Hotel hotel = new Hotel();
            string sql = $"SELECT h.ID, h.name, h.category, h.rate, h.city_id, c.name " +
                $"FROM hotels_data h JOIN cities_data c ON h.city_id = c.ID " +
                $"WHERE h.ID = {id};";
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    hotel.ID = Int32.Parse(reader[0].ToString());
                    hotel.Name = reader[1].ToString();
                    hotel.Category = reader[2].ToString();
                    hotel.Rate = float.Parse(reader[3].ToString());
                    hotel.City.ID = Int32.Parse(reader[4].ToString());
                    hotel.City.Name = reader[5].ToString();
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return hotel;
        }
        public static DepCity GetDepCityFromDb(int id)
        {
            DepCity depCity = new DepCity();
            string sql = $"SELECT d.ID, d.name, d.country_id, c.name, d.popular, d.airport_info " +
                $"FROM dep_cities d JOIN countries c ON d.country_id = c.ID " +
                $"WHERE d.ID = {id};";
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    depCity.ID = Int32.Parse(reader[0].ToString());
                    depCity.Name = reader[1].ToString();
                    depCity.Country.ID = Int32.Parse(reader[2].ToString());
                    depCity.Country.Name = reader[3].ToString();
                    depCity.Popular = Convert.ToBoolean(Int32.Parse(reader[4].ToString()));
                    depCity.Airport = JArray.Parse(reader[5].ToString());
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return depCity;
        }
        public static TourOperator GetTourOperatorFromDb(int id)
        {
            TourOperator tourOperator = new TourOperator();
            string sql = $"SELECT * FROM tour_operators WHERE ID = {id};";
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tourOperator.ID = Int32.Parse(reader[0].ToString());
                    tourOperator.Name = reader[1].ToString();
                    tourOperator.Inn = reader[2].ToString();
                    tourOperator.ContractNum = reader[3].ToString();
                    tourOperator.Adress = reader[4].ToString();
                    tourOperator.Email = reader[5].ToString();
                    tourOperator.Phone = reader[6].ToString();
                    tourOperator.SiteLink = reader[7].ToString();
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return tourOperator;
        }
    }
}
