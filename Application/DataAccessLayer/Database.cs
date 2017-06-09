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

    public partial class ImportName : Row
    {
        public string ButtonDelete { get; set; }
    }
}

namespace Database
{
    using System;
    using System.Collections.Generic;
    using Framework.Server.DataAccessLayer;

    [SqlName("MessageBox")]
    public class MessageBox : Row
    {
        [SqlName("Text")]
        [TypeCell(typeof(TableName_Text))]
        public string Text { get; set; }

        [SqlName("ButtonYes")]
        [TypeCell(typeof(TableName_ButtonYes))]
        public string ButtonYes { get; set; }

        [SqlName("ButtonNo")]
        [TypeCell(typeof(TableName_ButtonNo))]
        public string ButtonNo { get; set; }
    }

    public class TableName_Text : Cell<MessageBox> { }

    public class TableName_ButtonYes : Cell<MessageBox> { }

    public class TableName_ButtonNo : Cell<MessageBox> { }
}