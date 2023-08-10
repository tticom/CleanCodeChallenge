using System;

namespace FlightBooking.Core
{
    public class FlightReport
    {
        private readonly string VERTICAL_WHITE_SPACE = Environment.NewLine + Environment.NewLine;
        private readonly string NEW_LINE = Environment.NewLine;
        private const string INDENTATION = "    ";

        private ScheduledFlight scheduledFlight { get; set; }

        public FlightReport(ScheduledFlight flight)
        {
            scheduledFlight = flight;
        }

        public string ProduceReport()
        {
            if (scheduledFlight.DecisionToProceed)
                return FlightMayProceedReport();
            else
                return AlternativeFlightsReport();
        }

        public string FlightMayProceedReport()
        {
            string result = "Flight summary for " + scheduledFlight.FlightRoute.Title;
            result += VERTICAL_WHITE_SPACE;

            result += "Total passengers: " + scheduledFlight.SeatsTaken;
            result += NEW_LINE;
            result += INDENTATION + "General sales: " + scheduledFlight.NoOfGeneralPassengers;
            result += NEW_LINE;
            result += INDENTATION + "Loyalty member sales: " + scheduledFlight.NoOfLoyaltyPassengers;
            result += NEW_LINE;
            result += INDENTATION + "Airline employee comps: " + scheduledFlight.NoOfEmployeePassengers;
            if (scheduledFlight.NoOfDiscountPassengers > 0)
            {
                result += NEW_LINE;
                result += INDENTATION + "Discounted sales: " + scheduledFlight.NoOfDiscountPassengers;
            }

            result += VERTICAL_WHITE_SPACE;
            result += "Total expected baggage: " + scheduledFlight.TotalExpectedBaggage;

            result += VERTICAL_WHITE_SPACE;

            result += "Total revenue from flight: " + scheduledFlight.ProfitFromFlight;
            result += NEW_LINE;
            result += "Total costs from flight: " + scheduledFlight.CostOfFlights.ToString();
            result += NEW_LINE;

            result += (scheduledFlight.ProfitSurplus > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + scheduledFlight.ProfitSurplus;

            result += VERTICAL_WHITE_SPACE;

            result += "Total loyalty points given away: " + scheduledFlight.TotalLoyaltyPointsAccrued + NEW_LINE;
            result += "Total loyalty points redeemed: " + scheduledFlight.TotalLoyaltyPointsRedeemed + NEW_LINE;

            result += VERTICAL_WHITE_SPACE;

            result += "THIS FLIGHT MAY PROCEED";

            return result;

        }

        public string AlternativeFlightsReport()
        {

            string result = "FLIGHT MAY NOT PROCEED";

            var aircraft = AircraftFactory.FactoryMethod().SelectAlternativeAircraft(scheduledFlight);

            if (aircraft != null)
            {
                result += NEW_LINE;
                result += "Other more suitable aircraft are: ";
                result += NEW_LINE;
                foreach(var plane in aircraft)                
                {
                    result += NEW_LINE;
                    result += INDENTATION + plane.Name + " could handle this flight";
                }
                result += NEW_LINE;
            }

            return result;
        }
    }
}
