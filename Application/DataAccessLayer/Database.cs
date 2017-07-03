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
        protected override void LookUp(out Type typeRow, out List<Row> rowList)
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

    public partial class FrameworkFileStorage
    {
        [TypeCell(typeof(FrameworkFileStorage_Download))]
        public string Download { get; set; }

        public int? Year  { get; set; }
    }

    public partial class FrameworkFileStorage_Download : Cell<FrameworkFileStorage>
    {
        protected override void CellIsHtml(App app, string gridName, string index, ref bool result)
        {
            result = true;
        }

        protected override void CellValueToText(App app, string gridName, string index, ref string result)
        {
            result = null;
            if (UtilApplication.IndexEnumFromText(index) == IndexEnum.Index && Row.FileName != null)
            {
                string fileNameOnly = Row.FileName;
                if (Row.FileName.Contains("/"))
                {
                    fileNameOnly = Row.FileName.Substring(Row.FileName.LastIndexOf("/") + 1);
                    if (fileNameOnly.Length == 0)
                    {
                        fileNameOnly = Row.FileName;
                    }
                }
                result = string.Format("<a href={0}>{1}</a>", Row.FileName, fileNameOnly);
            }
        }
    }

    public partial class FrameworkFileStorage_Data : Cell<FrameworkFileStorage>
    {
        protected override void CellIsFileUpload(App app, string gridName, string index, ref bool result)
        {
            result = true;
        }

        protected override void CellValueToText(App app, string gridName, string index, ref string result)
        {
            result = "File Upload";
        }

        protected override void CellValueFromText(App app, string gridName, string index, ref string result)
        {
            string base64 = "base64,";
            if (result.StartsWith("data:") && result.Contains(base64))
            {
                result = result.Substring(result.IndexOf(base64) + base64.Length);
            }
        }
    }
}