using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    }
}