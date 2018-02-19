namespace Application
{
    using Framework.Application;
    using Framework.DataAccessLayer;
    using Framework.Component;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework;

    public class GridTrafficLight : Page
    {
        protected override void InitJson(App app)
        {
            new Label(this);
        }

        protected override void RunEnd(App app)
        {
            List.OfType<Label>().Single().Text = "IsModify=";
        }
    }

    public class PageGridDatabaseBrowse : Page
    {
        protected override void InitJson(App app)
        {
            PageShow<PageFlight>(app);
            return;

            var gridNameMaster = new GridName<Database.dbo.TableName>();
            //
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
            new GridFieldSingle(cellHeader1);
            var cellHeader2 = new LayoutCell(rowHeader) { CssClass = "col-sm-6 c" };
            // Row
            var rowHeader2 = new LayoutRow(container) { CssClass = "r" };
            var cellHeader3 = new LayoutCell(rowHeader2) { CssClass = "col-sm-12 c" };
            app.PageShow<GridTrafficLight>(cellHeader3);
            // Row
            var rowContent = new LayoutRow(container);
            var cellContent1 = new LayoutCell(rowContent) { CssClass = "col-sm-6 c d1" };
            var cellContent2 = new LayoutCell(rowContent) { CssClass = "col-sm-6 c2 d2" };
            new Grid(cellContent1, gridNameMaster);
            new Grid(cellContent2, new GridName("Detail"));

            var rowFooter = new LayoutRow(container);
            var cellFooter1 = new LayoutCell(rowFooter) { CssClass = "col-sm-6 c" };
            var literal = new Literal(cellFooter1);
            literal.TextHtml = "Hello <b>Literal</b>";
            literal.CssClass = "y";
            var cellFooter2 = new LayoutCell(rowFooter) { CssClass = "col-sm-6 c" };
            var button = new Button(cellFooter2) { Text = "Hello" };
            //
            // app.GridData.LoadDatabase(gridNameMaster);
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
            new Button(this) { Text = "Flight" };
            new Label(this) { Text = "VersionServer=" + UtilFramework.VersionServer };
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
            if (List.OfType<Button>().ToArray()[3].IsClick)
            {
                PageShow<PageFlight>(app);
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
            new Button(this) { Text = "None" };
        }

        public void Init(AppEventArg e)
        {
            this.AppEventArgJson = UtilApplication.AppEventArgToJson(e);
            //
            label.Text = string.Format("Delete? ({0})", ((Database.dbo.Flight)e.App.GridData.RowGet(e.GridName, e.Index)).AirportText);
        }

        protected override void RunBegin(App app)
        {
            if (ListAll().OfType<Button>().Where(item => item.Text == "Yes").Single().IsClick)
            {
                AppEventArg e = AppEventArg(app);
                Row row = app.GridData.RowGet(e.GridName, e.Index);
                UtilDataAccessLayer.Delete(row);
                app.GridData.LoadDatabaseReload(e.GridName);
                PageShow<PageGridDatabaseBrowse>(app);
            }
            if (ListAll().OfType<Button>().Where(item => item.Text == "No").Single().IsClick)
            {
                PageShow<PageGridDatabaseBrowse>(app);
            }
        }

        public string AppEventArgJson;

        public AppEventArg AppEventArg(App app)
        {
            return UtilApplication.AppEventArgFromJson(app, AppEventArgJson);
        }
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
