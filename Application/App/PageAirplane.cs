namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Session;

    public class PageAirplane : Page
    {
        public PageAirplane(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            new Html(this) { TextHtml = "<h1>Airplane</h1>" };
            await new Grid(this).LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<RawWikipediaAircraft>();
        }

        /// <summary>
        /// Add some annotation to the grid data like hyperlink or render as image.
        /// </summary>
        protected override void GridCellAnnotation(Grid grid, string fieldName, GridRowEnum gridRowEnum, Row row, GridCellAnnotationResult result)
        {
            var aircraft = row as RawWikipediaAircraft;

            if (fieldName == nameof(RawWikipediaAircraft.Model) && aircraft?.ModelImageUrl != null)
            {
                result.HtmlLeft = string.Format("<img src='{0}' width='128px' />", aircraft.ModelImageUrl);
            }

            if (fieldName == nameof(RawWikipediaAircraft.ModelUrl) && aircraft?.ModelUrl != null)
            {
                result.Html = string.Format("<a href='{0}' target='_blank'>{1}", aircraft.ModelUrl, "Wikipedia");
            }

            if (fieldName == nameof(RawWikipediaAircraft.ModelImageUrl) && aircraft?.ModelImageUrl != null)
            {
                result.Html = string.Format("<a href='{0}' target='_blank'>{1}", aircraft.ModelImageUrl, "Image");
            }
        }

        protected override Task<bool> GridUpdateAsync(Grid grid, Row row, Row rowNew, DatabaseEnum databaseEnum)
        {
            return Task.FromResult(true);
        }

        protected override IQueryable GridLookupQuery(Grid grid, Row row, string fieldName, string text)
        {
            if (fieldName == nameof(RawWikipediaAircraft.IataCode))
            {
                return Data.Query<CountryDisplayCache>().Where(item => item.IsFlagIconCss == true && (item.Code == null || item.Code.StartsWith(text)));
            }
            return base.GridLookupQuery(grid, row, fieldName, text);
        }

        protected override string GridLookupRowSelected(Grid grid, string fieldName, GridRowEnum gridRowEnum, Row rowLookupSelected)
        {
            return ((CountryDisplayCache)rowLookupSelected).Code;
        }
    }
}
