namespace BuildTool
{
    using System.Collections.Generic;
    using Framework.BuildTool;

    /// <summary>
    /// Overwrite method RegisterCommand(); to add additional custom commands.
    /// </summary>
    public class AppBuildToolDemo : AppBuildTool
    {
        protected override void RegisterCommand(List<Command> commandList)
        {
            base.RegisterCommand(commandList);
        }
    }
}
