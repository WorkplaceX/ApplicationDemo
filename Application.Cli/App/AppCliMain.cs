namespace Application.Cli
{
    using Database.Demo; // Framework and Application contain same namespace.
    using DatabaseBuiltIn.Demo;
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
                FolderNameServer = "Application.Server/Framework/Website/Default/",
                DomainNameList = new List<string>(new string[] { "localhost" }),
                FolderNameNpmBuild = "Website/Default/",
                FolderNameDist = "Website/Default/dist/",
            });
        }

        /// <summary>
        /// Cli Generate.
        /// </summary>
        protected override void CommandGenerateBuiltIn(List<GenerateBuiltInItem> list)
        {
            // LanguageBuiltIn
            var languageList = Data.Select(Data.Query<LanguageBuiltIn>());
            list.Add(GenerateBuiltInItem.Create(languageList, true));

            // NavigationBuiltIn
            var navigationList = Data.Select(Data.Query<NavigationBuiltIn>());
            list.Add(GenerateBuiltInItem.Create(navigationList));
        }

        /// <summary>
        /// Returns true if a next level exists.
        /// </summary>
        /// <param name="rowAllList">All rows.</param>
        /// <param name="rowLevelList">Rows of current level.</param>
        /// <returns>Returns rows of next level.</returns>
        private static bool NavigationBuiltInLevel(List<NavigationBuiltIn> rowAllList, ref List<NavigationBuiltIn> rowLevelList)
        {
            if (rowLevelList == null)
            {
                rowLevelList = rowAllList.Where(row => row.ParentId == null).ToList();
            }
            else
            {
                var idList = rowLevelList.Select(row => (int?)row.Id).ToList();
                rowLevelList = rowAllList.Where(row => idList.Contains(row.ParentId)).ToList();
            }
            return rowLevelList.Count() != 0;
        }

        /// <summary>
        /// Cli Deploy.
        /// </summary>
        protected override void CommandDeployDbBuiltIn(List<DeployDbBuiltInItem> list)
        {
            list.Add(DeployDbBuiltInItem.Create(LanguageBuiltInTableApplication.RowList, nameof(LanguageBuiltIn.Name), null));

            var rowList = NavigationBuiltInTableApplicationCli.RowList;

            List<NavigationBuiltIn> rowLevelList = null;
            while (NavigationBuiltInLevel(rowList, ref rowLevelList)) // Step through all levels.
            {
                list.Add(DeployDbBuiltInItem.Create(rowLevelList, nameof(NavigationBuiltIn.Name), null));
            }
        }
    }

    /// <summary>
    /// Custom cli command.
    /// </summary>
    public class MyCommand : CommandBase
    {
        public MyCommand(AppCli appCli) 
            : base(appCli, "my", "My custom cli command")
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
