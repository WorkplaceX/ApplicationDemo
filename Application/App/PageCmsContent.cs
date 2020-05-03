namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageCmsContent : Page
    {
        public PageCmsContent(ComponentJson owner) 
            : base(owner) 
        {
            var divContainer = new Div(this) { CssClass = "container" };

            Html = new Html(divContainer) { TextHtml = "<h1>Cms Content</h1>" };
        }

        public Html Html;

        public async Task Load(string path)
        {
            if (path.StartsWith(UtilCms.PathCms))
            {
                string pathCms = path.Substring(UtilCms.PathCms.Length - 1);
                var componentList = await Data.Query<CmsComponentDisplay>().Where(item => item.PagePath == pathCms || item.ParentPagePath == pathCms).QueryExecuteAsync();
                string textHtml = UtilCms.TextHtml(componentList.Single(item => item.PagePath == path), componentList);
                Html.TextHtml = textHtml;
            }
        }
    }
}
