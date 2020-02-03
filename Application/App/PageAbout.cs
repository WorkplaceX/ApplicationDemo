namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageAbout : Page
    {
        public PageAbout(ComponentJson owner) : base(owner) 
        {
            new Html(this) { TextHtml = "<h1>About</h1>" };
            new Html(this) { TextHtml = "This is an open source demo application to show WorkplaceX capabilities. It can be downloaded from GitHub." };
        }
    }
}
