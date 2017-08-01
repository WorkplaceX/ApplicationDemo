namespace BuildTool
{
    using System.Collections.Generic;
    using Framework.BuildTool;
    using Framework.Application;
    using Application;
    using Database.dbo;
    using Framework;

    /// <summary>
    /// Overwrite method RegisterCommand(); to add additional custom commands.
    /// </summary>
    public class AppBuildToolDemo : AppBuildTool
    {
        public AppBuildToolDemo(App app) 
            : base(app)
        {

        }

        protected override void DbFrameworkApplicationView(List<FrameworkApplicationView> result)
        {
            result.Add(new FrameworkApplicationView() { Name = "Demo", Path = "demo", Type = UtilFramework.TypeToName(typeof(AppDemo)), IsActive = true });
        }
    }
}
