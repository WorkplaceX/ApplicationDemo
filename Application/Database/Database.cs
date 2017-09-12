namespace Database.dbo
{
    using System;
    using System.Collections.Generic;
    using Framework.DataAccessLayer;
    using Application;
    using Framework.Application;
    using Framework.Component;

    public partial class AirportDisplay_AirportId
    {
        protected override void ColumnWidthPercent(ref double widthPercent)
        {
            widthPercent = 8;
        }
    }

    public partial class TableName
    {
        protected override void MasterDetail(App app, string gridNameMaster, Row rowMaster, ref bool isReload)
        {
            if (gridNameMaster == "Master")
            {
                var rowTableName = rowMaster as Database.dbo.TableName;
                string tableName = rowTableName.TableName2;
                if (tableName != null && tableName.IndexOf(".") != -1)
                {
                    Type typeRow = UtilDataAccessLayer.TypeRowFromName(tableName, typeof(AppDemo));
                    app.GridData.LoadDatabase("Detail", typeRow);
                }
            }
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
        [SqlColumn(null, typeof(Country_ButtonDelete))]
        public string ButtonDelete { get; set; }
    }

    public partial class Country_ButtonDelete : Cell<Country>
    {
        protected override void InfoCell(App app, string gridName, string index, InfoCell result)
        {
            result.CellEnum = GridCellEnum.Button;
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
        protected override void InfoCell(App app, string gridName, string index, InfoCell result)
        {
            result.CellEnum = GridCellEnum.Html;
        }
    }
}