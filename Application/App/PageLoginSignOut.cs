namespace Application.Demo
{
    using Framework.Json;

    public class PageLoginSignOut : Page
    {
        public PageLoginSignOut(ComponentJson owner) : base(owner) 
        {
            var pageMain = this.ComponentOwner<PageMain>();
            pageMain.LoginUser = null;
            pageMain.LoginUserPermissionDisplayList = null;

            DivContainer = new Div(this) { CssClass = "container" };

            new Html(DivContainer) { TextHtml = "<h1>User Sign Out</h1>" };
            new Html(DivContainer) { TextHtml = "You successfully signed out." };
        }

        public Div DivContainer;
    }
}
