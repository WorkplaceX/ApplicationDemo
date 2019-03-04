namespace Application
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database.dbo;
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Session;

    public class PageAirplane : Page
    {
        public PageAirplane() { }

        public PageAirplane(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "<h1>Airplane</h1>";
            await this.ComponentCreate<Grid>().LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<RawWikipediaAircraft>();
        }

        protected override void GridQueryConfig(Grid grid, string tableNameCSharp, ConfigResult result)
        {
            result.ConfigFieldQuery = Data.Query<FrameworkConfigFieldBuiltIn>().Where(item => item.TableNameCSharp == tableNameCSharp && item.ConfigName == grid.ConfigName);

            return;
            List<FrameworkConfigFieldBuiltIn> list = new List<FrameworkConfigFieldBuiltIn>();
            list.Add(new FrameworkConfigFieldBuiltIn() { FieldNameCSharp = "IcaoCode", Text = "My IATA", TableNameCSharp = "Demo.RawWikipediaAircraft", IsVisible = false });
            list.Add(new FrameworkConfigFieldBuiltIn() { FieldNameCSharp = "Model", Text = "My Model", TableNameCSharp = "Demo.RawWikipediaAircraft", Sort = 0.5 });
            result.ConfigFieldQuery = list.AsQueryable();
        }

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
    }
}
