using System;
using System.Linq;
using System.Collections.Generic;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        public FlightRoute FlightRoute { get; private set; }
        public IFlightRuleSet FlightRuleSet { get; private set; }
        public Plane Aircraft { get; private set; }
        public List<IPassenger> Passengers { get; private set; }
        public int SeatsTaken => Passengers.Count;
        public double CostOfFlights => FlightRoute.BaseCost * Passengers.Count;
        public int TotalExpectedBaggage { get; set; }
        public int TotalLoyaltyPointsRedeemed { get; set; }
        public int TotalLoyaltyPointsAccrued { get; set; }
        public double ProfitFromFlight { get; set; }
        public int NoOfGeneralPassengers => Passengers.Count(p => p as IGeneralPassenger != null);
        public int NoOfLoyaltyPassengers => Passengers.Count(p => p as ILoyaltyPassenger != null);
        public int NoOfEmployeePassengers => Passengers.Count(p => p as IAirlineEmployee != null);
        public int NoOfDiscountPassengers => Passengers.Count(p => p as IDiscounted != null);
        public double ProfitSurplus => ProfitFromFlight - CostOfFlights;

        public bool DecisionToProceed => FlightRuleSet.ClearedToProceed(this);

        public ScheduledFlight(FlightRoute flightRoute, IFlightRuleSet flightRules)
        {
            FlightRoute = flightRoute;
            FlightRuleSet = flightRules;
            Passengers = new List<IPassenger>();
        }

        public void AddPassenger(IPassenger passenger)
        {
            Passengers.Add(passenger);
        }

        public void SetAircraftForRoute(Plane aircraft)
        {
            Aircraft = aircraft;
        }

        public string GetSummary()
        {
            PerformCalculations();
            return new FlightReport(this).ProduceReport();
        }

        public void PerformCalculations()
        {
            foreach (var passenger in Passengers)
            {
                BaggageIncrement(passenger);
                FlightProfitCalculator(passenger);
                LoyaltyCalculations(passenger);
            }
        }

        public void BaggageIncrement(IPassenger passenger)
        {
            if (!(passenger is IDiscounted discounted))
                TotalExpectedBaggage++;
        }

        public void FlightProfitCalculator(IPassenger passenger)
        {
            if (passenger is IDiscounted discounted)
                ProfitFromFlight += (FlightRoute.BasePrice / 2);

            else if ((passenger is ILoyaltyPassenger loyaltyPassenger) && !((ILoyaltyPassenger)passenger).IsUsingLoyaltyPoints)
                ProfitFromFlight += FlightRoute.BasePrice;

            else if (!(passenger is IAirlineEmployee airlineEmployee))
                ProfitFromFlight += FlightRoute.BasePrice;
        }

        public void LoyaltyCalculations(IPassenger passedPassenger)
        {
            if (passedPassenger is ILoyaltyPassenger loyaltyPassenger)
            {
                var passenger = (ILoyaltyPassenger)passedPassenger;

                if (passenger.IsUsingLoyaltyPoints)
                {
                    int loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(FlightRoute.BasePrice));
                    passenger.LoyaltyPoints -= loyaltyPointsRedeemed;
                    TotalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                    ProfitFromFlight -= FlightRoute.BasePrice;
                }
                else
                {
                    TotalLoyaltyPointsAccrued += FlightRoute.LoyaltyPointsGained;
                }

                BaggageIncrement(passedPassenger);
            }
        }
    }
}
