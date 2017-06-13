﻿namespace Application
{
    using Framework.Server.Application;
    using Framework.Server.Application.Json;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PageGridDatabaseBrowse : PageGrid
    {
        protected override void ProcessInit()
        {
            base.ProcessInit();
            ProcessList.AddBefore<ProcessGridMasterIsClick, ProcessGridIsClickFalse>();
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
            new Button(ApplicationJson, "Browse") { TypePage = TypePage(), Name = "B" };
            new Button(ApplicationJson, "About") { TypePage = TypePage(), Name = "A" };
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
            label.Text = string.Format("Delete? ({0})", ((Database.dbo.ImportName)GridData().Row(gridName, index)).Name);
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
                GridData().LoadRow("Detail", null);
                GridData().SaveJson(ApplicationJson);
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

    public class ProcessGridMasterIsClick : ProcessBase<PageGrid>
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
