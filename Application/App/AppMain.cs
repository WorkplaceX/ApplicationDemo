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
            SettingEnum = SettingEnum.Bootstrap;
         
            if (IsSessionExpired)
            {
                HtmlSessionExpired = this.CreateAlert("Session expired!", AlertEnum.Warning);
                IsScrollToTop = true;
            }

            // new Custom01(this) { TextHtml = "Hello <b>World</b>" };

            PageMain = new PageMain(this);
            await PageMain.InitAsync();

            PageCmsContent = new PageCmsContent(this) { IsHide = true };

            // this.Button = new Button(this) { TextHtml = "Click" };
            // new Button(this) { TextHtml = "Click2" };

            Title = "Demo Application";
        }

        public PageMain PageMain;

        public PageCmsContent PageCmsContent;

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

        protected override async Task NavigateAsync(NavigateArgs args, NavigateResult result)
        {
            if (args.IsFileName(UtilCms.PathCmsFile(), out string fileName))
            {
                var row = (await Data.Query<CmsFile>().Where(item => item.FileName == fileName).QueryExecuteAsync()).FirstOrDefault();
                result.Data = row?.Data;
            }
            else
            {
                if (args.IsFileName("/shop/", out string fileNameShop))
                {
                    var row = (await Data.Query<ShopProductPhoto>().Where(item => item.FileName == fileNameShop).QueryExecuteAsync()).FirstOrDefault();
                    result.Data = row?.Data;
                }
                else
                {
                    if (args.IsNavigatePath(UtilCms.PathCmsPage(), out _))
                    {
                        result.IsSession = true;
                    }
                    else
                    {
                        var row = (await Data.Query<StorageFile>().Where(item => item.FileName == args.FileName).QueryExecuteAsync()).FirstOrDefault();
                        result.Data = row?.Data;
                    }
                }
            }
            if (args.IsNavigatePath("/filemanager/"))
            {
                result.IsSession = true;
            }
            if (args.IsNavigatePath("/roadmap/"))
            {
                result.IsSession = true;
            }
        }

        protected override async Task NavigateSessionAsync(NavigateArgs args, NavigateSessionResult result)
        {
            if (args.NavigatePath == "/")
            {
                PageMain.IsHide = false;
                PageCmsContent.IsHide = true;
                PageMain.GridNavigation.RowSelected = PageMain.GridNavigation.RowList.First();
            }
            if (args.IsNavigatePath(UtilCms.PathCmsPage(), out string path))
            {
                PageMain.IsHide = true;
                PageCmsContent.IsHide = false;
                await PageCmsContent.Load(path);
            }
            if (args.IsNavigatePath("/filemanager/"))
            {
                PageMain.GridNavigation.RowSelected = PageMain.GridNavigation.RowList.First(item => item.Name == "FileManager");
            }
            if (args.IsNavigatePath("/roadmap/"))
            {
                PageMain.GridNavigation.RowSelected = PageMain.GridNavigation.RowList.First(item => item.Name == "Roadmap");
            }
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
