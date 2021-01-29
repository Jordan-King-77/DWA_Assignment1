using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DWA_Assignment1.Controllers;
using System.Web.Mvc;
using DWA_Assignment1.Models;
using System.Net;
using System.Web.Http;
using System.Security.Claims;

namespace DWA_Assignment1_UnitTest
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void IndexReturnsView()
        {
            var home = new HomeController(new FakeMeetRepository());
            Assert.IsInstanceOfType(home.Index(), typeof(ViewResult));
        }

        [TestMethod]
        public void CreateMeetGetReturnsView()
        {
            var home = new HomeController();
            Assert.IsInstanceOfType(home.CreateMeet(), typeof(ViewResult));
        }

        [TestMethod]
        public void CreateMeetPostReturnView()
        {
            var home = new HomeController(new FakeMeetRepository());

            MeetViewModel m = new MeetViewModel
            {
                Name = "Unit Test Meet",
                Venue = "Unit Test Venue",
                PoolLength = "150m",
                DateString = "21 September 2021"
            };

            Assert.IsInstanceOfType(home.CreateMeet(m), typeof(ViewResult));
        }

        [TestMethod]
        public void FindMeetReturnsView()
        {
            var home = new HomeController(new FakeMeetRepository());

            Assert.IsInstanceOfType(home.FindMeet(5), typeof(ViewResult));
        }

        [TestMethod]
        public void FindMeetReturnsBadRequest()
        {
            var home = new HomeController(new FakeMeetRepository(false));

            var expected = (int)HttpStatusCode.BadRequest;

            var actionResult = home.FindMeet(null) as HttpStatusCodeResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(expected, actionResult.StatusCode);
        }

        [TestMethod]
        public void FindMeetReturnsNotFound()
        {
            var home = new HomeController(new FakeMeetRepository(false));
            Assert.IsInstanceOfType(home.FindMeet(9999), typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void SearchChildMeetsReturnsView()
        {
            var home = new HomeController(new FakeMeetRepository());

            Assert.IsInstanceOfType(home.SearchChildMeets("33da37a5-4586-4e87-909f-b187a1f9f58d"), typeof(ViewResult));
        }

        [TestMethod]
        public void SearchMeetsReturnsView()
        {
            var home = new HomeController(new FakeMeetRepository());

            Assert.IsInstanceOfType(home.SearchMeets(), typeof(ViewResult));
        }

        [TestMethod]
        public void SearchMeetResults_ValidViewModel()
        {
            var home = new HomeController(new FakeMeetRepository());

            SearchViewModel m = new SearchViewModel
            {
                MeetName = "Test Suite",
                MeetVenue = "London Swimming Centre",
                MeetStartDateString = "01/10/2021",
                MeetEndDateString = "01/11/2021"
            };

            Assert.IsInstanceOfType(home.SearchMeetResults(m), typeof(ViewResult));
        }

        [TestMethod]
        public void SearchMeetResults_InvalidViewModel()
        {
            var home = new HomeController(new FakeMeetRepository(false));

            SearchViewModel m = new SearchViewModel
            {
                MeetStartDateString = "01/10/2021"
            };

            var result = home.SearchMeetResults(m);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
