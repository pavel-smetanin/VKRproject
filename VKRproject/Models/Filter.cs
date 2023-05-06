namespace VKRproject.Models
{
    public class Filter: BaseFilter
    {
        public int CountryId { get; set;}
        public List<int> OperatorsId { get; set;}
        public string DateLower { get; set ; }
        public string DateUpper { get; set; }
        public int PriceLower { get; set; }
        public int PriceUpper { get; set; }
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
