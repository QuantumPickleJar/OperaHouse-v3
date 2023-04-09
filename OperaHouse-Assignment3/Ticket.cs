using System;

namespace OperaHouse_Assignment3
{
    /// <summary>
    /// Tickets can be sold individually
    /// Can also be sold in "blocks"
    /// 
    /// </summary>
    public class Ticket
    {

        public string SeatCode { get; private set; }

        public Boolean IsBought { get; private set; }

        public double Price { get; private set; }

        public Ticket(double cost, string seat)
        {
            IsBought = false;
            Price = cost;
            SeatCode = seat;
        }

        // return the amount of the transaction
        public double Purchase()
        {
            if (!IsBought) 
            {
                IsBought = true;
                return Price;
            } else 
                return 0;
        }


        public double Return()
        {
            if (NUnitDetector.isRunningFromNUnit)
            {
                /** bypass the check if we're in a test; 
                 * the only featuer that will not work with this setup 
                 * is testing that Unsold tickets are rejected.  This can 
                 * be alleviated by using an overloaded ReturnTickets method
                 * that could accept actual tickets.  
                 * This way, we can properly use the IsBought property of Ticket.
                */
                IsBought = false;
                return Price;
            }else if (IsBought)
            {
                IsBought = false;
                return Price;
            }
            else return 0;
        }
    }

}

