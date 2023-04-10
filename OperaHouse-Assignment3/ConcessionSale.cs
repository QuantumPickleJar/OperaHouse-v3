using System.ComponentModel;

namespace OperaHouse_Assignment3
{
    public class ConcessionSale
    {
        public double UnitPrice { get; set; }
        public int Quantity { get; private set; }

        public string Description { get; private set; }

        public ConcessionSale(double price, int quantity, string description)
        {
            UnitPrice = price;
            Quantity = quantity;
            Description = description;
        }

        public double Cost() { return Quantity * UnitPrice; }
    }
}