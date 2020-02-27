namespace Application
{
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System.Threading.Tasks;

    public class PageLoginProfile : Page
    {
        public PageLoginProfile(ComponentJson owner) 
            : base(owner) 
        {
            DivContainer = new Div(this) { CssClass = "container" };

            new Html(DivContainer) { TextHtml = "<h1>User Profile</h1>" };
            Grid = new GridLoginProfile(DivContainer);
        }

        public override async Task InitAsync()
        {
            await Grid.LoadAsync();
        }

        public Div DivContainer;

        public GridLoginProfile Grid;
    }

    public class GridLoginProfile : Grid<Row>
    {
        public GridLoginProfile(ComponentJson owner) : base(owner) { }
    }
}
