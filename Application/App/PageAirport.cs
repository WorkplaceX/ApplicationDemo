namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Json.Bing;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageAirport : Page
    {
        public PageAirport(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            new Html(this) { TextHtml = "<h1>Airport <i class='fas fa-plane-departure'></i></h1>" };
            new Html(this) { TextHtml = "Browse world's airports from <a href='https://openflights.org/data.html' target='_blank'>openflights.org</a> data and see location on Bing maps <a href='https://www.bingmapsportal.com/' target='_blank'>bingmapsportal.com</a>.<br/><br/>" };
            Grid = new Grid(this);
            BingMap = new BingMap(this) { Key = "AhMAiuDtXQ4130XXm2aJD9" 
                + "MF0KkVTvn3NjY11v9dHBpeuBEkZ96tEGw_Oy9O5w" 
                + "_I" };
            await Grid.LoadAsync();
        }

        public Grid Grid;

        public BingMap BingMap;

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == Grid)
            {
                return Data.Query<RawOpenFlightsAirport>();
            }
            return base.GridQuery(grid);
        }

        protected override Task GridRowSelectedAsync(Grid grid)
        {
            BingMap.Long = ((RawOpenFlightsAirport)grid.RowSelected).Longitude;
            BingMap.Lat = ((RawOpenFlightsAirport)grid.RowSelected).Latitude;
            return base.GridRowSelectedAsync(grid);
        }
    }
}
