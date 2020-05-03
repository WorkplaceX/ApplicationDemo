namespace Application.Cli
{
    using Database.Demo; // Framework and Application contain same namespace.
    using DatabaseIntegrate.Demo;
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
        protected override void CommandGenerateIntegrate(GenerateIntegrateResult result)
        {
            // Language
            result.Add(Data.Query<LanguageIntegrate>().OrderBy(item => item.IdName), isApplication: true);

            // Navigation
            result.Add(Data.Query<NavigationIntegrate>().OrderBy(item => item.IdName));
            result.AddReference<Navigation, Navigation>(nameof(Navigation.ParentId));

            // LoginPermission
            result.Add(Data.Query<LoginPermissionIntegrate>().Where(item => item.IsIntegrate == true && item.IsExist).OrderBy(item => item.IdName), isApplication: true);
            result.Add(Data.Query<LoginPermissionIntegrate>().Where(item => item.IsIntegrate == false && item.IsExist).OrderBy(item => item.IdName), isApplication: false);

            // LoginRole
            result.Add(Data.Query<LoginRoleIntegrate>().OrderBy(item => item.IdName));

            // LoginRolePermission
            result.Add(Data.Query<LoginRolePermissionIntegrate>().OrderBy(item => item.RoleIdName).ThenBy(item => item.PermissionIdName));
            result.AddReference<LoginRolePermission, LoginPermission>(nameof(LoginRolePermission.PermissionId));
            result.AddReference<LoginRolePermission, LoginRole>(nameof(LoginRolePermission.RoleId));

            // LoginUser
            result.Add(Data.Query<LoginUserIntegrate>().OrderBy(item => item.IdName));

            // LoginUserRole
            result.Add(Data.Query<LoginUserRoleIntegrate>().OrderBy(item => item.UserIdName).ThenBy(item => item.RoleIdName));
            result.AddReference<LoginUserRole, LoginRole>(nameof(LoginUserRole.RoleId));
            result.AddReference<LoginUserRole, LoginUser>(nameof(LoginUserRole.UserId));

            // Roadmap
            result.Add(Data.Query<RoadmapCategoryIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.IdName), isApplication: true); // Category
            result.Add(Data.Query<RoadmapModuleIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.IdName), isApplication: true); // Module
            result.Add(Data.Query<RoadmapPriorityIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.IdName), isApplication: true); // Priority
            result.Add(Data.Query<RoadmapStateIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.IdName), isApplication: true); // State
            result.Add(Data.Query<RoadmapIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.Name)); // Roadmap
            result.AddReference<Roadmap, LoginUser>(nameof(Roadmap.LoginUserId));
            result.AddReference<Roadmap, RoadmapCategory>(nameof(Roadmap.RoadmapCategoryId));
            result.AddReference<Roadmap, RoadmapModule>(nameof(Roadmap.RoadmapModuleId));
            result.AddReference<Roadmap, RoadmapPriority>(nameof(Roadmap.RoadmapPriorityId));
            result.AddReference<Roadmap, RoadmapState>(nameof(Roadmap.RoadmapStateId));

            // FileManager
            result.Add(Data.Query<StorageFile>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.FileName));

            // Cms
            result.Add(Data.Query<CmsCodeBlockTypeIntegrate>().OrderBy(item => item.Sort));
            result.Add(Data.Query<CmsComponentTypeIntegrate>().OrderBy(item => item.Sort), isApplication: true);
            result.Add(Data.Query<CmsComponentIntegrate>().OrderBy(item => item.Name));
            result.Add(Data.Query<CmsFile>().OrderBy(item => item.FileName));
            result.AddReference<CmsComponent, CmsCodeBlockType>(nameof(CmsComponent.CodeBlockTypeId));
            result.AddReference<CmsComponent, CmsComponentType>(nameof(CmsComponent.ComponentTypeId));
            result.AddReference<CmsComponent, CmsFile>(nameof(CmsComponent.ImageFileId));
            result.AddReference<CmsComponent, CmsFile>(nameof(CmsComponent.PageImageFileId));
            result.AddReference<CmsComponent, CmsComponent>(nameof(CmsComponent.ParentId));
        }

        /// <summary>
        /// Cli Deploy.
        /// </summary>
        protected override void CommandDeployDbIntegrate(DeployDbIntegrateResult result)
        {
            result.Add(LanguageIntegrateApplication.RowList, nameof(LanguageIntegrate.Name));

            // Navigation
            var rowList = NavigationIntegrateApplicationCli.RowList;
            result.Add(rowList, nameof(NavigationIntegrate.Name), (item) => item.Id, (item) => item.ParentId, (item) => item.Sort);

            // LoginPermission
            result.Add(LoginPermissionIntegrateApplication.RowList, nameof(LoginPermissionIntegrate.Name));
            result.Add(LoginPermissionIntegrateApplicationCli.RowList, nameof(LoginPermissionIntegrate.Name));

            // LoginRole
            result.Add(LoginRoleIntegrateApplicationCli.RowList, nameof(LoginRoleIntegrate.Name));

            // LoginRolePermission
            result.Add(LoginRolePermissionIntegrateApplicationCli.RowList, new string[] { nameof(LoginRolePermissionIntegrate.RoleId), nameof(LoginRolePermissionIntegrate.PermissionId) });

            // LoginUser
            result.Add(LoginUserIntegrateApplicationCli.RowList, nameof(LoginUserIntegrate.Name));

            // LoginUserRole
            result.Add(LoginUserRoleIntegrateApplicationCli.RowList, new string[] { nameof(LoginUserRoleIntegrate.UserId), nameof(LoginUserRoleIntegrate.RoleId) });

            // Roadmap
            result.Add(RoadmapCategoryIntegrateApplication.RowList, nameof(RoadmapCategoryIntegrate.Name)); // Category
            result.Add(RoadmapModuleIntegrateApplication.RowList, nameof(RoadmapModuleIntegrate.Name)); // Module
            result.Add(RoadmapPriorityIntegrateApplication.RowList, nameof(RoadmapPriorityIntegrate.Name)); // Priority
            result.Add(RoadmapStateIntegrateApplication.RowList, nameof(RoadmapStateIntegrate.Name)); // State
            result.Add(RoadmapIntegrateApplicationCli.RowList, nameof(RoadmapIntegrate.Name)); // Roadmap

            // FileManager
            result.Add(StorageFileApplicationCli.RowList, nameof(StorageFile.FileName));

            // Cms
            result.Add(CmsComponentTypeIntegrateApplication.RowList, nameof(CmsComponentTypeIntegrate.Name));
            result.Add(CmsCodeBlockTypeIntegrateApplicationCli.RowList, nameof(CmsCodeBlockTypeIntegrate.Name));
            result.Add(CmsFileApplicationCli.RowList, nameof(CmsFile.FileName));
            result.Add(CmsComponentIntegrateApplicationCli.RowList, nameof(CmsComponentIntegrate.Name), (item) => item.Id, (item) => item.ParentId, (item) => null);
        }
    }
}
