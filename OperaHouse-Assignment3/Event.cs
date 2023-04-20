using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment3
{
    public class Event
    {
        private string id;
        private int totalNumTickets;
        private double regularTicketPrice;

        public string ID { get { return id; } }
        public DateTime EventTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Title { get; set; }
        public Performer Performer { get; set; }
        public Stage Stage { get; set; }

        public bool ConcessionSales { get; set; }

        public int NumAvailableTickets
        {
            get
            {
                int result = 0;
                foreach (Ticket t in tickets)
                {
                    if (!t.Sold)
                        result++;
                }
                return result;
            }
        }

        public List<Ticket> tickets;

        public Event(string id, string title, Stage stage, Performer performer, int numTickets, double ticketPrice, DateTime eventTime, int durationMinutes, bool concessionSales)
        {
            this.id = id;
            this.Title = title;
            this.Stage = stage; 
            this.Performer = performer;
            this.totalNumTickets = numTickets;
            this.regularTicketPrice = ticketPrice;
            this.EventTime = eventTime;
            this.DurationMinutes = durationMinutes;
            this.ConcessionSales = concessionSales;
            tickets = new List<Ticket>();
            for (int i = 1; i <= numTickets; i++)
            {
                tickets.Add(new Ticket(i, ticketPrice));
            }
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
            double stageCost = Stage.costPerHour * DurationMinutes / 60.0 + Stage.cleaningFee;
            if (!IsWeekend())
                stageCost *= 0.9;
            return stageCost + Performer.Fee;
        }

        public double Profit()
        {
            return TicketSales() - ShowExpenses();
        }

        public bool Profitable()
        {
            return Profit() > 0;
        }

        public double SellTickets(int numTickets)
        {
            double sales = 0;
            int numSold = 0;
            foreach (Ticket t in tickets)
            {
                if (!t.Sold)
                {
                    sales += t.Price;
                    numSold++;
                    t.Sold = true;
                }
                if (numSold == numTickets)
                    return sales;
            }
            return 0;
        }

        public double ReturnTickets(List<int> ticketNums)
        {
            double result = 0;
            foreach (int i in ticketNums)
            {
                foreach (Ticket t in tickets)
                {
                    if (t.SeatNumber == i && t.Sold)
                    {
                        result += t.Price;
                        t.Sold = false;
                    }
                }
            }
            return result;
        }

        public double TicketSales()
        {
            double result = 0;
            foreach (Ticket t in tickets)
            {
                if (t.Sold)
                    result += t.Price;
            }
            return result;
        }

        /// <summary>
        /// Returns true if the number of available tickets is 0
        /// </summary>
        public bool IsSoldOut
        {
            get { return (NumAvailableTickets == 0); }

        }

    }

}

