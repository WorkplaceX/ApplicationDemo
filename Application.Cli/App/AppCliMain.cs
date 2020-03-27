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
            var languageList = Data.Select(Data.Query<LanguageBuiltIn>().OrderBy(item => item.IdName));
            list.Add(GenerateBuiltInItem.Create(languageList, isApplication: true));

            // Navigation
            var navigationList = Data.Select(Data.Query<NavigationBuiltIn>().OrderBy(item => item.IdName));
            list.Add(GenerateBuiltInItem.Create(navigationList));

            // LoginPermission
            var loginPermissionList = Data.Select(Data.Query<LoginPermissionBuiltIn>().Where(item => item.IsBuiltIn == true && item.IsExist).OrderBy(item => item.IdName));
            list.Add(GenerateBuiltInItem.Create(loginPermissionList, isApplication: true));
            loginPermissionList = Data.Select(Data.Query<LoginPermissionBuiltIn>().Where(item => item.IsBuiltIn == false && item.IsExist).OrderBy(item => item.IdName));
            list.Add(GenerateBuiltInItem.Create(loginPermissionList, isApplication: false));

            // LoginRole
            var loginRoleList = Data.Select(Data.Query<LoginRoleBuiltIn>().OrderBy(item => item.IdName));
            list.Add(GenerateBuiltInItem.Create(loginRoleList));

            // LoginRolePermission
            var loginRolePermissionList = Data.Select(Data.Query<LoginRolePermissionBuiltIn>().OrderBy(item => item.RoleIdName).ThenBy(item => item.PermissionIdName));
            list.Add(GenerateBuiltInItem.Create(loginRolePermissionList));

            // LoginUser
            var loginUserList = Data.Select(Data.Query<LoginUserBuiltIn>().OrderBy(item => item.IdName));
            list.Add(GenerateBuiltInItem.Create(loginUserList));

            // LoginUserRole
            var loginUserRoleList = Data.Select(Data.Query<LoginUserRoleBuiltIn>().OrderBy(item => item.UserIdName).ThenBy(item => item.RoleIdName));
            list.Add(GenerateBuiltInItem.Create(loginUserRoleList));

            // Roadmap
            var roadmapCategoryList = Data.Select(Data.Query<RoadmapCategoryBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName)); // Category
            list.Add(GenerateBuiltInItem.Create(roadmapCategoryList, isApplication: true));
            var roadmapModuleList = Data.Select(Data.Query<RoadmapModuleBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName)); // Module
            list.Add(GenerateBuiltInItem.Create(roadmapModuleList, isApplication: true));
            var roadmapPriorityList = Data.Select(Data.Query<RoadmapPriorityBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName)); // Priority
            list.Add(GenerateBuiltInItem.Create(roadmapPriorityList, isApplication: true));
            var roadmapStateList = Data.Select(Data.Query<RoadmapStateBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName)); // State
            list.Add(GenerateBuiltInItem.Create(roadmapStateList, isApplication: true));
            var roadmapList = Data.Select(Data.Query<RoadmapBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.Name)); // Roadmap
            list.Add(GenerateBuiltInItem.Create(roadmapList));

            // FileManager
            var fileList = Data.Select(Data.Query<File>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.FileName));
            list.Add(GenerateBuiltInItem.Create(fileList));

            // Cms
            var componentTypeList = Data.Select(Data.Query<CmsCodeBlockTypeBuiltIn>().OrderBy(item => item.Sort));
            list.Add(GenerateBuiltInItem.Create(componentTypeList));
            var codeBlockTypeList = Data.Select(Data.Query<CmsComponentTypeBuiltIn>().OrderBy(item => item.Sort));
            list.Add(GenerateBuiltInItem.Create(codeBlockTypeList));
            var textTypeList = Data.Select(Data.Query<CmsTextType>().OrderBy(item => item.Sort));
            list.Add(GenerateBuiltInItem.Create(textTypeList));
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
        protected override void CommandDeployDbBuiltIn(DeployDbBuiltInResult result)
        {
            result.Add(LanguageBuiltInTableApplication.RowList, nameof(LanguageBuiltIn.Name), null);

            var rowList = NavigationBuiltInTableApplicationCli.RowList;

            List<NavigationBuiltIn> rowLevelList = null;
            while (NavigationBuiltInLevel(rowList, ref rowLevelList)) // Step through all levels.
            {
                result.Add(rowLevelList, nameof(NavigationBuiltIn.Name), null);
            }

            // LoginPermission
            result.Add(LoginPermissionBuiltInTableApplication.RowList, nameof(LoginPermissionBuiltIn.Name), "Login");
            result.Add(LoginPermissionBuiltInTableApplicationCli.RowList, nameof(LoginPermissionBuiltIn.Name), "Login");

            // LoginRole
            result.Add(LoginRoleBuiltInTableApplicationCli.RowList, nameof(LoginRoleBuiltIn.Name), "Login");

            // LoginRolePermission
            result.Add(LoginRolePermissionBuiltInTableApplicationCli.RowList, new string[] { nameof(LoginRolePermissionBuiltIn.RoleId), nameof(LoginRolePermissionBuiltIn.PermissionId) }, "Login");

            // LoginUser
            result.Add(LoginUserBuiltInTableApplicationCli.RowList, nameof(LoginUserBuiltIn.Name), "Login");

            // LoginUserRole
            result.Add(LoginUserRoleBuiltInTableApplicationCli.RowList, new string[] { nameof(LoginUserRoleBuiltIn.UserId), nameof(LoginUserRoleBuiltIn.RoleId) }, "Login");

            // Roadmap
            result.Add(RoadmapCategoryBuiltInTableApplication.RowList, nameof(RoadmapCategoryBuiltIn.Name)); // Category
            result.Add(RoadmapModuleBuiltInTableApplication.RowList, nameof(RoadmapModuleBuiltIn.Name)); // Module
            result.Add(RoadmapPriorityBuiltInTableApplication.RowList, nameof(RoadmapPriorityBuiltIn.Name)); // Priority
            result.Add(RoadmapStateBuiltInTableApplication.RowList, nameof(RoadmapStateBuiltIn.Name)); // State
            result.Add(RoadmapBuiltInTableApplicationCli.RowList, nameof(RoadmapBuiltIn.Name)); // Roadmap

            // FileManager
            result.Add(FileTableApplicationCli.RowList, nameof(File.FileName));

            // Cms
            result.Add(CmsComponentTypeBuiltInTableApplicationCli.RowList, nameof(CmsComponentTypeBuiltIn.Name));
            result.Add(CmsCodeBlockTypeBuiltInTableApplicationCli.RowList, nameof(CmsCodeBlockTypeBuiltIn.Name));
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
