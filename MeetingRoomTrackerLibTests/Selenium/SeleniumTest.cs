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
        private static readonly string DriverDirectory = "C:\\seleniumDrivers\\chromedriver-win64";
        private static IWebDriver _driver;
        private static WebDriverWait _wait;
        private string BaseUrl = "https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net/";

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Initialize ChromeDriver
            _driver = new ChromeDriver(DriverDirectory);

            // wait for vue the page to load
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [ClassCleanup]
        public static void TearDown()
        {
            // Close the browser and dispose of the driver
            _driver.Dispose();
        }

       
        [TestInitialize]
        public void TestSetup()
        {
            // Navigate to the base URL before each test
            _driver.Navigate().GoToUrl(BaseUrl);
        }

        [TestMethod]
        public void TestMainHeader()
        {
            // Verify main page header exists
            var header = _wait.Until(d => d.FindElement(By.TagName("h1")));
            Assert.AreEqual("Meeting Room Tracker", header.Text);
        }
        [TestMethod]
        public void TestGlobalStats()
        {
            // Verify global stats cards exist
            Assert.IsNotNull(_driver.FindElement(By.XPath("//p[contains(text(),'Ledige lokaler')]")));
            Assert.IsNotNull(_driver.FindElement(By.XPath("//p[contains(text(),'Optaget')]")));
            Assert.IsNotNull(_driver.FindElement(By.XPath("//p[contains(text(),'Total')]")));
        }

        [TestMethod]
        public void TestOpenBuildingD()
        {
            // Navigate to the page
            _driver.Navigate().GoToUrl(BaseUrl);

            // Wait 2 seconds for Vue to render (simple "dumb" wait)
            System.Threading.Thread.Sleep(2000);

            // Find Building D by visible text and click it
            var buildingD = _driver.FindElement(By.XPath("//h3[contains(text(),'Bygning D')]"));
            buildingD.Click();

            // Wait a moment for rooms to appear
            System.Threading.Thread.Sleep(1000);

            // Find at least one room inside Building D
            var firstRoom = _driver.FindElement(By.XPath("//div[contains(@class,'cursor-pointer')]"));

            // Verify room exists
            Assert.IsNotNull(firstRoom, "No rooms found in Building D.");
        }


        [TestMethod]
        public void TestRoomRome()
        {
            // Wait for Vue to load buildings
            System.Threading.Thread.Sleep(2000);

            // Click Building D to expand it
            var buildingD = _driver.FindElement(By.XPath("//h3[contains(text(),'Bygning D')]"));
            buildingD.Click();
            System.Threading.Thread.Sleep(1200); // wait for transition (fade) to finish

            // Find the room named "Rome"
            var roomRome = _driver.FindElement(By.XPath("//div[div/strong[text()='Rome']]"));

            // Scroll it into view
            ((IJavaScriptExecutor)_driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center', behavior:'instant'});", roomRome);
            System.Threading.Thread.Sleep(300); // give time for scrolling

            // Click the room via JavaScript (always, no if/else)
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", roomRome);
        }




        [TestMethod]
        public void TestRoomViewDiscordLink()
        {
            // Wait for Vue to load buildings
            System.Threading.Thread.Sleep(2000);

            // Click Building D to expand
            var buildingD = _driver.FindElement(By.XPath("//h3[contains(text(),'Bygning D')]"));
            buildingD.Click();
            System.Threading.Thread.Sleep(1200); // wait for fade transition

            // Find and click the room "Rome"
            var roomRome = _driver.FindElement(By.XPath("//div[div/strong[text()='Rome']]"));
            ((IJavaScriptExecutor)_driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center', behavior:'instant'});", roomRome);
            System.Threading.Thread.Sleep(300);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", roomRome);

            // Wait for RoomView to render
            System.Threading.Thread.Sleep(500);

            // Find the Discord link inside RoomView
            var discordLink = _driver.FindElement(By.XPath("//a[contains(@href,'discord.gg')]"));

            // Assertions
            Assert.IsTrue(discordLink.Displayed, "Discord link in RoomView is not visible.");
            Assert.IsTrue(discordLink.GetAttribute("href").Contains("discord.gg"), "Discord link does not point to Discord.");
        }



        [TestMethod]
        public void TestFooterDiscordLink()
        {
            // Scroll to footer
            var footerLink = _wait.Until(d => d.FindElement(By.CssSelector("footer a[href*='discord.gg']")));

            // Verify the link is displayed and correct
            Assert.IsTrue(footerLink.Displayed, "Footer Discord link is not visible.");
            Assert.IsTrue(footerLink.GetAttribute("href").Contains("discord.gg"), "Footer Discord link does not point to a Discord URL.");
        }
    }
}
