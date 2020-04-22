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
            var container = new BootstrapContainer(this);
            new Html(container) { TextHtml = "<h1>Cms <i class='fas fa-pencil-alt'></i></h1>" };
            new Html(container) { TextHtml = "Content management v0.2" };
        }

        public override async Task InitAsync()
        {
            await new GridCmsComponent(this).LoadAsync();
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
        }

        protected override void LookupRowSelected(LookupRowSelectedArgs args, LookupRowSelectedResult result)
        {
            if (args.RowSelected is CmsComponentType row)
            {
                result.Text = row.Name;
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
        }
    }
}
