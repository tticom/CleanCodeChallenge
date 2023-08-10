namespace FlightBooking.Core
{
    public interface IFlightRule
    {
        bool IsValid(ScheduledFlight flightInformation);
    }

    public class DefaultProfitExceedsCosts : IFlightRule
    {
        public bool IsValid(ScheduledFlight flightInformation)
        {
            return flightInformation.ProfitSurplus > 0;
        }
    }

    public class DefaultSeatAvailabilityNotExceeded : IFlightRule
    {
        public bool IsValid(ScheduledFlight flightInformation)
        {
            return flightInformation.SeatsTaken <= flightInformation.Aircraft.NumberOfSeats;
        }
    }

    public class DefaultMinimumPercentageBooked : IFlightRule
    {
        public bool IsValid(ScheduledFlight flightInformation)
        {
            return flightInformation.SeatsTaken / (double)flightInformation.Aircraft.NumberOfSeats > flightInformation.FlightRoute.MinimumTakeOffPercentage;
        }
    }
}
