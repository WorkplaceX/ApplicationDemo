namespace Database.dbo
{
    using System;
    using System.Collections.Generic;
    using Framework.DataAccessLayer;
    using Application;
    using Framework.Application;
    using Framework.Component;
    using Framework;
    using System.Linq;

    public partial class Airport
    {
        public static GridNameTypeRow GridName { get { return new GridName<Airport>(); } }

        public static GridNameTypeRow GridNameCodeLookup { get { return new GridName<Airport>("CodeLookup"); } }

        public static GridNameTypeRow GridNameTextLookup { get { return new GridName<Airport>("TextLookup"); } }

        protected override IQueryable Query(App app, GridName gridName)
        {
            Flight flight = app.GridData.RowSelected(Flight.GridName);
            string airportCode = flight?.AirportCode;
            return UtilDataAccessLayer.Query<Airport>().Where(item => airportCode == null | item.Code == airportCode);
        }

        protected override IQueryable QueryLookup(Row rowLookup, AppEventArg e)
        {
            IQueryable result = null;
            if (e.GridName == GridNameCodeLookup)
            {
                Flight rowFlight = (Flight)rowLookup;
                if (rowFlight.AirportCode == null)
                {
                    result = UtilDataAccessLayer.Query<Airport>().OrderBy(item => item.Code); // Show all
                }
                else
                {
                    result = UtilDataAccessLayer.Query<Airport>().Where(item => item.Code.StartsWith(rowFlight.AirportCode)).OrderBy(item => item.Code);
                }
            }
            if (e.GridName == GridNameTextLookup)
            {
                Flight rowFlight = (Flight)rowLookup;
                if (rowFlight.AirportText == null)
                {
                    result = UtilDataAccessLayer.Query<Airport>().OrderBy(item => item.Text); // Show all
                }
                else
                {
                    result = UtilDataAccessLayer.Query<Airport>().Where(item => item.Text.Contains(rowFlight.AirportText)).OrderBy(item => item.Text);
                }
            }
            return result;
        }

        protected override void MasterIsClick(GridName gridNameMaster, Row rowMaster, ref bool isReload, AppEventArg e)
        {
            Flight flight = rowMaster as Flight;
            if (flight != null)
            {
                if (e.GridName == Airport.GridName)
                {
                    isReload = true;
                }
            }
        }
    }

    public partial class Flight
    {
        public static GridName<Flight> GridName
        {
            get
            {
                return new GridName<Flight>();
            }
        }

        private void Refresh()
        {
            UtilDataAccessLayer.Execute("EXEC FlightValid"); // Execute stored procedure.
            var flight = UtilDataAccessLayer.Query<Flight>().Where(item => item.Id == this.Id).Single();
            this.AirportValid = flight.AirportValid; // Update client cell
        }

        protected override void Update(Row row, Row rowNew, AppEventArg e)
        {
            this.AirportValid = null;
            base.Update(row, rowNew, e);
            Refresh();
        }

        protected override void Insert(Row rowNew, AppEventArg e)
        {
            this.AirportValid = null;
            base.Insert(rowNew, e);
            Refresh();
        }

        [SqlColumn(null, typeof(Flight_Delete))]
        public string Delete { get; set; }
    }

    public class Flight_Delete : Cell<Flight>
    {
        protected override void ConfigCell(ConfigCell result, AppEventArg e)
        {
            result.CellEnum = GridCellEnum.Button;
        }

        protected override void ButtonIsClick(ref bool isReload, AppEventArg e)
        {
            {
                //Flight rowNew = UtilDataAccessLayer.RowClone(Row);
                //rowNew.AirlineCode = "ZRH";
                //UtilDataAccessLayer.Update(Row, rowNew);
                //isReload = true;
            }
            {
                e.App.PageShow<PageMessageBoxDelete>(e.App.AppJson, false).Init(e);
                //UtilDataAccessLayer.Delete(Row);
                //isReload = true;
            }
        }
    }

    public partial class Flight_AirportValid
    {
        protected override void ConfigCell(ConfigCell result, AppEventArg e)
        {
            result.IsReadOnly = true;
            // There can be only one background image!
            result.CssClass.Remove("gridOk");
            result.CssClass.Remove("gridError");
            result.CssClass.Remove("gridReadOnly");
            if (Row.AirportValid != null)
            {
                if (Row.AirportValid == "Ok")
                {
                    result.CssClass.Add("gridOk");
                }
                else
                {
                    result.CssClass.Add("gridError");
                }
            }
        }
    }

    public partial class Flight_AirlineText
    {
        protected override void ConfigCell(ConfigCell result, AppEventArg e)
        {
            result.IsReadOnly = true;
            result.CssClass.Add("gridReadOnly");
        }
    }

    public partial class Flight_AirportCode
    {
        protected override void TextParse(string text, AppEventArg e)
        {
            base.TextParse(text, e);
            //
            Airport airport = UtilDataAccessLayer.Query<Airport>().Where(item => item.Code == text).FirstOrDefault();
            if (airport != null)
            {
                Row.AirportText = airport.Text;
            }
        }

        protected override GridNameTypeRow Lookup(AppEventArg e)
        {
            return Airport.GridNameCodeLookup;
        }

        protected override void LookupIsClick(Row rowLookup, AppEventArg e)
        {
            Airport airport = ((Airport)rowLookup);
            Row.AirportCode = airport.Code;
            Row.AirportText = airport.Text;
        }
    }

    public partial class Flight_AirportText
    {
        protected override void TextParse(string text, AppEventArg e)
        {
            base.TextParse(text, e);
            //
            Airport airport = UtilDataAccessLayer.Query<Airport>().Where(item => item.Text == text).FirstOrDefault();
            if (airport != null)
            {
                Row.AirportCode = airport.Code;
            }
        }

        protected override GridNameTypeRow Lookup(AppEventArg e)
        {
            return Airport.GridNameTextLookup;
        }

        protected override void LookupIsClick(Row rowLookup, AppEventArg e)
        {
            Airport airport = ((Airport)rowLookup);
            Row.AirportCode = airport.Code;
            Row.AirportText = airport.Text;
        }
    }

    public partial class AirportDisplay_AirportId
    {
        protected override void WidthPercent(ref double widthPercent)
        {
            widthPercent = 8;
        }
    }

    public partial class TableName
    {
        protected override void MasterIsClick(GridName gridNameMaster, Row rowMaster, ref bool isReload, AppEventArg e)
        {
            if (gridNameMaster == new GridName<Database.dbo.TableName>())
            {
                var rowTableName = rowMaster as Database.dbo.TableName;
                string tableName = rowTableName.TableName2;
                if (tableName != null && tableName.IndexOf(".") != -1)
                {
                    Type typeRow = UtilFramework.TypeFromName("Database." + tableName, typeof(AppDemo), typeof(Framework.UtilFramework));
                    // app.GridData.LoadDatabase(new GridNameTypeRow(typeRow, "Detail", true));
                }
            }
        }
    }


    public partial class AirportDisplay_CountryText
    {

    }

    public partial class Country : Row
    {
        [SqlColumn(null, typeof(Country_ButtonDelete))]
        public string ButtonDelete { get; set; }
    }

    public partial class Country_ButtonDelete : Cell<Country>
    {
        protected override void ConfigCell(ConfigCell result, AppEventArg e)
        {
            result.CellEnum = GridCellEnum.Button;
        }

        protected override void ButtonIsClick(ref bool isReload, AppEventArg e)
        {
            e.App.PageShow<PageMessageBoxDelete>(e.App.AppJson, false).Init(e);
        }

        protected override void RowValueToText(ref string result, AppEventArg e)
        {
            result = "Button";
        }
    }

    public partial class Country_Text : Cell<Country>
    {
        protected override void ConfigCell(ConfigCell result, AppEventArg e)
        {
            result.CellEnum = GridCellEnum.Html;
        }
    }
}