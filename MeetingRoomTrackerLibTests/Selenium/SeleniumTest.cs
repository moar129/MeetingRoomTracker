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
        }
        [TestMethod]
        public void TestFindElementById()
        {
            _driver.Navigate().GoToUrl("https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net/");
            var element = _driver.FindElement(By.Id("app"));
            Assert.IsNotNull(element);
        }
        [TestMethod]
        public void TestClickElement()
        {
            _driver.Navigate().GoToUrl("https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net/");
            var element = _driver.FindElement(By.Id("CLickBuilding"));
            element.Click();
            
        }
    }
}
