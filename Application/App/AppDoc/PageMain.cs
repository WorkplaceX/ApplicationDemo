namespace Application.Doc
{
    using Framework.Json;

    public class PageMain : Page
    {
        public PageMain(ComponentJson owner) 
            : base(owner) 
        {
            new Html(this)
            {
                TextHtml =
                @"
                <section class='hero is-primary'>
                  <div class='hero-body'>
                    <div class='container'>
                      <h1 class='title'>
                        WorkplaceX.org
                      </h1>
                      <h2 class='subtitle'>
                        Boost your Business App
                      </h2>
                    </div>
                  </div>
                </section>
                "
            };
        }
    }
}
