using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private List<Ticket> Roster { get; set; }

        public Event(string title, Performer performer, int numTickets, double ticketPrice, DateTime eventTime, int durationMinutes, bool concessionSales)
        {
            this.Title = title;
            this.Performer = performer;          
            this.totalNumTickets = numTickets;
            this.regularTicketPrice = ticketPrice;
            this.EventTime = eventTime;
            this.DurationMinutes = durationMinutes;
            this.ConcessionSales = concessionSales;

            Ticket item = new Ticket(ticketPrice, "");
            this.Roster = new List<Ticket>(numTickets);

            // iteratively populate tickets per the above datum
            for (int i = 0; i < numTickets; i++)
            {
                // TODO: generate a more fitting seat code
                Roster.Add(new Ticket(ticketPrice, ""));
            }
            // update the property to reflect the roster
            NumAvailableTickets = Roster.Count(); 
           
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

        // derive the cost from the number of available tickets + price
        public double TicketSales()
        {

            double profTickets = (totalNumTickets - NumAvailableTickets) * regularTicketPrice;
            return profTickets;

            //return ((totalNumTickets - NumAvailableTickets) * regularTicketPrice);

        }

        public bool Profitable()
        {
            return Profit() > 0;
        }

        public double SellTickets(int v)
        {
            if (NumAvailableTickets > 0)
            { 
                // avoid sheer oversale
                if (NumAvailableTickets - v >= 0) 
                {
                    NumAvailableTickets -= v;
                    return v * regularTicketPrice;
                } 
                else
                {
                    // attempt to sell v tickets until run out

                    int numSold = 0;
                    for (int i = NumAvailableTickets; i > 0; i--, NumAvailableTickets--)
                    {
                        Roster[i].Purchase();
                        numSold++;
                    }
                    return numSold * regularTicketPrice;
                }
            // if we reach here, the transaction failed.
            } else return 0;
        }

        public double ReturnTickets(List<int> ticketNums)
        {
            double amtOwed = 0;
            if (ticketNums.Count >= 0)
            {
                Ticket oldTicket = null;

                foreach (int t_id in ticketNums)
                {
                    // return immediately if any are bought

                    if (Roster.TryGetValue(t_id, out oldTicket))
            {
                        // watch NumAvailableTickets
                        amtOwed = Roster[t_id].Return();
                        //amtOwed = oldTicket.Price; see new one liner!

                    }
                    else return 0;

            }
            return amtOwed;
        }
    }

    /// <summary>
    /// Tickets can be sold individually
    /// Can also be sold in "blocks"
    /// 
    /// </summary>
    internal class Ticket
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
            if (IsBought)
            {
                IsBought = false;
                return Price;
            }
            else return 0;
        }
    }

}

