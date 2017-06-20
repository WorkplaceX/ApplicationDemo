namespace Application
{
    using Framework.Application;
    using Framework.JsonComponent;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GridTrafficLight : Page
    {
        protected override void InitJson(App app)
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
        protected override void InitJson(App app)
        {
            new GridKeyboard(this, "GridKeyboard");
            new Button(this, "Close") { Name = "Close"};
            this.CssClass = "container co";
            var container = this;
            var rowLogo = new LayoutRow(container, "RowLogo") { CssClass = "r" };
            var literalLogo = new Literal(rowLogo, "Logo");
            literalLogo.Html = "<img src='Logo.png' />";
            // Row
            var rowHeader = new LayoutRow(container, "Header") { CssClass = "r" };
            var cellHeader1 = new LayoutCell(rowHeader, "HeaderCell1") { CssClass = "col-sm-6" };
            new GridField(cellHeader1, "Field", null, null, null);
            var cellHeader2 = new LayoutCell(rowHeader, "HeaderCell2") { CssClass = "col-sm-6 c" };
            new Grid(cellHeader2, "LookUp", "LookUp");
            // Row
            var rowHeader2 = new LayoutRow(container, "Header") { CssClass = "r" };
            var cellHeader3 = new LayoutCell(rowHeader2, "HeaderCell3") { CssClass = "col-sm-12 c" };
            app.PageShow<GridTrafficLight>(cellHeader3);
            // Row
            var rowContent = new LayoutRow(container, "Content");
            var cellContent1 = new LayoutCell(rowContent, "ContentCell1") { CssClass = "col-sm-6 c d1" };
            var cellContent2 = new LayoutCell(rowContent, "ContentCell2") { CssClass = "col-sm-6 c2 d2" };
            new Grid(cellContent1, "Master", "Master");
            new Grid(cellContent2, "Detail", "Detail");

            var rowFooter = new LayoutRow(container, "Footer");
            var cellFooter1 = new LayoutCell(rowFooter, "FooterCell1") { CssClass = "col-sm-6 c" };
            var literal = new Literal(cellFooter1, "Literal");
            literal.Html = "Hello <b>Literal</b>";
            literal.CssClass = "y";
            var cellFooter2 = new LayoutCell(rowFooter, "FooterCell1") { CssClass = "col-sm-6 c" };
            var button = new Button(cellFooter2, "Hello");
            //
            app.GridData().LoadDatabase("Master", null, null, false, typeof(Database.dbo.TableName));
            app.GridData().SaveJson(app.AppJson);
        }

        protected override void RunBegin(App app)
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
                PageShow<PageMain>(app);
            }
        }
    }

    public class PageMain : Page
    {
        protected override void InitJson(App app)
        {
            this.CssClass = "container";
            new Button(this, "Browse");
            new Button(this, "Debug");
            new Button(this, "About");
        }

        protected override void RunBegin(App app)
        {
            if (List.OfType<Button>().ToArray()[0].IsClick)
            {
                PageShow<PageGridDatabaseBrowse>(app);
            }
            if (List.OfType<Button>().ToArray()[1].IsClick)
            {
                PageShow<PageDebug>(app);
            }
            if (List.OfType<Button>().ToArray()[2].IsClick)
            {
                PageShow<PageMessageBoxAbout>(app);
            }
        }
    }

    public class PageMessageBoxAbout : Page
    {
        protected override void InitJson(App app)
        {
            new Label(this, "(C) 2017 by Framework");
            new Button(this, "Ok");
        }

        protected override void RunBegin(App app)
        {
            if (List.OfType<Button>().First().IsClick)
            {
                PageShow<PageMain>(app);
            }
        }
    }

    public class PageMessageBoxDelete : Page
    {
        private Label label;

        protected override void InitJson(App app)
        {
            label = new Label(this, null);
            new Button(this, "Yes");
            new Button(this, "No");
        }

        public void Init(App app, string gridName, string index)
        {
            this.GridName = gridName;
            this.Index = index;
            //
            label.Text = string.Format("Delete? ({0})", ((Database.dbo.ImportName)app.GridData().Row(gridName, index)).Name);
        }

        protected override void RunBegin(App app)
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
                app.GridData().LoadRow("Detail", null);
                app.GridData().SaveJson(app.AppJson);
                //
                PageShow<PageGridDatabaseBrowse>(app);
            }
        }

        public string GridName;

        public string Index;
    }

    public class ProcessGridMasterIsClick : Process
    {
        protected override void Run(App app)
        {
            List<GridRow> rowList;
            if (app.AppJson.GridDataJson.RowList.TryGetValue("Master", out rowList))
            {
                foreach (GridRow gridRow in rowList)
                {
                    if (gridRow.IsClick)
                    {
                        if (UtilApplication.IndexToIndexEnum(gridRow.Index) == IndexEnum.Index)
                        {
                            GridData gridData = app.GridData();
                            var row = gridData.Row("Master", gridRow.Index) as Database.dbo.TableName;
                            string tableName = row.TableName2;
                            if (tableName != null && tableName.IndexOf(".") != -1)
                            {
                                tableName = tableName.Substring(tableName.IndexOf(".") + 1);
                                //
                                Type typeRow = Framework.DataAccessLayer.Util.TypeRowFromTableName(tableName, typeof(AppDemo));
                                gridData.LoadDatabase("Detail", null, null, false, typeRow);
                                gridData.SaveJson(app.AppJson);
                            }
                        }
                    }
                }
            }
        }
    }

    public class PageDebug : Page
    {
        protected override void InitJson(App app)
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


        protected override void RunBegin(App app)
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
                InitJson(app);
            }
            if (Button("Close")?.IsClick == true)
            {
                PageShow<PageMain>(app);
            }
        }
    }
}
