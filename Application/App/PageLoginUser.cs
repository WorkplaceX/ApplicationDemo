namespace Application
{
    using Database.Demo;
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
            await GridLoginUser.LoadAsync();

            new Html(DivContainer) { TextHtml = "<h1>User <i class='fas fa-user'></i> to Role <i class='fas fa-hat-cowboy'></i> Mapping</h1>" };
            GridLoginUserRole = new Grid(DivContainer);
            await GridLoginUserRole.LoadAsync();
        }

        public Div DivContainer;

        public Grid GridLoginUser;

        public Grid GridLoginUserRole;

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
                return Data.Query<LoginUserRoleDisplay>();
            }
            return base.GridQuery(grid);
        }
    }
}
