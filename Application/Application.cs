namespace Application
{
    using Framework.Server.Application;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Framework.Server.Application.Json;

    public class ApplicationServer : ApplicationServerBase
    {
        protected override void ApplicationJsonInit(ApplicationJson applicationJson)
        {
            applicationJson.GridDataJson = new GridDataJson();
            //
            new GridKeyboard(applicationJson, "GridKeyboard");
            var container = new LayoutContainer(applicationJson, "Container") { Class = "co" };
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
            MessageBoxInit(applicationJson);
            //
            GridDataServer gridDataServer = new GridDataServer();
            gridDataServer.LoadDatabase("Master", null, null, false, typeof(Database.dbo.TableName));
            gridDataServer.SaveJson(applicationJson);
        }

        private void MessageBoxInit(ApplicationJson applicationJson)
        {
            var container = new LayoutContainer(applicationJson, "Container");
            // var row = new LayoutRow(container, "Row");
            // var cell = new LayoutCell(row, "Cell1");
            Grid grid = new Grid(applicationJson, "MessageBox", "MessageBox");
            new GridField(applicationJson, "GridField", "MessageBox", "Text", "1");
            new GridField(applicationJson, "GridField", "MessageBox", "ButtonYes", "1");
            // grid.IsHide = true;
            //
            GridDataServer gridDataServer = new GridDataServer();
            Database.MessageBox messageBox = new Database.MessageBox();
            messageBox.Text = "Delete record?";
            messageBox.ButtonYes = "Yes";
            messageBox.ButtonNo = "No";
            gridDataServer.LoadRow("MessageBox", messageBox);
            gridDataServer.SaveJson(applicationJson);
        }

        protected override void ProcessInit()
        {
            base.ProcessInit();
            ProcessListInsertAfter<ProcessGridIsIsClick>(new ProcessGridRowFirstIsClick(this));
            ProcessListInsertAfter<ProcessGridIsIsClick>(new ProcessGridMasterIsClick(this));
        }

        protected override Type TypePageServerMain()
        {
            return typeof(PageServerMain);
        }
    }

    public class ProcessGridMasterIsClick : ProcessBase
    {
        public ProcessGridMasterIsClick(ApplicationServerBase applicationServer) :
            base(applicationServer)
        {

        }

        protected override void ProcessEnd(ApplicationJson applicationJson)
        {
            foreach (GridRow gridRow in applicationJson.GridDataJson.RowList["Master"])
            {
                if (gridRow.IsClick)
                {
                    if (Util.IndexToIndexEnum(gridRow.Index) == IndexEnum.Index)
                    {
                        GridDataServer gridDataServer = new GridDataServer();
                        gridDataServer.LoadJson(applicationJson, "Master", typeof(ApplicationServer));
                        var row = gridDataServer.Row("Master", gridRow.Index) as Database.dbo.TableName;
                        string tableName = row.TableName2;
                        if (tableName != null && tableName.IndexOf(".") != -1)
                        {
                            tableName = tableName.Substring(tableName.IndexOf(".") + 1);
                            //
                            Type typeRow = Framework.Server.DataAccessLayer.Util.TypeRowFromTableName(tableName, typeof(ApplicationServer));
                            gridDataServer = new GridDataServer();
                            gridDataServer.LoadDatabase("Detail", null, null, false, typeRow);
                            gridDataServer.SaveJson(applicationJson);
                        }
                    }
                }
            }
        }
    }

    public class PageServerMain : PageServer
    {
        protected override void ProcessApplicationJsonInit()
        {
            new Button(ApplicationJson, "Delete") { TypeNamePage = TypeNamePageServer() };
        }


        protected override void ProcessPage()
        {
            bool isClick = false;
            foreach (Component component in ApplicationJson.List)
            {
                Button button = component as Button;
                if (button != null)
                {
                    if (button.TypeNamePage == TypeNamePageServer() && button.IsClick)
                    {
                        isClick = true;
                        break;
                    }
                }
            }
            if (isClick)
            {
                var messageBox = ApplicationServer.PageServer<PageServerMessageBox>();
                messageBox.Init(GetType());
                messageBox.Process();
            }
        }
    }

    public class PageServerMessageBox : PageServer
    {
        public void Init(Type typePageServerReturn)
        {
            ReturnTypeNamePageServer = Framework.Util.TypeToTypeName(typePageServerReturn);
            Show();
        }

        protected override void ProcessApplicationJsonInit()
        {
            new Label(ApplicationJson, "Delete item?") { TypeNamePage = TypeNamePageServer() };
            new Button(ApplicationJson, "Yes") { TypeNamePage = TypeNamePageServer() };
            new Button(ApplicationJson, "No") { TypeNamePage = TypeNamePageServer() };
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
                    if (button.TypeNamePage == TypeNamePageServer() && button.IsClick)
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
                Type typePageServer = Framework.Util.TypeFromTypeName(ReturnTypeNamePageServer, ApplicationServer.GetType());
                ApplicationServer.PageServer(typePageServer).Show();
            }
        }

        public string ReturnTypeNamePageServer
        {
            get
            {
                return StateGet<string>(nameof(ReturnTypeNamePageServer));
            }
            set
            {
                StateSet(nameof(ReturnTypeNamePageServer), value);
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
