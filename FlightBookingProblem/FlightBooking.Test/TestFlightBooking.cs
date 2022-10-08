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
            SetupOriginalAllFlightData();
            var report = Regex.Replace(TestFlight.GetSummary(), @"\s", "");
            Assert.Equal(expected, report);
        }

        [Fact]
        public void Test_Benchmark_SeatsTaken()
        {
            var expected = 10;
            SetupOriginalAllFlightData();
            Assert.Equal(expected, TestFlight.SeatsTaken);
        }

        [Fact]
        public void Test_Benchmark_CostOfFlights()
        {
            var expected = 500d;
            SetupOriginalAllFlightData();
            Assert.Equal(expected, TestFlight.CostOfFlights);
        }

        [Fact]
        public void Test_Benchmark_NoOfGeneralPassenger()
        {
            var expected = 6;
            SetupOriginalAllFlightData();
            Assert.Equal(expected, TestFlight.NoOfGeneralPassengers);
        }

        [Fact]
        public void Test_Benchmark_NoOfLoyaltyPassenger()
        {
            var expected = 3;
            SetupOriginalAllFlightData();
            Assert.Equal(expected, TestFlight.NoOfLoyaltyPassengers);
        }

        [Fact]
        public void Test_Benchmark_NoOfEmployeePassenger()
        {
            var expected = 1;
            SetupOriginalAllFlightData();
            Assert.Equal(expected, TestFlight.NoOfEmployeePassengers);
        }

        [Fact]
        public void Test_Benchmark_ProfitSurplus()
        {
            var expected = 300;
            SetupOriginalAllFlightData();
            TestFlight.PerformCalculations();
            Assert.Equal(expected, TestFlight.ProfitSurplus);
        }

        [Fact]
        public void Test_Benchmark_DecisionToProceed()
        {
            var expected = true;
            SetupOriginalAllFlightData();
            TestFlight.PerformCalculations();
            Assert.Equal(expected, TestFlight.DecisionToProceed);
        }

        [Fact]
        public void Test_Discounted_CostOfFlights()
        {
            var expected = 550d;
            SetupOriginalAllFlightData();
            AddDiscountedPasseger();
            Assert.Equal(expected, TestFlight.CostOfFlights);
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

        protected void SetupOriginalAllFlightData()
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

        protected void AddDiscountedPasseger()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new DiscountPassenger
                {
                    Name = "Dave",
                    Age = 300
                });
            }
        }

        protected void AddGeneralPassengerSteveToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new GeneralPassenger
                {
                    Name = "Steve",
                    Age = 30
                });
            }
        }

        protected void AddGeneralPassengerMarkToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new GeneralPassenger
                {
                    Name = "Mark",
                    Age = 12
                });
            }
        }

        protected void AddGeneralPassengerJamesToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new GeneralPassenger
                {
                    Name = "James",
                    Age = 36
                });
            }
        }

        protected void AddGeneralPassengerJaneToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new GeneralPassenger
                {
                    Name = "Jane",
                    Age = 32
                });
            }
        }

        protected void AddLoyaltyMemberPassengerJohnToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new LoyaltyPassenger
                {
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
                TestFlight.AddPassenger(new LoyaltyPassenger
                {
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
                TestFlight.AddPassenger(new LoyaltyPassenger
                {
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
                TestFlight.AddPassenger(new AirlineEmployee
                {
                    Name = "Trevor",
                    Age = 47
                });
            }
        }

        protected void AddGeneralPassengerAlanToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new GeneralPassenger
                {
                    Name = "Alan",
                    Age = 34
                });
            }
        }

        protected void AddGeneralPassengerSuzyToFlight()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new GeneralPassenger
                {
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