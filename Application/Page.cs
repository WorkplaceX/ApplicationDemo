namespace Application
{
    using Framework.Server.Application;
    using Framework.Server.Application.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GridTrafficLight : Page
    {
        protected override void InitJson(ApplicationBase application)
        {
            new Label(this, null);
        }

        protected override void RunEnd()
        {
            List.OfType<Label>().Single().Text = "IsModify=";
        }
    }

    public class PageGridDatabaseBrowse : Page
    {
        protected override void InitJson(ApplicationBase application)
        {
            new GridKeyboard(this, "GridKeyboard");
            new Button(this, "Close") { Name = "Close"};
            var container = new LayoutContainer(this, "Container") { Class = "co"};
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
            application.PageShow<GridTrafficLight>(cellHeader3);
            // new LabelGridSaveState(cellHeader3, "GridSaveState"); // TODO
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
            application.GridData().LoadDatabase("Master", null, null, false, typeof(Database.dbo.TableName));
            application.GridData().SaveJson(application.ApplicationJson);
        }

        protected override void RunBegin(ApplicationBase application)
        {
            bool isClick = false;
            string name = null;
            foreach (Button button in List.OfType<Button>())
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
                PageShow<PageMain>(application);
            }
        }
    }

    public class PageMain : Page
    {
        protected override void InitJson(ApplicationBase application)
        {
            new Button(this, "Browse");
            new Button(this, "Debug");
            new Button(this, "About");
            
        }

        protected override void RunBegin(ApplicationBase application)
        {
            if (List.OfType<Button>().ToArray()[0].IsClick)
            {
                PageShow<PageGridDatabaseBrowse>(application);
            }
            if (List.OfType<Button>().ToArray()[1].IsClick)
            {
                PageShow<PageDebug>(application);
            }
            if (List.OfType<Button>().ToArray()[2].IsClick)
            {
                PageShow<PageMessageBoxAbout>(application);
            }
        }
    }

    public class PageMessageBoxAbout : Page
    {
        protected override void InitJson(ApplicationBase applicationBase)
        {
            new Label(this, "(C) 2017 by Framework");
            new Button(this, "Ok");
        }

        protected override void RunBegin(ApplicationBase application)
        {
            if (List.OfType<Button>().First().IsClick)
            {
                PageShow<PageMain>(application);
            }
        }
    }

    public class PageMessageBoxDelete : Page
    {
        private Label label;

        protected override void InitJson(ApplicationBase application)
        {
            label = new Label(this, null);
            new Button(this, "Yes");
            new Button(this, "No");
        }

        public void Init(ApplicationBase application, string gridName, string index)
        {
            this.GridName = gridName;
            this.Index = index;
            //
            label.Text = string.Format("Delete? ({0})", ((Database.dbo.ImportName)application.GridData().Row(gridName, index)).Name);
        }

        protected override void RunBegin(ApplicationBase application)
        {
            bool isClick = false;
            foreach (Button button in List.OfType<Button>())
            {
                if (button.IsClick)
                {
                    isClick = true;
                    break;
                }
            }
            //
            if (isClick)
            {
                application.GridData().LoadRow("Detail", null);
                application.GridData().SaveJson(application.ApplicationJson);
                //
                PageShow<PageGridDatabaseBrowse>(application);
            }
        }

        public string GridName;

        public string Index;
    }

    public class ProcessGridMasterIsClick : Process
    {
        protected override void Run(ApplicationBase application)
        {
            List<GridRow> rowList;
            if (application.ApplicationJson.GridDataJson.RowList.TryGetValue("Master", out rowList))
            {
                foreach (GridRow gridRow in rowList)
                {
                    if (gridRow.IsClick)
                    {
                        if (Util.IndexToIndexEnum(gridRow.Index) == IndexEnum.Index)
                        {
                            GridData gridData = application.GridData();
                            var row = gridData.Row("Master", gridRow.Index) as Database.dbo.TableName;
                            string tableName = row.TableName2;
                            if (tableName != null && tableName.IndexOf(".") != -1)
                            {
                                tableName = tableName.Substring(tableName.IndexOf(".") + 1);
                                //
                                Type typeRow = Framework.Server.DataAccessLayer.Util.TypeRowFromTableName(tableName, typeof(Application));
                                gridData.LoadDatabase("Detail", null, null, false, typeRow);
                                gridData.SaveJson(application.ApplicationJson);
                            }
                        }
                    }
                }
            }
        }
    }

    public class PageDebug : Page
    {
        protected override void InitJson(ApplicationBase application)
        {
            new Button(this, "Toggle X");
            new Button(this, "Remove Y");
            new Button(this, "X");
            new Button(this, "Y");
            new Button(this, "Reset");
            new Button(this, "Close");
        }

        public Button Button(string text)
        {
            return List.OfType<Button>().Where(item => item.Text == text).SingleOrDefault();
        }


        protected override void RunBegin(ApplicationBase application)
        {
            if (Button("Toggle X")?.IsClick == true)
            {
                Button("X").IsHide = !Button("X").IsHide;
            }
            if (Button("Remove Y")?.IsClick == true)
            {
                List.Remove(Button("Y"));
            }
            if (Button("Reset")?.IsClick == true)
            {
                List.Clear();
                InitJson(application);
            }
            if (Button("Close")?.IsClick == true)
            {
                PageShow<PageMain>(application);
            }
        }
    }
}
