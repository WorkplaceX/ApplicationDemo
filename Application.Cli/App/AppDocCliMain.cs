namespace Application.Cli.Doc
{
    using Database.Doc;
    using DatabaseIntegrate.Doc;
    using Framework.DataAccessLayer;
    using System.Linq;
    using static Framework.Cli.AppCli;

    public static class AppDocCliMain
    {
        public static void CommandGenerateIntegrate(GenerateIntegrateResult result)
        {
            // Navigate
            result.Add(Data.Query<NavigateIntegrate>().OrderBy(item => item.IdName));
            result.AddKey<Navigate>(nameof(Navigate.Name));
            result.AddReference<Navigate, Navigate>(nameof(Navigate.ParentId));

            // Language
            result.Add(Data.Query<LanguageIntegrate>().OrderBy(item => item.IdName), isApplication: true);
            result.AddKey<Language>(nameof(Language.Name));

            // LoginUser
            result.Add(Data.Query<LoginUserIntegrate>().Where(item => item.IsIntegrate == true).OrderBy(item => item.IdName), isApplication: true);
            result.AddKey<LoginUser>(nameof(LoginUser.Name));

            // LoginRole
            result.Add(Data.Query<LoginRoleIntegrate>().OrderBy(item => item.IdName), isApplication: true);
            result.AddKey<LoginRole>(nameof(LoginRole.Name));

            // LoginUserRole
            result.Add(Data.Query<LoginUserRoleIntegrate>().Where(item => item.LoginUserIsIntegrate == true).OrderBy(item => item.LoginUserIdName).ThenBy(item => item.LoginRoleIdName));
            result.AddKey<LoginUserRole>(nameof(LoginUserRole.LoginUserId), nameof(LoginUserRole.LoginRoleId));
            result.AddReference<LoginUserRole, LoginUser>(nameof(LoginUserRole.LoginUserId));
            result.AddReference<LoginUserRole, LoginRole>(nameof(LoginUserRole.LoginRoleId));

            // NavigateLoginRole
            result.Add(Data.Query<NavigateRoleIntegrate>().OrderBy(item => item.NavigateIdName).ThenBy(item => item.LoginRoleIdName));
            result.AddKey<NavigateRole>(nameof(NavigateRole.NavigateId), nameof(NavigateRole.LoginRoleId));
            result.AddReference<NavigateRole, Navigate>(nameof(NavigateRole.NavigateId));
            result.AddReference<NavigateRole, LoginRole>(nameof(NavigateRole.LoginRoleId));

            // StorageFile
            result.Add(Data.Query<StorageFileIntegrate>().OrderBy(item => item.IdName));
            result.AddKey<StorageFile>(nameof(StorageFile.FileName));
        }

        public static void CommandDeployDbIntegrate(DeployDbIntegrateResult result)
        {
            // Language
            result.Add(LanguageIntegrateApp.RowList);

            // Navigate
            var rowList = NavigateIntegrateAppCli.RowList;
            result.Add(rowList, (item) => item.IdName, (item) => item.ParentIdName, (item) => item.Sort);

            // LoginUser
            result.Add(LoginUserIntegrateApp.RowList);
            result.Add(LoginRoleIntegrateApp.RowList);
            result.Add(LoginUserRoleIntegrateAppCli.RowList);

            // NavigateLoginRole
            result.Add(NavigateRoleIntegrateAppCli.RowList);

            // StorageFile
            result.Add(StorageFileIntegrateAppCli.RowList);
        }
    }
}
