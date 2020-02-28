namespace Application
{
    using Database.Demo;
    using Framework.Json;
    using System.Threading.Tasks;

    public class PageRoadmap : Page
    {
        public PageRoadmap(ComponentJson owner) : base(owner) 
        {
            new Html(this) { TextHtml = "<h1>Roadmap</h1>" };
            new Html(this) { TextHtml = "This is the development roadmap showing the status of new features and reported bugs." };
        }

        public override async Task InitAsync()
        {
            await new Grid<RoadmapModule>(this).LoadAsync();
        }
    }
}
