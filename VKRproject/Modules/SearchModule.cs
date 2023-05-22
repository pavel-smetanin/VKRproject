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
        public List<ShortTour> SearchToursByFilter(Filter filter)
        {

            List<ShortTour> shortTours = new List<ShortTour>();
            string sql = SqlConstructor.ForShortToursTabJoin(toursTab) + $"WHERE t.country_id = 40 " +
                $"AND ('{filter.DateLower}' AND date_start <= '{filter.DateUpper}') " +
                $"AND (nights_count >= {filter.MinNightsCount} AND nights_count <= {filter.NightsCount}) " +
                $"AND adults_count = {filter.AdultsCount} AND child_count = {filter.ChildCount} " +
                $"AND (price >= {filter.PriceLower} AND price <= {filter.PriceUpper}) ";   
            if(filter.OpFlag)
            {
                sql += $" AND t.op_id IN ({SqlConstructor.ListInStr<int>(filter.OperatorsId)}) ";
            }
            if(filter.DepCityFlag)
            {
                sql += $" AND t.dep_city_id = {filter.DepCityId} ";
            }
            if(filter.MealFlag)
            {
                sql += $" AND t.meal_code = '{filter.MealCode}' ";
            }
            if(filter.CategoryFlag)
            {
                sql += $" AND h.category = '{filter.Category}'";
            }
            if(filter.RateFlag)
            {
                sql += $" AND h.rate >= {filter.Rate} ";
            }
            sql += $" ORDER BY date_start ASC;";
            shortTours = GetToursListFromDb(sql);
            return shortTours;
        }
        public List<ShortTour> SearchToursByFilter(QuestionFilter filter)
        {
            List<ShortTour> toursListResult = new List<ShortTour>();
            string sql = SqlConstructor.ForShortToursTabJoin(toursTab) + $"WHERE t.country_id = {filter.CountryID} AND " +
                $"(date_start >= STR_TO_DATE('{filter.StartDateLower}', '%d.%m.%Y') AND date_start <= STR_TO_DATE('{filter.StartDateUpper}', '%d.%m.%Y')) AND " +
                $"date_finish <= {filter.EndDate} AND " +
                $"nights_count <= {filter.NightsCount} AND adults_count = {filter.AdultsCount} AND child_count = {filter.ChildCount} AND " +
                $"(price >= {filter.PriceLower} AND price <= {filter.PriceUpper}) ORDER BY date_start DESC;";
            toursListResult = GetToursListFromDb(sql);
            return toursListResult;

        }
        public List<ShortTour> SearchToursByFilter(TourStatFilter filter)
        {
            List<ShortTour> toursListResult = new List<ShortTour>(); 
            string sql = SqlConstructor.ForShortToursTabJoin(toursTab) + $" WHERE t.country_id IN ({SqlConstructor.ListInStr<int>(filter.CountryIdList)}) AND " +
                $"(nights_count <= {filter.NightsCount} OR adults_count <= {filter.AdultsCount} OR child_count <= {filter.ChildCount} OR " +
                $"price <= {filter.Price}) ORDER BY date_start DESC;";
            toursListResult = GetToursListFromDb(sql);
            return toursListResult;
        }
        public List<ShortTour> SearchToursByClientOrder(int clientId)
        {
            List<ShortTour> toursListResult = new List<ShortTour>();
            string sql = SqlConstructor.ForShortToursTabJoin(toursArchiveTab) + $"JOIN orders_archive o ON t.ID = o.tour_id WHERE o.client_id = {clientId} ORDER BY o.order_date DESC;";
            toursListResult = GetToursListFromDb(sql);
            return toursListResult;
        }
        private List<ShortTour> GetToursListFromDb(string sql)
        {
            List<ShortTour> result = new List<ShortTour>();
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ShortTour tour = new Tour();
                    tour.ID = reader[0].ToString();
                    tour.Name = reader[1].ToString();
                    tour.DateStart = DateOnly.FromDateTime(DateTime.Parse(reader[2].ToString()));
                    tour.DateFinish = DateOnly.FromDateTime(DateTime.Parse(reader[3].ToString()));
                    tour.Hotel.ID = Int32.Parse(reader[4].ToString());
                    tour.Hotel.Name = reader[5].ToString();
                    tour.Hotel.Category = reader[6].ToString();
                    tour.City.ID = Int32.Parse(reader[7].ToString());
                    tour.City.Name = reader[8].ToString();
                    tour.Price = Int32.Parse(reader[9].ToString());
                    tour.ImgLink = reader[10].ToString();
                    result.Add(tour);
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return result;
        }
        
        public static string GetSqlTabJoin(string tabName)
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
