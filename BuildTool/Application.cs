namespace BuildTool
{
    using System.Collections.Generic;
    using Framework.BuildTool;
    using Framework.Application;
    using Application;

    /// <summary>
    /// Overwrite method RegisterCommand(); to add additional custom commands.
    /// </summary>
    public class AppBuildToolDemo : AppBuildTool
    {
        public AppBuildToolDemo(App app) 
            : base(app)
        {

        }

        protected override void RegisterCommand(List<Command> commandList)
        {
            base.RegisterCommand(commandList);
        }
    }
}
