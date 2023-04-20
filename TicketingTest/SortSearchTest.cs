using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperaHouse_Assignment3;
using System;
using System.Collections.Generic;

namespace TicketingTest
{
    [TestClass]
    public class SortSearchTest
    {

        Event shrek, deathShow, belushiShow;
        Stage main, lounge;
        Performer drDeath;
        Performer belushi;
        Performer osawaHigh;
        EventList events;

        [TestInitialize]
        public void SetUp()
        {
            osawaHigh = new Performer("Osawa High School", 0);
            drDeath = new Performer("Dr Death", 1500);
            belushi = new Performer("Jim Belushi", 3500);

            main = new Stage("Main Stage", 100, 150);
            lounge = new Stage("The Lounge", 75, 50);


            shrek = new Event("1", "Shrek", main, osawaHigh, 150, 12, new DateTime(2015, 4, 18, 19, 30, 0), 60, true);
            deathShow = new Event("2", "Dr. Death's Musical Adventures", lounge, drDeath, 200, 20, new DateTime(2015, 4, 25, 19, 0, 0), 90, true);
            belushiShow = new Event("3", "Belushi and the Board of Comedy", main, belushi, 160, 33, new DateTime(2015, 3, 4, 19, 45, 0), 120, false);
            events = new EventList();

            events.AddEvent(new List<Event> { shrek, deathShow, belushiShow });
        }


        [TestMethod]
        public void TestSortDefaultSort()
        {
            //perform a default sort
            events.Sort();

            string[] expected = new string[] { "3", "1", "2" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSortByDate()
        {
            //sort events by date
            events.SortByDate();

            string[] expected = new string[] { "3", "1", "2" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSortByTitle()
        {
            //sort events by title
            events.SortByTitle();
            string[] expected = new string[] { "3", "2", "1" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSortByPerformer()
        {
            events.SortByPerformer();
            string[] expected = new string[] { "2", "3", "1" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestSortByStage()
        {
            events.SortByStage();
            string[] expected = new string[] { "1", "3", "2" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSearchPerformer()
        {
            events.AddEvent(new Event("4", "Midsummer Night", main, osawaHigh, 100, 10, new DateTime(2015, 9, 30, 19, 30, 0), 90, true));

            List<Event> osawaEvents = events.SearchByPerformer("Osawa High School");

            string[] expected = new string[] { "1", "4" };
            string[] actual = eventIDs(osawaEvents);

            CollectionAssert.AreEqual(expected, actual);

            // test the partial search
            osawaEvents.Clear();
            osawaEvents = events.SearchByPerformerNickname("Osawa");
            actual = eventIDs(osawaEvents);

            // Since the expected dataset is the same, expected & actual go untouched
            CollectionAssert.AreEquivalent(expected, actual);
        }

        // TODO: multi-criteria sort

        [TestMethod]
        public void TestSoldOutShow()
        {
            deathShow.SellTickets(200); //sell out show
            belushiShow.SellTickets(10); //sell some tickets

            Assert.IsTrue(deathShow.IsSoldOut);
            Assert.IsFalse(belushiShow.IsSoldOut);
            Assert.IsFalse(shrek.IsSoldOut);
        }

        // test for criteria #3: performance shorter than N minutes
        [TestMethod]
        public void TestShortShows()
        {
            List<Event> shortShows = events.ShowsShorterThan(100); //Get shows shorter than 100 minutes

            string[] expected = new string[] { "1", "2" };
            string[] actual = eventIDs(shortShows);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAvailableTickets()
        {
            deathShow.SellTickets(200); //sell out show

            List<Event> openShows = events.OpenShows();

            string[] expected = new string[] { "1", "3" };
            string[] actual = eventIDs(openShows);

            CollectionAssert.AreEqual(expected, actual);
        }

        

        //Helper method that returns the IDs of all the events in the order they currently appear in the List.
        //This is for testing purposes only
        private string[] eventIDs(List<Event> events)
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
