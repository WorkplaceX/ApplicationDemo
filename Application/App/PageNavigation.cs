namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class PageNavigation : Page
    {
        public PageNavigation(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "<h1>Navigation</h1>";
            await this.ComponentCreate<Grid>().LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<Navigation>();
        }

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            // Remove all pages.
            this.ComponentList().OfType<Page>().ToList().ForEach(page => page.ComponentRemove());
            if (grid.GridRowSelected() is Navigation navigation)
            {
                if (navigation.PageName == "Country")
                {
                    await this.ComponentPageShowAsync<PageCountry>();
                }
            }
        }
    }
}
