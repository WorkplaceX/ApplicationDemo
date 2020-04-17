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

        protected override void CellAnnotationRow(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(StorageFile.Data))
            {
                if (args.Row.Data == null)
                {
                    result.IsFileUpload = true;
                }
                result.Html = string.Format("<a href='cms/{0}'>{1}</a>", args.Row.FileName, args.Row.FileName);
            }
        }

        protected override void CellAnnotation(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(args.Row.Data) && args.IsNew)
            {
                result.IsFileUpload = true;
            }
        }

        protected override void CellParseFileUpload(FileUploadArgs args, ParseResult result)
        {
            if (args.FieldName == nameof(StorageFile.Data))
            {
                args.Row.Data = args.Data;
                args.Row.FileName = args.FileName;
                result.IsHandled = true;
            }
        }

        protected override Task InsertAsync(InsertArgs args, InsertResult result)
        {
            args.RowNew.IsExist = true;

            return base.InsertAsync(args, result);
        }
    }
}
