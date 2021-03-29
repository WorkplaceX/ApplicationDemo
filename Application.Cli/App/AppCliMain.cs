namespace Application.Cli
{
    using Database.Demo; // Framework and Application contain same namespace.
    using DatabaseIntegrate.Demo;
    using Framework.Cli;
    using Framework.Cli.Config;
    using Framework.Cli.Generate;
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
                FolderNameAngular ="Application.Angular/LayoutDefault/",
            });
        }

        /// <summary>
        /// Cli Generate command.
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
            result.Add(Data.Query<LoginPermissionIntegrate>().Where(item => item.IsIntegrate == true && item.IsDelete == false).OrderBy(item => item.IdName), isApplication: true);
            result.Add(Data.Query<LoginPermissionIntegrate>().Where(item => item.IsIntegrate == false && item.IsDelete == false).OrderBy(item => item.IdName), isApplication: false);
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
            result.Add(Data.Query<RoadmapCategoryIntegrate>().Where(item => item.IsIntegrate && item.IsDelete == false).OrderBy(item => item.IdName), isApplication: true); // Category
            result.Add(Data.Query<RoadmapModuleIntegrate>().Where(item => item.IsIntegrate && item.IsDelete == false).OrderBy(item => item.IdName), isApplication: true); // Module
            result.Add(Data.Query<RoadmapPriorityIntegrate>().Where(item => item.IsIntegrate && item.IsDelete == false).OrderBy(item => item.IdName), isApplication: true); // Priority
            result.Add(Data.Query<RoadmapStateIntegrate>().Where(item => item.IsIntegrate && item.IsDelete == false).OrderBy(item => item.IdName), isApplication: true); // State
            result.Add(Data.Query<RoadmapIntegrate>().Where(item => item.IsIntegrate && item.IsDelete == false).OrderBy(item => item.Number).ThenBy(item => item.Name)); // Roadmap
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
            result.Add(Data.Query<StorageFile>().Where(item => item.IsIntegrate && item.IsDelete == false).OrderBy(item => item.FileName));
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
        /// Command cli generate.
        /// </summary>
        protected override void CommandGenerateFilter(GenerateFilterArgs args, GenerateFilterResult result)
        {
            result.FieldSqlList = args.FieldSqlList.Where(item => item.SchemaName == "Demo").ToList();
            result.TypeRowCalculatedList = new List<System.Type>();
        }

        /// <summary>
        /// Cli Deploy command.
        /// </summary>
        protected override void CommandDeployDbIntegrate(DeployDbIntegrateResult result)
        {
            // Language
            result.Add(LanguageIntegrateApp.RowList);

            // Navigation
            var rowList = NavigationIntegrateAppCli.RowList;
            result.Add(rowList, (item) => item.IdName, (item) => item.ParentIdName, (item) => item.Sort);

            // LoginPermission
            result.Add(LoginPermissionIntegrateApp.RowList);
            result.Add(LoginPermissionIntegrateAppCli.RowList);

            // LoginRole
            result.Add(LoginRoleIntegrateAppCli.RowList);

            // LoginRolePermission
            result.Add(LoginRolePermissionIntegrateAppCli.RowList);

            // LoginUser
            result.Add(LoginUserIntegrateAppCli.RowList);

            // LoginUserRole
            result.Add(LoginUserRoleIntegrateAppCli.RowList);

            // Roadmap
            result.Add(RoadmapCategoryIntegrateApp.RowList); // Category
            result.Add(RoadmapModuleIntegrateApp.RowList); // Module
            result.Add(RoadmapPriorityIntegrateApp.RowList); // Priority
            result.Add(RoadmapStateIntegrateApp.RowList); // State
            result.Add(RoadmapIntegrateAppCli.RowList); // Roadmap

            // FileManager
            result.Add(StorageFileAppCli.RowList);

            // Cms
            result.Add(CmsComponentTypeIntegrateApp.RowList);
            result.Add(CmsCodeBlockTypeIntegrateAppCli.RowList);
            result.Add(CmsFileAppCli.RowList);
            result.Add(CmsComponentIntegrateAppCli.RowList, (item) => item.IdName, (item) => item.ParentIdName, (item) => null);
        }
    }
}
