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

        protected override Task ProcessAsync()
        {
            if (Button.IsClick)
            {
                Button.TextHtml += ".";
            }
            return base.ProcessAsync();
        }

        public Div DivContainer;

        public Grid Grid;

        public Button Button;
    }
}
