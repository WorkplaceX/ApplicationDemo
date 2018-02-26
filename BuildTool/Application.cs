namespace BuildTool
{
    using System.Collections.Generic;
    using Framework.BuildTool;
    using Framework.Application;
    using Application;
    using Database.dbo;
    using Framework;

    /// <summary>
    /// Override method RegisterCommand(); to add additional custom commands.
    /// </summary>
    public class AppBuildToolDemo : AppBuildTool
    {
        public AppBuildToolDemo(App app) 
            : base(app)
        {

        }

        protected override void DbFrameworkApplicationView(List<FrameworkApplicationView> result)
        {
            result.Add(new FrameworkApplicationView() { Text = "Demo Application", Path = "demo", TypeName = UtilFramework.TypeToName(typeof(AppDemo)), IsActive = true });
        }
    }
}
