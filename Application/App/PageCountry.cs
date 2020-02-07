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
            new Html(this) { TextHtml = "Browse world's countries from <a href='https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2' target='_blank'>wikipedia.org</a> data. On how to convert and import data to database see <a href='https://github.com/WorkplaceX/Research/tree/master/Wikipedia' target='_blank'>github.com/WorkplaceX/Research</a>. Additionaly data is combined with <a href='https://flagicons.lipis.dev/' target='_blank'>flagicons.lipis.dev</a><br/><br/>" };
            await new Grid(this).LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<CountryDisplayCache>().Where(item => item.IsFlagIconCss == true);
        }

        private void GridCellAnnotationBool(string fieldName, Row row, Type typeRow, string nameof, GridCellAnnotationResult result)
        {
            if (fieldName == nameof)
            {
                if (row?.GetType() == typeRow)
                {
                    bool? value = (bool?)row.GetType().GetProperty(nameof).GetValue(row);
                    if (value == true)
                    {
                        result.Html = "<i class='fas fa-check'></i>";
                        result.Align = AlignEnum.Center;
                    }
                    if (value == false)
                    {
                        result.Html = "<i class='far fa-square'></i>";
                        result.Align = AlignEnum.Center;
                    }
                }
            }
        }

        protected override void GridCellAnnotation(Grid grid, string fieldName, Row row, GridCellAnnotationResult result)
        {
            var countryDisplay = row as CountryDisplayCache;
            if (fieldName == nameof(CountryDisplayCache.Country) && countryDisplay?.ASFlagIcon != null)
            {
                result.HtmlLeft = string.Format("<span class='flag-icon {0}'></span>", countryDisplay.ASFlagIcon);
            }

            if (fieldName == nameof(CountryDisplayCache.WikipediaCountryUrl) && countryDisplay?.WikipediaCountryUrl != null)
            {
                result.Html = string.Format("<a href='{0}' target='_blank'>{1}", countryDisplay?.WikipediaCountryUrl, "Wikipedia");
            }

            GridCellAnnotationBool(fieldName, row, typeof(CountryDisplayCache), nameof(CountryDisplayCache.IsFlagIconCss), result);
            GridCellAnnotationBool(fieldName, row, typeof(CountryDisplayCache), nameof(CountryDisplayCache.IsOpenFlightsAirline), result);
            GridCellAnnotationBool(fieldName, row, typeof(CountryDisplayCache), nameof(CountryDisplayCache.IsOpenFlightsAirport), result);
            GridCellAnnotationBool(fieldName, row, typeof(CountryDisplayCache), nameof(CountryDisplayCache.IsWikipedia), result);
        }
    }
}
