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

        protected override async Task InitAsync()
        {
            this.ComponentGetOrCreate<Html>(html => html.TextHtml = "Hello Demo2");
            await this.ComponentPageShowAsync<PageAirplane>();
            await this.ComponentPageShowAsync<PageCountry>();
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
