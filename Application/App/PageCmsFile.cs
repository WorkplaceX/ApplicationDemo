namespace Application
{
    using Database.Demo;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageCmsFile : Page
    {
        public PageCmsFile(ComponentJson owner) : base(owner) 
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>Cms File <i class='far fa-file'></i></h1>" };
        }

        public override async Task InitAsync()
        {
            await new GridCmsFile(this).LoadAsync();
        }
    }

    public class GridCmsFile : Grid<CmsFile>
    {
        public GridCmsFile(ComponentJson owner) : base(owner) { }

        protected override void CellAnnotation(CellAnnotationArgs args, CellAnnotationResult result)
        {
            if (args.FieldName == nameof(args.Row.Data))
            {
                result.IsFileUpload = true;
            }
        }
    }
}
