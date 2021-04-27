namespace Application.Demo
{
    using Database.Demo;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System.Threading.Tasks;

    public class PageCmsDoc : Page
    {
        public PageCmsDoc(ComponentJson owner) : base(owner)
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>Doc <i class='fas fa-book'></i></h1>" };
        }
    }
}
