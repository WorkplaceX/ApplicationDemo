namespace Application.Cli
{
    using Database.Demo; // Framework and Application contain same namespace.
    using DatabaseBuiltIn.Demo;
    using Framework.Cli;
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

        /// <summary>
        /// Set default values if file ConfigCli.json does not exist.
        /// </summary>
        protected override void InitConfigCli(ConfigCli configCli)
        {
            string appTypeName = typeof(AppMain).FullName + ", " + typeof(AppMain).Namespace;
            configCli.WebsiteList.Add(new ConfigCliWebsite()
            {
                DomainNameList = new List<ConfigCliWebsiteDomain>(new ConfigCliWebsiteDomain[] { new ConfigCliWebsiteDomain { EnvironmentName = "DEV", DomainName = "localhost", AppTypeName = appTypeName } }),
                FolderNameNpmBuild = "Application.Website/MasterDefault/",
                FolderNameDist = "Application.Website/MasterDefault/dist/",
            });
        }

        /// <summary>
        /// Cli Generate.
        /// </summary>
        protected override void CommandGenerateBuiltIn(GenerateBuiltInResult result)
        {
            // Language
            result.Add(Data.Query<LanguageBuiltIn>().OrderBy(item => item.IdName).QueryExecute(), isApplication: true);

            // Navigation
            result.Add(Data.Query<NavigationBuiltIn>().OrderBy(item => item.IdName).QueryExecute());

            // LoginPermission
            result.Add(Data.Query<LoginPermissionBuiltIn>().Where(item => item.IsBuiltIn == true && item.IsExist).OrderBy(item => item.IdName).QueryExecute(), isApplication: true);
            result.Add(Data.Query<LoginPermissionBuiltIn>().Where(item => item.IsBuiltIn == false && item.IsExist).OrderBy(item => item.IdName).QueryExecute(), isApplication: false);

            // LoginRole
            result.Add(Data.Query<LoginRoleBuiltIn>().OrderBy(item => item.IdName).QueryExecute());

            // LoginRolePermission
            result.Add(Data.Query<LoginRolePermissionBuiltIn>().OrderBy(item => item.RoleIdName).ThenBy(item => item.PermissionIdName).QueryExecute());

            // LoginUser
            result.Add(Data.Query<LoginUserBuiltIn>().OrderBy(item => item.IdName).QueryExecute());

            // LoginUserRole
            result.Add(Data.Query<LoginUserRoleBuiltIn>().OrderBy(item => item.UserIdName).ThenBy(item => item.RoleIdName).QueryExecute());

            // Roadmap
            result.Add(Data.Query<RoadmapCategoryBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName).QueryExecute(), isApplication: true); // Category
            result.Add(Data.Query<RoadmapModuleBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName).QueryExecute(), isApplication: true); // Module
            result.Add(Data.Query<RoadmapPriorityBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName).QueryExecute(), isApplication: true); // Priority
            result.Add(Data.Query<RoadmapStateBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName).QueryExecute(), isApplication: true); // State
            result.Add(Data.Query<RoadmapBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.Name).QueryExecute()); // Roadmap

            // FileManager
            result.Add(Data.Query<StorageFile>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.FileName).QueryExecute());

            // Cms
            result.Add(Data.Query<CmsCodeBlockTypeBuiltIn>().OrderBy(item => item.Sort).QueryExecute());
            result.Add(Data.Query<CmsComponentTypeBuiltIn>().OrderBy(item => item.Sort).QueryExecute(), isApplication: true);
            result.Add(Data.Query<CmsComponentBuiltIn>().OrderBy(item => item.Name).QueryExecute());
            result.Add(Data.Query<CmsFile>().OrderBy(item => item.FileName).QueryExecute());
        }

        /// <summary>
        /// Cli Deploy.
        /// </summary>
        protected override void CommandDeployDbBuiltIn(DeployDbBuiltInResult result)
        {
            result.Add(LanguageBuiltInApplication.RowList, nameof(LanguageBuiltIn.Name), null);

            var rowList = NavigationBuiltInApplicationCli.RowList;
            result.Add(rowList, nameof(NavigationBuiltIn.Name), (item) => item.Id, (item) => item.ParentId, (item) => item.Sort);

            // LoginPermission
            result.Add(LoginPermissionBuiltInApplication.RowList, nameof(LoginPermissionBuiltIn.Name), "Login");
            result.Add(LoginPermissionBuiltInApplicationCli.RowList, nameof(LoginPermissionBuiltIn.Name), "Login");

            // LoginRole
            result.Add(LoginRoleBuiltInApplicationCli.RowList, nameof(LoginRoleBuiltIn.Name), "Login");

            // LoginRolePermission
            result.Add(LoginRolePermissionBuiltInApplicationCli.RowList, new string[] { nameof(LoginRolePermissionBuiltIn.RoleId), nameof(LoginRolePermissionBuiltIn.PermissionId) }, "Login");

            // LoginUser
            result.Add(LoginUserBuiltInApplicationCli.RowList, nameof(LoginUserBuiltIn.Name), "Login");

            // LoginUserRole
            result.Add(LoginUserRoleBuiltInApplicationCli.RowList, new string[] { nameof(LoginUserRoleBuiltIn.UserId), nameof(LoginUserRoleBuiltIn.RoleId) }, "Login");

            // Roadmap
            result.Add(RoadmapCategoryBuiltInApplication.RowList, nameof(RoadmapCategoryBuiltIn.Name)); // Category
            result.Add(RoadmapModuleBuiltInApplication.RowList, nameof(RoadmapModuleBuiltIn.Name)); // Module
            result.Add(RoadmapPriorityBuiltInApplication.RowList, nameof(RoadmapPriorityBuiltIn.Name)); // Priority
            result.Add(RoadmapStateBuiltInApplication.RowList, nameof(RoadmapStateBuiltIn.Name)); // State
            result.Add(RoadmapBuiltInApplicationCli.RowList, nameof(RoadmapBuiltIn.Name)); // Roadmap

            // FileManager
            result.Add(StorageFileApplicationCli.RowList, nameof(StorageFile.FileName));

            // Cms
            result.Add(CmsComponentTypeBuiltInApplication.RowList, nameof(CmsComponentTypeBuiltIn.Name));
            result.Add(CmsCodeBlockTypeBuiltInApplicationCli.RowList, nameof(CmsCodeBlockTypeBuiltIn.Name));
            result.Add(CmsComponentBuiltInApplicationCli.RowList, nameof(CmsComponentBuiltIn.Name), (item) => item.Id, (item) => item.ParentId, (item) => null);
            result.Add(CmsFileApplicationCli.RowList, nameof(CmsFile.FileName));
        }
    }
}
