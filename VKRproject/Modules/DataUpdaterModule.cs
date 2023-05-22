using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Math.EC;
using VKRproject.Tools;

namespace VKRproject.Modules
{
    public class DataUpdaterModule
    {
        public static DateTime DateUpdate { get; private set; }
        public static int CountTours { get; private set; } = 0;
        public static int CountHotels { get; private set; } = 0;
        public static int CountCities { get; private set; } = 0;
        public static string Message { get; private set; } = "Message";
        public async Task Run()
        {
            DeleteData();
            //await UpdateCities();
            //await UpdateHotels();
            await UpdateTours();
            Console.WriteLine(DateTime.Now + " DataUpdater: the data of tours is update!");
        }

        private void DeleteData()
        {
            try
            {
                string citiesTable = "cities_data";
                string hotelsTable = "hotels_data";
                string toursTable = "tours_data";
                string sqlStr = $"DELETE FROM {toursTable};"; //DELETE FROM {hotelsTable}; DELETE FROM {citiesTable};";
                DbTool.ExcecuteQueryNonResult(sqlStr);
            }
            catch(Exception ex)
            {
                throw new Exception("Error in method DeleteData " + ex.Message);
            }
        }
        private async Task UpdateCities()
        {
            DateTime dateUpdate;
            
            try
            {
                string[] urls = { "https://module.sletat.ru/Main.svc/GetCities?countryId=40", "https://module.sletat.ru/Main.svc/GetCities?countryId=150" };
                JArray jsonArray = new JArray();
                JObject[] jsonObjects = await HttpApiTool.GetRequestWithJsonObjects(urls);

                string tableName = "cities_data";
                string columnStr = "(id, name, country_id, descr_url, popular)";
                string sqlStr = $"INSERT INTO {tableName} {columnStr} VALUES ";
                
                for(int i = 0; i < jsonObjects.Length; i++)
                {
                    jsonArray.Merge((JArray)jsonObjects[i]["GetCitiesResult"]["Data"]);
                }
                for (int i = 0; i < jsonArray.Count; i++)
                {
                    var element = jsonArray[i];
                    sqlStr += $"({element["Id"]}, '{element["Name"]}', {element["CountryId"]}, '{element["DescriptionUrl"]}', {element["IsPopular"]}),";
                }
                sqlStr = sqlStr.TrimEnd(',') + ';';
                DbTool.ExcecuteQueryNonResult(sqlStr);
                dateUpdate = DateTime.Now;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in method UpdateCities: " + ex.Message);
            }
        }
        private async Task UpdateHotels()
        {
            DateTime dateUpdate;
            try
            {
                string[] urls = { "https://module.sletat.ru/Main.svc/GetHotels?countryId=40", "https://module.sletat.ru/Main.svc/GetHotels?countryId=150" };
                JArray jsonArray = new JArray();
                JObject[] jsonObjects = await HttpApiTool.GetRequestWithJsonObjects(urls);

                string tableName = "hotels_data";
                string columnStr = "(id, name, category, rate, city_id)";
                string sqlStr = $"INSERT INTO {tableName} {columnStr} VALUES ";

                for (int i = 0; i < jsonObjects.Length; i++)
                {
                    jsonArray.Merge((JArray)jsonObjects[i]["GetHotelsResult"]["Data"]);
                }
                for (int i = 0; i < jsonArray.Count; i++)
                {
                    var element = jsonArray[i];
                    sqlStr += $"({element["Id"]}, '{element["Name"]}', '{element["StarName"]}', {element["Rate"].ToString().Replace(',', '.')}, {element["TownId"]}),";
                }
                sqlStr = sqlStr.TrimEnd(',') + ';';
                DbTool.ExcecuteQueryNonResult(sqlStr);
                dateUpdate = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in method UpdateHotels: " + ex.Message);
            }

        }
        private async Task UpdateTours() 
        {
            DateTime dateUpdate;
            try
            {
                string[] urls = { "http://module.sletat.ru/Main.svc/GetTours?groupBy=hotel&countryId=150", "http://module.sletat.ru/Main.svc/GetTours?groupBy=hotel&countryId=40" };
                JArray jsonArray = new JArray();
                var test1 = File.ReadAllText("C:/Users/Пользователь/Desktop/Лекции и практики/Дипломная работа/Проект ПО/API источников/Тестовые данные/ТурыЕгипет.json");
                var test2 = File.ReadAllText("C:/Users/Пользователь/Desktop/Лекции и практики/Дипломная работа/Проект ПО/API источников/Тестовые данные/ТурыРоссия.json");
                JObject[] jsonObjects = { JObject.Parse(test1), JObject.Parse(test2) };//await HttpApiTool.GetRequestWithJsonObjects(urls);
                string tableName = "tours_data";
                string columnStr = "(id, op_id, hotel_id, city_id, country_id, name, room, meal_code, accom_code, date_start, date_finish, " +
                    "nights_count, adults_count, child_count, op_links, img_link, dep_city_id, price)";
                string sqlStr = $"INSERT INTO {tableName} {columnStr} VALUES ";

                for (int i = 0; i < jsonObjects.Length; i++)
                {
                    jsonArray.Merge((JArray)jsonObjects[i]["GetToursResult"]["Data"]["aaData"]);
                }
                for (int i = 0; i < jsonArray.Count; i++)
                {
                    var element = (JArray)jsonArray[i];
                    sqlStr += $"('{element[0]}', {element[1]}, {element[3]}, {element[5]}, {element[30]}, '{element[6]}', '{element[9]}', " +
                        $"'{element[10]}', '{element[11]}', STR_TO_DATE('{element[12]}', '%d.%m.%Y'), STR_TO_DATE('{element[13]}', '%d.%m.%Y'), {element[14]}, {element[16]}, {element[17]}, " +
                        $"'{element[20]}', '{element[29]}', {element[32]}, {element[42]}),";
                }
                sqlStr = sqlStr.TrimEnd(',') + ';';
                DbTool.ExcecuteQueryNonResult(sqlStr);
                dateUpdate = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in method UpdateHotels: " + ex.Message);
            }
        }
    }
}
