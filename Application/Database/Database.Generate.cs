namespace Database.dbo
{
    using Framework.DataAccessLayer;
    using System;

    [SqlTable("dbo", "Airport")]
    public partial class Airport : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(Airport_Id))]
        public int Id { get; set; }

        [SqlName("Text")]
        [TypeCell(typeof(Airport_Text))]
        public string Text { get; set; }

        [SqlName("Code")]
        [TypeCell(typeof(Airport_Code))]
        public string Code { get; set; }

        [SqlName("CountryId")]
        [TypeCell(typeof(Airport_CountryId))]
        public int? CountryId { get; set; }
    }

    public partial class Airport_Id : Cell<Airport> { }

    public partial class Airport_Text : Cell<Airport> { }

    public partial class Airport_Code : Cell<Airport> { }

    public partial class Airport_CountryId : Cell<Airport> { }

    [SqlTable("dbo", "AirportDisplay")]
    public partial class AirportDisplay : Row
    {
        [SqlName("AirportId")]
        [TypeCell(typeof(AirportDisplay_AirportId))]
        public int AirportId { get; set; }

        [SqlName("AirportText")]
        [TypeCell(typeof(AirportDisplay_AirportText))]
        public string AirportText { get; set; }

        [SqlName("AirportCode")]
        [TypeCell(typeof(AirportDisplay_AirportCode))]
        public string AirportCode { get; set; }

        [SqlName("CountryId")]
        [TypeCell(typeof(AirportDisplay_CountryId))]
        public int? CountryId { get; set; }

        [SqlName("CountryText")]
        [TypeCell(typeof(AirportDisplay_CountryText))]
        public string CountryText { get; set; }

        [SqlName("CountryContinent")]
        [TypeCell(typeof(AirportDisplay_CountryContinent))]
        public string CountryContinent { get; set; }
    }

    public partial class AirportDisplay_AirportId : Cell<AirportDisplay> { }

    public partial class AirportDisplay_AirportText : Cell<AirportDisplay> { }

    public partial class AirportDisplay_AirportCode : Cell<AirportDisplay> { }

    public partial class AirportDisplay_CountryId : Cell<AirportDisplay> { }

    public partial class AirportDisplay_CountryText : Cell<AirportDisplay> { }

    public partial class AirportDisplay_CountryContinent : Cell<AirportDisplay> { }

    [SqlTable("dbo", "Country")]
    public partial class Country : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(Country_Id))]
        public int Id { get; set; }

        [SqlName("Text")]
        [TypeCell(typeof(Country_Text))]
        public string Text { get; set; }

        [SqlName("TextShort")]
        [TypeCell(typeof(Country_TextShort))]
        public string TextShort { get; set; }

        [SqlName("Continent")]
        [TypeCell(typeof(Country_Continent))]
        public string Continent { get; set; }
    }

    public partial class Country_Id : Cell<Country> { }

    public partial class Country_Text : Cell<Country> { }

    public partial class Country_TextShort : Cell<Country> { }

    public partial class Country_Continent : Cell<Country> { }

    [SqlTable("dbo", "LoLoation")]
    public partial class LoLoation : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(LoLoation_Id))]
        public int Id { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(LoLoation_Name))]
        public string Name { get; set; }
    }

    public partial class LoLoation_Id : Cell<LoLoation> { }

    public partial class LoLoation_Name : Cell<LoLoation> { }

    [SqlTable("dbo", "LoRole")]
    public partial class LoRole : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(LoRole_Id))]
        public int Id { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(LoRole_Name))]
        public string Name { get; set; }

        [SqlName("IsAdmin")]
        [TypeCell(typeof(LoRole_IsAdmin))]
        public bool IsAdmin { get; set; }
    }

    public partial class LoRole_Id : Cell<LoRole> { }

    public partial class LoRole_Name : Cell<LoRole> { }

    public partial class LoRole_IsAdmin : Cell<LoRole> { }

    [SqlTable("dbo", "LoRoleAccess")]
    public partial class LoRoleAccess : Row
    {
        [SqlName("UserId")]
        [TypeCell(typeof(LoRoleAccess_UserId))]
        public int UserId { get; set; }

        [SqlName("UserName")]
        [TypeCell(typeof(LoRoleAccess_UserName))]
        public string UserName { get; set; }

        [SqlName("LoationId")]
        [TypeCell(typeof(LoRoleAccess_LoationId))]
        public int? LoationId { get; set; }

        [SqlName("LoationName")]
        [TypeCell(typeof(LoRoleAccess_LoationName))]
        public string LoationName { get; set; }
    }

    public partial class LoRoleAccess_UserId : Cell<LoRoleAccess> { }

    public partial class LoRoleAccess_UserName : Cell<LoRoleAccess> { }

    public partial class LoRoleAccess_LoationId : Cell<LoRoleAccess> { }

    public partial class LoRoleAccess_LoationName : Cell<LoRoleAccess> { }

    [SqlTable("dbo", "LoRoleLoation")]
    public partial class LoRoleLoation : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(LoRoleLoation_Id))]
        public int Id { get; set; }

        [SqlName("RoleId")]
        [TypeCell(typeof(LoRoleLoation_RoleId))]
        public int? RoleId { get; set; }

        [SqlName("UserId")]
        [TypeCell(typeof(LoRoleLoation_UserId))]
        public int? UserId { get; set; }

        [SqlName("LoationId")]
        [TypeCell(typeof(LoRoleLoation_LoationId))]
        public int LoationId { get; set; }

        [SqlName("IsActive")]
        [TypeCell(typeof(LoRoleLoation_IsActive))]
        public bool IsActive { get; set; }
    }

    public partial class LoRoleLoation_Id : Cell<LoRoleLoation> { }

    public partial class LoRoleLoation_RoleId : Cell<LoRoleLoation> { }

    public partial class LoRoleLoation_UserId : Cell<LoRoleLoation> { }

    public partial class LoRoleLoation_LoationId : Cell<LoRoleLoation> { }

    public partial class LoRoleLoation_IsActive : Cell<LoRoleLoation> { }

    [SqlTable("dbo", "LoRoleMatrix")]
    public partial class LoRoleMatrix : Row
    {
        [SqlName("UserId")]
        [TypeCell(typeof(LoRoleMatrix_UserId))]
        public int UserId { get; set; }

        [SqlName("UserName")]
        [TypeCell(typeof(LoRoleMatrix_UserName))]
        public string UserName { get; set; }

        [SqlName("LoationId")]
        [TypeCell(typeof(LoRoleMatrix_LoationId))]
        public int LoationId { get; set; }

        [SqlName("LoationName")]
        [TypeCell(typeof(LoRoleMatrix_LoationName))]
        public string LoationName { get; set; }

        [SqlName("RoleId")]
        [TypeCell(typeof(LoRoleMatrix_RoleId))]
        public int RoleId { get; set; }

        [SqlName("RoleName")]
        [TypeCell(typeof(LoRoleMatrix_RoleName))]
        public string RoleName { get; set; }

        [SqlName("IsRole")]
        [TypeCell(typeof(LoRoleMatrix_IsRole))]
        public bool? IsRole { get; set; }

        [SqlName("IsDirect")]
        [TypeCell(typeof(LoRoleMatrix_IsDirect))]
        public bool? IsDirect { get; set; }

        [SqlName("IsAdmin")]
        [TypeCell(typeof(LoRoleMatrix_IsAdmin))]
        public bool? IsAdmin { get; set; }

        [SqlName("IsAdminModule")]
        [TypeCell(typeof(LoRoleMatrix_IsAdminModule))]
        public bool? IsAdminModule { get; set; }

        [SqlName("IsAccess")]
        [TypeCell(typeof(LoRoleMatrix_IsAccess))]
        public int? IsAccess { get; set; }
    }

    public partial class LoRoleMatrix_UserId : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_UserName : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_LoationId : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_LoationName : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_RoleId : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_RoleName : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsRole : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsDirect : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsAdmin : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsAdminModule : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsAccess : Cell<LoRoleMatrix> { }

    [SqlTable("dbo", "LoRoleUser")]
    public partial class LoRoleUser : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(LoRoleUser_Id))]
        public int Id { get; set; }

        [SqlName("UserId")]
        [TypeCell(typeof(LoRoleUser_UserId))]
        public int UserId { get; set; }

        [SqlName("RoleId")]
        [TypeCell(typeof(LoRoleUser_RoleId))]
        public int RoleId { get; set; }

        [SqlName("IsActive")]
        [TypeCell(typeof(LoRoleUser_IsActive))]
        public bool IsActive { get; set; }
    }

    public partial class LoRoleUser_Id : Cell<LoRoleUser> { }

    public partial class LoRoleUser_UserId : Cell<LoRoleUser> { }

    public partial class LoRoleUser_RoleId : Cell<LoRoleUser> { }

    public partial class LoRoleUser_IsActive : Cell<LoRoleUser> { }

    [SqlTable("dbo", "SyRole")]
    public partial class SyRole : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(SyRole_Id))]
        public int Id { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(SyRole_Name))]
        public string Name { get; set; }

        [SqlName("IsAdmin")]
        [TypeCell(typeof(SyRole_IsAdmin))]
        public bool? IsAdmin { get; set; }
    }

    public partial class SyRole_Id : Cell<SyRole> { }

    public partial class SyRole_Name : Cell<SyRole> { }

    public partial class SyRole_IsAdmin : Cell<SyRole> { }

    [SqlTable("dbo", "SyRoleAccess")]
    public partial class SyRoleAccess : Row
    {
        [SqlName("UserId")]
        [TypeCell(typeof(SyRoleAccess_UserId))]
        public int UserId { get; set; }

        [SqlName("UserName")]
        [TypeCell(typeof(SyRoleAccess_UserName))]
        public string UserName { get; set; }

        [SqlName("RoleId")]
        [TypeCell(typeof(SyRoleAccess_RoleId))]
        public int? RoleId { get; set; }

        [SqlName("RoleName")]
        [TypeCell(typeof(SyRoleAccess_RoleName))]
        public string RoleName { get; set; }

        [SqlName("IsAdmin")]
        [TypeCell(typeof(SyRoleAccess_IsAdmin))]
        public bool? IsAdmin { get; set; }
    }

    public partial class SyRoleAccess_UserId : Cell<SyRoleAccess> { }

    public partial class SyRoleAccess_UserName : Cell<SyRoleAccess> { }

    public partial class SyRoleAccess_RoleId : Cell<SyRoleAccess> { }

    public partial class SyRoleAccess_RoleName : Cell<SyRoleAccess> { }

    public partial class SyRoleAccess_IsAdmin : Cell<SyRoleAccess> { }

    [SqlTable("dbo", "SyRoleUser")]
    public partial class SyRoleUser : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(SyRoleUser_Id))]
        public int Id { get; set; }

        [SqlName("UserId")]
        [TypeCell(typeof(SyRoleUser_UserId))]
        public int UserId { get; set; }

        [SqlName("RoleId")]
        [TypeCell(typeof(SyRoleUser_RoleId))]
        public int RoleId { get; set; }

        [SqlName("IsActive")]
        [TypeCell(typeof(SyRoleUser_IsActive))]
        public bool IsActive { get; set; }
    }

    public partial class SyRoleUser_Id : Cell<SyRoleUser> { }

    public partial class SyRoleUser_UserId : Cell<SyRoleUser> { }

    public partial class SyRoleUser_RoleId : Cell<SyRoleUser> { }

    public partial class SyRoleUser_IsActive : Cell<SyRoleUser> { }

    [SqlTable("dbo", "SyUser")]
    public partial class SyUser : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(SyUser_Id))]
        public int Id { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(SyUser_Name))]
        public string Name { get; set; }
    }

    public partial class SyUser_Id : Cell<SyUser> { }

    public partial class SyUser_Name : Cell<SyUser> { }

    [SqlTable("dbo", "TableName")]
    public partial class TableName : Row
    {
        [SqlName("TableName2")]
        [TypeCell(typeof(TableName_TableName2))]
        public string TableName2 { get; set; }

        [SqlName("IsView")]
        [TypeCell(typeof(TableName_IsView))]
        public bool? IsView { get; set; }
    }

    public partial class TableName_TableName2 : Cell<TableName> { }

    public partial class TableName_IsView : Cell<TableName> { }

    [SqlTable("dbo", "YContentText")]
    public partial class YContentText : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(YContentText_Id))]
        public int Id { get; set; }

        [SqlName("LayerId")]
        [TypeCell(typeof(YContentText_LayerId))]
        public int LayerId { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(YContentText_Name))]
        public string Name { get; set; }

        [SqlName("Text")]
        [TypeCell(typeof(YContentText_Text))]
        public string Text { get; set; }

        [SqlName("IsActive")]
        [TypeCell(typeof(YContentText_IsActive))]
        public bool? IsActive { get; set; }
    }

    public partial class YContentText_Id : Cell<YContentText> { }

    public partial class YContentText_LayerId : Cell<YContentText> { }

    public partial class YContentText_Name : Cell<YContentText> { }

    public partial class YContentText_Text : Cell<YContentText> { }

    public partial class YContentText_IsActive : Cell<YContentText> { }

    [SqlTable("dbo", "YContentTextDisplay")]
    public partial class YContentTextDisplay : Row
    {
        [SqlName("SessionId")]
        [TypeCell(typeof(YContentTextDisplay_SessionId))]
        public int SessionId { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(YContentTextDisplay_Name))]
        public string Name { get; set; }

        [SqlName("Text")]
        [TypeCell(typeof(YContentTextDisplay_Text))]
        public string Text { get; set; }

        [SqlName("Path")]
        [TypeCell(typeof(YContentTextDisplay_Path))]
        public string Path { get; set; }
    }

    public partial class YContentTextDisplay_SessionId : Cell<YContentTextDisplay> { }

    public partial class YContentTextDisplay_Name : Cell<YContentTextDisplay> { }

    public partial class YContentTextDisplay_Text : Cell<YContentTextDisplay> { }

    public partial class YContentTextDisplay_Path : Cell<YContentTextDisplay> { }

    [SqlTable("dbo", "YContentType")]
    public partial class YContentType : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(YContentType_Id))]
        public int Id { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(YContentType_Name))]
        public string Name { get; set; }
    }

    public partial class YContentType_Id : Cell<YContentType> { }

    public partial class YContentType_Name : Cell<YContentType> { }

    [SqlTable("dbo", "YLayer")]
    public partial class YLayer : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(YLayer_Id))]
        public int Id { get; set; }

        [SqlName("ParentId")]
        [TypeCell(typeof(YLayer_ParentId))]
        public int? ParentId { get; set; }

        [SqlName("Application")]
        [TypeCell(typeof(YLayer_Application))]
        public string Application { get; set; }

        [SqlName("Language")]
        [TypeCell(typeof(YLayer_Language))]
        public string Language { get; set; }

        [SqlName("Screen")]
        [TypeCell(typeof(YLayer_Screen))]
        public string Screen { get; set; }

        [SqlName("User")]
        [TypeCell(typeof(YLayer_User))]
        public string User { get; set; }
    }

    public partial class YLayer_Id : Cell<YLayer> { }

    public partial class YLayer_ParentId : Cell<YLayer> { }

    public partial class YLayer_Application : Cell<YLayer> { }

    public partial class YLayer_Language : Cell<YLayer> { }

    public partial class YLayer_Screen : Cell<YLayer> { }

    public partial class YLayer_User : Cell<YLayer> { }

    [SqlTable("dbo", "YLayerDeclare")]
    public partial class YLayerDeclare : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(YLayerDeclare_Id))]
        public string Id { get; set; }

        [SqlName("LayerTypeId")]
        [TypeCell(typeof(YLayerDeclare_LayerTypeId))]
        public int LayerTypeId { get; set; }

        [SqlName("LayerIdDeclare")]
        [TypeCell(typeof(YLayerDeclare_LayerIdDeclare))]
        public string LayerIdDeclare { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(YLayerDeclare_Name))]
        public string Name { get; set; }
    }

    public partial class YLayerDeclare_Id : Cell<YLayerDeclare> { }

    public partial class YLayerDeclare_LayerTypeId : Cell<YLayerDeclare> { }

    public partial class YLayerDeclare_LayerIdDeclare : Cell<YLayerDeclare> { }

    public partial class YLayerDeclare_Name : Cell<YLayerDeclare> { }

    [SqlTable("dbo", "YLayerHierarchy")]
    public partial class YLayerHierarchy : Row
    {
        [SqlName("LayerId")]
        [TypeCell(typeof(YLayerHierarchy_LayerId))]
        public int? LayerId { get; set; }

        [SqlName("LayerIdContain")]
        [TypeCell(typeof(YLayerHierarchy_LayerIdContain))]
        public int? LayerIdContain { get; set; }

        [SqlName("Level")]
        [TypeCell(typeof(YLayerHierarchy_Level))]
        public int? Level { get; set; }
    }

    public partial class YLayerHierarchy_LayerId : Cell<YLayerHierarchy> { }

    public partial class YLayerHierarchy_LayerIdContain : Cell<YLayerHierarchy> { }

    public partial class YLayerHierarchy_Level : Cell<YLayerHierarchy> { }

    [SqlTable("dbo", "YLayerPath")]
    public partial class YLayerPath : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(YLayerPath_Id))]
        public int Id { get; set; }

        [SqlName("SessionId")]
        [TypeCell(typeof(YLayerPath_SessionId))]
        public int SessionId { get; set; }

        [SqlName("ContentTypeId")]
        [TypeCell(typeof(YLayerPath_ContentTypeId))]
        public int ContentTypeId { get; set; }

        [SqlName("LayerId")]
        [TypeCell(typeof(YLayerPath_LayerId))]
        public int LayerId { get; set; }

        [SqlName("LayerIdContain")]
        [TypeCell(typeof(YLayerPath_LayerIdContain))]
        public int LayerIdContain { get; set; }

        [SqlName("Level")]
        [TypeCell(typeof(YLayerPath_Level))]
        public int Level { get; set; }
    }

    public partial class YLayerPath_Id : Cell<YLayerPath> { }

    public partial class YLayerPath_SessionId : Cell<YLayerPath> { }

    public partial class YLayerPath_ContentTypeId : Cell<YLayerPath> { }

    public partial class YLayerPath_LayerId : Cell<YLayerPath> { }

    public partial class YLayerPath_LayerIdContain : Cell<YLayerPath> { }

    public partial class YLayerPath_Level : Cell<YLayerPath> { }

    [SqlTable("dbo", "YLayerType")]
    public partial class YLayerType : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(YLayerType_Id))]
        public int Id { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(YLayerType_Name))]
        public string Name { get; set; }
    }

    public partial class YLayerType_Id : Cell<YLayerType> { }

    public partial class YLayerType_Name : Cell<YLayerType> { }

    [SqlTable("dbo", "YLayerView")]
    public partial class YLayerView : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(YLayerView_Id))]
        public int Id { get; set; }

        [SqlName("ParentId")]
        [TypeCell(typeof(YLayerView_ParentId))]
        public int? ParentId { get; set; }

        [SqlName("LayerDeclareIdApplication")]
        [TypeCell(typeof(YLayerView_LayerDeclareIdApplication))]
        public int? LayerDeclareIdApplication { get; set; }

        [SqlName("LayerDeclareIdLanguage")]
        [TypeCell(typeof(YLayerView_LayerDeclareIdLanguage))]
        public int? LayerDeclareIdLanguage { get; set; }

        [SqlName("LayerDeclareIdScreen")]
        [TypeCell(typeof(YLayerView_LayerDeclareIdScreen))]
        public int? LayerDeclareIdScreen { get; set; }

        [SqlName("LayerDeclareIdUser")]
        [TypeCell(typeof(YLayerView_LayerDeclareIdUser))]
        public int? LayerDeclareIdUser { get; set; }

        [SqlName("Path")]
        [TypeCell(typeof(YLayerView_Path))]
        public string Path { get; set; }
    }

    public partial class YLayerView_Id : Cell<YLayerView> { }

    public partial class YLayerView_ParentId : Cell<YLayerView> { }

    public partial class YLayerView_LayerDeclareIdApplication : Cell<YLayerView> { }

    public partial class YLayerView_LayerDeclareIdLanguage : Cell<YLayerView> { }

    public partial class YLayerView_LayerDeclareIdScreen : Cell<YLayerView> { }

    public partial class YLayerView_LayerDeclareIdUser : Cell<YLayerView> { }

    public partial class YLayerView_Path : Cell<YLayerView> { }

    [SqlTable("dbo", "YSession")]
    public partial class YSession : Row
    {
        [SqlName("Id")]
        [TypeCell(typeof(YSession_Id))]
        public int Id { get; set; }

        [SqlName("Name")]
        [TypeCell(typeof(YSession_Name))]
        public Guid Name { get; set; }

        [SqlName("LayerDeclareIdApplication")]
        [TypeCell(typeof(YSession_LayerDeclareIdApplication))]
        public int LayerDeclareIdApplication { get; set; }

        [SqlName("LayerDeclareIdLanguage")]
        [TypeCell(typeof(YSession_LayerDeclareIdLanguage))]
        public int? LayerDeclareIdLanguage { get; set; }

        [SqlName("LayerDeclareIdScreen")]
        [TypeCell(typeof(YSession_LayerDeclareIdScreen))]
        public int? LayerDeclareIdScreen { get; set; }

        [SqlName("LayerDeclareIdUser")]
        [TypeCell(typeof(YSession_LayerDeclareIdUser))]
        public int? LayerDeclareIdUser { get; set; }

        [SqlName("LayerIdText")]
        [TypeCell(typeof(YSession_LayerIdText))]
        public int? LayerIdText { get; set; }

        [SqlName("LayerIdFile")]
        [TypeCell(typeof(YSession_LayerIdFile))]
        public int? LayerIdFile { get; set; }
    }

    public partial class YSession_Id : Cell<YSession> { }

    public partial class YSession_Name : Cell<YSession> { }

    public partial class YSession_LayerDeclareIdApplication : Cell<YSession> { }

    public partial class YSession_LayerDeclareIdLanguage : Cell<YSession> { }

    public partial class YSession_LayerDeclareIdScreen : Cell<YSession> { }

    public partial class YSession_LayerDeclareIdUser : Cell<YSession> { }

    public partial class YSession_LayerIdText : Cell<YSession> { }

    public partial class YSession_LayerIdFile : Cell<YSession> { }
}
