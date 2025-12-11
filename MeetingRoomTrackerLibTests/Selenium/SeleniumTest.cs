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
        public void TestClickElement()
        {
            _driver.Navigate().GoToUrl("https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net/");
            var el = _driver.FindElement(By.Id("CLickBuilding"));
            Assert.IsNotNull("CLickBuilding");
            el.Click();

        }
    }
}
