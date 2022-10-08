using System;

namespace FlightBooking.Core
{
    public interface IPassenger
    {
        string Name { get; set; }
        int Age { get; set; }
        int AllowedBags { get; set; }
    }

    public interface IAirlineEmployee : IPassenger
    {
        bool Paying { get; }
    }

    public interface IGeneralPassenger: IPassenger
    {
        bool Paying { get; }
    }

    public interface ILoyaltyPassenger: IPassenger
    {
        int LoyaltyPoints { get; set; }
        bool IsUsingLoyaltyPoints { get; set; }
    }

    public interface IDiscounted: IPassenger { }

    public class LoyaltyPassenger: IPassenger, ILoyaltyPassenger
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int AllowedBags { get; set; }
        public int LoyaltyPoints { get; set; }
        public bool IsUsingLoyaltyPoints { get; set; }
    }

    public class GeneralPassenger : IPassenger, IGeneralPassenger
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int AllowedBags { get; set; }
        public bool Paying => true;
    }

    public class AirlineEmployee : IPassenger, IAirlineEmployee 
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int AllowedBags { get; set; }
        public bool Paying => false;
    }

    public class DiscountPassenger : IPassenger, IDiscounted
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int AllowedBags { get { return 0; } set { throw new NotSupportedException("DiscountPassenger"); } }
        public bool Paying => true;
    }
}
