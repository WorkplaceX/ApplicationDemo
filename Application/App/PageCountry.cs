namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.Dal;
    using Framework.Json;
    using Framework.Session;

    public class PageCountry : Page
    {
        public PageCountry() { }

        public PageCountry(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "<h1>Country</h1>";
            await this.ComponentCreate<Grid>().LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return UtilDal.Query<CountryDisplay>();
        }

        protected override void GridCellAnnotation(Grid grid, string fieldName, GridRowEnum gridRowEnum, Row row, GridCellAnnotationResult result)
        {
            var countryDisplay = row as CountryDisplay;
            if (fieldName == nameof(CountryDisplay.Country) && countryDisplay?.ASFlagIcon != null)
            {
                result.HtmlLeft = string.Format("<span class='flag-icon {0}'></span>", countryDisplay.ASFlagIcon);
            }
        }
    }
}
