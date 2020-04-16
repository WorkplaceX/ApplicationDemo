namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageCmsFile : Page
    {
        public PageCmsFile(ComponentJson owner) : base(owner) 
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>Cms File <i class='far fa-file'></i></h1>" };
        }
    }
}
