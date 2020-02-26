namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class PageAirplane : Page
    {
        public PageAirplane(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            new Html(this) { TextHtml = "<h1>Airplane <i class='fas fa-plane'></i></h1>" };
            new Html(this) { TextHtml = "Browse airplanes from <a href='https://en.wikipedia.org/wiki/List_of_aircraft_type_designators' target='_blank'>wikipedia.org</a> data. On how to convert and import data into sql database see <a href='https://github.com/WorkplaceX/Research/tree/master/Wikipedia' target='_blank'>github.com/WorkplaceX/Research</a>.<br/><br/>" };

            await new GridAirplane(this).LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<RawWikipediaAircraft>();
        }

        /// <summary>
        /// Add some annotation to the grid data like hyperlink or render as image.
        /// </summary>
        protected override void GridCellAnnotation(Grid grid, string fieldName, Row row, GridCellAnnotationResult result)
        {
            var aircraft = row as RawWikipediaAircraft;

            if (fieldName == nameof(RawWikipediaAircraft.Model) && aircraft?.ModelImageUrl != null)
            {
                result.HtmlLeft = string.Format("<img src='{0}' width='128' />", aircraft.ModelImageUrl);
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

        protected override IQueryable GridLookupQuery(Grid grid, Row row, string fieldName, string text)
        {
            if (fieldName == nameof(RawWikipediaAircraft.IataCode))
            {
                return Data.Query<CountryDisplayCache>().Where(item => item.IsFlagIconCss == true && (item.Code.StartsWith(text == null ? "" : text)));
            }
            return base.GridLookupQuery(grid, row, fieldName, text);
        }

        protected override string GridLookupRowSelected(Grid grid)
        {
            return ((CountryDisplayCache)grid.RowSelected).Code;
        }
    }

    public class GridAirplane : Grid<RawWikipediaAircraft>
    {
        public GridAirplane(ComponentJson owner) : base(owner) { }

        protected override Task<bool> UpdateAsync(RawWikipediaAircraft row, RawWikipediaAircraft rowNew, DatabaseEnum databaseEnum)
        {
            return Task.FromResult(true);
        }
    }
}
