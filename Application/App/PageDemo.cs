﻿namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class PageDemo : Page
    {
        public PageDemo(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            new Html(this) { TextHtml = "<h1>Demo</h1>" };
            NavBar = new BootstrapNavbar(this);

            DivContainer divContainer = new DivContainer(this) { CssClass = "row" };
            Div divCol0 = new Div(divContainer) { CssClass = "col" };
            Div divCol1 = new Div(divContainer) { CssClass = "col" };
            Div divCol2 = new Div(divContainer) { CssClass = "col" };

            GridNavigation = new Grid2(divCol0);
            GridLanguage = new Grid2(divCol1);
            Content = new Div(divCol2);

            await GridNavigation.LoadAsync();
            await GridLanguage.LoadAsync();
            NavBar.GridAdd(GridNavigation);
            NavBar.GridAdd(GridLanguage);
            NavBar.BrandTextHtml = "Demo<b>App</b>";
        }

        public BootstrapNavbar NavBar;

        public Grid2 GridNavigation;

        public Grid2 GridLanguage;

        public Div Content;

        protected override IQueryable GridQuery(Grid2 grid)
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

        protected override async Task GridRowSelectedAsync(Grid2 grid)
        {
            if (grid == GridNavigation)
            {
                Content.List.Clear();
                
                if (((Navigation)grid.RowSelected).PageName == "Country")
                {
                    await Content.ComponentPageShowAsync<PageCountry>();
                }
            }
        }
    }
}
