namespace VKRproject.Models.ViewModels
{
    public class AnalyticViewModel
    {
        public Dictionary<string, int> CountriesDict { get; set; }
        public Dictionary<string, int> OperatorsDict { get; set; }
        public KeyValuePair<string, int> MaxNightsCount { get; set; }
        public KeyValuePair<string, int> MinNightsCount { get; set; }
        public KeyValuePair<string, int> MaxPrice { get; set; }
        public KeyValuePair<string, int> MinPrice { get; set; }
        public KeyValuePair<string, int> MaxCountry { get; set; }
        public KeyValuePair<string, int> MaxCity { get; set; }
        public KeyValuePair<string, int> MaxHotel { get; set; }
    }
}
