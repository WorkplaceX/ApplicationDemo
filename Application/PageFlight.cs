namespace Application
{
    using Database.dbo;
    using Framework;
    using Framework.Application;
    using Framework.Component;
    using Framework.Server;

    public class PageFlight : Page
    {
        protected override void InitJson(App app)
        {
            var literalLogo = new Literal(this);
            string url = UtilServer.EmbeddedUrl(app, "/Arrival.png");
            literalLogo.TextHtml = string.Format("<img class='imgLogo' src='{0}' />", url);
            new GridFieldSingle(this).CssClass = "gridFieldSingle";
            //
            var gridName = Flight.GridName;
            new Grid(this, gridName);
            // app.GridData.LoadDatabase(gridName);
            //
            new Grid(this, Airport.GridName);
            app.GridData.LoadDatabaseInit(Airport.GridName);
            //
            new Label(this).Text = UtilFramework.VersionServer;
        }
    }
}
