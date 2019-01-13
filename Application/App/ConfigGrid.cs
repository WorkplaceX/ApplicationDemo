namespace Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database.dbo;
    using Framework.DataAccessLayer;
    using Framework.Json;

    public class Config : Page
    {
        public Config() : this(null) { }

        public Config(ComponentJson owner) : base(owner) { }

        protected override async Task InitAsync()
        {
            this.ComponentCreate<Html>().TextHtml = "<h1>Config Grid</h1>";
            await GridConfigGrid().LoadAsync();
            this.ComponentCreate<Html>().TextHtml = "<h1>Config Field</h1>";
            GridConfigField();
        }

        public Grid GridConfigGrid()
        {
            return this.ComponentGetOrCreate<Grid>("ConfigGrid");
        }

        public Grid GridConfigField()
        {
            return this.ComponentGetOrCreate<Grid>("ConfigField");
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == GridConfigGrid())
            {
                return Data.Query<FrameworkConfigGridBuiltIn>();
            }
            if (grid == GridConfigField())
            {
                var rowSelected = (FrameworkConfigGridBuiltIn)GridConfigGrid().GridRowSelected();
                return Data.Query<FrameworkConfigFieldDisplay>().Where(item => item.ConfigGridId == rowSelected.Id);
            }
            return base.GridQuery(grid);
        }

        protected override async Task<bool> GridInsertAsync(Grid grid, Row rowNew, DatabaseEnum databaseEnum)
        {
            if (grid == GridConfigGrid())
            {
                var rowDest = new FrameworkConfigGrid();
                rowDest.IsExist = true;
                Data.RowCopy(rowNew, rowDest);
                await Data.InsertAsync(rowDest);
                var rowSelect = (await Data.SelectAsync(Data.Query<FrameworkConfigGridBuiltIn>().Where(item => item.Id == rowDest.Id))).Single(); // Read back from view.
                Data.RowCopy(rowSelect, rowNew);
                return true;
            }
            else
            {
                return await base.GridInsertAsync(grid, rowNew, databaseEnum);
            }
        }

        protected override async Task<bool> GridUpdateAsync(Grid grid, Row row, Row rowNew, DatabaseEnum databaseEnum)
        {
            if (grid == GridConfigGrid())
            {
                var rowDest = new FrameworkConfigGrid();
                Data.RowCopy(rowNew, rowDest);
                await Data.UpdateAsync(rowDest);
                var rowSelect = (await Data.SelectAsync(Data.Query<FrameworkConfigGridBuiltIn>().Where(item => item.Id == rowDest.Id))).Single(); // Read back from view.
                Data.RowCopy(rowSelect, rowNew);
                return true;
            }
            else
            {
                return await base.GridUpdateAsync(grid, row, rowNew, databaseEnum);
            }
        }

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            if (grid == GridConfigGrid())
            {
                var configGrid = (FrameworkConfigGridBuiltIn)grid.GridRowSelected();
                await GridConfigField().LoadAsync();
            }
        }
    }
}
