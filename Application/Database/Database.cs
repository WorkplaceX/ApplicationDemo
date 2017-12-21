namespace Database.dbo
{
    using System;
    using System.Collections.Generic;
    using Framework.DataAccessLayer;
    using Application;
    using Framework.Application;
    using Framework.Component;
    using Framework;
    using System.Linq;

    public partial class AirportDisplay_AirportId
    {
        protected override void ColumnWidthPercent(ref double widthPercent)
        {
            widthPercent = 8;
        }
    }

    public partial class TableName
    {
        protected override void MasterIsClick(App app, GridName gridNameMaster, Row rowMaster, ref bool isReload)
        {
            if (gridNameMaster == new GridName<Database.dbo.TableName>())
            {
                var rowTableName = rowMaster as Database.dbo.TableName;
                string tableName = rowTableName.TableName2;
                if (tableName != null && tableName.IndexOf(".") != -1)
                {
                    Type typeRow = UtilFramework.TypeFromName("Database." + tableName, typeof(AppDemo), typeof(Framework.UtilFramework));
                    app.GridData.LoadDatabase(new GridNameTypeRow(typeRow, "Detail", true));
                }
            }
        }
    }


    public partial class AirportDisplay_CountryText
    {
        protected override void CellLookup(out IQueryable query)
        {
            query = UtilDataAccessLayer.Query<Country>().Take(10);
        }
    }

    public partial class Country : Row
    {
        [SqlColumn(null, typeof(Country_ButtonDelete))]
        public string ButtonDelete { get; set; }
    }

    public partial class Country_ButtonDelete : Cell<Country>
    {
        protected override void InfoCell(App app, GridName gridName, Index index, InfoCell result)
        {
            result.CellEnum = GridCellEnum.Button;
        }

        protected override void CellButtonIsClick(App app, GridName gridName, Index index, Row row, string fieldName, ref bool isReload)
        {
            app.PageShow<PageMessageBoxDelete>(app.AppJson, false).Init(app, gridName, index);
        }

        protected override void CellRowValueToText(App app, GridName gridName, Index index, ref string result)
        {
            result = "Button";
        }
    }

    public partial class Country_Text : Cell<Country>
    {
        protected override void InfoCell(App app, GridName gridName, Index index, InfoCell result)
        {
            result.CellEnum = GridCellEnum.Html;
        }
    }
}