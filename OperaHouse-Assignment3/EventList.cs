using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment3
{
    public class EventList
    {
        private List<Event> events;

        public void AddEvent(Event e)
        {
            throw new NotImplementedException();
        }

        public void AddEvent(List<Event> events)
        {
            throw new NotImplementedException();
        }


        public void SortByDate()
        {
            throw new NotImplementedException();
        }

        public void SortByTitle()
        {
            throw new NotImplementedException();
        }

        public List<Event> SearchByPerformer(string name)
        {
            throw new NotImplementedException();
        }

        public List<Event> OpenShows()
        {
            throw new NotImplementedException();
        }

        public void Sort()
        {
            throw new NotImplementedException();
        }

        public List<Event> ShowsShorterThan(int minutes)
        {
            throw new NotImplementedException();
        }

        //Helper method that returns the IDs of all the events in the order listed in the List 
        //This is for testing purposes only
        public string[] eventIDs()
        {
            string[] ids = new string[events.Count];
            int i = 0;
            foreach (Event e in events)
            {
                ids[i] = e.ID;
                i++;
            }
            return ids;
        }

    }
}
