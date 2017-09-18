namespace Application
{
    using Framework.Application;
    using Framework.DataAccessLayer;
    using Framework.Component;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GridTrafficLight : Page
    {
        protected override void InitJson(App app)
        {
            new Label(this);
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
            new GridKeyboard(this);
            new Button(this) { Name = "Close", Text = "Close" };
            this.CssClass = "container co";
            var container = this;
            var rowLogo = new LayoutRow(container) { CssClass = "r" };
            var literalLogo = new Literal(rowLogo);
            literalLogo.TextHtml = "<img src='/Logo.png' />";
            // Row
            var rowHeader = new LayoutRow(container) { CssClass = "r" };
            var cellHeader1 = new LayoutCell(rowHeader) { CssClass = "col-sm-6" };
            new GridField(cellHeader1, null, null, null);
            var cellHeader2 = new LayoutCell(rowHeader) { CssClass = "col-sm-6 c" };
            new Grid(cellHeader2, "LookUp");
            // Row
            var rowHeader2 = new LayoutRow(container) { CssClass = "r" };
            var cellHeader3 = new LayoutCell(rowHeader2) { CssClass = "col-sm-12 c" };
            app.PageShow<GridTrafficLight>(cellHeader3);
            // Row
            var rowContent = new LayoutRow(container);
            var cellContent1 = new LayoutCell(rowContent) { CssClass = "col-sm-6 c d1" };
            var cellContent2 = new LayoutCell(rowContent) { CssClass = "col-sm-6 c2 d2" };
            new Grid(cellContent1, "Master");
            new Grid(cellContent2, "Detail");

            var rowFooter = new LayoutRow(container);
            var cellFooter1 = new LayoutCell(rowFooter) { CssClass = "col-sm-6 c" };
            var literal = new Literal(cellFooter1);
            literal.TextHtml = "Hello <b>Literal</b>";
            literal.CssClass = "y";
            var cellFooter2 = new LayoutCell(rowFooter) { CssClass = "col-sm-6 c" };
            var button = new Button(cellFooter2) { Text = "Hello" };
            //
            app.GridData.LoadDatabase<Database.dbo.TableName>("Master");
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
            new Button(this) { Text = "Browse" };
            new Button(this) { Text = "Debug" };
            new Button(this) { Text = "About" };
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
            new Label(this) { Text = "(C) 2017 by Framework" };
            new Button(this) { Text = "Ok" };
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
            label = new Label(this);
            new Button(this) { Text = "Yes" };
            new Button(this) { Text = "No" };
        }

        public void Init(App app, string gridName, Index index)
        {
            this.GridName = gridName;
            this.Index = index.Value;
            //
            label.Text = string.Format("Delete? ({0})", ((Database.dbo.Country)app.GridData.Row(gridName, index)).Text);
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
                app.GridData.LoadRow("Detail", null);
                //
                PageShow<PageGridDatabaseBrowse>(app);
            }
        }

        public string GridName;

        public string Index;
    }

    public class PageDebug : Page
    {
        protected override void InitJson(App app)
        {
            new Button(this) { Text = "Toggle X" };
            new Button(this) { Text = "Remove Y" };
            new Button(this) { Text = "X" };
            new Button(this) { Text = "Y" };
            new Button(this) { Text = "Reset" };
            new Button(this) { Text = "Close" };
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
