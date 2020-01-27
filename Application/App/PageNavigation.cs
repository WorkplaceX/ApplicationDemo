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

        public override async Task InitAsync()
        {
            new Html(this) { TextHtml = "<h1>Navigation</h1>" };
            await new Grid(this).LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<Navigation>();
        }

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            // Remove all pages.
            this.List.OfType<Page>().ToList().ForEach(page => page.ComponentRemove());
            if (grid.RowSelected is Navigation navigation)
            {
                if (navigation.PageName == "Country")
                {
                    await this.ComponentPageShowAsync<PageCountry>();
                }
            }
        }
    }
}
