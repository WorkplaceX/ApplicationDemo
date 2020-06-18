namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageCms : Page
    {
        public PageCms(ComponentJson owner) : base(owner) 
        {
        }

        public override async Task InitAsync()
        {
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>Cms <i class='fas fa-pencil-alt'></i></h1>" };
            new Html(container) { TextHtml = "Content management v0.2" };
            var grid = new GridCmsComponent(this);
            var html = new Html(this);
            grid.Html = html;
            await grid.LoadAsync();
        }
    }

    public class GridCmsComponent : Grid<CmsComponentDisplay>
    {
        public GridCmsComponent(ComponentJson owner) : base(owner) { }

        protected override async Task UpdateAsync(UpdateArgs args, UpdateResult result)
        {
            await Data.UpdateAsync(Data.RowCopy<CmsComponent>(args.Row));

            result.IsHandled = true;
        }

        protected override async Task InsertAsync(InsertArgs args, InsertResult result)
        {
            args.Row.Name = Guid.NewGuid();
            args.Row.IsExist = true;

            var row = Data.RowCopy<CmsComponent>(args.Row);
            await Data.InsertAsync(row);
            args.Row.Id = row.Id;

            result.IsHandled = true;
        }

        protected override void LookupQuery(LookupQueryArgs args, LookupQueryResult result)
        {
            if (args.FieldName == nameof(args.Row.ComponentTypeText))
            {
                result.Query = Data.Query<CmsComponentType>();
            }
            if (args.FieldName == nameof(args.Row.PageImageFileName))
            {
                result.Query = Data.Query<CmsFile>();
            }
            if (args.FieldName == nameof(args.Row.ImageFileName))
            {
                result.Query = Data.Query<CmsFile>();
            }
            if (args.FieldName == nameof(args.Row.CodeBlockTypeText))
            {
                result.Query = Data.Query<CmsCodeBlockType>();
            }
        }

        protected override void LookupRowSelected(LookupRowSelectedArgs args, LookupRowSelectedResult result)
        {
            if (args.RowSelected is CmsComponentType componentType)
            {
                result.Text = componentType.Name;
            }
            if (args.RowSelected is CmsFile cmsFile)
            {
                result.Text = cmsFile.FileName;
            }
            if (args.RowSelected is CmsCodeBlockType codeBlockType)
            {
                result.Text = codeBlockType.Name;
            }
        }

        protected override async Task CellParseAsync(ParseArgs args, ParseResult result)
        {
            if (args.FieldName == nameof(args.Row.ComponentTypeText))
            {
                var row = (await Data.Query<CmsComponentType>().Where(item => item.Name == args.Text).QueryExecuteAsync()).FirstOrDefault();
                if (row != null)
                {
                    result.Row.ComponentTypeId = row.Id;
                    result.Row.ComponentTypeText = row.Name;
                }
                else
                {
                    result.ErrorParse = "Component not found!";
                }
                result.IsHandled = true;
            }

            if (args.FieldName == nameof(args.Row.PageImageFileName))
            {
                var row = (await Data.Query<CmsFile>().Where(item => item.FileName == args.Text).QueryExecuteAsync()).FirstOrDefault();
                if (row != null)
                {
                    result.Row.PageImageFileId = row.Id;
                    result.Row.PageImageFileName = row.FileName;
                }
                else
                {
                    result.ErrorParse = "File not found!";
                    result.IsHandled = true;
                }
            }

            if (args.FieldName == nameof(args.Row.ImageFileName))
            {
                var row = (await Data.Query<CmsFile>().Where(item => item.FileName == args.Text).QueryExecuteAsync()).FirstOrDefault();
                if (row != null)
                {
                    result.Row.ImageFileId = row.Id;
                    result.Row.ImageFileName = row.FileName;
                }
                else
                {
                    result.ErrorParse = "File not found!";
                    result.IsHandled = true;
                }
            }

            if (args.FieldName == nameof(args.Row.CodeBlockTypeText))
            {
                var row = (await Data.Query<CmsCodeBlockType>().Where(item => item.Name == args.Text).QueryExecuteAsync()).FirstOrDefault();
                if (row != null)
                {
                    result.Row.CodeBlockTypeId = row.Id;
                    result.Row.CodeBlockTypeText = row.Name;
                }
                else
                {
                    result.ErrorParse = "CodeBlockType not found!";
                    result.IsHandled = true;
                }
            }
        }

        public Html Html;

        protected override async Task RowSelectedAsync()
        {
            if (Html != null)
            {
                var componentList = await Data.Query<CmsComponentDisplay>().Where(item => item.Id == RowSelected.Id || item.ParentId == RowSelected.Id).QueryExecuteAsync();
                string textHtml = UtilCms.ComponentToTextHtml(componentList.SingleOrDefault(item => item.Id == RowSelected.Id), componentList);
                string textMd = UtilCms.ComponentToTextMd(componentList.SingleOrDefault(item => item.Id == RowSelected.Id), componentList).Replace("\r\n", "<br/>");
                UtilCms.ComponentFromTextMd(textMd);
                Html.TextHtml = textHtml;
                Html.IsNoSanatize = true;
            }
        }
    }
}
