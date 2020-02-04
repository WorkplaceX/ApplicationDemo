namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageAirport : Page
    {
        public PageAirport(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            new Html(this) { TextHtml = "<h1>Airport</h1>" };
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
