namespace Application
{
    using Framework.Application;
    using Framework.Component;

    public class PageHome : Page
    {
        protected override void InitJson(App app)
        {
            new Literal(this) { TextHtml = "<h1>Home</h1>" };
        }
    }

    public class PageAbout : Page
    {
        protected override void InitJson(App app)
        {
            new Literal(this) { TextHtml = "<h1>About</h1>" };
        }
    }
}
