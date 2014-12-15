using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace TestProject
{
    class SearchWidget : BaseWidget
    {
        public SearchWidget(IWebDriver driver)
            : base(driver)
        {
        }

        [FindsBy(How = How.Id, Using = "searchbottom")]
        public IWebElement searchBottom { get; set; }

        // create & locate routeFrom element by using id = "i"
        [FindsBy(How = How.Id, Using = "i")]
        public IWebElement routeFrom { get; set; }

        [FindsBy(How = How.Id, Using = "e")]
        public IWebElement routeTo { get; set; }

        [FindsBy(How = How.Id, Using = "v")]
        public IWebElement routeVia { get; set; }

        [FindsBy(How = How.Id, Using = "u")]
        public IWebElement reductionSlct { get; set; }

        [FindsBy(How = How.Name, Using = "go")]
        public IWebElement searchButton { get; set; }

        Dictionary<searchOptions, string> searchOptionLocators = new Dictionary<searchOptions, string>()
        {
            { searchOptions.PotjegyNelkul , "s1" },
            { searchOptions.AtszallasNelkul , "sk" },
            { searchOptions.HelyiKozlekedesNelkul , "hkn" },
            { searchOptions.BudapestBerlettel , "sb" },
            { searchOptions.Kerekparral , "s2" },
            { searchOptions.BudapestFejpalyaudvaronAt , "s1" }
        };

        public enum searchOptions
        {
            PotjegyNelkul,
            AtszallasNelkul,
            HelyiKozlekedesNelkul,
            BudapestBerlettel,
            Kerekparral,
            BudapestFejpalyaudvaronAt,
        }

        public void searchForRoute(String fromCity, String toCity, String viaCity)
        {
            // use SendKeys method on routeFrom, routeTo and routeVia inputfileds to set given parameters
            routeTo.SendKeys(toCity);
            routeFrom.SendKeys(fromCity);
            routeVia.SendKeys(viaCity);
        }

        public void setReductionViaText(String reductionText)
        {
            new SelectElement(reductionSlct).SelectByText(reductionText);
        }

        public void setSearchOptionTo(searchOptions searchOption)
        {
            searchBottom.FindElement(By.Id(searchOptionLocators[searchOption])).Click();
        }

        public ResultWidget clickTimetableButton()
        {
            searchButton.Click();
            return new ResultWidget(_driver);
        }


        internal void SetReductionByValue(string val)
        {
            var select = _driver.FindElement(By.Id("u"));


            SelectElement selector = new SelectElement(select);
            selector.SelectByValue(val);
        }
    }
}
