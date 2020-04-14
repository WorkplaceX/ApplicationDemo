namespace Application
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

    public class GridFile : Grid<File>
    {
        public GridFile(ComponentJson owner) : base(owner) { }

        protected override void CellAnnotation(CellAnnotationArgs args, CellAnnotationResult result)
        {
            if (args.FieldName == nameof(File.Data))
            {
                result.IsFileUpload = true;
                result.Html = string.Format("<a href='{0}'>{1}</a>", args.Row.FileName, args.Row.FileName);
            }
        }

        protected override void CellParseFileUpload(CellParseFileUploadArgs args, CellParseResult result)
        {
            if (args.FieldName == nameof(File.Data))
            {
                args.Row.Data = args.Data;
                args.Row.FileName = args.FileName;
                result.IsHandled = true;
            }
        }
    }
}
