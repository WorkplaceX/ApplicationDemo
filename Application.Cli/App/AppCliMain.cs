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
            configCli.EnvironmentGet().WebsiteList.Add(new ConfigCliWebsite()
            {
                AppTypeName = typeof(AppMain).FullName + ", " + typeof(AppMain).Namespace,
                FolderNameServer = "Application.Server/Framework/Application.Website/Default/",
                DomainNameList = new List<string>(new string[] { "localhost" }),
                FolderNameNpmBuild = "Application.Website/Default/",
                FolderNameDist = "Application.Website/Default/dist/",
            });
        }

        /// <summary>
        /// Cli Generate.
        /// </summary>
        protected override void CommandGenerateBuiltIn(List<GenerateBuiltInItem> list)
        {
            // Language
            var languageList = Data.Select(Data.Query<LanguageBuiltIn>());
            list.Add(GenerateBuiltInItem.Create(languageList, isApplication: true));

            // Navigation
            var navigationList = Data.Select(Data.Query<NavigationBuiltIn>());
            list.Add(GenerateBuiltInItem.Create(navigationList));

            // LoginPermission
            var loginPermissionList = Data.Select(Data.Query<LoginPermissionBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist));
            list.Add(GenerateBuiltInItem.Create(loginPermissionList, isApplication: true));
            loginPermissionList = Data.Select(Data.Query<LoginPermissionBuiltIn>().Where(item => item.IsBuiltIn == false && item.IsExist));
            list.Add(GenerateBuiltInItem.Create(loginPermissionList, isApplication: false));

            // LoginRole
            var loginRoleList = Data.Select(Data.Query<LoginRoleBuiltIn>());
            list.Add(GenerateBuiltInItem.Create(loginRoleList));

            // LoginRolePermission
            var loginRolePermissionList = Data.Select(Data.Query<LoginRolePermissionBuiltIn>());
            list.Add(GenerateBuiltInItem.Create(loginRolePermissionList));

            // LoginUser
            var loginUserList = Data.Select(Data.Query<LoginUserBuiltIn>());
            list.Add(GenerateBuiltInItem.Create(loginUserList));

            // LoginUserRole
            var loginUserRoleList = Data.Select(Data.Query<LoginUserRoleBuiltIn>());
            list.Add(GenerateBuiltInItem.Create(loginUserRoleList));
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

            // LoginPermission
            list.Add(DeployDbBuiltInItem.Create(LoginPermissionBuiltInTableApplication.RowList, nameof(LoginPermissionBuiltIn.Name), "Login"));
            list.Add(DeployDbBuiltInItem.Create(LoginPermissionBuiltInTableApplicationCli.RowList, nameof(LoginPermissionBuiltIn.Name), "Login"));

            // LoginRole
            list.Add(DeployDbBuiltInItem.Create(LoginRoleBuiltInTableApplicationCli.RowList, nameof(LoginRoleBuiltIn.Name), "Login"));

            // LoginRolePermission
            list.Add(DeployDbBuiltInItem.Create(LoginRolePermissionBuiltInTableApplicationCli.RowList, new string[] { nameof(LoginRolePermissionBuiltIn.RoleId), nameof(LoginRolePermissionBuiltIn.PermissionId) }, "Login"));

            // LoginUser
            list.Add(DeployDbBuiltInItem.Create(LoginUserBuiltInTableApplicationCli.RowList, nameof(LoginUserBuiltIn.Name), "Login"));

            // LoginUserRole
            list.Add(DeployDbBuiltInItem.Create(LoginUserRoleBuiltInTableApplicationCli.RowList, new string[] { nameof(LoginUserRoleBuiltIn.UserId), nameof(LoginUserRoleBuiltIn.RoleId) }, "Login"));
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
