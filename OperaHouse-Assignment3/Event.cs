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
        private int totalNumTickets;
        private double regularTicketPrice;
        public DateTime EventTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Title { get; set; }
        public Performer Performer { get; set; }
        public Stage Stage { get; set; }

        public bool IsSellingConcessions { get; set; }


        //public List<ConcessionSale>? ConcessionsLog { get; private set; }
        public List<ConcessionSale> ConcessionsLog { get; private set; }

        public int NumAvailableTickets { get; private set; }

        // Holds the available pool of unsold tickets
        //private List<Ticket> Roster { get; set; }

        private Dictionary<int, Ticket> Roster { get; set; }


        public Event(string title, Performer performer, int numTickets, double ticketPrice, DateTime eventTime, int durationMinutes, bool concessionSales)
        {

            this.Title = title;
            this.Performer = performer;          
            this.totalNumTickets = numTickets;
            this.regularTicketPrice = ticketPrice;
            this.EventTime = eventTime;
            this.DurationMinutes = durationMinutes;
            this.IsSellingConcessions = concessionSales;
            // considered creating a Nullable class/struct to 
            // void populating if ConcessionSales is false
            this.ConcessionsLog = new List<ConcessionSale>();
            //this.Roster = new List<Ticket>(numTickets);
            this.Roster = new Dictionary<int, Ticket>(numTickets);


            // iteratively populate tickets per the above datum
            for (int i = 0; i < numTickets; i++)
            {
                // if (i = 0)th ticket, code will be 'A0'
                // otherwise, we use i(0) as a base code:
                string nextCode = i == 0 ? "A0" : NextSeatCode(Roster[i - 1].SeatCode);

                var tck = new Ticket(i, regularTicketPrice, nextCode);

                Roster.Add(i, tck);
                //BindTicketToSeat(ref tck);

            }
            // update the property to reflect the roster
            NumAvailableTickets = Roster.Count(); 
           
        }

        private string NextSeatCode(string prevCode)
        {
            // thanks to the ternary statement in CTOR for-loop, we can assume a non-null code
            char aisle = prevCode[0];
            int row = int.Parse(prevCode.Substring(1));


            if (row < totalNumTickets % 26)
                row++;
            else
            {
                row = 0;
                aisle = (char)(aisle + 1);
            }

            return $"{aisle}{row}";
        }



        public override string ToString()
        {
            string result = Title + " by " + Performer + " on " + EventTime.ToShortDateString();
            result += " at " + EventTime.ToShortTimeString() + ". Concessions: ";
            result += IsSellingConcessions ? "Yes. " : "No. ";
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
            return (IsSellingConcessions) ? (ConcessionSales() + TicketSales()) - ShowExpenses()
                            : TicketSales() - ShowExpenses();
        }

        private double ConcessionSales()
        {
            return ConcessionsLog.Sum(c => c.Cost());
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
       
        public double SellConcession(double cost, int quantity, string item)
        {
            ConcessionSale sale = new ConcessionSale(cost, quantity, item);
            ConcessionsLog.Add(sale);
            return sale.Cost();
        }

        public double SellTickets(int v)
        {
            if (NumAvailableTickets > 0)
            { 
                // avoid sheer oversale
                if (NumAvailableTickets - v < 0 || v > totalNumTickets) 
                {
                    // limit v to match what can be supplied
                    v = Math.Min(NumAvailableTickets, v);
                }
                // attempt to sell v tickets until run out
                int numSold = 0; 
                for (int i = 0; i < v; i++, NumAvailableTickets--)
                {
                    Roster[i].Purchase();
                    numSold++;
                }

                return numSold * regularTicketPrice;
            // if we reach here, the transaction failed.
            } else return 0;
        }
        
        /**
         * The question: 
         * in the line given we're just given a range of ints.  
         * Am I to understand that these ints should be traced 
         * to some sort of TicketID property?  
         * 
         * If not, then the manner in which we're testing ReturnTickets
         * would need to be altered.  Modifying teacher provided code 
         * previously has been a sign of "going in the wrong direction"
         * 
         *
         */
        public double ReturnTickets(List<int> ticketNums)
        {
            double amtOwed = 0;
            if (ticketNums.Count >= 0)
            {
                Ticket oldTicket = null;

                //for (int i = 0; i < ticketNums; i++)
                //{
                //    if (Roster[i].IsBought)
                //        amtOwed += Roster[i].Return();
                //}

                foreach (int t_id in ticketNums)
                {
                    if (Roster.TryGetValue(t_id, out oldTicket))
                    {
                        // watch NumAvailableTickets
                        amtOwed += Roster[t_id].Return();
                        NumAvailableTickets++;
                        //amtOwed = oldTicket.Price; see new one liner!

                    }
                    else return 0;
                }
            }
            return amtOwed;
        }
    }

}

