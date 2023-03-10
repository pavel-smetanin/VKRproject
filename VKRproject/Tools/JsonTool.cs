using System.Text.Json;
using System.Text.Json.Nodes;

namespace VKRproject.Tools
{
    public static class JsonTool
    {
        public static T GetModel<T>(string jsonStr)
        {
            try
            {
                T model = JsonSerializer.Deserialize<T>(jsonStr);
                if (model != null)
                    return model;
                else
                    throw new Exception("Json object is not Desserialize to model ");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            catch
            {
                throw new Exception("JsonStr is not readable");
            }
        }
        public static List<T> GetModelList<T>(string jsonStr)
        {
            try
            {
                List<T> modelList = JsonSerializer.Deserialize<List<T>>(jsonStr);
                if (modelList != null)
                    return modelList;
                else
                    throw new Exception("Json object is not Desserialize to model ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            catch
            {
                throw new Exception("JsonStr is not readable");
            }
        }
        public static void WriteJsonFile(string path, string jsonStr)
        {
            try
            {
                if (path.Contains(".json"))
                    File.WriteAllText(path, jsonStr);
                else
                    throw new Exception("No filename with format .json");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void WriteJsonFile(string path, JsonObject jsonObject)
        {
            try
            {
                WriteJsonFile(path, jsonObject.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void WriteJsonFile(string path, JsonArray jsonArray)
        {
            try
            {
                WriteJsonFile(path, jsonArray.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
