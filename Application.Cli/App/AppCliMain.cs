namespace Application.Cli
{
    extern alias Application;

    using Application::Database.Demo; // Framework and Application contain same namespace.
    using Framework.Cli.Command;
    using Framework.Cli.Config;
    using Framework.DataAccessLayer;
    using Microsoft.Extensions.CommandLineUtils;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Command line interface application.
    /// </summary>
    public class AppCliMain : AppCli
    {
        public AppCliMain() : 
            base(
                typeof(CountryDisplayCache).Assembly, // Register Application.Database dll
                typeof(AppMain).Assembly) // Register Application dll
        {

        }

        protected override void RegisterCommand()
        {
            new MyCommand(this);

            base.RegisterCommand();
        }

        /// <summary>
        /// Set default values if file ConfigCli.json does not exist.
        /// </summary>
        protected override void InitConfigCli(ConfigCli configCli)
        {
            configCli.WebsiteList.Add(new ConfigCliWebsite()
            {
                AppTypeName = typeof(AppMain).FullName + ", " + typeof(AppMain).Namespace,
                FolderNameServer = "Default",
                DomainNameList = new List<string>(),
                FolderNameNpmBuild = "WebsiteDefault/",
                FolderNameDist = "WebsiteDefault/dist/",
            });
        }

        protected override List<GenerateBuiltInItem> GenerateBuiltInList()
        {
            var result = base.GenerateBuiltInList();
            var rowList = Data.Query<Language>().ToList().Cast<Row>().ToList();
            result.Add(new GenerateBuiltInItem(false, typeof(Language), rowList));
            return result;
        }

        protected override List<DeployDbBuiltInItem> DeployDbBuiltInList()
        {
            var result = base.DeployDbBuiltInList();
            // var rowList = Data.Query<My>().Cast<Row>().ToList();
            // result.Add(new DeployDbBuiltInItem(rowList, new string[] { nameof(My.I2d) }, null));
            return result;
        }
    }

    public class MyCommand : CommandBase
    {
        public MyCommand(AppCli appCli) 
            : base(appCli, "My", "My command")
        {

        }

        public CommandOption My;

        protected override void Register(CommandLineApplication configuration)
        {
            this.My = configuration.Option("-m", "My Option", CommandOptionType.NoValue);
        }

        protected override void Execute()
        {
            Console.WriteLine("My");
            if (My.Value() == "on")
            {
                Console.WriteLine("With option");
            }
        }
    }
}
