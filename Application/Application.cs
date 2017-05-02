namespace Application
{
    using Framework.Server.Application;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class BusinessApplication : ApplicationBase
    {
        protected override JsonApplication JsonApplicationCreate()
        {
            JsonApplication result = new JsonApplication();
            result.Session = Guid.NewGuid();
            //
            new GridKeyboard(result, "GridKeyboard");
            var container = new LayoutContainer(result, "Container") { Class = "co" };
            var rowLogo = new LayoutRow(container, "RowLogo") { Class = "r" };
            var literalLogo = new Literal(rowLogo, "Logo");
            literalLogo.Html = "<img src='Logo.png' />";
            var rowHeader = new LayoutRow(container, "Header") { Class = "r" };
            var cellHeader1 = new LayoutCell(rowHeader, "HeaderCell1") { Class = "c" };
            new GridField(cellHeader1, "Field", null, null, null);
            var cellHeader2 = new LayoutCell(rowHeader, "HeaderCell2") { Class = "c" };
            new Grid(cellHeader2, "LookUp", "LookUp");
            var rowContent = new LayoutRow(container, "Content");
            var cellContent1 = new LayoutCell(rowContent, "ContentCell1") { Class = "c" };
            var cellContent2 = new LayoutCell(rowContent, "ContentCell2") { Class = "c2" };
            new Grid(cellContent1, "Master", "Master");
            new Grid(cellContent2, "Detail", "Detail");
            var rowFooter = new LayoutRow(container, "Footer");
            var cellFooter1 = new LayoutCell(rowFooter, "FooterCell1") { Class = "c" };
            var literal = new Literal(cellFooter1, "Literal");
            literal.Html = "Hello <b>Literal</b>";
            literal.Class = "y";
            var cellFooter2 = new LayoutCell(rowFooter, "FooterCell1") { Class = "c" };
            var button = new Button(cellFooter2, "Hello");
            //
            var gridData = new GridData();
            result.GridData = gridData;
            Util.GridToJson(result, "Master", typeof(Database.dbo.TableName));
            gridData.ColumnList["Master"].Where(item => item.FieldName == "TableName2").First().IsUpdate = true;
            //
            return result;
        }

        protected override void ProcessInit()
        {
            base.ProcessInit();
            ProcessListInsertAfter<ProcessGridIsIsClick>(new ProcessGridRowFirstIsClick());
            ProcessListInsertAfter<ProcessGridIsIsClick>(new ProcessGridMasterIsClick());
        }
    }

    public class ProcessGridMasterIsClick : ProcessBase
    {
        protected override void ProcessEnd(JsonApplication jsonApplication)
        {
            foreach (GridRow gridRow in jsonApplication.GridData.RowList["Master"])
            {
                if (gridRow.IsClick)
                {
                    var list = Util.GridFromJson(jsonApplication, "Master", typeof(BusinessApplication)).RowList.Cast<Database.dbo.TableName>();
                    string tableName = list.ElementAt(int.Parse(gridRow.Index)).TableName2;
                    // string tableName = jsonApplicationOut.GridData.CellList["Master"]["TableName2"][gridRow.Index].V as string;
                    tableName = tableName.Substring(tableName.IndexOf(".") + 1);
                    Type typeRow = Framework.Server.DataAccessLayer.Util.TypeRowFromTableName(tableName, typeof(BusinessApplication));
                    Util.GridToJson(jsonApplication, "Detail", typeRow);
                }
            }
        }
    }
}
