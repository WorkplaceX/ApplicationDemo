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
        }

        protected override void ProcessApplicationJsonInit()
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
                Application.PageRemove<PageDatabaseBrowse>();
                Application.Page<PageMain>().Show();
            }
        }
    }

    public class PageMain : Page
    {
        protected override void ProcessApplicationJsonInit()
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
                var messageBox = Application.Page<PageMessageBox>();
                messageBox.Init(GetType());
                messageBox.Process();
            }
            if (isClick && name == "B")
            {
                var page = Application.Page<PageDatabaseBrowse>();
                page.Show();
                page.Process();
            }
        }
    }

    public class PageMessageBox : Page
    {
        public void Init(Type typePageReturn)
        {
            ReturnTypePage = Framework.Util.TypeToString(typePageReturn);
            Show();
        }

        protected override void ProcessApplicationJsonInit()
        {
            new Label(ApplicationJson, "Delete item?") { TypePage = TypePage() };
            new Button(ApplicationJson, "Yes") { TypePage = TypePage() };
            new Button(ApplicationJson, "No") { TypePage = TypePage() };
            Show();
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
                Application.Page(typePage).Show();
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
}
