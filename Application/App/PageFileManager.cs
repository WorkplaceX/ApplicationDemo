﻿namespace Application
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

        protected override void CellAnnotationRow(AnnotationArgs args, AnnotationResult result)
        {
            if (args.FieldName == nameof(StorageFile.Data))
            {
                result.IsFileUpload = true;
                result.Html = string.Format("<a href='{0}'>{1}</a>", args.Row.FileName, args.Row.FileName);
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
    }
}
