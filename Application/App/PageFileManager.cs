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

        protected override void CellAnnotation(File row, string fieldName, CellAnnotationResult result)
        {
            if (fieldName == nameof(File.Description))
            {
                result.IsFileUpload = true;
            }
        }

        protected override void CellParseFileUpload(File row, string fieldName, string fileName, byte[] data, CellParseResult result)
        {
            row.Data = data;
            result.IsHandled = true;
        }
    }
}
