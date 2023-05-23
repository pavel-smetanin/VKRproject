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
        public static bool ErrorFlag { get; private set; } = false;
        public async Task Run()
        {
            try
            {
                string sqlCities = await LoadCities();
                string sqlHotels = await LoadHotels();
                string sqlTours = await LoadTours();
                UpdateData(sqlCities, sqlHotels, sqlTours);
                DateUpdate = DateTime.Now;
                Console.WriteLine(DateUpdate + " DataUpdater: the data of tours is update!");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"{DateTime.Now} DataUpdater: erorr " + ex.Message);
                ErrorFlag = true;
            }
        }
        private async Task<string> LoadCities()
        {
            try
            {
                string[] urls = ConfigProvider.GetUrlsArray("cities"); 
                JArray jsonArray = new JArray();
                JObject[] jsonObjects = await HttpApiTool.GetRequestWithJsonObjects(urls);

                for(int i = 0; i < jsonObjects.Length; i++)
                {
                    jsonArray.Merge((JArray)jsonObjects[i]["GetCitiesResult"]["Data"]);
                }
                if (jsonArray.Count > 0)
                {
                    CountCities = jsonArray.Count;
                    string tableName = "cities_data";
                    string columnStr = "(id, name, country_id, descr_url, popular)";
                    string sqlStr = $"INSERT INTO {tableName} {columnStr} VALUES ";
                    for (int i = 0; i < jsonArray.Count; i++)
                    {
                        var element = jsonArray[i];
                        sqlStr += $"({element["Id"]}, '{element["Name"]}', {element["CountryId"]}, '{element["DescriptionUrl"]}', {element["IsPopular"]}),";
                    }
                    sqlStr = sqlStr.TrimEnd(',') + ';';
                    return sqlStr;
                }
                else
                {
                    throw new Exception("No data: the count of cities is 0 or null");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error in method LoadCities: " + ex.Message);
            }
        }
        private async Task<string> LoadHotels()
        {
            try
            {
                string[] urls = ConfigProvider.GetUrlsArray("hotels");
                JArray jsonArray = new JArray();
                JObject[] jsonObjects = await HttpApiTool.GetRequestWithJsonObjects(urls);

                for (int i = 0; i < jsonObjects.Length; i++)
                {
                    jsonArray.Merge((JArray)jsonObjects[i]["GetHotelsResult"]["Data"]);
                }
                if (jsonArray.Count > 0)
                {
                    CountHotels = jsonArray.Count;
                    string tableName = "hotels_data";
                    string columnStr = "(id, name, category, rate, city_id)";
                    string sqlStr = $"INSERT INTO {tableName} {columnStr} VALUES ";
                    for (int i = 0; i < jsonArray.Count; i++)
                    {
                        var element = jsonArray[i];
                        sqlStr += $"({element["Id"]}, '{element["Name"]}', '{element["StarName"]}', {element["Rate"].ToString().Replace(',', '.')}, {element["TownId"]}),";
                    }
                    sqlStr = sqlStr.TrimEnd(',') + ';';
                    return sqlStr;
                }
                else
                {
                    throw new Exception("No data: the count of hotels is 0 or null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in method UpdateHotels: " + ex.Message);
            }

        }
        private async Task<string> LoadTours() 
        {
            try
            {
                string[] urls = ConfigProvider.GetUrlsArray("tours");
                JArray jsonArray = new JArray();
                var test1 = File.ReadAllText("C:/Users/Пользователь/Desktop/Лекции и практики/Дипломная работа/Проект ПО/API источников/Тестовые данные/ТурыЕгипет.json");
                var test2 = File.ReadAllText("C:/Users/Пользователь/Desktop/Лекции и практики/Дипломная работа/Проект ПО/API источников/Тестовые данные/ТурыРоссия.json");
                JObject[] jsonObjects = { JObject.Parse(test1), JObject.Parse(test2) };//await HttpApiTool.GetRequestWithJsonObjects(urls);
                
                for (int i = 0; i < jsonObjects.Length; i++)
                {
                    jsonArray.Merge((JArray)jsonObjects[i]["GetToursResult"]["Data"]["aaData"]);
                }
                if (jsonArray.Count > 0)
                {
                    CountTours = jsonArray.Count;
                    string tableName = "tours_data";
                    string columnStr = "(id, op_id, hotel_id, city_id, country_id, name, room, meal_code, accom_code, date_start, date_finish, " +
                        "nights_count, adults_count, child_count, op_links, img_link, dep_city_id, price)";
                    string sqlStr = $"INSERT INTO {tableName} {columnStr} VALUES ";
                    for (int i = 0; i < jsonArray.Count; i++)
                    {
                        var element = (JArray)jsonArray[i];
                        sqlStr += $"('{element[0]}', {element[1]}, {element[3]}, {element[5]}, {element[30]}, '{element[6]}', '{element[9]}', " +
                            $"'{element[10]}', '{element[11]}', STR_TO_DATE('{element[12]}', '%d.%m.%Y'), STR_TO_DATE('{element[13]}', '%d.%m.%Y'), {element[14]}, {element[16]}, {element[17]}, " +
                            $"'{element[20]}', '{element[29]}', {element[32]}, {element[42]}),";
                    }
                    sqlStr = sqlStr.TrimEnd(',') + ';';
                    return sqlStr;
                }
                else
                {
                    throw new Exception("No data: the count of tours is 0 or null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in method LoadHotels: " + ex.Message);
            }
        }
        private void UpdateData(string sqlCities, string sqlHotels, string sqlTours)
        {
            try
            {
                DbTool.OpenDbConnection();
                string sqlScript = "BEGIN;";
                sqlScript += " DELETE FROM tours_data; DELETE FROM hotels_data; DELETE FROM cities_data; " +
                    $"{sqlCities} + {sqlHotels} + {sqlTours} ";
                sqlScript += " COMMIT; ";
                DbTool.ExcecuteQueryNonResultAndOpen(sqlScript);
            }
            catch(Exception ex)
            {
                string sqlCancel = " ROLLBACK; ";
                DbTool.ExcecuteQueryNonResultAndOpen(sqlCancel);
                DbTool.CloseDbConnection();
                throw new Exception("Error in method UpdateData: " + ex.Message);
            }
            DbTool.CloseDbConnection();
        }
    }
}
