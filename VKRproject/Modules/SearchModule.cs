using VKRproject.Models;
using VKRproject.Tools;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace VKRproject.Modules
{
    public class SearchModule
    {
        public List<Tour> GetToursListByFilter(Filter filter)
        {
            try
            {/*
                Filter filter = new Filter
                {
                    CountryId = 40,
                    DateLower = DateOnly.Parse("01.02.2014"),
                    DateUpper = DateOnly.Parse("28.02.2014"),
                    NightsCount = 13,
                    AdultsCount = 2,
                    ChildCount = 0,
                    PriceLower = 1000,
                    PriceUpper = 100000
                };*/
                List<Tour> toursListResult = new List<Tour>();
                string tableName = "tours_data";
                string sql = $"SELECT t.ID, t.op_id, op.name, t.hotel_id, t.city_id, c_d.name, t.country_id, c.name, t.name, t.room, t.meal_code, t.accom_code," +
                    $"t.date_start, t.date_finish, t.nights_count, t.adults_count, t.child_count, t.op_links, t.img_link, t.dep_city_id, d.name, t.price " +
                    $"FROM tours_data t JOIN tour_operators op ON t.op_id = op.ID " +
                    $"JOIN cities_data c_d ON t.city_id = c_d.ID " +
                    $"JOIN countries c ON t.country_id = c.ID " +
                    $"JOIN dep_cities d ON t.dep_city_id = d.ID " +
                    $"WHERE t.country_id = {filter.CountryId} AND " +
                    $"(date_start >= STR_TO_DATE('{filter.DateLower}', '%Y-%m-%d') AND date_start <= STR_TO_DATE('{filter.DateUpper}', '%Y-%m-%d')) AND " +
                    $"nights_count <= {filter.NightsCount} AND adults_count = {filter.AdultsCount} AND child_count = {filter.ChildCount} AND " +
                    $"(price >= {filter.PriceLower} AND price <= {filter.PriceUpper}) ORDER BY date_start DESC;";
                string sql2 = $"SELECT * FROM {tableName} WHERE country_id = 40 LIMIT 5;";
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
                        toursListResult.Add(tour);
                    }
                }
                reader.Close();
                DbTool.CloseDbConnection();
                return toursListResult;
            }
            catch(Exception ex)
            {
                throw new Exception("Erorr in SearchModule: " + ex.Message);
            }
        }

    }
}
