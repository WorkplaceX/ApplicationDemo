namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageMain : Page
    {
        public PageMain(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            NavBar = new BootstrapNavbar(this);
            GridNavigation = new Grid(this) { IsHide = true };
            GridLanguage = new Grid(this) { IsHide = true };
            Content = new Div(this);

            await GridNavigation.LoadAsync();
            await GridLanguage.LoadAsync();

            NavBar.GridAdd(GridNavigation);
            NavBar.GridAdd(GridLanguage);
            NavBar.BrandTextHtml = "Demo<b>App</b>";
        }

        public BootstrapNavbar NavBar;

        public Grid GridNavigation;

        public Grid GridLanguage;

        public Div Content;

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == GridNavigation)
            {
                return Data.Query<Navigation>();
            }
            if (grid == GridLanguage)
            {
                return Data.Query<Language>();
            }
            return base.GridQuery(grid);
        }

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            if (grid == GridNavigation)
            {
                Content.ComponentListClear();

                if (((Navigation)grid.RowSelected).PageName == "Home")
                {
                    await new PageAirplane(Content).InitAsync();
                    await new PageCountry(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "Airport")
                {
                    await new PageAirport(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "About")
                {
                    await new PageAbout(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "Roadmap")
                {
                    await new PageRoadmap(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "LoginUser")
                {
                    await new PageLoginUser(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "LoginRole")
                {
                    await new PageLoginRole(Content).InitAsync();
                }
            }
        }
    }
}
