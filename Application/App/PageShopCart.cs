namespace Application
{
    using Database.Demo;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System.Threading.Tasks;

    public class PageShopCart : Page
    {
        public PageShopCart(ComponentJson owner) : base(owner) 
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>Shopping Cart <i class='fas fa-shopping-cart'></i></h1>" };
            new Html(container) { TextHtml = "Products in your shopping cart" };
        }
    }
}
