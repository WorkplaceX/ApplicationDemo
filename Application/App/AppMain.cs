namespace Application
{
    using Framework.Json;
    using System.Threading.Tasks;

    public class AppMain : AppJson
    {
        public override async Task InitAsync()
        {
            if (this.IsSessionExpired)
            {
                this.HtmlSessionExpired = this.BootstrapAlert("Session expired!", BootstrapAlertEnum.Warning);
                this.IsScrollToTop = true;
            }

            bool isDemoPage = false;

            if (isDemoPage == false)
            {
                await new PageMain(this).InitAsync();
                // await new PageAirplane(this).InitAsync();
                // await new PageCountry(this).InitAsync();
            }
            else
            {
                await new PageDemo(this).InitAsync();
            }

            this.Button = new Button(this) { TextHtml = "Click" };
            new Button(this) { TextHtml = "Click2" };
        }

        public Button Button;

        public Html HtmlSessionExpired;

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
