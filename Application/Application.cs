﻿namespace Application
{
    using Database.dbo;
    using Framework;
    using Framework.Application;
    using Framework.Component;
    using Framework.Server;
    using System;

    /// <summary>
    /// AppSelector has to be in same assembly like App classes.
    /// </summary>
    public class AppSelectorDemo : AppSelector
    {
        public AppSelectorDemo()
        {
            // UtilFramework.UnitTest();
            // UtilFramework.UnitTest(typeof(AppDemo)); // Enable InMemory database.
        }
    }

    public class AppDemo : App
    {
        protected override Type TypePageMain()
        {
            return typeof(PageNavigation);
            // return typeof(PageCmsConfig);
        }

        public static FrameworkLoginPermission PermissionAirportFull()
        {
            return new FrameworkLoginPermission("AirportFull", "Add, update and remove Airport", "MasterData", "AdminApp");
        }

        public static FrameworkLoginPermission PermissionFlightFull()
        {
            return new FrameworkLoginPermission("FlightFull", "Add, update and remove Flight", "TransactionData", "AdminApp");
        }

        public static FrameworkLoginPermission PermissionFlightReadOnly()
        {
            return new FrameworkLoginPermission("FlightReadOnly", "Read Flight", "Guest", "TransactionData", "AdminApp");
        }
    }

    public class PageNavigation : Page
    {
        public PageNavigation() { }

        public PageNavigation(Component owner)
            : base(owner)
        {

        }

        protected override void InitJson(App app)
        {
            new Navigation(this);
            //
            new Label(this).Text = UtilFramework.VersionServer;
        }
    }

    public class PageFlight : Page
    {
        protected override void InitJson(App app)
        {
            var literalImage = new Literal(this);
            string url = UtilServer.EmbeddedUrl(app, "/Arrival.png");
            literalImage.TextHtml = string.Format("<img class='imgLogo' src='{0}' />", url);
            new GridFieldSingle(this).CssClass = "gridFieldSingle";
            //
            var gridName = Flight.GridName;
            new Grid(this, gridName);
            // app.GridData.LoadDatabase(gridName);
            //
            new Grid(this, Airport.GridName);
            app.GridData.LoadDatabaseInit(Airport.GridName);
        }
    }

    public class PageAirport : Page
    {
        public PageAirport() { }

        public PageAirport(Component owner)
            : base(owner)
        {

        }

        protected override void InitJson(App app)
        {
            var literalImage = new Literal(this);
            string url = UtilServer.EmbeddedUrl(app, "/Airport.jpg");
            literalImage.TextHtml = string.Format("<img class='imgLogo' src='{0}' />", url);
            new Literal(this) { TextHtml = "<h1>Airport List<h1>" };
            new Literal(this) { TextHtml = "<p>Following list shos all of the world's airports.<p>" };
            new Grid(this, Airport.GridName);
        }
    }

    public class PageAirline : Page
    {
        public PageAirline() { }

        public PageAirline(Component owner)
            : base(owner)
        {

        }

        protected override void InitJson(App app)
        {
            var literalImage = new Literal(this);
            string url = UtilServer.EmbeddedUrl(app, "/Airline.png");
            literalImage.TextHtml = string.Format("<img class='imgLogo' src='{0}' />", url);
            new Literal(this) { TextHtml = "<h1>Airline List<h1>" };
            new Literal(this) { TextHtml = "<p>Following list shos all airlines and the countries they belong to.<p>" };
            new Grid(this, new GridName<Airline>());
        }
    }
}