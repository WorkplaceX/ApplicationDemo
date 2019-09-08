namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class PageDemo : Page
    {
        public PageDemo() { }

        public PageDemo(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "<h1>Demo</h1>";
            NavBar();
            await GridNavigation().LoadAsync();
            await GridLanguage().LoadAsync();
            NavBar().GridIndexList.Add(GridNavigation().Index.Value);
            NavBar().GridIndexList.Add(GridLanguage().Index.Value);
            NavBar().BrandTextHtml = "Demo<b>App</b>";
        }

        public BootstrapNavbar NavBar()
        {
            return this.ComponentGetOrCreate<BootstrapNavbar>();
        }

        public Grid GridNavigation()
        {
            return this.ComponentGetOrCreate<DivContainer>(div => div.CssClass="row").ComponentGetOrCreate<Div>(0, div => div.CssClass = "col").ComponentGetOrCreate<Grid>();
        }

        public Grid GridLanguage()
        {
            return this.ComponentGetOrCreate<DivContainer>(div => div.CssClass = "row").ComponentGetOrCreate<Div>(1, div => div.CssClass = "col").ComponentGetOrCreate<Grid>();
        }

        public Div Content()
        {
            return this.ComponentGetOrCreate<DivContainer>(div => div.CssClass = "row").ComponentGetOrCreate<Div>(2, div => div.CssClass = "col").ComponentGetOrCreate<Div>();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == GridNavigation())
            {
                return Data.Query<Navigation>();
            }
            if (grid == GridLanguage())
            {
                return Data.Query<Language>();
            }
            return base.GridQuery(grid);
        }

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            if (grid == GridNavigation())
            {
                Content().List.Clear();
                if (grid.GridRowSelected<Navigation>().PageName == "Country")
                {
                    await Content().ComponentPageShowAsync<PageCountry>();
                }
            }
        }
    }
}
