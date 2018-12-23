namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.Dal;
    using Framework.Json;

    public class PageCountry : Page
    {
        public PageCountry() { }

        public PageCountry(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            await this.ComponentCreate<Grid>().LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return UtilDal.Query<CountryDisplay>();
        }
    }
}
