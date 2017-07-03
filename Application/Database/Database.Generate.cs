namespace Database.dbo
{
    using Framework.DataAccessLayer;

    [SqlName("Airport")]
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

    [SqlName("AirportDisplay")]
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

    [SqlName("Country")]
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

    [SqlName("LoLoation")]
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

    [SqlName("LoRole")]
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

    [SqlName("LoRoleAccess")]
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

    [SqlName("LoRoleLoation")]
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

    [SqlName("LoRoleMatrix")]
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

    [SqlName("LoRoleUser")]
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

    [SqlName("SyRole")]
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

    [SqlName("SyRoleAccess")]
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

    [SqlName("SyRoleUser")]
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

    [SqlName("SyUser")]
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

    [SqlName("TableName")]
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
}
