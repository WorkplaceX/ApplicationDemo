namespace BuildTool
{
    using System.Collections.Generic;
    using Framework.BuildTool;
    using System.Linq;

    public class AppBuildToolDemo : AppBuildTool
    {
        protected override void RegisterCommand(List<Command> commandList)
        {
            commandList.Remove(commandList.OfType<CommandRunSql>().Single());
            commandList.Add(new CommandRunSqlDemo());
        }
    }

    public class CommandRunSqlDemo : CommandRunSql
    {
        public override void Run()
        {
            if (Framework.UtilFramework.IsLinux == false)
            {
                BuildTool.Airport.Script.Run();
            }
            base.Run();
        }
    }
}
