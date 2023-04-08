using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment3
{
    public class Event
    {
        private int totalNumTickets;
        private double regularTicketPrice;
        public DateTime EventTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Title { get; set; }
        public Performer Performer { get; set; }
        public Stage Stage { get; set; }
       
        public bool ConcessionSales { get; set; }
        public int NumAvailableTickets { get; private set; }

        public Event(string title, Performer performer, int numTickets, double ticketPrice, DateTime eventTime, int durationMinutes, bool concessionSales)
        {
            this.Title = title;
            this.Performer = performer;          
            this.totalNumTickets = numTickets;
            this.regularTicketPrice = ticketPrice;
            this.EventTime = eventTime;
            this.DurationMinutes = durationMinutes;
            this.ConcessionSales = concessionSales;
        }


     

        public override string ToString()
        {
            string result = Title + " by " + Performer + " on " + EventTime.ToShortDateString();
            result += " at " + EventTime.ToShortTimeString() + ". Concessions: ";
            result += ConcessionSales ? "Yes. " : "No. ";
            result += "Tickets available: " + NumAvailableTickets;
            return result;
        }

    

        public bool IsWeekend()
        {
            if (EventTime.DayOfWeek == DayOfWeek.Sunday || EventTime.DayOfWeek == DayOfWeek.Saturday)
                return true;
            else return false;

        }

        public double ShowExpenses()
        {
            double stageCost = Stage.costPerHour * DurationMinutes/60.0 + Stage.cleaningFee;
            if (!IsWeekend())
                stageCost *= 0.9;
            return stageCost + Performer.Fee;

        }

        public double Profit()
        {
            return TicketSales() - ShowExpenses();
        }

        private double TicketSales()
        {
            throw new NotImplementedException();
        }

        public bool Profitable()
        {
            return Profit() > 0;
        }

        public double SellTickets(int v)
        {
            if (NumAvailableTickets > 0) {
                // make sure we don't oversell tickets
                if (NumAvailableTickets - v > 0) {
                    NumAvailableTickets -= v;
                    return v * regularTicketPrice;
                }
                // if we reach here, the transaction failed.
                return 0;
            } else 
                return 0;
        }
    }

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
            if (!IsBought) {
                IsBought = true;
                return Price;
            } else 
                return 0;
        }

    }

}

