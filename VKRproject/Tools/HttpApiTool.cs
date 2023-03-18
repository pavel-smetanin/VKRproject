using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Policy;
using System.Text.Json.Nodes;

namespace VKRproject.Tools
{
    public static class HttpApiTool
    {
        private static HttpClient httpClient = new HttpClient();
        public static async Task<string> GetRequest(string apiUrl)
        {
            try
            {   
                var response = await httpClient.GetAsync(apiUrl);
                if (response.StatusCode == HttpStatusCode.OK)
                    return await response.Content.ReadAsStringAsync();
                else
                    throw new Exception("Server status: " + response.StatusCode.ToString());
            }
            catch(Exception ex)
            {
                throw new Exception("Request is failed! " + ex.Message);
            }
        }
        public static async Task<JObject[]> GetRequestWithJsonObject(string[] urls)
        {
            try
            {
                JObject[] result = new JObject[urls.Length];
                for (int i = 0; i < urls.Length; i++)
                {
                    var str = await HttpApiTool.GetRequest(urls[i]);
                    result[i] = (JObject)JObject.Parse(str);
                }
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("Erorr " + ex.Message);
            }
        }
    }
}