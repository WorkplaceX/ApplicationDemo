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
                return Data.Query<Navigation>().OrderBy(item => item.Sort);
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
                if (((Navigation)grid.RowSelected).PageName == "Home")
                {
                    Content.ComponentListClear();
                    await new PageAirplane(Content).InitAsync();
                    await new PageCountry(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "Airport")
                {
                    Content.ComponentListClear();
                    await new PageAirport(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "About")
                {
                    Content.ComponentListClear();
                    await new PageAbout(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "Roadmap")
                {
                    Content.ComponentListClear();
                    await new PageRoadmap(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "LoginUser")
                {
                    Content.ComponentListClear();
                    await new PageLoginUser(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).PageName == "LoginRole")
                {
                    Content.ComponentListClear();
                    await new PageLoginRole(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).Name == "LoginSignIn")
                {
                    Content.ComponentListClear();
                    await new PageLoginSignIn(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).Name == "LoginSignOut")
                {
                    Content.ComponentListClear();
                    await new PageLoginSignOut(Content).InitAsync();
                }

                if (((Navigation)grid.RowSelected).Name == "LoginProfile")
                {
                    Content.ComponentListClear();
                    await new PageLoginProfile(Content).InitAsync();
                }
            }
        }
    }
}
