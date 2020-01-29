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
            new Html(this) { TextHtml = "<h1>Country</h1>" };
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
