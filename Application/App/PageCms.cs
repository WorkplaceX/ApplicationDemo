namespace Application
{
    using Database.Demo;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System.Threading.Tasks;

    public class PageCms : Page
    {
        public PageCms(ComponentJson owner) : base(owner) 
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>Cms <i class='fas fa-pencil-alt'></i></h1>" };
            new Html(container) { TextHtml = "Content management v0.2" };
        }
    }
}
