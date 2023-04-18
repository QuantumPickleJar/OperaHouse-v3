using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperaHouse_Assignment3;

namespace OperaHouse_Assignment3
{
    internal class DateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return x.EventTime.CompareTo(y.EventTime);
        }
    }
    
    
    


class EventComparer : IComparer<Event>
{
    public enum SortBy { EventTime, NumAvaliableTickets, Title, Performer };

    public SortBy SortOrder { get; set; } = SortBy.EventTime;
    public bool Descending { get; set; } = false;
    internal int CompareTo(Performer performer)
    {
       
    }

    public int Compare(Event x, Event y)
    {
        int result = 0;
        switch (SortOrder)
        {
            case SortBy.EventTime:
                result = x.EventTime.CompareTo(y.EventTime);
                break;
            case SortBy.NumAvaliableTickets:
                result = x.NumAvailableTickets.CompareTo(y.NumAvailableTickets);
                break;
            case SortBy.Title:
                result = x.Title.CompareTo(y.Title);
                break;
            case SortBy.Performer:
                result = x.Performer.CompareTo(y.Performer);
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

}☻