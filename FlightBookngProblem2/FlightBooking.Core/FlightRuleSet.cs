using System.Collections.Generic;

namespace FlightBooking.Core
{
    public interface IFlightRuleSet
    {
        bool ClearedToProceed(ScheduledFlight flightInformation);
    }

    public class DefaultFlightRuleSet : IFlightRuleSet
    {
        List<IFlightRule> rules;

        public DefaultFlightRuleSet()
        {
            rules = new List<IFlightRule>
            {
                new DefaultProfitExceedsCosts(),
                new DefaultSeatAvailabilityNotExceeded(),
                new DefaultMinimumPercentageBooked()
            };
        }

        public bool ClearedToProceed(ScheduledFlight flightInformation)
        {
            var isValid = true;
            foreach (var rule in rules)
            {
                isValid &= rule.IsValid(flightInformation);
                if (!isValid)
                    return false;
            }

            return isValid;
        }
    }
}
