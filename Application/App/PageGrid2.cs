namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
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

        protected override void GridCellParse(Grid2 grid, Row row, string fieldName, string text, out bool isHandled)
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
                base.GridCellParse(grid, row, fieldName, text, out isHandled);
            }
        }

        protected override IQueryable Grid2Query(Grid2 grid)
        {
            return Data.Query<Database.Demo.Navigation>();
        }
    }
}
