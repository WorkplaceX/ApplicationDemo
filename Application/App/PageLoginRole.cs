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
            new Html(DivColLeft) { TextHtml = "Define user roles." };
            GridLoginRole = new Grid(DivColLeft);
            await GridLoginRole.LoadAsync();
            new Html(DivColRight) { TextHtml = "<h1>Permission <i class='fas fa-key'></i></h1>" };
            new Html(DivColRight) { TextHtml = "Define permissions." };
            GridLoginPermission = new Grid(DivColRight);
            await GridLoginPermission.LoadAsync();

            new Html(DivContainer) { TextHtml = "<h1>Role <i class='fas fa-hat-cowboy'></i> to Permission <i class='fas fa-key'></i> Mapping</h1>" };
            new Html(DivContainer) { TextHtml = "Assign permissions to roles." };
            GridLoginRolePermission = new Grid(DivContainer);
            await GridLoginRolePermission.LoadAsync();
        }

        public Div DivContainer;

        public DivContainer DivRow;

        public Div DivColLeft;
        
        public Div DivColRight;

        public Grid GridLoginRole;

        public Grid GridLoginPermission;

        public Grid GridLoginRolePermission;

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
                return Data.Query<LoginRolePermissionDisplay>().OrderBy(item => item.LoginRoleName).ThenBy(item => item.LoginPermissionName);
            }
            return base.GridQuery(grid);
        }
    }
}
