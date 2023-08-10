namespace FlightBooking.Core
{
    public class FlightRoute
    {
        private readonly string _origin;
        private readonly string _destination;

        public FlightRoute(string origin, string destination)
        {
            _origin = origin;
            _destination = destination;
        }

        public string Title { get { return _origin + " to " + _destination; } }
        public double BasePrice { get; set; }
        public double BaseCost { get; set; }
        public int LoyaltyPointsGained { get; set; }
        public double MinimumTakeOffPercentage { get; set; }        
    }
}
