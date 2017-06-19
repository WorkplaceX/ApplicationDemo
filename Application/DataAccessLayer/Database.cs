namespace Database.dbo
{
    using System;
    using System.Collections.Generic;
    using Framework.Server.DataAccessLayer;
    using Application;
    using Framework.Server.Application;

    public partial class AirportDisplay_AirportId
    {
        protected override void ColumnWidthPercent(ref double widthPercent)
        {
            widthPercent = 8;
        }
    }

    public partial class TableName_TableName2
    {
        protected override void LookUp(out Type typeRow, out List<Row> rowList)
        {
            typeRow = typeof(Database.dbo.LoRole);
            rowList = Framework.Server.DataAccessLayer.Util.Select(typeRow, null, null, false, 0, 5);
        }
    }

    public partial class ImportName : Row
    {
        [TypeCell(typeof(ImportName_ButtonDelete))]
        public string ButtonDelete { get; set; }
    }

    public partial class ImportName_ButtonDelete : Cell<ImportName>
    {
        protected override void CellProcessButtonIsClick(App app, string gridName, string index, string fieldName)
        {
            app.PageShow<PageMessageBoxDelete>(app.AppJson).Init(app, gridName, index);
        }
    }
}