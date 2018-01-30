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
        public static GridName AirportMaster { get { return new GridName<Airport>("Master"); } }

        public static GridNameTypeRow AirportCodeLookup { get { return new GridName<Airport>("AirportCodeLookup"); } }

        public static GridNameTypeRow AirportTextLookup { get { return new GridName<Airport>("AirportTextLookup"); } }

        protected override IQueryable Query(App app, GridName gridName)
        {
            return base.Query(app, gridName);
        }

        protected override IQueryable QueryLookup(Row rowLookup, ApplicationEventArgument e)
        {
            IQueryable result = null;
            if (e.GridName == AirportCodeLookup)
            {
                Flight rowFlight = (Flight)rowLookup;
                result = UtilDataAccessLayer.Query<Airport>().Where(item => item.Code.StartsWith(rowFlight.AirportCode) | rowFlight.AirportCode == null).OrderBy(item => item.Code);
            }
            if (e.GridName == AirportTextLookup)
            {
                Flight rowFlight = (Flight)rowLookup;
                result = UtilDataAccessLayer.Query<Airport>().Where(item => item.Text.Contains(rowFlight.AirportText) | rowFlight.AirportText == null).OrderBy(item => item.Text);
            }
            return result;
        }
    }

    public partial class Flight
    {
        public static GridNameTypeRow GridNameDetail
        {
            get
            {
                return new GridName<Flight>("Detail");
            }
        }

        private void Refresh()
        {
            UtilDataAccessLayer.Execute("EXEC FlightValid"); // Execute stored procedure.
            var flight = UtilDataAccessLayer.Query<Flight>().Where(item => item.Id == this.Id).Single();
            this.AirportValid = flight.AirportValid; // Update client cell
        }

        protected override void Update(App app, GridName gridName, Index index, Row row, Row rowNew)
        {
            this.AirportValid = null;
            base.Update(app, gridName, index, row, rowNew);
            Refresh();
        }

        protected override void Insert(App app, GridName gridName, Index index, Row rowNew)
        {
            this.AirportValid = null;
            base.Insert(app, gridName, index, rowNew);
            Refresh();
        }
    }

    public partial class Flight_AirportValid
    {
        protected override void DesignCell(App app, GridName gridName, Index index, DesignCell result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                result.CssClass.Clear();
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
    }

    public partial class Flight_AirportCode
    {
        protected override void CellTextParse(App app, GridName gridName, Index index, string columnName, string text)
        {
            base.CellTextParse(app, gridName, index, columnName, text);
            //
            Airport airport = UtilDataAccessLayer.Query<Airport>().Where(item => item.Code == text).FirstOrDefault();
            if (airport != null)
            {
                Row.AirportText = airport.Text;
            }
        }

        protected override GridNameTypeRow CellLookup(ApplicationEventArgument e)
        {
            return Airport.AirportCodeLookup;
        }

        protected override void CellLookupIsClick(Row rowLookup, ApplicationEventArgument e)
        {
            Airport airport = ((Airport)rowLookup);
            Row.AirportCode = airport.Code;
            Row.AirportText = airport.Text;
        }
    }

    public partial class Flight_AirportText
    {
        protected override void CellTextParse(App app, GridName gridName, Index index, string columnName, string text)
        {
            base.CellTextParse(app, gridName, index, columnName, text);
            //
            Airport airport = UtilDataAccessLayer.Query<Airport>().Where(item => item.Text == text).FirstOrDefault();
            if (airport != null)
            {
                Row.AirportCode = airport.Code;
            }
        }

        protected override GridNameTypeRow CellLookup(ApplicationEventArgument e)
        {
            return Airport.AirportTextLookup;
        }

        protected override void CellLookupIsClick(Row rowLookup, ApplicationEventArgument e)
        {
            Airport airport = ((Airport)rowLookup);
            Row.AirportCode = airport.Code;
            Row.AirportText = airport.Text;
        }
    }

    public partial class AirportDisplay_AirportId
    {
        protected override void ColumnWidthPercent(ref double widthPercent)
        {
            widthPercent = 8;
        }
    }

    public partial class TableName
    {
        protected override void MasterIsClick(App app, GridName gridNameMaster, Row rowMaster, ref bool isReload)
        {
            if (gridNameMaster == new GridName<Database.dbo.TableName>())
            {
                var rowTableName = rowMaster as Database.dbo.TableName;
                string tableName = rowTableName.TableName2;
                if (tableName != null && tableName.IndexOf(".") != -1)
                {
                    Type typeRow = UtilFramework.TypeFromName("Database." + tableName, typeof(AppDemo), typeof(Framework.UtilFramework));
                    app.GridData.LoadDatabase(new GridNameTypeRow(typeRow, "Detail", true));
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
        protected override void DesignCell(App app, GridName gridName, Index index, DesignCell result)
        {
            result.CellEnum = GridCellEnum.Button;
        }

        protected override void CellButtonIsClick(App app, GridName gridName, Index index, Row row, string columnName, ref bool isReload)
        {
            app.PageShow<PageMessageBoxDelete>(app.AppJson, false).Init(app, gridName, index);
        }

        protected override void CellRowValueToText(App app, GridName gridName, Index index, ref string result)
        {
            result = "Button";
        }
    }

    public partial class Country_Text : Cell<Country>
    {
        protected override void DesignCell(App app, GridName gridName, Index index, DesignCell result)
        {
            result.CellEnum = GridCellEnum.Html;
        }
    }
}