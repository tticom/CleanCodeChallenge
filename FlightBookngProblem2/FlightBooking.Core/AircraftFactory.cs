using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooking.Core
{
    public class NoSuitablePlaneException : Exception
    {
        public NoSuitablePlaneException() : base() { }
        public NoSuitablePlaneException(string message) : base(message) { }
        public NoSuitablePlaneException(string message, Exception innerException) : base(message, innerException) { }
    }

    interface IAircraftSelectionStrategy
    {
        Plane SelectPlane(IEnumerable<Plane> aircraft, ScheduledFlight flightInformation);
        IEnumerable<Plane> SelectAlternativePlanes(IEnumerable<Plane> aircraft, ScheduledFlight flightInformation);
    }

    class AircraftSelectionStrategy : IAircraftSelectionStrategy
    {
        public Plane SelectPlane(IEnumerable<Plane> aircraft, ScheduledFlight flightInformation)
        {
            var suitablePlane = aircraft
                .Where(plane => plane.NumberOfSeats >= flightInformation.SeatsTaken)
                .OrderBy(plane => plane.NumberOfSeats)
                .FirstOrDefault();

            if (suitablePlane != null)
            {
                return suitablePlane;
            }
            return null;
        }

        public IEnumerable<Plane> SelectAlternativePlanes(IEnumerable<Plane> aircraft, ScheduledFlight flightInformation)
        {
            var suitableAircraft = aircraft
                .Where(plane => plane.NumberOfSeats > flightInformation.SeatsTaken)
                .OrderBy(plane => plane.NumberOfSeats);

            if (suitableAircraft != null)
            {
                return suitableAircraft;
            }
            return null;
        }
    }

    public class AircraftFactory
    {
        private IEnumerable<Plane> aircraft;
        private IAircraftSelectionStrategy strategy;

        private AircraftFactory()
        {
            aircraft = new List<Plane> {
                new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12 },
                new Plane { Id = 456, Name = "ATR 640", NumberOfSeats = 44 },
                new Plane { Id = 789, Name = "Bombardier Q400", NumberOfSeats = 76 }
            };

            strategy = new AircraftSelectionStrategy();
        }

        public static AircraftFactory FactoryMethod()
        {
            return new AircraftFactory();
        }

        public Plane SelectSuitableAircraft(ScheduledFlight scheduledFlight)
        {
            return strategy.SelectPlane(aircraft, scheduledFlight);
        }

        public IEnumerable<Plane> SelectAlternativeAircraft(ScheduledFlight scheduledFlight)
        {
            return strategy.SelectAlternativePlanes(aircraft, scheduledFlight);
        }
    }
}
