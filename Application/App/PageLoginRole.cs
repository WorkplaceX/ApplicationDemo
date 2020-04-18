namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageLoginRole : Page
    {
        public PageLoginRole(ComponentJson owner) : base(owner) { }

        public override async Task InitAsync()
        {
            DivContainer = new BootstrapContainer(this);

            DivRow = new BootstrapRow(DivContainer);
            DivColLeft = new BootstrapCol(DivRow);
            DivColRight = new BootstrapCol(DivRow);

            new Html(DivColLeft) { TextHtml = "<h1>Role <i class='fas fa-hat-cowboy'></i></h1>" };
            new Html(DivColLeft) { TextHtml = "Define User Roles." };
            GridLoginRole = new GridLoginRole(DivColLeft);
            new Html(DivColRight) { TextHtml = "<h1>Permission <i class='fas fa-key'></i></h1>" };
            new Html(DivColRight) { TextHtml = "Define Permissions." };
            GridLoginPermission = new GridLoginPermission(DivColRight);

            new Html(DivContainer) { TextHtml = "<h1>Role <i class='fas fa-hat-cowboy'></i> to Permission <i class='fas fa-key'></i> Mapping</h1>" };
            new Html(DivContainer) { TextHtml = "Assign Permissions to Roles." };
            GridLoginRolePermission = new GridLoginRolePermission(DivContainer);

            await Task.WhenAll(GridLoginRole.LoadAsync(), GridLoginPermission.LoadAsync());
        }

        public BootstrapContainer DivContainer;

        public BootstrapRow DivRow;

        public BootstrapCol DivColLeft;
        
        public Div DivColRight;

        public GridLoginRole GridLoginRole;

        public GridLoginPermission GridLoginPermission;

        public GridLoginRolePermission GridLoginRolePermission;
    }

    public class GridLoginRole : Grid<LoginRole>
    {
        public GridLoginRole(ComponentJson owner) : base(owner) { }

        protected override async Task RowSelectedAsync()
        {
            var page = this.ComponentOwner<PageLoginRole>();

            await page.GridLoginRolePermission.LoadAsync();
        }
    }

    public class GridLoginPermission : Grid<LoginPermission>
    {
        public GridLoginPermission(ComponentJson owner) : base(owner) { }
    }

    public class GridLoginRolePermission : Grid<LoginRolePermissionDisplay>
    {
        public GridLoginRolePermission(ComponentJson owner) : base(owner) { }

        protected override void Query(QueryArgs args, QueryResult result)
        {
            var page = this.ComponentOwner<PageLoginRole>();

            result.Query = args.Query.Where(item => item.RoleId == page.GridLoginRole.RowSelected.Id);
        }

        protected override async Task UpdateAsync(UpdateArgs args, UpdateResult result)
        {
            var loginRolePermission = new LoginRolePermission();
            Data.RowCopy(args.Row, loginRolePermission);

            await Data.UpsertAsync(loginRolePermission, new string[] { nameof(LoginRolePermission.RoleId), nameof(LoginRolePermission.PermissionId) });

            result.IsHandled = true;
        }
    }
}
