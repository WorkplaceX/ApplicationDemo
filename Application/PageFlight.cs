namespace Application
{
    using Database.dbo;
    using Framework.Application;
    using Framework.Component;

    public class PageFlight : Page
    {
        protected override void InitJson(App app)
        {
            new GridFieldSingle(this);
            //
            var gridName = Flight.GridNameDetail;
            new Grid(this, gridName);
            app.GridData.LoadDatabase(gridName);
        }
    }
}
