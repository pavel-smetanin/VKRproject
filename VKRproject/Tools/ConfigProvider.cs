using Microsoft.Extensions.Configuration;

namespace VKRproject.Tools
{
    public static class ConfigProvider
    {
        private static string fileMain = "Properties/appsettings.json";
        public static IConfiguration Configuration { get; private set; }
        public static IConfiguration PrivateConfig { get; private set; }
        public static IConfiguration ApiCongig { get; private set; }
        static ConfigProvider()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile(fileMain).Build();
            if(PrivateConfig == null) 
            {
                PrivateConfig = new ConfigurationBuilder().AddJsonFile(Configuration["ConfigFiles:Private"]).Build();
            }
            if(ApiCongig == null) 
            { 
                ApiCongig = new ConfigurationBuilder().AddJsonFile(Configuration["ConfigFiles:Api"]).Build();
            }
        }
        public static string[] GetUrlsArray(string arrayName)
        {
            if (ApiCongig != null)
            {
                var list = ApiCongig.GetSection(arrayName).Get<string[]>();
                return list;
            }
            else
                return null;
        }
    }
}
