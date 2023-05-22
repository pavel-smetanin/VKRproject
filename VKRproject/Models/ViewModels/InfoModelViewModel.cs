namespace VKRproject.Models.ViewModels
{
    public class InfoModelViewModel:LayoutViewModel
    {
        public City City { get; set; } = new City();
        public DepCity DepCity { get; set; } = new DepCity();
        public Hotel Hotel { get; set; } = new Hotel();
        public TourOperator TourOperator { get; set; } = new TourOperator();
        public Country Country { get; set; } = new Country();
        public Tour Tour { get; set; } = new Tour();
    }
}
