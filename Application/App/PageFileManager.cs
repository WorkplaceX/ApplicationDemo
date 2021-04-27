namespace Application.Demo
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageFileManager : Page
    {
        public PageFileManager(ComponentJson owner) : base(owner) 
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>File Manager <i class='far fa-file'></i></h1>" };
            this.GridFile = new GridFile(container);
        }

        public override async Task InitAsync()
        {
            await GridFile.LoadAsync();
        }

        public GridFile GridFile;
    }

    public class GridFile : Grid<StorageFile>
    {
        public GridFile(ComponentJson owner) : base(owner) { }

        protected override void CellAnnotation(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(StorageFile.Data))
            {
                result.IsFileUpload = true;
                result.Html = string.Format("<a href='{0}'>{1}</a>", args.Row.FileName, args.Row.FileName);
            }
        }

        protected override void CellAnnotationFilterNew(AnnotationFilterNewArgs args, AnnotationResult result)
        {
            if (args.IsNew && args.FieldName == nameof(args.Row.Data))
            {
                // Show upload icon for new record.
                result.IsFileUpload = true;
            }
        }

        protected override void Truncate(TruncateArgs args)
        {
            // Truncate big data from server session state.
            args.Row.Data = null;
        }

        private void FileUpload(StorageFile row, FileUploadArgs fileUpload)
        {
            if (fileUpload?.FieldName == nameof(StorageFile.Data))
            {
                row.Data = fileUpload.Data;
                row.FileName = fileUpload.FileName;
            }
        }

        protected override async Task UpdateAsync(UpdateArgs args, UpdateResult result)
        {
            if (args.Row.Data == null)
            {
                // Load truncated data back in before record update.
                args.Row.Data = (await Data.Query<StorageFile>().Where(item => item.Id == args.Row.Id).QueryExecuteAsync()).Single().Data;
            }

            FileUpload(args.Row, args.FileUpload);
            await Data.UpdateAsync(args.Row);
            result.IsHandled = true;
        }

        protected override async Task InsertAsync(InsertArgs args, InsertResult result)
        {
            FileUpload(args.Row, args.FileUpload);
            await Data.InsertAsync(args.Row);
            result.IsHandled = true;
        }
    }
}
