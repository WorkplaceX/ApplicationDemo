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
            Grid = new Grid(DivContainer);

            Button = new Button(DivContainer) { TextHtml = "Login" };
        }

        public override async Task InitAsync()
        {
            await Grid.LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return Data.Query<LoginUser>(DatabaseEnum.MemorySingleton);
        }

        public Html AlertError;

        protected override async Task ProcessAsync()
        {
            AlertError.ComponentRemove();
            if (Button.IsClick)
            {
                var loginUserSession = (LoginUser)Grid.RowSelected;
                var loginUser = (await Data.SelectAsync(Data.Query<LoginUser>().Where(item => item.Name == loginUserSession.Name))).SingleOrDefault();
                if (loginUser == null)
                {
                    this.AlertError = this.BootstrapAlert("Username or password wrong!", BootstrapAlertEnum.Error);
                }
                else
                {
                    var pageMain = this.ComponentOwner<PageMain>();
                    pageMain.LoginUserPermissionDisplayList = await Data.SelectAsync<LoginUserPermissionDisplay>(Data.Query<LoginUserPermissionDisplay>().Where(item => item.UserName == loginUser.Name));
                }
                Button.TextHtml = string.Format("User={0};", ((LoginUser)Grid.RowSelected).Name);
            }
        }

        public Div DivContainer;

        public Grid Grid;

        public Button Button;
    }
}
