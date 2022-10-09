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
            rules = new List<IFlightRule>();
            rules.Add(new DefaultProfitExceedsCosts());
            rules.Add(new DefaultSeatAvailabilityNotExceeded());
            rules.Add(new DefaultMinimumPercentageBooked());
        }

        public bool ClearedToProceed(ScheduledFlight flightInformation)
        {
            var isValid = true;
            foreach (var rule in rules)
                isValid &= rule.IsValid(flightInformation);
            return isValid;
        }
    }
}
