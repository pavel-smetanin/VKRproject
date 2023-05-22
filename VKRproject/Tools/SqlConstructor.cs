namespace VKRproject.Tools
{
    public static class SqlConstructor
    {
        public static string ForShortToursTabJoin(string tabName)
        {
            return $"SELECT t.ID, t.name, t.date_start, t.date_finish, t.hotel_id, h.name, h.category, t.city_id, c.name, t.price, t.img_link " +
                $"FROM {tabName} t JOIN cities_data c ON t.city_id = c.ID JOIN hotels_data h ON t.hotel_id = h.ID ";
        }
        public static string ForToursTabJoin(string tabName)
        {
            return "SELECT t.ID, t.op_id, op.name, t.hotel_id, h.name, h.category, t.city_id, c_d.name, t.country_id, c.name, c.visa, " +
                "t.name, t.room, t.meal_code, m.description, t.accom_code, a.description, " +
                "t.date_start, t.date_finish, t.nights_count, t.adults_count, t.child_count, t.op_links, " +
                "t.img_link, t.dep_city_id, d.name, t.price " +
                $"FROM {tabName} t JOIN tour_operators op ON t.op_id = op.ID " +
                "JOIN hotels_data h ON t.hotel_id = h.ID " +
                "JOIN cities_data c_d ON t.city_id = c_d.ID " +
                "JOIN meals_type m ON t.meal_code = m.code " +
                "JOIN accoms_type a ON t.accom_code = a.code " +
                "JOIN countries c ON t.country_id = c.ID " +
                "JOIN dep_cities d ON t.dep_city_id = d.ID ";
        }
        public static string ListInStr<T>(List<T> list)
        {
            string result = "";
            foreach (var l in list)
            {
                result += l.ToString() + ',';
            }
            result = result.TrimEnd(',');
            return result;
        }
    }
}
