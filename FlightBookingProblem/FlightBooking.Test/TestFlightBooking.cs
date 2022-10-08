using FlightBooking.Core;
using System.Text.RegularExpressions;
using Xunit;

namespace FlightBooking.Test
{
    public class TestFlightBooking: FlightBookingTestBase
    {
        [Fact]
        public void Test_Benchmark_OriginalReport()
        {
            var expected = "Flight summary for London to ParisTotal passengers: 10    General sales: 6    Loyalty member sales: 3    Airline employee comps: 1Total expected baggage: 13Total revenue from flight: 800Total costs from flight: 500Flight generating profit of: 300Total loyalty points given away: 10Total loyalty points redeemed: 100THIS FLIGHT MAY PROCEED".Replace(" ", "");
            SetupAllFlightData();
            var report = Regex.Replace(TestFlight.GetSummary(), @"\s", "");
            Assert.Equal(expected, report);
        }
    }

    public class FlightBookingTestBase : IDisposable
    {
        protected FlightRoute? TestRoute { get; set; }
        protected ScheduledFlight? TestFlight { get; set; }

        protected FlightBookingTestBase()
        {
            SetupAirlineData();            
        }

        private void SetupAirlineData()
        {
            TestRoute = new FlightRoute("London", "Paris")
            {
                BaseCost = 50,
                BasePrice = 100,
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };

            TestFlight = new ScheduledFlight(TestRoute);

            TestFlight.SetAircraftForRoute(
                    new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12 });
        }

        protected void SetupAllFlightData()
        {
            AddGeneralPassengerSteveToFlight();
            AddGeneralPassengerMarkToFlight();
            AddGeneralPassengerJamesToFlight();
            AddGeneralPassengerJaneToFlight();
            AddLoyaltyMemberPassengerJohnToFlight();
            AddLoyaltyMemberPassengerSarahToFlight();
            AddLoyaltyMemberPassengerJackToFlight();
            AddAirlineEmployeePassengerTrevorToFlight();
            AddGeneralPassengerAlanToFlight();
            AddGeneralPassengerSuzyToFlight();
        }

        protected void AddGeneralPassengerSteveToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Steve",
                    Age = 30
                });
            }
        }

        protected void AddGeneralPassengerMarkToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Mark",
                    Age = 12
                });
            }
        }

        protected void AddGeneralPassengerJamesToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "James",
                    Age = 36
                });
            }
        }

        protected void AddGeneralPassengerJaneToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Jane",
                    Age = 32
                });
            }
        }

        protected void AddLoyaltyMemberPassengerJohnToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.LoyaltyMember,
                    Name = "John",
                    Age = 29,
                    LoyaltyPoints = 1000,
                    IsUsingLoyaltyPoints = true
                });
            }
        }

        protected void AddLoyaltyMemberPassengerSarahToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.LoyaltyMember,
                    Name = "Sarah",
                    Age = 45,
                    LoyaltyPoints = 1250,
                    IsUsingLoyaltyPoints = false
                });
            }
        }

        protected void AddLoyaltyMemberPassengerJackToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.LoyaltyMember,
                    Name = "Jack",
                    Age = 60,
                    LoyaltyPoints = 50,
                    IsUsingLoyaltyPoints = false
                });
            }
        }

        protected void AddAirlineEmployeePassengerTrevorToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.AirlineEmployee,
                    Name = "Trevor",
                    Age = 47
                });
            }
        }

        protected void AddGeneralPassengerAlanToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Alan",
                    Age = 34
                });
            }
        }

        protected void AddGeneralPassengerSuzyToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Suzy",
                    Age = 21
                });
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                TestFlight = null;
                TestRoute = null;                    
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}