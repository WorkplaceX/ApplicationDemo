﻿namespace Database.dbo
{
    using System;
    using System.Collections.Generic;
    using Framework.Server.DataAccessLayer;
    using Application;

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
            rowList = Util.Select(typeRow, null, null, false, 0, 5);
        }
    }

    public partial class ImportName : Row
    {
        [TypeCell(typeof(ImportName_ButtonDelete))]
        public string ButtonDelete { get; set; }
    }

    public partial class ImportName_ButtonDelete : Cell<ImportName>
    {
        protected override void CellProcessButtonIsClick(Framework.Server.Application.PageGrid pageGrid, string gridName, string index, string fieldName)
        {
            pageGrid.Application.PageShow<PageMessageBoxDelete>(false).Init(gridName, index);
        }
    }
}