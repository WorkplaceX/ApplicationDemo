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
            await Grid.LoadAsync();
        }

        public Grid Grid;

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == Grid)
            {
                return Data.Query<RawOpenFlightsAirport>();
            }
            return base.GridQuery(grid);
        }
    }
}
