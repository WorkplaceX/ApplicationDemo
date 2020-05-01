namespace Application
{
    using Framework.Json;

    public class PageCmsContent : Page
    {
        public PageCmsContent(ComponentJson owner) 
            : base(owner) 
        {
            var divContainer = new Div(this) { CssClass = "container" };

            new Html(divContainer) { TextHtml = "<h1>Cms Content</h1>" };
        }
    }
}
