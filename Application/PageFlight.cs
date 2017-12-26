namespace Application
{
    using Database.dbo;
    using Framework.Application;
    using Framework.Component;

    public class PageFlight : Page
    {
        protected override void InitJson(App app)
        {
            var gridName = new GridName<Flight>();
            new Grid(this, gridName);
            app.GridData.LoadDatabase(gridName);
        }
    }
}
