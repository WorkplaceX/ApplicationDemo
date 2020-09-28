namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageLoginSignIn : Page
    {
        public PageLoginSignIn(ComponentJson owner) 
            : base(owner) 
        {
            DivContainer = new Div(this) { CssClass = "container" };

            new Html(DivContainer) { TextHtml = "<h1>User Sign In</h1>" };
            Grid = new GridSignIn(DivContainer);

            Button = new Button(DivContainer) { TextHtml = "Login" };
        }

        public override async Task InitAsync()
        {
            await Grid.LoadAsync();
        }

        public Html AlertError;

        protected override async Task ProcessAsync()
        {
            AlertError.ComponentRemove();
            if (Button.IsClick)
            {
                var loginUserSession = (LoginUser)Grid.RowSelected;
                var loginUser = (await Data.Query<LoginUser>().Where(item => item.Name == loginUserSession.Name).QueryExecuteAsync()).SingleOrDefault();
                if (loginUser == null)
                {
                    this.AlertError = this.CreateAlert("Username or password wrong!", AlertEnum.Error);
                }
                else
                {
                    var pageMain = this.ComponentOwner<PageMain>();
                    pageMain.LoginUser = loginUser;
                    pageMain.LoginUserPermissionDisplayList = await Data.Query<LoginUserPermissionDisplay>().Where(item => item.UserName == loginUser.Name).QueryExecuteAsync();
                }
                Button.TextHtml = string.Format("User={0};", ((LoginUser)Grid.RowSelected).Name);
            }
        }

        public Div DivContainer;

        public GridSignIn Grid;

        public Button Button;
    }

    public class GridSignIn : Grid<LoginUser>
    {
        public GridSignIn(ComponentJson owner) : base(owner) { }

        protected override void Query(QueryArgs args, QueryResult result)
        {
            result.Query = Data.Query<LoginUser>(DatabaseEnum.Memory);
        }
    }
}
