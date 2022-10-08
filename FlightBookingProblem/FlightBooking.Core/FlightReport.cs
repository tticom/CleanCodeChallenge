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
            string result = "Flight summary for " + scheduledFlight.FlightRoute.Title;
            result += VERTICAL_WHITE_SPACE;

            result += "Total passengers: " + scheduledFlight.SeatsTaken;
            result += NEW_LINE;
            result += INDENTATION + "General sales: " + scheduledFlight.NoOfGeneralPassengers;
            result += NEW_LINE;
            result += INDENTATION + "Loyalty member sales: " + scheduledFlight.NoOfLoyaltyPassengers;
            result += NEW_LINE;
            result += INDENTATION + "Airline employee comps: " + scheduledFlight.NoOfEmployeePassengers;

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

            if (scheduledFlight.DecisionToProceed)
                result += "THIS FLIGHT MAY PROCEED";
            else
                result += "FLIGHT MAY NOT PROCEED";

            return result;
        }
    }
}
