// Do not modify this file. It's generated by Framework.Cli.

namespace DatabaseBuiltIn.Demo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.DataAccessLayer;
    using Database.Demo;

    public static class LanguageBuiltInTableApplication
    {
        public enum IdNameEnum { [IdNameEnum(null)]None = 0, [IdNameEnum("German")]German = 1, [IdNameEnum("French")]French = 2, [IdNameEnum("English")]English = 3, [IdNameEnum("Italian")]Italian = 4 }

        public static LanguageBuiltIn Row(IdNameEnum value)
        {
            return RowList.Where(item => item.IdName == IdNameEnumAttribute.IdNameFromEnum(value)).SingleOrDefault();
        }

        public static List<LanguageBuiltIn> RowList
        {
            get
            {
                var result = new List<LanguageBuiltIn>();
                result.Add(new LanguageBuiltIn() { Id = 1, IdName = "German", Name = "German", TextHtml = "<span class=\"flag-icon flag-icon-de\"></span> German" });
                result.Add(new LanguageBuiltIn() { Id = 2, IdName = "French", Name = "French", TextHtml = "<span class=\"flag-icon flag-icon-fr\"></span> French" });
                result.Add(new LanguageBuiltIn() { Id = 3, IdName = "English", Name = "English", TextHtml = "<span class=\"flag-icon flag-icon-gb\"></span> English" });
                result.Add(new LanguageBuiltIn() { Id = 4, IdName = "Italian", Name = "Italian", TextHtml = "<span class=\"flag-icon flag-icon-it\"></span> Italian" });
                return result;
            }
        }
    }

    public static class LoginPermissionBuiltInTableApplication
    {
        public enum IdNameEnum { [IdNameEnum(null)]None = 0, [IdNameEnum("Guest")]Guest = 1, [IdNameEnum("Administrator")]Administrator = 2, [IdNameEnum("Developer")]Developer = 3 }

        public static LoginPermissionBuiltIn Row(IdNameEnum value)
        {
            return RowList.Where(item => item.IdName == IdNameEnumAttribute.IdNameFromEnum(value)).SingleOrDefault();
        }

        public static List<LoginPermissionBuiltIn> RowList
        {
            get
            {
                var result = new List<LoginPermissionBuiltIn>();
                result.Add(new LoginPermissionBuiltIn() { Id = 1, IdName = "Guest", Name = "Guest", Description = "Guest permission", IsBuiltIn = true, IsExist = true });
                result.Add(new LoginPermissionBuiltIn() { Id = 2, IdName = "Administrator", Name = "Administrator", Description = "Administrator permission", IsBuiltIn = true, IsExist = true });
                result.Add(new LoginPermissionBuiltIn() { Id = 3, IdName = "Developer", Name = "Developer", Description = "Developer permission", IsBuiltIn = true, IsExist = true });
                return result;
            }
        }
    }
}
