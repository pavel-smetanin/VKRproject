using VKRproject.Models;

namespace VKRproject.Modules
{
    public class TourStatModule
    {
        public List<TourStat> TourStats { get; set; }
        public TourStatFilter Filter { get; private set; }
        public TourStatFilter GetTourStatFilter(List<TourStat> tourStats, bool rateFlag = false)
        {
            TourStats = tourStats;
            Filter = new TourStatFilter();
            if(rateFlag)
            {
                NormalizeTourStatsByRate();
                SetTourStatFilter();
                return Filter;
            }
            else
            {
                SetTourStatFilter();
                return Filter;
            }
        }
        private void NormalizeTourStatsByRate()
        {
            foreach(var t in TourStats) 
            {
                if(t.Rate != 0)
                {
                    if(t.Rate < 5)
                    {
                        TourStats.Remove(t);
                    }
                }
            }
        }
        private void SetTourStatFilter()
        {
            if (TourStats.Count > 0)
            {
                if (TourStats.Count == 1)
                {
                    Filter.SetTourStatToFilter(TourStats[0]);
                    return;
                }
                if (TourStats.Count > 1)
                {
                    int sumNights = 0;
                    int sumAdults = 0;
                    int sumChilds = 0;
                    int sumPrices = 0;
                    List<int> countries = new List<int>();
                    foreach (var t in TourStats)
                    {
                        sumNights += t.NightsCount;
                        sumAdults += t.AdultsCount;
                        sumChilds += t.ChildCount;
                        sumPrices += t.Price;
                        countries.Add(t.CountryId);
                    }
                    Filter.NightsCount = sumNights / TourStats.Count;
                    Filter.AdultsCount = sumAdults / TourStats.Count;
                    Filter.ChildCount = sumChilds / TourStats.Count;
                    Filter.Price = sumPrices / TourStats.Count;
                    Filter.CountryIdList = countries.Distinct().ToList();
                    return;
                }
            }
            Filter = null;
            return;
        }
    }
}
