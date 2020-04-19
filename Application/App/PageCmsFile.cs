namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
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

        protected override void Truncate(TruncateArgs args)
        {
            foreach (var item in args.RowList)
            {
                if (item.Data != null && item.Data.Length != 0)
                {
                    item.Data = new byte[0];
                }
            }
        }

        protected override void CellAnnotationRow(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(args.Row.FileName))
            {
                if (args.Row.Data == null)
                {
                    result.IsFileUpload = true;
                }
                else
                {
                    result.HtmlRight = string.Format("<a href='cms/{0}'><i class='fas fa-external-link-alt'></i></a>", args.Row.FileName);
                }
            }
        }

        protected override void CellAnnotation(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(args.Row.FileName) && args.IsNew)
            {
                result.IsFileUpload = true;
            }
        }

        protected override void CellParseFileUpload(FileUploadArgs args, ParseResult result)
        {
            if (args.FieldName == nameof(args.Row.FileName))
            {
                result.Row.Data = args.Data;
                if (args.Row.FileName == null)
                {
                    result.Row.FileName = args.FileName;
                }

                result.IsHandled = true;
            }
        }
    }
}
