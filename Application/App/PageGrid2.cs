namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Session;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageGrid2 : Page
    {
        public PageGrid2(ComponentJson owner) 
            : base(owner)
        {

        }

        public override async Task InitAsync()
        {
            await new Grid2(this).LoadAsync();
        }

        protected override string GridCellText(Grid2 grid, Row row, string fieldName)
        {
            if (fieldName == nameof(Navigation.Sort))
            {
                return string.Format("{0} {1}", ((Navigation)row).Sort, ((Navigation)row).PageName);
            }
            else
            {
                return base.GridCellText(grid, row, fieldName);
            }
        }

        private bool ParseUom(string text, out double value, out string measurement)
        {
            bool isValue = true;
            string valueText = null;
            value = 0;
            measurement = valueText;
            foreach (var item in text)
            {
                if (isValue)
                {
                    if ((item >= '0' && item <= '9') || item == '.')
                    {
                        valueText += item;
                    }
                    else
                    {
                        isValue = false;
                    }
                }
                if (isValue == false)
                {
                    measurement += item;
                }
            }
            measurement = measurement?.Trim();
            return double.TryParse(valueText, out value);
        }

        protected override void GridCellParse(Grid2 grid, Row row, string fieldName, string text, out bool isHandled, ref string errorParse)
        {
            if (fieldName == nameof(Navigation.Sort))
            {
                bool isNumber = true;
                string numberText = null;
                string measure = numberText;
                foreach (var item in text)
                {
                    if (isNumber)
                    {
                        if ((item >= '0' && item <= '9') || item == '.')
                        {
                            numberText += item;
                        }
                        else
                        {
                            isNumber = false;
                        }
                    }
                    if (isNumber == false)
                    {
                        measure += item;
                    }
                }
                measure = measure?.Trim();
                ((Navigation)row).Sort = double.Parse(numberText);
                ((Navigation)row).PageName = measure;
                isHandled = true;
            }
            else
            {
                base.GridCellParse(grid, row, fieldName, text, out isHandled, ref errorParse);
            }
        }

        protected override void GridCellParseFilter(Grid2 grid, string fieldName, string text, Grid2Filter filter, out bool isHandled, ref string errorParse)
        {
            isHandled = false;
            if (fieldName == nameof(Navigation.Sort))
            {
                bool isParse = ParseUom(text, out double value, out string measurement);
                filter.ValueSet(nameof(Navigation.Sort), value, FilterOperator.Equal, string.Format("{0} {1}", value, measurement), !isParse);
                filter.ValueSet(nameof(Navigation.PageName), measurement, FilterOperator.Equal, measurement, !isParse || measurement == null);
                if (!isParse && text != "")
                {
                    errorParse = "Could not parse Uom!";
                }
                isHandled = true;
            }
        }

        protected override IQueryable Grid2Query(Grid2 grid)
        {
            return Data.Query<Database.Demo.Navigation>();
        }
    }
}
