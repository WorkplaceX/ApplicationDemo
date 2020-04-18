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

    public class GridCmsFile : Grid<CmsFileDisplay>
    {
        public GridCmsFile(ComponentJson owner) : base(owner) { }

        protected override void CellAnnotationRow(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(args.Row.FileName))
            {
                if (args.Row.IsData == false)
                {
                    result.IsFileUpload = true;
                }
                else
                {
                    result.HtmlRight = string.Format("<a href='cms/{0}'><i class='fas fa-external-link-alt'></i></a>", args.Row.FileName);
                }
            }
            // result.Html = string.Format("<a href='cms/{0}'>{1}</a>", args.Row.FileName, args.Row.FileName);
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
                if (args.IsNew)
                {
                    result.Row.FileName = args.FileName;
                }
                result.Row.DataUpload = args.Data;
                result.Row.IsData = true;
                result.IsHandled = true;
            }
        }

        protected override async Task UpdateAsync(UpdateArgs args, UpdateResult result)
        {
            var row = (await Data.Query<CmsFile>().Where(item => item.Id == args.Row.Id).QueryExecuteAsync()).Single(); // Load data row
            Data.RowCopy(args.RowNew, row);
            if (args.RowNew.DataUpload != null)
            {
                row.Data = args.RowNew.DataUpload;
            }

            await Data.UpdateAsync(row);

            result.IsHandled = true;
        }

        protected override async Task InsertAsync(InsertArgs args, InsertResult result)
        {
            args.RowNew.IsExist = true;

            // Insert
            var row = Data.RowCopy<CmsFile>(args.RowNew);
            if (args.RowNew.DataUpload != null)
            {
                row.Data = args.RowNew.DataUpload;
                args.RowNew.IsData = true;
            }
            await Data.InsertAsync(row);
            args.RowNew.Id = row.Id;

            args.RowNew.DataUpload = null; // Do not store in session state.

            result.IsHandled = true;
        }
    }
}
