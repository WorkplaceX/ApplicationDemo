namespace Application
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class PageCountry : Page
    {
        public PageCountry(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            new Html(this) { TextHtml = "<h1>Country <i class='fas fa-globe'></i></h1>" };
            new Html(this) { TextHtml = "Browse world's countries from <a href='https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2' target='_blank'>wikipedia.org</a> data. On how to convert and import data to database see <a href='https://github.com/WorkplaceX/Util/tree/master/Wikipedia' target='_blank'>github.com/WorkplaceX/Util</a>. Additionaly data is combined with <a href='https://flagicons.lipis.dev/' target='_blank'>flagicons.lipis.dev</a><br/><br/>" };
            await new GridCountry(this).LoadAsync();
        }
    }

    public class GridCountry : Grid<CountryDisplayCache>
    {
        public GridCountry(ComponentJson owner) : base(owner) { }

        protected override IQueryable<CountryDisplayCache> Query()
        {
            return base.Query().Where(item => item.IsFlagIconCss == true);
        }

        private void CellAnnotationBool(string fieldName, Row row, Type typeRow, string nameof, CellAnnotationResult result)
        {
            if (fieldName == nameof)
            {
                if (row?.GetType() == typeRow)
                {
                    bool? value = (bool?)row.GetType().GetProperty(nameof).GetValue(row);
                    if (value == true)
                    {
                        result.Html = "<i class='fas fa-check'></i>";
                        result.Align = Grid.CellAnnotationAlignEnum.Center;
                    }
                    if (value == false)
                    {
                        result.Html = "<i class='far fa-square'></i>";
                        result.Align = Grid.CellAnnotationAlignEnum.Center;
                    }
                }
            }
        }

        protected override void CellAnnotation(CellAnnotationArgs args, CellAnnotationResult result)
        {
            var countryDisplay = args.Row as CountryDisplayCache;
            if (args.FieldName == nameof(CountryDisplayCache.Country) && countryDisplay?.ASFlagIcon != null)
            {
                result.HtmlLeft = string.Format("<span class='flag-icon {0}'></span>", countryDisplay.ASFlagIcon);
            }

            if (args.FieldName == nameof(CountryDisplayCache.WikipediaCountryUrl) && countryDisplay?.WikipediaCountryUrl != null)
            {
                result.Html = string.Format("<a href='{0}' target='_blank'>{1}", countryDisplay?.WikipediaCountryUrl, "Wikipedia");
            }

            CellAnnotationBool(args.FieldName, args.Row, typeof(CountryDisplayCache), nameof(CountryDisplayCache.IsFlagIconCss), result);
            CellAnnotationBool(args.FieldName, args.Row, typeof(CountryDisplayCache), nameof(CountryDisplayCache.IsOpenFlightsAirline), result);
            CellAnnotationBool(args.FieldName, args.Row, typeof(CountryDisplayCache), nameof(CountryDisplayCache.IsOpenFlightsAirport), result);
            CellAnnotationBool(args.FieldName, args.Row, typeof(CountryDisplayCache), nameof(CountryDisplayCache.IsWikipedia), result);
        }
    }
}
