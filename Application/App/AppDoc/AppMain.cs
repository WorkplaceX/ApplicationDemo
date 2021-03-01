namespace Application.Doc
{
    using Framework.Json;

    public class AppMain : AppJson
    {
        public AppMain()
        {
            CssFrameworkEnum = CssFrameworkEnum.Bulma;
            PageMain = new PageMain(this);
            Title = "WorkplaceX - Business Application Framework";
        }

        public PageMain PageMain;
    }
}
