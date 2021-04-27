namespace Application.Demo
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
            var componentList = await Data.Query<CmsComponentDisplay>().Where(item => item.PagePath == path || item.ParentPagePath == path).QueryExecuteAsync();
            string textHtml = UtilCms.TextHtml(componentList.Single(item => item.PagePath == path), componentList);
            Html.TextHtml = textHtml;
            Html.IsNoSanatize = true;
        }
    }
}
