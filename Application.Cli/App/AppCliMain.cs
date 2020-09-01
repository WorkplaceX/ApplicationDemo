namespace Application.Cli
{
    using Database.Demo; // Framework and Application contain same namespace.
    using DatabaseIntegrate.Demo;
    using Framework.Cli;
    using Framework.Cli.Config;
    using Framework.DataAccessLayer;
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
                FolderNameNpmBuild = "Application.Website/LayoutDefault/",
                FolderNameDist = "Application.Website/LayoutDefault/dist/",
            });
        }

        /// <summary>
        /// Cli Generate.
        /// </summary>
        protected override void CommandGenerateIntegrate(GenerateIntegrateResult result)
        {
            // Language
            result.Add(Data.Query<LanguageIntegrate>().OrderBy(item => item.IdName), isApplication: true);
            result.AddKey<Language>(nameof(Language.Name));

            // Navigation
            result.Add(Data.Query<NavigationIntegrate>().OrderBy(item => item.IdName));
            result.AddKey<Navigation>(nameof(Navigation.Name));
            result.AddReference<Navigation, Navigation>(nameof(Navigation.ParentId));

            // LoginPermission
            result.Add(Data.Query<LoginPermissionIntegrate>().Where(item => item.IsIntegrate == true && item.IsExist).OrderBy(item => item.IdName), isApplication: true);
            result.Add(Data.Query<LoginPermissionIntegrate>().Where(item => item.IsIntegrate == false && item.IsExist).OrderBy(item => item.IdName), isApplication: false);
            result.AddKey<LoginPermission>(nameof(LoginPermission.Name));

            // LoginRole
            result.Add(Data.Query<LoginRoleIntegrate>().OrderBy(item => item.IdName));
            result.AddKey<LoginRole>(nameof(LoginRole.Name));

            // LoginRolePermission
            result.Add(Data.Query<LoginRolePermissionIntegrate>().OrderBy(item => item.RoleIdName).ThenBy(item => item.PermissionIdName));
            result.AddKey<LoginRolePermission>(nameof(LoginRolePermission.RoleId), nameof(LoginRolePermission.PermissionId));
            result.AddReference<LoginRolePermission, LoginPermission>(nameof(LoginRolePermission.PermissionId));
            result.AddReference<LoginRolePermission, LoginRole>(nameof(LoginRolePermission.RoleId));

            // LoginUser
            result.Add(Data.Query<LoginUserIntegrate>().OrderBy(item => item.IdName));
            result.AddKey<LoginUser>(nameof(LoginUser.Name));

            // LoginUserRole
            result.Add(Data.Query<LoginUserRoleIntegrate>().OrderBy(item => item.UserIdName).ThenBy(item => item.RoleIdName));
            result.AddKey<LoginUserRole>(nameof(LoginUserRole.UserId), nameof(LoginUserRole.RoleId));
            result.AddReference<LoginUserRole, LoginRole>(nameof(LoginUserRole.RoleId));
            result.AddReference<LoginUserRole, LoginUser>(nameof(LoginUserRole.UserId));

            // Roadmap
            result.Add(Data.Query<RoadmapCategoryIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.IdName), isApplication: true); // Category
            result.Add(Data.Query<RoadmapModuleIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.IdName), isApplication: true); // Module
            result.Add(Data.Query<RoadmapPriorityIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.IdName), isApplication: true); // Priority
            result.Add(Data.Query<RoadmapStateIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.IdName), isApplication: true); // State
            result.Add(Data.Query<RoadmapIntegrate>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.Number).ThenBy(item => item.Name)); // Roadmap
            result.AddKey<RoadmapCategory>(nameof(RoadmapCategory.Name));
            result.AddKey<RoadmapModule>(nameof(RoadmapModule.Name));
            result.AddKey<RoadmapPriority>(nameof(RoadmapPriority.Name));
            result.AddKey<RoadmapState>(nameof(RoadmapState.Name));
            result.AddKey<Roadmap>(nameof(Roadmap.Name));
            result.AddReference<Roadmap, LoginUser>(nameof(Roadmap.LoginUserId));
            result.AddReference<Roadmap, RoadmapCategory>(nameof(Roadmap.RoadmapCategoryId));
            result.AddReference<Roadmap, RoadmapModule>(nameof(Roadmap.RoadmapModuleId));
            result.AddReference<Roadmap, RoadmapPriority>(nameof(Roadmap.RoadmapPriorityId));
            result.AddReference<Roadmap, RoadmapState>(nameof(Roadmap.RoadmapStateId));

            // FileManager
            result.Add(Data.Query<StorageFile>().Where(item => item.IsIntegrate && item.IsExist).OrderBy(item => item.FileName));
            result.AddKey<StorageFile>(nameof(StorageFile.FileName));

            // Cms
            result.Add(Data.Query<CmsCodeBlockTypeIntegrate>().OrderBy(item => item.Sort));
            result.Add(Data.Query<CmsComponentTypeIntegrate>().OrderBy(item => item.Sort), isApplication: true);
            result.Add(Data.Query<CmsFile>().OrderBy(item => item.FileName));
            result.Add(Data.Query<CmsComponentIntegrate>().OrderBy(item => item.PagePath).ThenBy(item => item.Sort).ThenBy(item => item.Name));
            result.AddKey<CmsCodeBlockType>(nameof(CmsCodeBlockType.Name));
            result.AddKey<CmsComponentType>(nameof(CmsComponentType.Name));
            result.AddKey<CmsFile>(nameof(CmsFile.FileName));
            result.AddKey<CmsComponent>(nameof(CmsComponent.Name));
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
            result.Add(LanguageIntegrateApplication.RowList);

            // Navigation
            var rowList = NavigationIntegrateApplicationCli.RowList;
            result.Add(rowList, (item) => item.IdName, (item) => item.ParentIdName, (item) => item.Sort);

            // LoginPermission
            result.Add(LoginPermissionIntegrateApplication.RowList);
            result.Add(LoginPermissionIntegrateApplicationCli.RowList);

            // LoginRole
            result.Add(LoginRoleIntegrateApplicationCli.RowList);

            // LoginRolePermission
            result.Add(LoginRolePermissionIntegrateApplicationCli.RowList);

            // LoginUser
            result.Add(LoginUserIntegrateApplicationCli.RowList);

            // LoginUserRole
            result.Add(LoginUserRoleIntegrateApplicationCli.RowList);

            // Roadmap
            result.Add(RoadmapCategoryIntegrateApplication.RowList); // Category
            result.Add(RoadmapModuleIntegrateApplication.RowList); // Module
            result.Add(RoadmapPriorityIntegrateApplication.RowList); // Priority
            result.Add(RoadmapStateIntegrateApplication.RowList); // State
            result.Add(RoadmapIntegrateApplicationCli.RowList); // Roadmap

            // FileManager
            result.Add(StorageFileApplicationCli.RowList);

            // Cms
            result.Add(CmsComponentTypeIntegrateApplication.RowList);
            result.Add(CmsCodeBlockTypeIntegrateApplicationCli.RowList);
            result.Add(CmsFileApplicationCli.RowList);
            result.Add(CmsComponentIntegrateApplicationCli.RowList, (item) => item.IdName, (item) => item.ParentIdName, (item) => null);
        }
    }
}
