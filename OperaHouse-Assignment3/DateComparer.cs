using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment3
{
    internal class DateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return x.EventTime.CompareTo(y.EventTime);
        }
    }
}
