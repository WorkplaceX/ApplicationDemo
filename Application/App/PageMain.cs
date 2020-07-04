﻿namespace Application
{
    using Database.Demo;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageMain : Page
    {
        public PageMain(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            NavBar = new BootstrapNavbarMain(this);
            GridNavigation = new GridNavigation (this) { IsHide = true };
            var gridLanguage = new GridLanguage(this) { IsHide = true };
            Content = new Div(this);

            await Task.WhenAll(GridNavigation.LoadAsync(), gridLanguage.LoadAsync());

            NavBar.GridAdd(GridNavigation);
            NavBar.GridAdd(gridLanguage, isSelectedMode: true);
            NavBar.BrandTextHtml = "Demo<b>App</b>";
        }

        public GridNavigation GridNavigation;

        public BootstrapNavbar NavBar;

        public Div Content;

        /// <summary>
        /// Gets LoginUser. Currently singed in user.
        /// </summary>
        public LoginUser LoginUser;

        /// <summary>
        /// Gets LoginUserPermissionDisplayList. Permissions of currently signed in user.
        /// </summary>
        public List<LoginUserPermissionDisplay> LoginUserPermissionDisplayList;
    }

    public class GridNavigation : Grid<Navigation>
    {
        public GridNavigation(ComponentJson owner) : base(owner) { }

        protected override void Query(QueryArgs args, QueryResult result)
        {
            result.Query = args.Query.OrderBy(item => item.Sort);
        }

        protected override async Task RowSelectedAsync()
        {
            var pageMain = this.ComponentOwner<PageMain>();
            if (RowSelected.PageName == "Home")
            {
                pageMain.Content.ComponentListClear();
                await new PageAirplane(pageMain.Content).InitAsync();
                await new PageCountry(pageMain.Content).InitAsync();
            }

            if (RowSelected.PageName == "Airport")
            {
                pageMain.Content.ComponentListClear();
                await new PageAirport(pageMain.Content).InitAsync();
            }

            if (RowSelected.PageName == "About")
            {
                pageMain.Content.ComponentListClear();
                await new PageAbout(pageMain.Content).InitAsync();
            }

            if (RowSelected.PageName == "Roadmap")
            {
                pageMain.Content.ComponentListClear();
                await new PageRoadmap(pageMain.Content).InitAsync();
                this.ComponentOwner<AppJson>().Navigate("/roadmap/");
            }

            if (RowSelected.PageName == "LoginUser")
            {
                pageMain.Content.ComponentListClear();
                await new PageLoginUser(pageMain.Content).InitAsync();
            }

            if (RowSelected.PageName == "LoginRole")
            {
                pageMain.Content.ComponentListClear();
                await new PageLoginRole(pageMain.Content).InitAsync();
            }

            if (RowSelected.Name == "LoginSignIn")
            {
                pageMain.Content.ComponentListClear();
                await new PageLoginSignIn(pageMain.Content).InitAsync();
            }

            if (RowSelected.Name == "LoginSignOut")
            {
                pageMain.Content.ComponentListClear();
                await new PageLoginSignOut(pageMain.Content).InitAsync();
            }

            if (RowSelected.Name == "LoginProfile")
            {
                pageMain.Content.ComponentListClear();
                await new PageLoginProfile(pageMain.Content).InitAsync();
            }

            if (RowSelected.Name == "FileManager")
            {
                pageMain.Content.ComponentListClear();
                await new PageFileManager(pageMain.Content).InitAsync();
                this.ComponentOwner<AppJson>().Navigate("/filemanager/");
            }

            if (RowSelected.Name == "Shop")
            {
                pageMain.Content.ComponentListClear();
                await new PageShop(pageMain.Content).InitAsync();
            }

            if (RowSelected.Name == "ShopCart")
            {
                pageMain.Content.ComponentListClear();
                await new PageShopCart(pageMain.Content).InitAsync();
            }

            if (RowSelected.Name == "Cms")
            {
                pageMain.Content.ComponentListClear();
                await new PageCms(pageMain.Content).InitAsync();
            }

            if (RowSelected.Name == "CmsDoc")
            {
                pageMain.Content.ComponentListClear();
                await new PageCmsDoc(pageMain.Content).InitAsync();
            }

            if (RowSelected.Name == "CmsFile")
            {
                pageMain.Content.ComponentListClear();
                await new PageCmsFile(pageMain.Content).InitAsync();
            }
        }
    }

    public class GridLanguage : Grid<Language>
    {
        public GridLanguage(ComponentJson owner) : base(owner) { }
    }

    public class BootstrapNavbarMain : BootstrapNavbar
    {
        public BootstrapNavbarMain(ComponentJson owner) : base(owner) { }

        protected override void ButtonTextHtml(BootstrapNavbarButtonArgs args, BootstrapNavbarButtonResult result)
        {
            var pageMain = this.ComponentOwner<PageMain>();

            if (args.Row is Navigation navigation)
            {
                if (navigation.Name == "LoginUser")
                {
                    if (pageMain.LoginUser?.Name != null)
                    {
                        result.TextHtml = navigation.TextHtml + " (" + pageMain.LoginUser.Name + ")";
                    }
                }
            }
        }
    }
}
