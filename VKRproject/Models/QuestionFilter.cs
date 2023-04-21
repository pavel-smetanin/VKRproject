namespace VKRproject.Models
{
    public class QuestionFilter
    {
        public int CountryID { get; set; } = -1;
        public string StartDateLower { get; set; }
        public string StartDateUpper { get; set;}
        public string EndDate { get; set;}
        public int NightsCount { get; set; } = -1;
        public int AdultsCount { get; set; } = -1;
        public int ChildCount { get; set; } = -1;
        public int PriceLower { get; set; } = -1;
        public int PriceUpper { get; set; } = -1;
        public bool IsFieldFill()
        {
            if (CountryID > 0 && StartDateLower != null && StartDateUpper != null &&
                EndDate != null && NightsCount > 0 && AdultsCount > 0 && ChildCount >= 0 &&
                PriceLower > 0 && PriceUpper > 0)
                return true;
            else
                return false;
        }
        public void Reset()
        {
            CountryID = -1;
            StartDateLower = null;
            StartDateUpper = null;
            EndDate = null;
            NightsCount = -1;
            AdultsCount = -1;
            ChildCount = -1;
            PriceLower = -1;
            PriceUpper = -1;
        }
    }
}
