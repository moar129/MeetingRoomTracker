using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomTrackerLibTests.Selenium
{
    [TestClass]
    public class SeleniumTest
    {
        private static readonly string DriverDirectory = "C:\\seleniumDrivers";
        private static IWebDriver _driver;
        private static WebDriverWait _wait;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);

            // wait for vue the page to load
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestWebsiteLoads()
        {
            _driver.Navigate().GoToUrl("https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net/");
            Assert.AreEqual("Meeting Room Tracker", _driver.Title);
            var header = _driver.FindElement(By.TagName("h1"));
            Assert.IsNotNull(header);
        }
        [TestMethod]
        public void TestClickBuildingAccordion()
        {
            _driver.Navigate().GoToUrl("https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net/");

            // Wait for the building header to appear
            var buildingHeader = _wait.Until(d => d.FindElement(By.Id("CLickBuilding")));
            Assert.IsNotNull(buildingHeader);

            // Click to open
            buildingHeader.Click();

            // Wait for the room list to appear
            var rooms = _wait.Until(d => d.FindElements(By.CssSelector(".border-bottom.border-secondary")));
            Assert.IsTrue(rooms.Count > 0, "No rooms found after opening the building.");
        }

        [TestMethod]
        public void TestRoomsSortedByAvailabilityAndName()
        {
            _driver.Navigate().GoToUrl("https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net/");
            var roomNames = new List<string>();
            var roomStatus = new List<string>();

            // Open building accordion
            var buildingHeader = _wait.Until(d => d.FindElement(By.Id("CLickBuilding")));
            buildingHeader.Click();

            // Wait for rooms
            var rooms = _wait.Until(d => d.FindElements(By.CssSelector(".border-bottom.border-secondary")));
            foreach (var room in rooms)
            {
                var name = room.FindElement(By.CssSelector(".cursor-pointer strong")).Text;
                var status = room.FindElement(By.CssSelector("span.rounded-pill")).Text;

                roomNames.Add(name);
                roomStatus.Add(status);
            }

            // 1. Check that all free rooms come first
            bool seenOccupied = false;
            foreach (var status in roomStatus)
            {
                if (status == "Optaget")
                {
                    seenOccupied = true;
                }
                else if (status == "Ledig" && seenOccupied)
                {
                    Assert.Fail("Free room appears after an occupied room.");
                }
            }
            // 2. Check alphabetical order for free rooms
            var freeRoomNames = roomNames.Where((n, i) => roomStatus[i] == "Ledig").ToList();
            var sortedFree = new List<string>(freeRoomNames);
            sortedFree.Sort(StringComparer.Ordinal);
            CollectionAssert.AreEqual(sortedFree, freeRoomNames, "Free rooms are not sorted alphabetically.");
        }



    }
}
