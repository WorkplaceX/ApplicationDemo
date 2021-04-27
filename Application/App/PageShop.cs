namespace Application.Demo
{
    using Database.Demo;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System.Threading.Tasks;

    public class PageShop : Page
    {
        public PageShop(ComponentJson owner) : base(owner) 
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>Shop <i class='fas fa-store'></i></h1>" };
            new Html(container) { TextHtml = "Browse products" };
            this.Grid = new GridProduct(container);
        }

        public Grid Grid;

        public override async Task InitAsync()
        {
            await Grid.LoadAsync();
        }
    }

    public class GridProduct : Grid<ShopProductPhoto>
    {
        public GridProduct(ComponentJson owner) : base(owner) { }

        protected override void CellAnnotation(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(ShopProductPhoto.Data))
            {
                result.Html = string.Format("<img src='{0}'/>", "shop/" + args.Row.FileName);
            }
        }
    }
}
