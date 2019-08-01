// Do not modify this file. It's generated by Framework.Cli.

namespace Database.Demo
{
    using Framework.DataAccessLayer;
    using System;

    [SqlTable("Demo", "CountryDisplay")]
    public class CountryDisplay : Row
    {
        [SqlField("Country", FrameworkTypeEnum.Nvarcahr)]
        public string Country { get; set; }

        [SqlField("Code", FrameworkTypeEnum.Nvarcahr)]
        public string Code { get; set; }

        [SqlField("IsWikipedia", FrameworkTypeEnum.Bit)]
        public bool? IsWikipedia { get; set; }

        [SqlField("IsOpenFlightsAirport", FrameworkTypeEnum.Bit)]
        public bool? IsOpenFlightsAirport { get; set; }

        [SqlField("IsOpenFlightsAirline", FrameworkTypeEnum.Bit)]
        public bool? IsOpenFlightsAirline { get; set; }

        [SqlField("IsFlagIconCss", FrameworkTypeEnum.Bit)]
        public bool? IsFlagIconCss { get; set; }

        [SqlField("WikipediaCountryUrl", FrameworkTypeEnum.Nvarcahr)]
        public string WikipediaCountryUrl { get; set; }

        [SqlField("ASFlagIcon", FrameworkTypeEnum.Nvarcahr)]
        public string ASFlagIcon { get; set; }
    }

    [SqlTable("Demo", "CountryDisplayCache")]
    public class CountryDisplayCache : Row
    {
        [SqlField("Id", true, FrameworkTypeEnum.Int)]
        public int Id { get; set; }

        [SqlField("Country", FrameworkTypeEnum.Nvarcahr)]
        public string Country { get; set; }

        [SqlField("Code", FrameworkTypeEnum.Nvarcahr)]
        public string Code { get; set; }

        [SqlField("IsWikipedia", FrameworkTypeEnum.Bit)]
        public bool? IsWikipedia { get; set; }

        [SqlField("IsOpenFlightsAirport", FrameworkTypeEnum.Bit)]
        public bool? IsOpenFlightsAirport { get; set; }

        [SqlField("IsOpenFlightsAirline", FrameworkTypeEnum.Bit)]
        public bool? IsOpenFlightsAirline { get; set; }

        [SqlField("IsFlagIconCss", FrameworkTypeEnum.Bit)]
        public bool? IsFlagIconCss { get; set; }

        [SqlField("WikipediaCountryUrl", FrameworkTypeEnum.Nvarcahr)]
        public string WikipediaCountryUrl { get; set; }

        [SqlField("ASFlagIcon", FrameworkTypeEnum.Nvarcahr)]
        public string ASFlagIcon { get; set; }
    }

    [SqlTable("Demo", "Language")]
    public class Language : Row
    {
        [SqlField("Id", true, FrameworkTypeEnum.Int)]
        public int Id { get; set; }

        [SqlField("LanguageName", FrameworkTypeEnum.Nvarcahr)]
        public string LanguageName { get; set; }
    }

    [SqlTable("Demo", "LanguageBuiltIn")]
    public class LanguageBuiltIn : Row
    {
        [SqlField("Id", FrameworkTypeEnum.Int)]
        public int Id { get; set; }

        [SqlField("IdName", FrameworkTypeEnum.Nvarcahr)]
        public string IdName { get; set; }

        [SqlField("LanguageName", FrameworkTypeEnum.Nvarcahr)]
        public string LanguageName { get; set; }
    }

    [SqlTable("Demo", "Navigation")]
    public class Navigation : Row
    {
        [SqlField("Id", true, FrameworkTypeEnum.Int)]
        public int Id { get; set; }

        [SqlField("ParentId", FrameworkTypeEnum.Int)]
        public int? ParentId { get; set; }

        [SqlField("Text", FrameworkTypeEnum.Nvarcahr)]
        public string Text { get; set; }

        [SqlField("PageName", FrameworkTypeEnum.Nvarcahr)]
        public string PageName { get; set; }

        [SqlField("Sort", FrameworkTypeEnum.Float)]
        public double? Sort { get; set; }
    }

    [SqlTable("Demo", "Raw.FlagIconCss.Country")]
    public class RawFlagIconCssCountry : Row
    {
        [SqlField("Code", FrameworkTypeEnum.Nvarcahr)]
        public string Code { get; set; }

        [SqlField("FlagIcon", FrameworkTypeEnum.Nvarcahr)]
        public string FlagIcon { get; set; }
    }

    [SqlTable("Demo", "Raw.OpenFlights.Airline")]
    public class RawOpenFlightsAirline : Row
    {
        [SqlField("Airline ID", FrameworkTypeEnum.Nvarcahr)]
        public string AirlineID { get; set; }

        [SqlField("Name", FrameworkTypeEnum.Nvarcahr)]
        public string Name { get; set; }

        [SqlField("Alias", FrameworkTypeEnum.Nvarcahr)]
        public string Alias { get; set; }

        [SqlField("IATA", FrameworkTypeEnum.Nvarcahr)]
        public string IATA { get; set; }

        [SqlField("ICAO", FrameworkTypeEnum.Nvarcahr)]
        public string ICAO { get; set; }

        [SqlField("Callsign", FrameworkTypeEnum.Nvarcahr)]
        public string Callsign { get; set; }

        [SqlField("Country", FrameworkTypeEnum.Nvarcahr)]
        public string Country { get; set; }

        [SqlField("Active", FrameworkTypeEnum.Nvarcahr)]
        public string Active { get; set; }
    }

    [SqlTable("Demo", "Raw.OpenFlights.Airport")]
    public class RawOpenFlightsAirport : Row
    {
        [SqlField("Airport ID", FrameworkTypeEnum.Nvarcahr)]
        public string AirportID { get; set; }

        [SqlField("Name", FrameworkTypeEnum.Nvarcahr)]
        public string Name { get; set; }

        [SqlField("City", FrameworkTypeEnum.Nvarcahr)]
        public string City { get; set; }

        [SqlField("Country", FrameworkTypeEnum.Nvarcahr)]
        public string Country { get; set; }

        [SqlField("IATA", FrameworkTypeEnum.Nvarcahr)]
        public string IATA { get; set; }

        [SqlField("ICAO", FrameworkTypeEnum.Nvarcahr)]
        public string ICAO { get; set; }

        [SqlField("Latitude", FrameworkTypeEnum.Nvarcahr)]
        public string Latitude { get; set; }

        [SqlField("Longitude", FrameworkTypeEnum.Nvarcahr)]
        public string Longitude { get; set; }

        [SqlField("Altitude", FrameworkTypeEnum.Nvarcahr)]
        public string Altitude { get; set; }

        [SqlField("Timezone", FrameworkTypeEnum.Nvarcahr)]
        public string Timezone { get; set; }

        [SqlField("DST", FrameworkTypeEnum.Nvarcahr)]
        public string DST { get; set; }

        [SqlField("Tz database time zone", FrameworkTypeEnum.Nvarcahr)]
        public string Tzdatabasetimezone { get; set; }

        [SqlField("Type", FrameworkTypeEnum.Nvarcahr)]
        public string Type { get; set; }

        [SqlField("Source", FrameworkTypeEnum.Nvarcahr)]
        public string Source { get; set; }
    }

    [SqlTable("Demo", "Raw.OpenFlights.Plane")]
    public class RawOpenFlightsPlane : Row
    {
        [SqlField("Name", FrameworkTypeEnum.Nvarcahr)]
        public string Name { get; set; }

        [SqlField("IATA Code", FrameworkTypeEnum.Nvarcahr)]
        public string IATACode { get; set; }

        [SqlField("ICAO Code", FrameworkTypeEnum.Nvarcahr)]
        public string ICAOCode { get; set; }
    }

    [SqlTable("Demo", "Raw.Wikipedia.Aircraft")]
    public class RawWikipediaAircraft : Row
    {
        [SqlField("IcaoCode", FrameworkTypeEnum.Nvarcahr)]
        public string IcaoCode { get; set; }

        [SqlField("IataCode", FrameworkTypeEnum.Nvarcahr)]
        public string IataCode { get; set; }

        [SqlField("Model", FrameworkTypeEnum.Nvarcahr)]
        public string Model { get; set; }

        [SqlField("ModelUrl", FrameworkTypeEnum.Nvarcahr)]
        public string ModelUrl { get; set; }

        [SqlField("ModelTitle", FrameworkTypeEnum.Nvarcahr)]
        public string ModelTitle { get; set; }

        [SqlField("ModelImageUrl", FrameworkTypeEnum.Nvarcahr)]
        public string ModelImageUrl { get; set; }
    }

    [SqlTable("Demo", "Raw.Wikipedia.Country")]
    public class RawWikipediaCountry : Row
    {
        [SqlField("Code", FrameworkTypeEnum.Nvarcahr)]
        public string Code { get; set; }

        [SqlField("Country", FrameworkTypeEnum.Nvarcahr)]
        public string Country { get; set; }

        [SqlField("CountryUrl", FrameworkTypeEnum.Nvarcahr)]
        public string CountryUrl { get; set; }

        [SqlField("Year", FrameworkTypeEnum.Nvarcahr)]
        public string Year { get; set; }

        [SqlField("CcTLD", FrameworkTypeEnum.Nvarcahr)]
        public string CcTLD { get; set; }

        [SqlField("CcTLDUrl", FrameworkTypeEnum.Nvarcahr)]
        public string CcTLDUrl { get; set; }

        [SqlField("Iso", FrameworkTypeEnum.Nvarcahr)]
        public string Iso { get; set; }

        [SqlField("IsoUrl", FrameworkTypeEnum.Nvarcahr)]
        public string IsoUrl { get; set; }

        [SqlField("Notes", FrameworkTypeEnum.Nvarcahr)]
        public string Notes { get; set; }
    }
}
