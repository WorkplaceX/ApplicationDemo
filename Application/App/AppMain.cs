namespace Application
{
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Threading.Tasks;

    public class AppMain : AppJson
    {
        public AppMain() : this(null) { }

        public AppMain(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            //var modal = await this.ComponentPageShowAsync<BootstrapModal>();
            //modal.DivModalBody().ComponentCreate<Html>().TextHtml = "Body";
            //modal.DivModalHeader().ComponentCreate<Html>().TextHtml = "Header";

            //this.ComponentGetOrCreate<Html>(html => html.TextHtml = "Hello Demo");
            await this.ComponentPageShowAsync<PageAirplane>();
            await this.ComponentPageShowAsync<PageCountry>();
        }

        protected override Task<bool> GridUpdateAsync(Grid grid, Row row, Row rowNew, DatabaseEnum databaseEnum)
        {
            return base.GridUpdateAsync(grid, row, rowNew, databaseEnum);
        }
    }
}
