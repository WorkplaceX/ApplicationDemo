namespace Application
{
    using Framework.Application;
    using Framework.Json;
    using System;
    using System.Threading.Tasks;

    public class AppCompany : AppJson
    {
        public AppCompany() : this(null) { }

        public AppCompany(ComponentJson owner) : base(owner) { }

        protected override Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "My Company!";
            return base.InitAsync();
        }

    }

    public class AppMain : AppJson
    {
        public AppMain() : this(null) { }

        public AppMain(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            this.ComponentGetOrCreate<Html>(html => html.TextHtml = "Hello Demo2");
            await this.ComponentPageShowAsync<PageAirplane>();
            await this.ComponentPageShowAsync<PageCountry>();
        }
    }
}
