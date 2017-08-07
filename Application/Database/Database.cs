namespace Database.dbo
{
    using System;
    using System.Collections.Generic;
    using Framework.DataAccessLayer;
    using Application;
    using Framework.Application;

    public partial class AirportDisplay_AirportId
    {
        protected override void ColumnWidthPercent(ref double widthPercent)
        {
            widthPercent = 8;
        }
    }

    public partial class TableName_TableName2
    {
        protected override void CellLookUp(out Type typeRow, out List<Row> rowList)
        {
            typeRow = typeof(Database.dbo.LoRole);
            rowList = UtilDataAccessLayer.Select(typeRow, null, null, false, 0, 5);
        }
    }

    public partial class Country : Row
    {
        [TypeCell(typeof(Country_ButtonDelete))]
        public string ButtonDelete { get; set; }
    }

    public partial class Country_ButtonDelete : Cell<Country>
    {
        protected override void CellIsButton(App app, string gridName, string index, ref bool result)
        {
            result = true;
        }

        protected override void CellProcessButtonIsClick(App app, string gridName, string index, string fieldName)
        {
            app.PageShow<PageMessageBoxDelete>(app.AppJson, false).Init(app, gridName, index);
        }

        protected override void CellValueToText(App app, string gridName, string index, ref string result)
        {
            result = "Button";
        }
    }

    public partial class Country_Text : Cell<Country>
    {
        protected override void CellIsHtml(App app, string gridName, string index, ref bool result)
        {
            result = true;
        }
    }
}