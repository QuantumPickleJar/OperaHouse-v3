using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment3 
{
    
    /// <summary>
    /// Designed to encapsulate a collection of Event objects
    /// and allowed for performing sorting and seraching on the events.
    /// </summary>
    public class EventList
    {
        private List<Event> events;

        public EventList()
        {
            events = new List<Event>();
        }

        public void AddEvent(Event e)
        {
            events.Add(e);
        }

        public void AddEvent(List<Event> events)
        {
            this.events.AddRange(events);
        }

        public void SortByStage()
        {
            events.Sort(new StageComparer());
        }

        public void SortByDate()
        {
            events.Sort(new DateComparer());
        }
        public void SortByPerformer()
        {
            events.Sort(new PerformerComparer());
        }

        public void SortByTitle()
        {
            events.Sort(new EventComparer() { SortOrder = EventComparer.SortBy.Title });
        }

        public List<Event> SearchByStage(string stagename)
        {
            return events.Where(s => s.Stage.StageName.Equals(stagename)).ToList();
        }
        
        public List<Event> SearchByPerformer(string name)
        {
            return events.Where(e => e.Performer.Name.Equals(name)).ToList();
        }

        public List<Event> SearchByPerformerNickname(string nickname)
        {
            /** Because I'm lazy, we have three LINQ choices: 
                StartsWith(), EndsWith(), and Contains().
             */
            return events.Where(e => e.Performer.Name.Contains(nickname)).ToList();
        }

        /// <summary>
        /// A show is considered to be OPEN if there are still available tickets
        /// <remark>Time should also be factored in for extra readability</remark>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Event> OpenShows()
        {
            var results = events.Where(e => e.NumAvailableTickets > 0).ToList();


            // TODO: check if the time is reasonable

            return results;
        }

        public void Sort()
        {
            events.Sort(new EventComparer());
        }

        public List<Event> ShowsShorterThan(int minutes)
        {
            return events.Where(e => e.DurationMinutes < minutes).ToList();
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
