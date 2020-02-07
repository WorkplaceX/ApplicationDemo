namespace Application
{
    using Framework.Json;

    public class PageLoginSignOut : Page
    {
        public PageLoginSignOut(ComponentJson owner) : base(owner) 
        {
            DivContainer = new Div(this) { CssClass = "container" };

            new Html(DivContainer) { TextHtml = "<h1>Sign Out</h1>" };
            new Html(DivContainer) { TextHtml = "You successfully signed out." };
        }

        public Div DivContainer;
    }
}
