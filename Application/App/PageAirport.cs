namespace Application
{
    using Database.Demo;
    using Framework.Json;
    using Framework.Json.Bing;
    using System.Threading.Tasks;

    public class PageAirport : Page
    {
        public PageAirport(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            new Html(this) { TextHtml = "<h1>Airport <i class='fas fa-plane-departure'></i></h1>" };
            new Html(this) { TextHtml = "Browse world's airports from <a href='https://openflights.org/data.html' target='_blank'>openflights.org</a> data and see location on Bing maps <a href='https://www.bingmapsportal.com/' target='_blank'>bingmapsportal.com</a>.<br/><br/>" };
            var grid = new GridAirport(this);
            BingMap = new BingMap(this) { Key = "AhMAiuDtXQ4130XXm2aJD9" 
                + "MF0KkVTvn3NjY11v9dHBpeuBEkZ96tEGw_Oy9O5w" 
                + "_I" };
            await grid.LoadAsync();
        }

        public BingMap BingMap;
    }

    public class GridAirport : Grid<RawOpenFlightsAirport>
    {
        public GridAirport(ComponentJson owner) : base(owner) { }

        protected override Task RowSelectedAsync()
        {
            var pageAirport = this.ComponentOwner<PageAirport>();
            pageAirport.BingMap.Long = RowSelected.Longitude;
            pageAirport.BingMap.Lat = RowSelected.Latitude;
            return base.RowSelectedAsync();
        }
    }
}
