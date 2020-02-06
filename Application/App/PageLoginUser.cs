namespace Application
{
    using Database.Demo;
    using DatabaseBuiltIn.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageLoginUser : Page
    {
        public PageLoginUser(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            DivContainer = new Div(this) { CssClass = "container" };

            new Html(DivContainer) { TextHtml = "<h1>User <i class='fas fa-user'></i></h1>" };
            GridLoginUser = new Grid(DivContainer);

            new Html(DivContainer) { TextHtml = "<h1>User <i class='fas fa-user'></i> to Role <i class='fas fa-hat-cowboy'></i> Mapping</h1>" };
            GridLoginUserRole = new Grid(DivContainer);

            new Html(DivContainer) { TextHtml = "<h1>Permission <i class='fas fa-key'></i></h1>" };
            new Html(DivContainer) { TextHtml = "User has the following permissions:" };
            GridLoginUserPermission = new Grid(DivContainer);

            await GridLoginUser.LoadAsync();
        }

        public Div DivContainer;

        public Grid GridLoginUser;

        public Grid GridLoginUserRole;

        public Grid GridLoginUserPermission;

        protected override async Task<bool> GridUpdateAsync(Grid grid, Row row, Row rowNew, DatabaseEnum databaseEnum)
        {
            if (grid == GridLoginUserRole)
            {
                var loginUserRoleDisplay = (LoginUserRoleDisplay)rowNew;
                var loginUserRole = new LoginUserRole();
                Data.RowCopy(loginUserRoleDisplay, loginUserRole);

                await Data.UpsertAsync(loginUserRole, new string[] { nameof(LoginUserRole.LoginUserId), nameof(LoginUserRole.LoginRoleId) });
                return true;
            }
            return false;
        }

        protected override void GridCellAnnotation(Grid grid, string fieldName, Row row, GridCellAnnotationResult result)
        {
            if (fieldName == nameof(LoginUser.Password))
            {
                result.IsPassword = true;
            }
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == GridLoginUser)
            {
                return Data.Query<LoginUser>();
            }
            if (grid == GridLoginUserRole)
            {
                return Data.Query<LoginUserRoleDisplay>().Where(item => item.LoginUserId == ((LoginUser)GridLoginUser.RowSelected).Id);
            }
            if (grid == GridLoginUserPermission)
            {
                return Data.Query<LoginUserPermissionDisplay>().Where(item => item.LoginUserId == ((LoginUser)GridLoginUser.RowSelected).Id);
            }
            return base.GridQuery(grid);
        }

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            if (grid == GridLoginUser)
            {
                // Load detail data grids
                await Task.WhenAll(GridLoginUserRole.LoadAsync(), GridLoginUserPermission.LoadAsync());
            }
        }
    }
}
