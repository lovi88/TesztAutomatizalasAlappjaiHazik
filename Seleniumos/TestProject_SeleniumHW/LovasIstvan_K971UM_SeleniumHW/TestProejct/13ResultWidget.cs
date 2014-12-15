using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class ResultWidget : BaseWidget
    {
        public ResultWidget(IWebDriver driver)
            : base(driver)
        {

        }

        [FindsBy(How = How.Id, Using = "timetable")]
        public IWebElement timetable { get; set; }

        IReadOnlyCollection<IWebElement> GetTableRows()
        {
            return timetable.FindElements(By.CssSelector("#timetable>table>tbody>tr"));
        }

        public bool CheckIfPriceIsCorrect(string time, string from_location, string price)
        {
            var rows = GetTableRows();

            var rowsForTime = RowsWhereStringPresentsInDeparture(rows, time);
            if (rowsForTime.Count() == 0) throw new TravelException("There is no train starts at " + time);

            var rowsForDeparture = RowsWhereStringPresentsInDeparture(rows, from_location);
            if (rowsForDeparture.Count() == 0) throw new TravelException("There is no train starts from " + from_location);

            var commonRows = rowsForTime.Intersect(rowsForDeparture);
            if (commonRows.Count() == 0) throw new TravelException("There is no train starts at " + time + " from " + from_location);

            var isOK = IsThereAnyRowWhereStringPresentsInPrice(commonRows,price);
            
            return isOK;
        }


        private IEnumerable<IWebElement> RowsWhereStringPresentsInDeparture(IReadOnlyCollection<IWebElement> rows, string str)
        {
            List<IWebElement> inTime = new List<IWebElement>();
            foreach (var r in rows)
            {
                if (IsStringPresentsInDeparture(r, str))
                {
                    inTime.Add(r);
                }
            }

            return inTime;
        }


        private bool IsStringPresentsInDeparture(IWebElement element, string str)
        {
            var departureArrivalCells = element.FindElements(By.CssSelector(".l"));
            if (departureArrivalCells.Count==0)
            {
                return false;
            }

            return IsCellContainsString(departureArrivalCells[0], str);
        }

        private bool IsCellContainsString(IWebElement cell, string str)
        {
            return IsStringContainsCaseInSensitive(cell.Text, str);
        }

        private bool IsThereAnyRowWhereStringPresentsInPrice(IEnumerable<IWebElement> rows, string price)
        {
            foreach (var r in rows)
            {
                var price_cell_text = r.FindElement(By.CssSelector(".r.jegymagylnk")).Text;

                if (IsStringContainsCaseInSensitive(price_cell_text, price))
                {
                    return true;
                }
            }

            return false;
        }

        bool IsStringContainsCaseInSensitive(string contains, string value) { 
            return contains.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
