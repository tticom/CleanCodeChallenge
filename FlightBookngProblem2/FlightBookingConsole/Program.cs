using System;
using FlightBooking.Core;

namespace FlightBookingProblem
{
    class Program
    {
        private static ScheduledFlight _scheduledFlight;
        private static AircraftFactory _aircraftFactory = AircraftFactory.FactoryMethod();

        static void Main(string[] args)
        {
            SetupAirlineData();

            string command;
            do
            {
                command = Console.ReadLine() ?? "";
                var enteredText = command.ToLower();
                if (enteredText.Contains("print summary"))
                {
                    Console.WriteLine();
                    Console.WriteLine(_scheduledFlight.GetSummary());
                }
                else if (enteredText.Contains("add general"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new GeneralPassenger
                    {
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3])
                    });
                }
                else if (enteredText.Contains("add loyalty"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new LoyaltyPassenger
                    {
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3]),
                        LoyaltyPoints = Convert.ToInt32(passengerSegments[4]),
                        IsUsingLoyaltyPoints = Convert.ToBoolean(passengerSegments[5]),
                    });
                }
                else if (enteredText.Contains("add discounted"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new DiscountPassenger
                    {
                        Name = passengerSegments[2],
                        Age = Convert.ToInt32(passengerSegments[3]),
                    });
                }
                else if (enteredText.Contains("add airline"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new AirlineEmployee
                    {
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3]),
                    });
                }
                else if (enteredText.Contains("exit"))
                {
                    Environment.Exit(1);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("UNKNOWN INPUT");
                    Console.ResetColor();
                }
            } while (command != "exit");
        }

        private static void SetupAirlineData()
        {
            FlightRoute londonToParis = new FlightRoute("London", "Paris")
            {
                BaseCost = 50, 
                BasePrice = 100, 
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };

            _scheduledFlight = new ScheduledFlight(londonToParis, new DefaultFlightRuleSet());
            _scheduledFlight.SetAircraftForRoute(_aircraftFactory.SelectSuitableAircraft(_scheduledFlight));
        }
    }
}
