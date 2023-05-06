namespace VKRproject.Models
{
    public class TourStatFilter : BaseFilter
    {
        public List<int> CountryIdList { get; set; }
        public int Price { get; set; }
        public void SetTourStatToFilter(TourStat tourStat)
        {
            CountryIdList.Add(tourStat.CountryId);
            NightsCount = tourStat.NightsCount;
            AdultsCount = tourStat.AdultsCount;
            ChildCount = tourStat.ChildCount;
            Price = tourStat.Price;
        }
    }
}
