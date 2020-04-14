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
                DomainNameList = new List<ConfigCliWebsiteDomain>(new ConfigCliWebsiteDomain[] { new ConfigCliWebsiteDomain { EnvironmentName="DEV", DomainName = "localhost", AppTypeName = appTypeName } }),
                FolderNameNpmBuild = "Application.Website/Default/",
                FolderNameDist = "Application.Website/Default/dist/",
            });
        }

        /// <summary>
        /// Cli Generate.
        /// </summary>
        protected override void CommandGenerateBuiltIn(GenerateBuiltInResult result)
        {
            // Language
            result.Add(Data.Select(Data.Query<LanguageBuiltIn>().OrderBy(item => item.IdName)), isApplication: true);

            // Navigation
            result.Add(Data.Select(Data.Query<NavigationBuiltIn>().OrderBy(item => item.IdName)));

            // LoginPermission
            result.Add(Data.Select(Data.Query<LoginPermissionBuiltIn>().Where(item => item.IsBuiltIn == true && item.IsExist).OrderBy(item => item.IdName)), isApplication: true);
            result.Add(Data.Select(Data.Query<LoginPermissionBuiltIn>().Where(item => item.IsBuiltIn == false && item.IsExist).OrderBy(item => item.IdName)), isApplication: false);

            // LoginRole
            result.Add(Data.Select(Data.Query<LoginRoleBuiltIn>().OrderBy(item => item.IdName)));

            // LoginRolePermission
            result.Add(Data.Select(Data.Query<LoginRolePermissionBuiltIn>().OrderBy(item => item.RoleIdName).ThenBy(item => item.PermissionIdName)));

            // LoginUser
            result.Add(Data.Select(Data.Query<LoginUserBuiltIn>().OrderBy(item => item.IdName)));

            // LoginUserRole
            result.Add(Data.Select(Data.Query<LoginUserRoleBuiltIn>().OrderBy(item => item.UserIdName).ThenBy(item => item.RoleIdName)));

            // Roadmap
            result.Add(Data.Select(Data.Query<RoadmapCategoryBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName)), isApplication: true); // Category
            result.Add(Data.Select(Data.Query<RoadmapModuleBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName)), isApplication: true); // Module
            result.Add(Data.Select(Data.Query<RoadmapPriorityBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName)), isApplication: true); // Priority
            result.Add(Data.Select(Data.Query<RoadmapStateBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.IdName)), isApplication: true); // State
            result.Add(Data.Select(Data.Query<RoadmapBuiltIn>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.Name))); // Roadmap

            // FileManager
            result.Add(Data.Select(Data.Query<File>().Where(item => item.IsBuiltIn && item.IsExist).OrderBy(item => item.FileName)));

            // Cms
            result.Add(Data.Select(Data.Query<CmsCodeBlockTypeBuiltIn>().OrderBy(item => item.Sort)));
            result.Add(Data.Select(Data.Query<CmsComponentTypeBuiltIn>().OrderBy(item => item.Sort)));
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
}
