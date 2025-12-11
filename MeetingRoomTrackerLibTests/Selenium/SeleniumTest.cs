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
        // Folder where ChromeDriver executable is located
        private static readonly string DriverDirectory = "C:\\seleniumDrivers\\chromedriver-win64";

        // The main Selenium driver object used to control the browser
        private static IWebDriver _driver;

        // Wait object to pause execution until elements appear (max 10 seconds)
        private static WebDriverWait _wait;

        // URL of the website we are testing
        private string BaseUrl = "https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net/";

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Start Chrome browser
            _driver = new ChromeDriver(DriverDirectory);

            // Create a wait object for waiting up to 10 seconds for elements to appear
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [ClassCleanup]
        public static void TearDown()
        {
            // Close the browser when all tests are done
            _driver.Dispose();
        }

        [TestInitialize]
        public void TestSetup()
        {
            // Open the website before each test
            _driver.Navigate().GoToUrl(BaseUrl);
        }

        [TestMethod]
        public void TestMainHeader()
        {
            // Wait until the main header <h1> appears, then check its text
            var header = _wait.Until(d => d.FindElement(By.TagName("h1")));
            Assert.AreEqual("Meeting Room Tracker", header.Text);
        }

        [TestMethod]
        public void TestGlobalStats()
        {
            // Check that the global statistics cards exist on the page
            Assert.IsNotNull(_driver.FindElement(By.XPath("//p[contains(text(),'Ledige lokaler')]")));
            Assert.IsNotNull(_driver.FindElement(By.XPath("//p[contains(text(),'Optaget')]")));
            Assert.IsNotNull(_driver.FindElement(By.XPath("//p[contains(text(),'Total')]")));
        }

        [TestMethod]
        public void TestOpenBuildingD()
        {
            // Wait until Building D appears and click it to expand
            var buildingD = _wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(),'Bygning D')]")));
            buildingD.Click();

            // Wait until at least one room appears inside Building D
            var firstRoom = _wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'cursor-pointer')]")));

            // Verify the room exists
            Assert.IsNotNull(firstRoom, "No rooms found in Building D.");
        }

        [TestMethod]
        public void TestRoomRome()
        {
            // Wait for Building D to appear and click to expand
            var buildingD = _wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(),'Bygning D')]")));
            buildingD.Click();

            // Wait for the room "Rome" to appear
            var roomRome = _wait.Until(d => d.FindElement(By.XPath("//div[div/strong[text()='Rome']]")));

            // Scroll the room into view in the browser
            ((IJavaScriptExecutor)_driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center', behavior:'instant'});", roomRome);

            // Small pause to ensure scroll finishes
            Thread.Sleep(200);

            // Click the room using JavaScript (safer than normal click in some cases)
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", roomRome);
        }

        [TestMethod]
        public void TestRoomViewDiscordLink()
        {
            // Open Building D
            var buildingD = _wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(),'Bygning D')]")));
            buildingD.Click();

            // Open room "Rome"
            var roomRome = _wait.Until(d => d.FindElement(By.XPath("//div[div/strong[text()='Rome']]")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", roomRome);
            Thread.Sleep(200); // small pause for scroll
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", roomRome);

            // Wait for the RoomView to appear
            var discordLink = _wait.Until(d => d.FindElement(By.XPath("//a[contains(@href,'discord.gg')]")));

            // Check that the link is visible and points to Discord
            Assert.IsTrue(discordLink.Displayed, "Discord link in RoomView is not visible.");
            Assert.IsTrue(discordLink.GetAttribute("href").Contains("discord.gg"), "Discord link does not point to Discord.");
        }

        [TestMethod]
        public void TestRoomRomeDetails()
        {
            // Open Building D
            var buildingD = _wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(),'Bygning D')]")));
            buildingD.Click();

            // Open room "Rome"
            var roomRome = _wait.Until(d => d.FindElement(By.XPath("//div[div/strong[text()='Rome']]")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", roomRome);
            Thread.Sleep(200);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", roomRome);

            // Wait for RoomView to render
            var roomName = _wait.Until(d => d.FindElement(By.CssSelector("h2[data-testid='room-name']")));
            Assert.AreEqual("Rome", roomName.Text, "Room name is incorrect.");

            // Room number
            var roomNumber = _driver.FindElement(By.CssSelector("p[data-testid='room-number']"));
            Assert.IsTrue(!string.IsNullOrEmpty(roomNumber.Text), "Room number is missing.");

            // Status
            var status = _driver.FindElement(By.CssSelector("span[data-testid='room-status']"));
            Assert.IsTrue(status.Text == "Ledig" || status.Text == "Optaget", "Room status is incorrect.");
        }

        [TestMethod]
        public void TestRoomRomeHistorySimple()
        {
            // Open Building D
            var buildingD = _wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(),'Bygning D')]")));
            buildingD.Click();

            // Open room "Rome"
            var roomRome = _wait.Until(d => d.FindElement(By.XPath("//div[div/strong[text()='Rome']]")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", roomRome);
            Thread.Sleep(200);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", roomRome);

            // Wait for history card to appear
            var historyCard = _wait.Until(d => d.FindElement(By.XPath("//div[h3[contains(text(),'Historik i dag')]]")));
            Assert.IsTrue(historyCard.Displayed, "Room history card is not visible.");

            // Check that either timeline entries or the empty message exists
            var hasEntries = _driver.FindElements(By.XPath("//div[starts-with(@data-testid,'history-')]")).Count > 0;
            var hasEmptyMsg = _driver.FindElements(By.XPath(".//p[contains(text(),'Ingen møder registreret i dag')]")).Count > 0;
            Assert.IsTrue(hasEntries || hasEmptyMsg, "No timeline entries or empty state message found.");
        }

        [TestMethod]
        public void TestFooterDiscordLink()
        {
            // Wait until footer link to Discord is visible
            var footerLink = _wait.Until(d => d.FindElement(By.CssSelector("footer a[href*='discord.gg']")));

            // Check that the link is displayed and correct
            Assert.IsTrue(footerLink.Displayed, "Footer Discord link is not visible.");
            Assert.IsTrue(footerLink.GetAttribute("href").Contains("discord.gg"), "Footer Discord link does not point to a Discord URL.");
        }
    }
}
