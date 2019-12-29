namespace Application
{
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Threading.Tasks;

    public class AppMain : AppJson
    {
        protected override async Task InitAsync()
        {
            if (this.IsSessionExpired)
            {
                this.HtmlSessionExpired = this.BootstrapAlert("Session expired!", BootstrapAlertEnum.Warning);
            }

            bool isDemoPage = false;

            if (isDemoPage == false)
            {
                await this.ComponentPageShowAsync<PageAirplane>();
                await this.ComponentPageShowAsync<PageCountry>();
            }
            else
            {
                await this.ComponentPageShowAsync<PageDemo>();
            }

            this.Button = this.ComponentCreate<Button>((button) => button.TextHtml = "Click");
            this.ComponentCreate<Button>((button) => button.TextHtml = "Click2");
        }

        protected override Task ButtonClickAsync(Button button)
        {
            if (button == Button)
            {
                button.TextHtml += ".";
            }

            return base.ButtonClickAsync(button);
        }

        public Button Button;

        public Html HtmlSessionExpired;

        protected override Task<bool> GridUpdateAsync(Grid grid, Row row, Row rowNew, DatabaseEnum databaseEnum)
        {
            return base.GridUpdateAsync(grid, row, rowNew, databaseEnum);
        }

        protected override Task ProcessAsync()
        {
            if (this.HtmlSessionExpired != null && this.IsSessionExpired == false)
            {
                this.HtmlSessionExpired.ComponentRemove();
            }
            return base.ProcessAsync();
        }
    }

    public class AppX : AppJson
    {
        protected override Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "Hello ApplicationX";

            return base.InitAsync();
        }
    }
}
