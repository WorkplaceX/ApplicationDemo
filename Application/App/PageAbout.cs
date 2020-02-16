namespace Application
{
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System.Threading.Tasks;

    public class PageAbout : Page
    {
        public PageAbout(ComponentJson owner) : base(owner) 
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>About <i class='fas fa-bullhorn'></i></h1>" };
            new Html(container) { TextHtml = "This is an open source demo application to show WorkplaceX capabilities. It can be downloaded from <a href='https://github.com/WorkplaceX/ApplicationDemo' target='_blank'>github.com/WorkplaceX/ApplicationDemo</a>." };
        }
    }
}
