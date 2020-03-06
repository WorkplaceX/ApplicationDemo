namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
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

            await new PageMain(this).InitAsync();

            // this.Button = new Button(this) { TextHtml = "Click" };
            // new Button(this) { TextHtml = "Click2" };
        }

        public Button Button;

        public Html HtmlSessionExpired;

        protected override Task ProcessAsync()
        {
            // if (Button.IsClick)
            // {
            //     Button.TextHtml += ".";
            // }
            if (this.HtmlSessionExpired != null && this.IsSessionExpired == false)
            {
                this.HtmlSessionExpired.ComponentRemove();
            }
            return base.ProcessAsync();
        }

        protected override async Task<byte[]> FileDownload(string fileName)
        {
            var result = (await Data.SelectAsync(Data.Query<File>().Where(item => item.FileName == fileName))).FirstOrDefault();
            return result?.Data;
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
