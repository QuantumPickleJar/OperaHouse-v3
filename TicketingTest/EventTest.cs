using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperaHouse_Assignment3;
using System.Collections.Generic;

namespace TicketingTest
{
    [TestClass]
    public class EventTest
    {
        Event shrek, deathShow, belushiShow;
        Stage main, lounge;
        Performer drDeath;
        Performer belushi;
        
        [TestInitialize]
        public void SetUp()
        {
            Performer osawaHigh = new Performer("Osawa High School", 0);
            shrek = new Event("Shrek", osawaHigh, 150, 12, new DateTime(2015, 4, 18, 19, 30, 0), 60, true);
            drDeath = new Performer("Dr Death", 1500);
            belushi = new Performer("Jim Belushi", 3500);
            deathShow = new Event("Dr. Death's Musical Adventures", drDeath, 200, 20, new DateTime(2015, 4, 25, 19, 0, 0), 60, true);
            belushiShow = new Event("Belushi and the Board of Comedy", belushi, 160, 33, new DateTime(2015, 3, 4, 19, 45, 0), 60, false);
            main = new Stage("Main Stage", 100, 150);
            lounge = new Stage("The Lounge", 75, 50);
        }

        [TestMethod]
        public void NumTicketsTest()
        {
            Assert.AreEqual(150, shrek.NumAvailableTickets);
        }
        [TestMethod]
        public void SellTicketsTest()
        {
            Assert.AreEqual(150, shrek.NumAvailableTickets);
            double amountSold = shrek.SellTickets(10); //sell 10 tickets
            Assert.AreEqual(120, amountSold); //for $120
            Assert.AreEqual(140, shrek.NumAvailableTickets); //only 140 tickets left
            amountSold = shrek.SellTickets(140); //sell the rest of the tickets
            Assert.AreEqual(140 * 12, amountSold);
            Assert.AreEqual(0, shrek.NumAvailableTickets);
       
        }

        [TestMethod]
        public void SellTooManyTicketsTest()
        {
            Assert.AreEqual(150, shrek.NumAvailableTickets);
            double amountSold = shrek.SellTickets(151);
            Assert.AreEqual(150 * 12, amountSold); 
            
        }

        [TestMethod]
        public void SalesTest()
        {
            deathShow.Stage = main;
            double amountSold = deathShow.SellTickets(10);
            Assert.AreEqual(10 * 20, amountSold);
            deathShow.SellTickets(5);
            Assert.AreEqual(10 * 20 + 5 * 20, deathShow.TicketSales()); //Check the total ticket sales
            deathShow.SellTickets(200); //sell out
            Assert.AreEqual(200 * 20, deathShow.TicketSales());

        }

        [TestMethod]
        public void ReturnTicketsTest()
        {
            shrek.SellTickets(5);
            List<int> ticketNums = new List<int>((new int[] { 1, 2, 3 }));

            double amountReturned = shrek.ReturnTickets(ticketNums);//Return tickets num 1,2,3
            Assert.AreEqual(3*12, amountReturned);
            Assert.AreEqual(148, shrek.NumAvailableTickets); 
        }

        [TestMethod]
        public void DayOfWeekTest()
        {
            Assert.IsTrue(deathShow.IsWeekend());
            Assert.IsTrue(shrek.IsWeekend());
            Assert.IsFalse(belushiShow.IsWeekend());

        }

        [TestMethod]
        public void ProfitTest()
        {
            deathShow.Stage = main;
            deathShow.SellTickets(200);
            double profit = 20 * 200 - 1500 - 150 - 100;
            Assert.AreEqual(profit, deathShow.Profit());
            Assert.IsTrue(deathShow.Profitable());

            // test that we can profit without any tickets
            // (saves us from writing another test method)
            shrek.Stage = lounge;
            shrek.SellTickets(20);
            shrek.SellConcession(5.00, 3, "hot dog");
            shrek.SellConcession(10.00, 1, "large popcorn");
            profit = (25.00 + (20 * 12)) - 125;

            Assert.IsTrue(shrek.IsSellingConcessions);
            Assert.AreEqual(profit, shrek.Profit());
            Assert.IsTrue(shrek.Profitable());
        }


        [TestMethod]
        public void ProfitWithConcessionsTest()
        {
            deathShow.Stage = main;
            deathShow.SellTickets(200);
            double profit = 20 * 200 - 1500 - 150 - 100;
            Assert.AreEqual(profit, deathShow.Profit());
            Assert.IsTrue(deathShow.Profitable());
        }

        


        [TestMethod]
        public void ExpensesTest()
        {
            deathShow.Stage = main;
            double expenses = drDeath.Fee + main.costPerHour * 1 + main.cleaningFee;
            Assert.AreEqual(expenses, deathShow.ShowExpenses());

            belushiShow.Stage = main;

            expenses = main.costPerHour * 1 + main.cleaningFee;
            expenses -= expenses * 0.1;
            expenses += belushi.Fee;

            Assert.AreEqual(expenses, belushiShow.ShowExpenses());

        }


    }
}
