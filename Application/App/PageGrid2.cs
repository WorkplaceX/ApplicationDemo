namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageGrid2 : Page
    {
        public PageGrid2(ComponentJson owner) 
            : base(owner)
        {

        }

        public override async Task InitAsync()
        {
            await new Grid2(this).LoadAsync();
        }

        protected override IQueryable Grid2Query(Grid2 grid)
        {
            return Data.Query<RawWikipediaAircraft>();
        }
    }
}
