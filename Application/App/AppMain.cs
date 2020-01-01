namespace Application
{
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Threading.Tasks;

    public class AppMain : AppJson
    {
        public override async Task InitAsync()
        {
            if (this.IsSessionExpired)
            {
                this.HtmlSessionExpired = this.BootstrapAlert("Session expired!", BootstrapAlertEnum.Warning);
            }

            bool isDemoPage = false;

            if (isDemoPage == false)
            {
                await new PageGrid2(this).InitAsync();

                // await this.ComponentPageShowAsync<PageAirplane>();
                // await this.ComponentPageShowAsync<PageCountry>();
            }
            else
            {
                await this.ComponentPageShowAsync<PageDemo>();
            }

            this.Button = new Button(this) { TextHtml = "Click" };
            new Button(this) { TextHtml = "Click2" };
        }

        public Button Button;

        public Html HtmlSessionExpired;

        protected override Task<bool> GridUpdateAsync(Grid grid, Row row, Row rowNew, DatabaseEnum databaseEnum)
        {
            return base.GridUpdateAsync(grid, row, rowNew, databaseEnum);
        }

        protected override Task ProcessAsync()
        {
            if (Button.IsClick)
            {
                Button.TextHtml += ".";
            }
            if (this.HtmlSessionExpired != null && this.IsSessionExpired == false)
            {
                this.HtmlSessionExpired.ComponentRemove();
            }
            return base.ProcessAsync();
        }
    }

    public class AppX : AppJson
    {
        public override Task InitAsync()
        {
            new Html(this) { TextHtml = "Hello ApplicationX" };

            return base.InitAsync();
        }
    }
}
