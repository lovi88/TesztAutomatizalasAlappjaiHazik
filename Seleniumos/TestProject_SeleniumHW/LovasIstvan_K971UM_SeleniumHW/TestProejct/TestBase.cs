using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TestProject
{
    [TestFixture]
    class TestBase
    {
        IWebDriver driver;

        public IWebDriver Driver
        {
            get
            { return driver; }
            set
            {   driver.Quit();
                driver = value; }
        }
        
        [SetUp]
        virtual protected void Setup()
        {
            FirefoxProfile profile = new FirefoxProfile();
            profile.EnableNativeEvents = false;
            // Create a new instance of the Firefox driver
            driver = new FirefoxDriver(profile);
        }

        [TearDown]
        protected void Teardown()
        {
            driver.Quit();
        }

        protected void waitUntilDisplayed(By locator)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until((d) => { return d.FindElement(locator).Displayed; });
        }

        
    }
}
