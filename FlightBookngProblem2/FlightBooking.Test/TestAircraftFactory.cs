using FlightBooking.Core;

namespace FlightBooking.Test
{
    public class TestAircraftFactory : FlightBookingTestBase
    {
        [Fact]
        public void Test_AircraftFactory_SelectSuitableAircraft_ReturnsAntonovskyIfNoSeatsAreTaken()
        {
            var expected = Antonovsky;
            SetupOriginalAllFlightData();
            var plane = AircraftFactoryMethod.SelectSuitableAircraft(TestFlight);
            Assert.Equal(expected.Name, plane.Name);
        }

        [Fact]
        public void Test_AircraftFactory_SelectSuitableAircraft_ReturnsATRIfFourteenSeatsAreTaken()
        {
            var expected = ATR;
            SetupOriginalAllFlightData();
            AddAllDiscountedPassegers();
            var plane = AircraftFactoryMethod.SelectSuitableAircraft(TestFlight);
            Assert.Equal(expected.Name, plane.Name);
        }

        [Fact]
        public void Test_AircraftFactory_SelectSuitableAircraft_ReturnsQ400IfFiftySeatsAreTaken()
        {
            var expected = Q400;
            SetupOriginalAllFlightData();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            AddAllDiscountedPassegers();
            var plane = AircraftFactoryMethod.SelectSuitableAircraft(TestFlight);
            Assert.Equal(expected.Name, plane.Name);
        }
    }
}
