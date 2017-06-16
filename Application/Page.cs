namespace Application
{
    using Framework.Server.Application;
    using Framework.Server.Application.Json;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PageGridDatabaseBrowse : PageGrid
    {
        protected override void ProcessInit(ProcessList processList)
        {
            base.ProcessInit(processList);
            processList.AddBefore<ProcessGridMasterIsClick, ProcessGridIsClickFalse>();
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

    public class PageMain2 : Page2
    {
        protected override void Init(ApplicationBase applicationBase)
        {
            new Button(this, "About2");
        }

        public Button ButtonAbout()
        {
            return (Button)List[0];
        }

        protected override void ProcessBegin(ApplicationBase application)
        {
            if (ButtonAbout().IsClick)
            {
                Page2Show<PageMessageBoxAbout2>(application);
            }
        }
    }

    public class PageMessageBoxAbout2 : Page2
    {
        protected override void Init(ApplicationBase applicationBase)
        {
            new Label(this, "(C) 2017 by Framework2");
            new Button(this, "Ok");
        }

        protected override void ProcessBegin(ApplicationBase application)
        {
            if (List.OfType<Button>().First().IsClick)
            {
                Page2Show<PageMain2>(application);
            }
        }
    }

    public class PageMain : Page
    {
        protected override void ApplicationJsonInit()
        {
            new Button(ApplicationJson, "Browse") { TypePage = TypePage(), Name = "B" };
            new Button(ApplicationJson, "About") { TypePage = TypePage(), Name = "A" };
            new Button(ApplicationJson, "Debug") { TypePage = TypePage(), Name = "D" };
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
            if (isClick && name == "B")
            {
                Application.PageShow<PageGridDatabaseBrowse>();
            }
            if (isClick && name == "A")
            {
                Application.PageShow<PageMessageBoxAbout>();
            }
            if (isClick && name == "D")
            {
                Application.PageShow<PageDebug>();
            }
        }
    }

    public class PageMessageBoxAbout : Page
    {
        protected override void ApplicationJsonInit()
        {
            new Label(ApplicationJson, "(C) 2017 by Framework") { TypePage = TypePage() };
            new Button(ApplicationJson, "Ok") { TypePage = TypePage() };
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
                Application.PageShow<PageMain>();
            }
        }
    }

    public class PageMessageBoxDelete : Page
    {
        public void Init(string gridName, string index)
        {
            this.GridName = gridName;
            this.Index = index;
            //
            label.Text = string.Format("Delete? ({0})", ((Database.dbo.ImportName)Application.GridData().Row(gridName, index)).Name);
        }

        private Label label;

        protected override void ApplicationJsonInit()
        {
            label = new Label(ApplicationJson, null) { TypePage = TypePage() };
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
                Application.GridData().LoadRow("Detail", null);
                Application.GridData().SaveJson(ApplicationJson);
                //
                Application.PageRemove(GetType());
                Application.PageShow<PageGridDatabaseBrowse>();
            }
        }

        public string GridName
        {
            get
            {
                return StateGet<string>();
            }
            set
            {
                StateSet(value);
            }
        }

        public string Index
        {
            get
            {
                return StateGet<string>();
            }
            set
            {
                StateSet(value);
            }
        }
    }

    public class ProcessGridMasterIsClick : ProcessBase
    {
        protected override void Process()
        {
            foreach (GridRow gridRow in ApplicationJson.GridDataJson.RowList["Master"])
            {
                if (gridRow.IsClick)
                {
                    if (Util.IndexToIndexEnum(gridRow.Index) == IndexEnum.Index)
                    {
                        GridData gridData = Application.GridData();
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

    public class PageDebug : Page
    {
        protected override void ApplicationJsonInit()
        {
            // var p = new PageComponent(ApplicationJson, "Process") { TypePage = TypePage() };
            new Button(ApplicationJson, "Toggle X") { TypePage = TypePage() };
            new Button(ApplicationJson, "Remove Y") { TypePage = TypePage() };
            new Button(ApplicationJson, "X") { TypePage = TypePage() };
            new Button(ApplicationJson, "Y") { TypePage = TypePage() };
            new Button(ApplicationJson, "Reset") { TypePage = TypePage() };
            new Button(ApplicationJson, "Close") { TypePage = TypePage() };

            //new Label(p, "Hello");

            //var layoutContainer = new LayoutContainer(ApplicationJson, "Container");
            //var rowHeader = new LayoutRow(layoutContainer, null);
            //var cell1 = new LayoutCell(rowHeader, "Cell1");// { Class = "col-sm-6" };
            //new Label(cell1, "Cell1 Content");
            //var cell2 = new LayoutCell(rowHeader, "Cell2");// { Class = "col-sm-6" };
            //new Label(cell2, "Cell2 Content");
            //new Button(p, "P2");
            //new Button(ApplicationJson, "Browse") { TypePage = TypePage(), Name = "B" };
            //new Button(ApplicationJson, "About") { TypePage = TypePage(), Name = "A" };
        }

        protected override void ProcessPage()
        {
            if (ApplicationJson.ListAll<Button>()[0].IsClick)
            {
                ApplicationJson.ListAll<Button>()[2].IsHide = !ApplicationJson.ListAll<Button>()[2].IsHide;
            }
            if (ApplicationJson.ListAll<Button>()[1].IsClick)
            {
                ApplicationJson.List.Remove(ApplicationJson.ListAll<Button>()[3]);
            }
            if (ApplicationJson.ListAll<Button>()[3].IsClick)
            {
                ApplicationJson.List.Clear();
                new Button(ApplicationJson, "Toggle X") { TypePage = TypePage() };
                new Button(ApplicationJson, "Remove Y") { TypePage = TypePage() };
                new Button(ApplicationJson, "X") { TypePage = TypePage() };
                new Button(ApplicationJson, "Y") { TypePage = TypePage() };
                new Button(ApplicationJson, "Reset") { TypePage = TypePage() };
                new Button(ApplicationJson, "Close") { TypePage = TypePage() };
            }
            if (ApplicationJson.ListAll<Button>().Count > 5 && ApplicationJson.ListAll<Button>()[5].IsClick)
            {
                Application.PageShow<PageMain>();
            }
        }
    }
}
