using FlightBooking.Core;

namespace FlightBooking.Test
{
    public class TestFlightBooking: FlightBookingTestBase
    {
        [Fact]
        public void Test1()
        {

        }
    }

    public class FlightBookingTestBase : IDisposable
    {
        private bool disposedValue;

        protected FlightRoute? TestRoute { get; set; }
        protected ScheduledFlight? TestFlight { get; set; }

        protected FlightBookingTestBase()
        {
            SetupAirlineData();
            SetupFlightData();
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

        private void SetupFlightData()
        {
            if (TestFlight != null)
            {
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Steve",
                    Age = 30
                });
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Mark",
                    Age = 12
                });
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "James",
                    Age = 36
                });
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Jane",
                    Age = 32
                });
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.LoyaltyMember,
                    Name = "John",
                    Age = 29,
                    LoyaltyPoints = 1000,
                    IsUsingLoyaltyPoints = true
                });
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.LoyaltyMember,
                    Name = "Sarah",
                    Age = 45,
                    LoyaltyPoints = 1250,
                    IsUsingLoyaltyPoints = false
                });
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.LoyaltyMember,
                    Name = "Jack",
                    Age = 60,
                    LoyaltyPoints = 50,
                    IsUsingLoyaltyPoints = false
                });
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.AirlineEmployee,
                    Name = "Trevor",
                    Age = 47
                });
                TestFlight.AddPassenger(new Passenger
                {
                    Type = PassengerType.General,
                    Name = "Alan",
                    Age = 34
                });
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
            if (!disposedValue)
            {
                if (disposing)
                {
                    TestFlight = null;
                    TestRoute = null;                    
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TestBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}