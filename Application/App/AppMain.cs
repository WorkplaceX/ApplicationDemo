namespace Application
{
    using Framework.Application;
    using Framework.Json;
    using System;
    using System.Threading.Tasks;

    public class AppMain : AppJson
    {
        public AppMain() : this(null) { }

        public AppMain(ComponentJson owner)
            : base(owner)
        {

        }

        protected override Task InitAsync()
        {
            this.ComponentGetOrCreate<Html>(html => html.TextHtml = "Hello Demo");
            return base.InitAsync();
        }
    }

    public class AppSelectorDemo : AppSelector
    {
        protected override Type TypeAppJson()
        {
            return typeof(AppMain);
        }
    }
}
