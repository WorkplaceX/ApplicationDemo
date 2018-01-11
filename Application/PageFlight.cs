namespace Application
{
    using Database.dbo;
    using Framework;
    using Framework.Application;
    using Framework.Component;

    public class PageFlight : Page
    {
        protected override void InitJson(App app)
        {
            var literalLogo = new Literal(this);
            literalLogo.TextHtml = "<img class='imgLogo' src='/Arrival.png' />";
            new GridFieldSingle(this).CssClass = "gridFieldSingle";
            //
            var gridName = Flight.GridNameDetail;
            new Grid(this, gridName);
            app.GridData.LoadDatabase(gridName);
            //
            new Label(this).Text = UtilFramework.VersionServer;
        }
    }
}
