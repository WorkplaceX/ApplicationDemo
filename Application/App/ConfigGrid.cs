namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.dbo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class Config : Page
    {
        public Config() : this(null) { }

        public Config(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            await GridConfigGrid().LoadAsync();
        }

        public Grid GridConfigGrid()
        {
            return this.ComponentGetOrCreate<Grid>("ConfigGrid");
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<FrameworkConfigGridBuiltIn>();
        }
    }
}
