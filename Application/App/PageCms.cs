namespace Application
{
    using Database.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using Framework.Json.Bootstrap;
    using System;
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
            await Data.UpdateAsync(Data.RowCopy<CmsComponent>(args.RowNew));

            result.IsHandled = true;
        }

        protected override async Task InsertAsync(InsertArgs args, InsertResult result)
        {
            args.RowNew.Name = Guid.NewGuid();

            var row = Data.RowCopy<CmsComponent>(args.RowNew);
            await Data.InsertAsync(row);
            args.RowNew.Id = row.Id;

            result.IsHandled = true;
        }
    }
}
