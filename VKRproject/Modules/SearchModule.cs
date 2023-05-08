using VKRproject.Models;
using VKRproject.Tools;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace VKRproject.Modules
{
    public class SearchModule
    {
        private string toursTab = "tours_data";
        private string toursArchiveTab = "tours_archive";
        public List<Tour> SearchToursByFilter(Filter filter)
        {
            try
            {
                List<Tour> toursListResult = new List<Tour>();
                string sql = GetSqlTabJoin(toursTab) + $"WHERE t.country_id = {filter.CountryId} AND " +
                    $"(date_start >= STR_TO_DATE('{filter.DateLower}', '%d.%m.%Y') AND date_start <= STR_TO_DATE('{filter.DateUpper}', '%d.%m.%Y')) AND " +
                    $"nights_count <= {filter.NightsCount} AND adults_count = {filter.AdultsCount} AND child_count = {filter.ChildCount} AND " +
                    $"(price >= {filter.PriceLower} AND price <= {filter.PriceUpper}) ORDER BY date_start DESC;";
                toursListResult = GetToursListFromDb(sql);
                return toursListResult;
            }
            catch(Exception ex)
            {
                throw new Exception("Erorr in SearchModule: " + ex.Message);
            }
        }
        public List<Tour> SearchToursByFilter(QuestionFilter filter)
        {
            List<Tour> toursListResult = new List<Tour>();
            string sql = GetSqlTabJoin(toursTab) + $"WHERE t.country_id = {filter.CountryID} AND " +
                $"(date_start >= STR_TO_DATE('{filter.StartDateLower}', '%d.%m.%Y') AND date_start <= STR_TO_DATE('{filter.StartDateUpper}', '%d.%m.%Y')) AND " +
                $"date_finish <= {filter.EndDate} AND " +
                $"nights_count <= {filter.NightsCount} AND adults_count = {filter.AdultsCount} AND child_count = {filter.ChildCount} AND " +
                $"(price >= {filter.PriceLower} AND price <= {filter.PriceUpper}) ORDER BY date_start DESC;";
            toursListResult = GetToursListFromDb(sql);
            return toursListResult;

        }
        public List<Tour> SearchToursByFilter(TourStatFilter filter)
        {
            List<Tour> toursListResult = new List<Tour>();
            string sql = GetSqlTabJoin(toursTab) + $"WHERE t.country_id IN ({ListInStr<int>(filter.CountryIdList)}) AND " +
                $"(nights_count <= {filter.NightsCount} OR adults_count <= {filter.AdultsCount} OR child_count <= {filter.ChildCount} OR " +
                $"price <= {filter.Price}) ORDER BY date_start DESC;";
            toursListResult = GetToursListFromDb(sql);
            return toursListResult;
        }
        public List<Tour> SearchToursByClientOrder(int clientId)
        {
            List<Tour> toursListResult = new List<Tour>();
            string sql = GetSqlTabJoin(toursArchiveTab) + $"JOIN orders_archive o ON t.ID = o.tour_id WHERE o.client_id = {clientId} ORDER BY o.order_date DESC;";
            toursListResult = GetToursListFromDb(sql);
            return toursListResult;
        }
        private List<Tour> GetToursListFromDb(string sql)
        {
            List<Tour> result = new List<Tour>();
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Tour tour = new Tour();
                    tour.ID = reader[0].ToString();
                    tour.TourOperator.ID = Int32.Parse(reader[1].ToString());
                    tour.TourOperator.Name = reader[2].ToString();
                    tour.Hotel.ID = Int32.Parse(reader[3].ToString());
                    tour.City.ID = Int32.Parse(reader[4].ToString());
                    tour.City.Name = reader[5].ToString();
                    tour.Country.ID = Int32.Parse(reader[6].ToString());
                    tour.Country.Name = reader[7].ToString();
                    tour.Name = reader[8].ToString();
                    tour.Room = reader[9].ToString();
                    tour.MealType.Code = reader[10].ToString();
                    tour.AccomType.Code = reader[11].ToString();
                    tour.DateStart = DateOnly.FromDateTime(DateTime.Parse(reader[12].ToString()));
                    tour.DateFinish = DateOnly.FromDateTime(DateTime.Parse(reader[13].ToString()));
                    tour.NightsCount = Int32.Parse(reader[14].ToString());
                    tour.AdultsCount = Int32.Parse(reader[15].ToString());
                    tour.ChildCount = Int32.Parse(reader[16].ToString());
                    tour.OpLinks = JArray.Parse(reader[17].ToString());
                    tour.ImgLink = reader[18].ToString();
                    tour.DepCity.ID = Int32.Parse(reader[19].ToString());
                    tour.DepCity.Name = reader[20].ToString();
                    tour.Price = Int32.Parse(reader[21].ToString());
                    result.Add(tour);
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return result;
        }
        private string ListInStr<T>(List<T> list)
        {
            string result = "";
            foreach(var l in list)
            {
                result += l.ToString() + ',';
            }
            result = result.TrimEnd(',');
            return result;
        }
        private string GetSqlTabJoin(string tabName)
        {
            return "SELECT t.ID, t.op_id, op.name, t.hotel_id, t.city_id, c_d.name, t.country_id, c.name, t.name, t.room, t.meal_code, t.accom_code," +
                    "t.date_start, t.date_finish, t.nights_count, t.adults_count, t.child_count, t.op_links, t.img_link, t.dep_city_id, d.name, t.price " +
                    $"FROM {tabName} t JOIN tour_operators op ON t.op_id = op.ID " +
                    "JOIN cities_data c_d ON t.city_id = c_d.ID " +
                    "JOIN countries c ON t.country_id = c.ID " +
                    "JOIN dep_cities d ON t.dep_city_id = d.ID ";
        }
    }
}
