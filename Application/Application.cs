namespace Application
{
    using Framework.Server.Application;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ApplicationX : ApplicationBase
    {
        protected override JsonApplication JsonApplicationCreate()
        {
            JsonApplication result = new JsonApplication();
            result.Session = Guid.NewGuid();
            //
            new GridKeyboard(result, "GridKeyboard");
            var container = new LayoutContainer(result, "Container");
            var rowHeader = new LayoutRow(container, "Header");
            var cellHeader1 = new LayoutCell(rowHeader, "HeaderCell1");
            new GridField(cellHeader1, "Field", null, null, null);
            var rowContent = new LayoutRow(container, "Content");
            var cellContent1 = new LayoutCell(rowContent, "ContentCell1");
            new Grid(cellContent1, "Master", "Master");
            var cellContent2 = new LayoutCell(rowContent, "ContentCell2");
            new Grid(cellContent2, "Detail", "Detail");
            var rowFooter = new LayoutRow(container, "Footer");
            var cellFooter = new LayoutCell(rowFooter, "FooterCell1");
            var button = new Button(cellFooter, "Hello");
            //
            var gridData = new GridData();
            gridData.LoadToJsonGrid("Master", typeof(Database.dbo.TableName));
            gridData.ColumnList["Master"].Where(item => item.FieldName == "TableName2").First().IsUpdate = true;
            gridData.LoadToJsonGrid("Detail", typeof(Database.dbo.AirportDisplay));
            result.GridData = gridData;
            //
            return result;
        }

        protected override void ProcessGridIsClick(JsonApplication jsonApplicationOut)
        {
            foreach (GridRow gridRow in jsonApplicationOut.GridData.RowList["Master"])
            {
                if (gridRow.IsClick)
                {
                    var list = jsonApplicationOut.GridData.LoadFromJson("Master", typeof(ApplicationX)).Cast<Database.dbo.TableName>();
                    string tableName = list.ElementAt(int.Parse(gridRow.Index)).TableName2;
                    // string tableName = jsonApplicationOut.GridData.CellList["Master"]["TableName2"][gridRow.Index].V as string;
                    tableName = tableName.Substring(tableName.IndexOf(".") + 1);
                    Type typeRow = Framework.Server.DataAccessLayer.Util.TypeRowFromTableName(tableName, typeof(ApplicationX));
                    jsonApplicationOut.GridData.LoadToJsonGrid("Detail", typeRow);
                }
            }
        }
    }
}
