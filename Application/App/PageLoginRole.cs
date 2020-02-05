namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageLoginRole : Page
    {
        public PageLoginRole(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            DivContainer = new Div(this) { CssClass = "container" };

            DivRow = new DivContainer(DivContainer) { CssClass = "row" };
            DivColLeft = new Div(DivRow) { CssClass = "col" };
            DivColRight = new Div(DivRow) { CssClass = "col" };

            new Html(DivColLeft) { TextHtml = "<h1>Role <i class='fas fa-hat-cowboy'></i></h1>" };
            new Html(DivColLeft) { TextHtml = "Define User Roles." };
            GridLoginRole = new Grid(DivColLeft);
            new Html(DivColRight) { TextHtml = "<h1>Permission <i class='fas fa-key'></i></h1>" };
            new Html(DivColRight) { TextHtml = "Define Permissions." };
            GridLoginPermission = new Grid(DivColRight);

            new Html(DivContainer) { TextHtml = "<h1>Role <i class='fas fa-hat-cowboy'></i> to Permission <i class='fas fa-key'></i> Mapping</h1>" };
            new Html(DivContainer) { TextHtml = "Assign Permissions to Roles." };
            GridLoginRolePermission = new Grid(DivContainer);

            await Task.WhenAll(GridLoginRole.LoadAsync(), GridLoginPermission.LoadAsync());
        }

        public Div DivContainer;

        public DivContainer DivRow;

        public Div DivColLeft;
        
        public Div DivColRight;

        public Grid GridLoginRole;

        public Grid GridLoginPermission;

        public Grid GridLoginRolePermission;

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            if (grid == GridLoginRole)
            {
                await GridLoginRolePermission.LoadAsync();
            }
        }

        protected override async Task<bool> GridUpdateAsync(Grid grid, Row row, Row rowNew, DatabaseEnum databaseEnum)
        {
            if (grid == GridLoginRolePermission)
            {
                var loginRolePermissionDisplay = (LoginRolePermissionDisplay)rowNew;
                var loginRolePermission = new LoginRolePermission();
                Data.RowCopy(loginRolePermissionDisplay, loginRolePermission);

                await Data.UpsertAsync(loginRolePermission, new string[] { nameof(LoginRolePermission.LoginRoleId), nameof(LoginRolePermission.LoginPermissionId) });
                return true;
            }
            return false;
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == GridLoginRole)
            {
                return Data.Query<LoginRole>();
            }
            if (grid == GridLoginPermission)
            {
                return Data.Query<LoginPermission>();
            }
            if (grid == GridLoginRolePermission)
            {
                return Data.Query<LoginRolePermissionDisplay>().Where(item => item.LoginRoleId == ((LoginRole)GridLoginRole.RowSelected).Id);
            }
            return base.GridQuery(grid);
        }
    }
}
