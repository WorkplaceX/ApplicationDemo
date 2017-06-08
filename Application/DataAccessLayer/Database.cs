namespace Database.dbo
{
    using System;
    using System.Collections.Generic;
    using Framework.Server.DataAccessLayer;

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

}