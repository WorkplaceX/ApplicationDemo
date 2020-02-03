namespace Application
{
    using Framework.Json;

    public class PageRoadmap : Page
    {
        public PageRoadmap(ComponentJson owner) : base(owner) 
        {
            new Html(this) { TextHtml = "<h1>Roadmap</h1>" };
            new Html(this) { TextHtml = "This is the development roadmap showing the status of new features and reported bugs." };
        }
    }
}
