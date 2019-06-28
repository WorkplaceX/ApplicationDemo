namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using DatabaseApplication.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class PageDemo : Page
    {
        public PageDemo() { }

        public PageDemo(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "<h1>Language</h1>";
            await this.ComponentCreate<Grid>().LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<Language>();
        }
    }
}
