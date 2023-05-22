namespace VKRproject.Models
{
    public class Filter: BaseFilter
    {
        public int CountryId { get; set;}
        public bool OpFlag { get; set; } 
        public List<int> OperatorsId { get; set;}
        public bool DepCityFlag { get; set; }
        public int DepCityId { get; set; }
        public string DateLower { get; set ; }
        public string DateUpper { get; set; }
        public int MinNightsCount { get; set; }
        public int PriceLower { get; set; }
        public int PriceUpper { get; set; }
        public bool MealFlag { get; set; } 
        public string MealCode { get; set; }
        public bool CategoryFlag { get; set; } 
        public string Category { get; set;  }
        public bool RateFlag { get; set; } 
        public double Rate { get; set; }
        public static Filter FromQuestionFilterToFilter(QuestionFilter qF)
        {
            return new Filter
            {
                CountryId = qF.CountryID,
                DateLower = qF.StartDateLower,
                DateUpper = qF.StartDateUpper,
                NightsCount = qF.NightsCount,
                AdultsCount = qF.AdultsCount,
                ChildCount = qF.ChildCount,
                PriceLower = qF.PriceLower,
                PriceUpper = qF.PriceUpper
            };
        }
    }
}
