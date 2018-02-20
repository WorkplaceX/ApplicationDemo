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
        public static GridNameWithType GridName { get { return new GridName<Airport>(); } }

        public static GridNameWithType GridNameCodeLookup { get { return new GridName<Airport>("CodeLookup"); } }

        public static GridNameWithType GridNameTextLookup { get { return new GridName<Airport>("TextLookup"); } }

        //protected override IQueryable Query(App app, GridName gridName)
        //{
        //    Flight flight = app.GridData.RowSelected(Flight.GridName);
        //    string airportCode = flight?.AirportCode;
        //    return UtilDataAccessLayer.Query<Airport>().Where(item => airportCode == null | item.Code == airportCode);
        //}

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

    public partial class Airline
    {
        public static GridName<Airline> AirlineLookup = new GridName<Airline>("Lookup");
    }

    public partial class Flight_AirlineText
    {
        protected override void ConfigCell(ConfigCell result, AppEventArg e)
        {
            // result.IsReadOnly = true;
            // result.CssClass.Add("gridReadOnly");
        }

        protected override void Lookup(out GridNameWithType gridName, out IQueryable query)
        {
            gridName = Airline.AirlineLookup;
            if (Row.AirlineText == null)
            {
                query = UtilDataAccessLayer.Query<Airline>().OrderBy(item => item.Text);
            }
            else
            {
                query = UtilDataAccessLayer.Query<Airline>().Where(item => item.Text.Contains(Row.AirlineText)).OrderBy(item => item.Text);
            }
        }

        protected override void LookupIsClick(Row rowLookup, AppEventArg e)
        {
            Airline airline = (Airline)rowLookup;
            Row.AirlineText = airline.Text;
            Row.AirlineCode = airline.Code;
        }
    }

    public partial class Flight_AirportCode
    {
        protected override void TextParse(ref string text, bool isDeleteKey, AppEventArg e)
        {
            base.TextParse(ref text, isDeleteKey, e);
            //
            string textLocal = text;
            Airport airport = UtilDataAccessLayer.Query<Airport>().Where(item => item.Code == textLocal).FirstOrDefault();
            if (airport != null)
            {
                Row.AirportText = airport.Text;
            }
        }

        protected override void Lookup(out GridNameWithType gridName, out IQueryable query)
        {
            gridName = Airport.GridNameCodeLookup;
            if (Row.AirportCode == null)
            {
                query = UtilDataAccessLayer.Query<Airport>().OrderBy(item => item.Code); // Show all
            }
            else
            {
                query = UtilDataAccessLayer.Query<Airport>().Where(item => item.Code.StartsWith(Row.AirportCode)).OrderBy(item => item.Code);
            }
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
        protected override void TextParse(ref string text, bool isDeleteKey, AppEventArg e)
        {
            base.TextParse(ref text, isDeleteKey, e);
            //
            string textLocal = text;
            Airport airport = UtilDataAccessLayer.Query<Airport>().Where(item => item.Text == textLocal).FirstOrDefault();
            if (airport != null)
            {
                Row.AirportCode = airport.Code;
            }
        }

        protected override void Lookup(out GridNameWithType gridName, out IQueryable query)
        {
            gridName = Airport.GridNameTextLookup;
            if (Row.AirportText == null)
            {
                query = UtilDataAccessLayer.Query<Airport>().OrderBy(item => item.Text); // Show all
            }
            else
            {
                query = UtilDataAccessLayer.Query<Airport>().Where(item => item.Text.Contains(Row.AirportText)).OrderBy(item => item.Text);
            }
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
                    // app.GridData.LoadDatabase(new GridNameWithType(typeRow, "Detail", true));
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