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
            new Html(this) { TextHtml = "Browse airplanes from <a href='https://en.wikipedia.org/wiki/List_of_aircraft_type_designators' target='_blank'>wikipedia.org</a> data. On how to convert and import data into sql database see <a href='https://github.com/WorkplaceX/Util/tree/master/Wikipedia' target='_blank'>github.com/WorkplaceX/Util</a>.<br/><br/>" };

            await new GridAirplane(this).LoadAsync();
        }
    }

    public class GridAirplane : Grid<RawWikipediaAircraft>
    {
        public GridAirplane(ComponentJson owner) : base(owner) { }


        protected override Task UpdateAsync(UpdateArgs args, UpdateResult result)
        {
            result.IsHandled = true;
            return base.UpdateAsync(args, result);
        }

        /// <summary>
        /// Add some annotation to the grid data like hyperlink or render as image.
        /// </summary>
        protected override void CellAnnotation(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(RawWikipediaAircraft.Model) && args.Row?.ModelImageUrl != null)
            {
                result.HtmlLeft = string.Format("<img src='{0}' width='128' />", args.Row.ModelImageUrl);
            }

            if (args.FieldName == nameof(RawWikipediaAircraft.ModelUrl) && args.Row?.ModelUrl != null)
            {
                result.Html = string.Format("<a href='{0}' target='_blank'>{1}", args.Row.ModelUrl, "Wikipedia");
            }

            if (args.FieldName == nameof(RawWikipediaAircraft.ModelImageUrl) && args.Row?.ModelImageUrl != null)
            {
                result.Html = string.Format("<a href='{0}' target='_blank'>{1}", args.Row.ModelImageUrl, "Image");
            }
        }

        protected override void LookupQuery(LookupQueryArgs args, LookupQueryResult result)
        {
            if (args.FieldName == nameof(RawWikipediaAircraft.IataCode))
            {
                result.Query = Data.Query<CountryDisplayCache>().Where(item => item.IsFlagIconCss == true && (item.Code.StartsWith(args.Text == null ? "" : args.Text)));
            }
        }

        protected override void LookupRowSelected(LookupRowSelectedArgs args, LookupRowSelectedResult result)
        {
            result.Text = ((CountryDisplayCache)args.RowSelected).Code;
        }
    }
}
