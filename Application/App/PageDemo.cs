namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class PageDemo : Page
    {
        public PageDemo() { }

        public PageDemo(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "<h1>Demo</h1>";
            await GridNavigation().LoadAsync();
            await GridLanguage().LoadAsync();
        }

        public Grid GridNavigation()
        {
            return this.ComponentGetOrCreate<BootstrapRow>()[0].ComponentGetOrCreate<Grid>();
        }

        public Grid GridLanguage()
        {
            return this.ComponentGetOrCreate<BootstrapRow>()[1].ComponentGetOrCreate<Grid>();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == GridNavigation())
            {
                return Data.Query<Navigation>();
            }
            if (grid == GridLanguage())
            {
                return Data.Query<Language>();
            }
            return base.GridQuery(grid);
        }
    }
}
