namespace Application
{
    using Framework.Server.Application;
    using Framework.Server.Application.Json;
    using System;
    using System.Linq;

    public class PageDatabaseBrowse : PageGrid
    {
        protected override void ProcessInit()
        {
            base.ProcessInit();
            ProcessList.AddBefore<ProcessGridMasterIsClick2, ProcessGridIsClickFalse>();
        }

        protected override void ApplicationJsonInit()
        {
            ApplicationJson.GridDataJson = new GridDataJson();
            //
            new GridKeyboard(ApplicationJson, "GridKeyboard");
            new Button(ApplicationJson, "Close") { Name = "Close", TypePage = TypePage() };
            var container = new LayoutContainer(ApplicationJson, "Container") { Class = "co", TypePage = TypePage() };
            var rowLogo = new LayoutRow(container, "RowLogo") { Class = "r" };
            var literalLogo = new Literal(rowLogo, "Logo");
            literalLogo.Html = "<img src='Logo.png' />";
            var rowHeader = new LayoutRow(container, "Header") { Class = "r" };
            var cellHeader1 = new LayoutCell(rowHeader, "HeaderCell1") { Class = "c" };
            new GridField(cellHeader1, "Field", null, null, null);
            var cellHeader2 = new LayoutCell(rowHeader, "HeaderCell2") { Class = "c" };
            new Grid(cellHeader2, "LookUp", "LookUp");
            var rowHeader2 = new LayoutRow(container, "Header") { Class = "r" };
            var cellHeader3 = new LayoutCell(rowHeader, "HeaderCell3") { Class = "c" };
            new LabelGridSaveState(cellHeader3, "GridSaveState");
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
            GridData gridData = new GridData();
            gridData.LoadDatabase("Master", null, null, false, typeof(Database.dbo.TableName));
            gridData.SaveJson(ApplicationJson);
        }

        protected override void ProcessPage()
        {
            bool isClick = false;
            string name = null;
            foreach (Button button in ApplicationJson.ListAll<Button>())
            {
                if (button.Name == "Close" && button.IsClick)
                {
                    isClick = true;
                    name = button.Name;
                    break;
                }
            }
            if (isClick && name == "Close")
            {
                Application.PageRemove(GetType());
                Application.PageShow<PageMain>();
            }
        }
    }

    public class PageMain : Page
    {
        protected override void ApplicationJsonInit()
        {
            new Button(ApplicationJson, "Delete") { TypePage = TypePage(), Name = "D" };
            new Button(ApplicationJson, "Browse") { TypePage = TypePage(), Name = "B" };
        }


        protected override void ProcessPage()
        {
            bool isClick = false;
            string name = null;
            foreach (Component component in ApplicationJson.List)
            {
                Button button = component as Button;
                if (button != null)
                {
                    if (button.TypePage == TypePage() && button.IsClick)
                    {
                        isClick = true;
                        name = button.Name;
                        break;
                    }
                }
            }
            if (isClick && name == "D")
            {
                Application.PageShow<PageMessageBox>().Init(GetType());
            }
            if (isClick && name == "B")
            {
                Application.PageShow<PageDatabaseBrowse>();
            }
        }
    }

    public class PageMessageBox : Page
    {
        public void Init(Type typePageReturn)
        {
            ReturnTypePage = Framework.Util.TypeToString(typePageReturn);
        }

        protected override void ApplicationJsonInit()
        {
            new Label(ApplicationJson, "Delete item?") { TypePage = TypePage() };
            new Button(ApplicationJson, "Yes") { TypePage = TypePage() };
            new Button(ApplicationJson, "No") { TypePage = TypePage() };
        }

        protected override void ProcessPage()
        {
            bool isClick = false;
            string text = null;
            foreach (Component component in ApplicationJson.List)
            {
                Button button = component as Button;
                if (button != null)
                {
                    if (button.TypePage == TypePage() && button.IsClick)
                    {
                        text = button.Text;
                        isClick = true;
                        break;
                    }
                }
            }
            if (isClick)
            {
                ReturnText = text;
                Type typePage = Framework.Util.TypeFromString(ReturnTypePage, Application.GetType());
                Application.PageRemove(GetType());
                Application.PageShow(typePage);
            }
        }

        public string ReturnTypePage
        {
            get
            {
                return StateGet<string>(nameof(ReturnTypePage));
            }
            set
            {
                StateSet(nameof(ReturnTypePage), value);
            }
        }

        public string ReturnText
        {
            get
            {
                return StateGet<string>(nameof(ReturnText));
            }
            set
            {
                StateSet(nameof(ReturnText), value);
            }
        }
    }

    public class ProcessGridMasterIsClick2 : ProcessBase<PageGrid>
    {
        protected override void Process()
        {
            foreach (GridRow gridRow in ApplicationJson.GridDataJson.RowList["Master"])
            {
                if (gridRow.IsClick)
                {
                    if (Util.IndexToIndexEnum(gridRow.Index) == IndexEnum.Index)
                    {
                        GridData gridData = Page.GridData();
                        var row = gridData.Row("Master", gridRow.Index) as Database.dbo.TableName;
                        string tableName = row.TableName2;
                        if (tableName != null && tableName.IndexOf(".") != -1)
                        {
                            tableName = tableName.Substring(tableName.IndexOf(".") + 1);
                            //
                            Type typeRow = Framework.Server.DataAccessLayer.Util.TypeRowFromTableName(tableName, typeof(Application));
                            gridData.LoadDatabase("Detail", null, null, false, typeRow);
                            gridData.SaveJson(ApplicationJson);
                        }
                    }
                }
            }
        }
    }
}
