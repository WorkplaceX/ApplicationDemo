namespace Application
{
    using Database.Demo;
    using DatabaseIntegrate.Demo;
    using Framework.DataAccessLayer;
    using Framework.Json;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageRoadmap : Page
    {
        public PageRoadmap(ComponentJson owner) : base(owner) 
        {
            new Html(this) { TextHtml = "<h1>Roadmap</h1>" };
            new Html(this) { TextHtml = "This is the development roadmap showing the status of new features and reported bugs." };
        }

        public override async Task InitAsync()
        {
            await new GridRoadmap(this).LoadAsync();
        }
    }

    public class GridRoadmap : Grid<RoadmapDisplay>
    {
        public GridRoadmap(ComponentJson owner) : base(owner) { }

        protected override async Task InsertAsync(InsertArgs args, InsertResult result)
        {
            Roadmap roadmap = new Roadmap();
            Data.RowCopy(args.Row, roadmap);
            roadmap.Name = Guid.NewGuid();
            await Data.InsertAsync(roadmap);
            args.Row.Id = roadmap.Id; // Get new id from db
            args.Row.Name = roadmap.Name;
            args.Row.IsExist = true;
            args.Row.Date = DateTime.Today;
            result.IsHandled = true;
        }

        protected override async Task UpdateAsync(UpdateArgs args, UpdateResult result)
        {
            Roadmap roadmapNew = new Roadmap();
            Data.RowCopy(args.Row, roadmapNew);
            await Data.UpdateAsync(roadmapNew);
            result.IsHandled = true;
        }

        protected override void LookupQuery(LookupQueryArgs args, LookupQueryResult result)
        {
            if (args.FieldName == nameof(RoadmapDisplay.RoadmapCategoryText))
            {
                result.Query = Data.Query<RoadmapCategory>();
            }
            if (args.FieldName == nameof(RoadmapDisplay.RoadmapModuleText))
            {
                result.Query = Data.Query<RoadmapModule>();
            }
            if (args.FieldName == nameof(RoadmapDisplay.RoadmapPriorityText))
            {
                result.Query = Data.Query<RoadmapPriority>();
            }
            if (args.FieldName == nameof(RoadmapDisplay.RoadmapStateText))
            {
                result.Query = Data.Query<RoadmapState>();
            }
            if (args.FieldName == nameof(RoadmapDisplay.LoginUserText))
            {
                result.Query = Data.Query<LoginUser>();
            }
        }

        protected override void LookupRowSelected(LookupRowSelectedArgs args, LookupRowSelectedResult result)
        {
            if (args.RowSelected is RoadmapCategory roadmapCategory) // Category
            {
                result.Text = roadmapCategory.Text;
            }
            if (args.RowSelected is RoadmapModule roadmapModule) // Module
            {
                result.Text = roadmapModule.Text;
            }
            if (args.RowSelected is RoadmapPriority roadmapPriority) // Priority
            {
                result.Text = roadmapPriority.Text;
            }
            if (args.RowSelected is RoadmapState roadmapState) // State
            {
                result.Text = roadmapState.Text;
            }
            if (args.RowSelected is LoginUser loginUser) // User
            {
                result.Text = loginUser.Name;
            }
        }

        protected override async Task CellParseAsync(ParseArgs args, ParseResult result)
        {
            // Category
            if (args.FieldName == nameof(RoadmapDisplay.RoadmapCategoryText))
            {
                if (args.Text == "")
                {
                    args.Row.RoadmapCategoryId = null;
                    args.Row.RoadmapCategoryIdName = null;
                    args.Row.RoadmapCategoryText = "";
                }
                else
                {
                    var roadmapCategory = (await Data.Query<RoadmapCategory>().Where(item => item.Text == args.Text).QueryExecuteAsync()).FirstOrDefault();
                    if (roadmapCategory == null)
                    {
                        result.ErrorParse = "Category not found!";
                    }
                    else
                    {
                        args.Row.RoadmapCategoryId = roadmapCategory.Id;
                        args.Row.RoadmapCategoryIdName = roadmapCategory.Name;
                        args.Row.RoadmapCategoryText = roadmapCategory.Text;
                    }
                }
                result.IsHandled = true;
            }

            // Module
            if (args.FieldName == nameof(RoadmapDisplay.RoadmapModuleText))
            {
                if (args.Text == "")
                {
                    args.Row.RoadmapModuleId = null;
                    args.Row.RoadmapModuleIdName = null;
                    args.Row.RoadmapModuleText = "";
                }
                else
                {
                    var roadmapModule = (await Data.Query<RoadmapModule>().Where(item => item.Text == args.Text).QueryExecuteAsync()).FirstOrDefault();
                    if (roadmapModule == null)
                    {
                        result.ErrorParse = "Module not found!";
                    }
                    else
                    {
                        args.Row.RoadmapModuleId = roadmapModule.Id;
                        args.Row.RoadmapModuleIdName = roadmapModule.Name;
                        args.Row.RoadmapModuleText = roadmapModule.Text;
                    }
                }
                result.IsHandled = true;
            }

            // Priority
            if (args.FieldName == nameof(RoadmapDisplay.RoadmapPriorityText))
            {
                if (args.Text == "")
                {
                    args.Row.RoadmapPriorityId = null;
                    args.Row.RoadmapPriorityIdName = null;
                    args.Row.RoadmapPriorityText = "";
                }
                else
                {
                    var roadmapPriority = (await Data.Query<RoadmapPriority>().Where(item => item.Text == args.Text).QueryExecuteAsync()).FirstOrDefault();
                    if (roadmapPriority == null)
                    {
                        result.ErrorParse = "Priority not found!";
                    }
                    else
                    {
                        args.Row.RoadmapPriorityId = roadmapPriority.Id;
                        args.Row.RoadmapPriorityIdName = roadmapPriority.Name;
                        args.Row.RoadmapPriorityText = roadmapPriority.Text;
                    }
                }
                result.IsHandled = true;
            }

            // State
            if (args.FieldName == nameof(RoadmapDisplay.RoadmapStateText))
            {
                if (args.Text == "")
                {
                    args.Row.RoadmapStateId = null;
                    args.Row.RoadmapStateIdName = null;
                    args.Row.RoadmapStateText = "";
                }
                else
                {
                    var roadmapState = (await Data.Query<RoadmapState>().Where(item => item.Text == args.Text).QueryExecuteAsync()).FirstOrDefault();
                    if (roadmapState == null)
                    {
                        result.ErrorParse = "State not found!";
                    }
                    else
                    {
                        args.Row.RoadmapStateId = roadmapState.Id;
                        args.Row.RoadmapStateIdName = roadmapState.Name;
                        args.Row.RoadmapStateText = roadmapState.Text;
                    }
                }
                result.IsHandled = true;
            }

            // User
            if (args.FieldName == nameof(RoadmapDisplay.LoginUserText))
            {
                if (args.Text == "")
                {
                    args.Row.LoginUserId = null;
                    args.Row.LoginUserText = "";
                }
                else
                {
                    var loginUser = (await Data.Query<LoginUser>().Where(item => item.Name == args.Text).QueryExecuteAsync()).FirstOrDefault();
                    if (loginUser == null)
                    {
                        result.ErrorParse = "User name not found!";
                    }
                    else
                    {
                        args.Row.LoginUserId = loginUser.Id;
                        args.Row.LoginUserText = loginUser.Name; // Without database row reload!
                    }
                }
                result.IsHandled = true;
            }

            // Number
            if (args.FieldName == nameof(RoadmapDisplay.Number))
            {
                string text = args.Text.Replace("#", "");
                result.Row.Number = int.Parse(text);
                result.IsHandled = true;
            }
        }

        protected override void CellAnnotationRow(AnnotationArgs args, AnnotationResult result)
        {
            // Priority
            if (args.FieldName == nameof(args.Row.RoadmapPriorityText))
            {
                var idEnum = RoadmapPriorityIntegrateApplication.IdName(args.Row.RoadmapPriorityIdName);
                string bootstrapColor = null;
                switch (idEnum)
                {
                    case RoadmapPriorityIntegrateApplication.IdNameEnum.Low:
                        bootstrapColor = "text-success"; // Green
                        break;
                    case RoadmapPriorityIntegrateApplication.IdNameEnum.Medium:
                        bootstrapColor = "text-primary"; // Blue
                        break;
                    case RoadmapPriorityIntegrateApplication.IdNameEnum.High:
                        bootstrapColor = "text-warning"; // Orange
                        break;
                    case RoadmapPriorityIntegrateApplication.IdNameEnum.Critical:
                        bootstrapColor = "text-danger"; // Red
                        break;
                }
                result.HtmlLeft = $"<i class='fas fa-circle {bootstrapColor}'></i>";
            }

            // Category
            if (args.FieldName == nameof(args.Row.RoadmapCategoryText))
            {
                var idEnum = RoadmapCategoryIntegrateApplication.IdName(args.Row.RoadmapCategoryIdName);
                switch (idEnum)
                {
                    case RoadmapCategoryIntegrateApplication.IdNameEnum.Feature:
                        result.HtmlLeft = "<i class='fas fa-box-open text-primary'></i>"; // Blue
                        break;
                    case RoadmapCategoryIntegrateApplication.IdNameEnum.Bug:
                        result.HtmlLeft = "<i class='fas fa-bug text-danger'></i>"; // Red
                        break;
                    case RoadmapCategoryIntegrateApplication.IdNameEnum.Analyze:
                        result.HtmlLeft = "<i class='fas fa-vial text-info'></i>"; // Green
                        break;
                }
            }

            // State
            if (args.FieldName == nameof(args.Row.RoadmapStateText))
            {
                var idEnum = RoadmapStateIntegrateApplication.IdName(args.Row.RoadmapStateIdName);
                if (idEnum == RoadmapStateIntegrateApplication.IdNameEnum.Done)
                {
                    result.HtmlRight = "<i class='fas fa-check' text-success></i>"; // Green
                }
            }
            if (args.FieldName == nameof(args.Row.Description))
            {
                var idEnum = RoadmapStateIntegrateApplication.IdName(args.Row.RoadmapStateIdName);
                if (idEnum == RoadmapStateIntegrateApplication.IdNameEnum.Done)
                {
                    result.HtmlRight = "<i class='fas fa-check' text-success></i>"; // Green
                }
            }

            // Module
            if (args.FieldName == nameof(args.Row.RoadmapModuleText))
            {
                var idEnum = RoadmapModuleIntegrateApplication.IdName(args.Row.RoadmapModuleIdName);
                if (idEnum == RoadmapModuleIntegrateApplication.IdNameEnum.Framework)
                {
                    result.HtmlLeft = "<i class='fas fa-microchip text-primary'></i>"; // Blue
                }
                else
                {
                    result.HtmlLeft = "<i class='fas fa-desktop text-info'></i>"; // Green
                }
            }

            // Number
            if (args.FieldName == nameof(args.Row.Number))
            {
                result.Align = CellAnnotationAlignEnum.Right;
            }
        }

        protected override void CellText(CellTextArgs args, CellTextResult result)
        {
            if (args.FieldName == nameof(args.Row.Number))
            {
                result.Text = "#" + args.Row.Number.ToString();
            }
        }
    }
}
