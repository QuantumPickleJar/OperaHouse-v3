using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperaHouse_Assignment3;

namespace OperaHouse_Assignment3
{
    public class DateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return x.EventTime.CompareTo(y.EventTime);
        }
    }

    public class PerformerComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            if (x.Performer.Name.Equals(y.Performer.Name))
            {
                // if the two names are the same, default to Fee◘
                return x.Performer.Fee.CompareTo(y.Performer.Fee);
            }
            else
                return x.Performer.Name.CompareTo(y.Performer.Name);
        }

    }

    public class StageComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            if (x.Stage.StageName.Equals(y.Stage.StageName))
            {
                // if the names are the same, use fee
                if (x.Stage.cleaningFee > y.Stage.cleaningFee)
                    return 1;

                else if (x.Stage.cleaningFee < y.Stage.cleaningFee)
                {
                    return -1;
                }
                else
                {
                    // if THOSE are the same, use the rate.
                    if (x.Stage.costPerHour > y.Stage.costPerHour)
                        return 1;

                    else if (x.Stage.costPerHour < y.Stage.costPerHour)
                    {
                        return -1;
                    }
                    // only now can we truly say they are equal
                    else return 0;
                }
            }

            // Compare by name.
            else return x.Stage.StageName.CompareTo(y.Stage.StageName);
        }

    }

    public class EventComparer : IComparer<Event>
    {
        public enum SortBy { EventTime, NumAvaliableTickets, Title, Performer, Stage};

        public SortBy SortOrder { get; set; } = SortBy.EventTime;
        public bool Descending { get; set; } = false;


        public int Compare(Event x, Event y)
        {
            int result = 0;
            switch (SortOrder)
            {
                case SortBy.EventTime:
                    // result = x.EventTime.CompareTo(y.EventTime);
                    result = new DateComparer().Compare(x, y);
                    break;
                case SortBy.NumAvaliableTickets:
                    result = x.NumAvailableTickets.CompareTo(y.NumAvailableTickets);
                    break;
                case SortBy.Title:
                    result = x.Title.CompareTo(y.Title);
                    break;
                case SortBy.Performer:
                    // use the PerformerComparer object
                    result = new PerformerComparer().Compare(x, y);
                    break;
                case SortBy.Stage:
                    result = new StageComparer().Compare(x, y);
                    break;
                default:
                    throw new ArgumentException("Invalid sort order.");
            }
            if (Descending)
            {
                result = -result;
            }
            return result;
        }
    }
}