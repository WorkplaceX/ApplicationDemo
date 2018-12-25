namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.Dal;
    using Framework.Json;
    using Framework.Session;

    public class PageAirplane : Page
    {
        public PageAirplane() { }

        public PageAirplane(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            await this.ComponentCreate<Grid>().LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return UtilDal.Query<RawWikipediaAircraft>();
        }



        protected override void GridCellAnnotation(Grid grid, string fieldName, GridRowEnum gridRowEnum, Row row, GridCellAnnotationResult result)
        {
            var aircraft = row as RawWikipediaAircraft;
            if (fieldName == nameof(RawWikipediaAircraft.Model) && aircraft?.ModelImageUrl != null)
            {
                result.HtmlLeft = string.Format("<img src='{0}' width='128px' />", aircraft.ModelImageUrl);
            }
        }
    }
}
