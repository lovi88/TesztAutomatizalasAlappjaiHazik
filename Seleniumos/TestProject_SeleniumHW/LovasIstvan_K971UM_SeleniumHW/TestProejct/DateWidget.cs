using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class DateWidget : BaseWidget
    {
        public DateWidget(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.Id, Using = "cal_tbody")]
        public IWebElement datePickerTable { get; set; }

        [FindsBy(How = How.Id, Using = "cal_heading_months_lt")]
        public IWebElement datePickerPreviousMonthButton { get; set; }

        [FindsBy(How = How.Id, Using = "cal_heading_months_gt")]
        public IWebElement datePickerNextMonthButton { get; set; }


        public void SetDate(int year, int month, int day)
        {
            ActivateDate(new DateTime(year, month, day));
        }

        private void ActivateDate(DateTime dateToActivate)
        {

            var actualSelectedDate = GetActualSelectedDate();
            int differenceInMonth = DifferenceInMonth(actualSelectedDate, dateToActivate);

            RollToActualMonth(differenceInMonth);
            ClickToActualDate(dateToActivate);
        }

        private void ClickToActualDate(DateTime dateToActivate)
        {
            //a monthot 0tól indexelik
            string id = "cal_df_" + dateToActivate.Year + "/" + (dateToActivate.Month - 1) + "/" + dateToActivate.Day;

            datePickerTable.FindElement(By.Id(id)).Click();
        }

        private void RollToActualMonth(int differenceInMonth)
        {
            if (differenceInMonth < 0)
            {
                ClickNext(Math.Abs(differenceInMonth + 1));
            }
            else if (differenceInMonth > 0)
            {
                ClickPrevious(differenceInMonth);
            }
        }


        /// <summary>
        /// Returns the month difference between two dates
        /// </summary>
        /// <param name="lValue"></param>
        /// <param name="rValue"></param>
        /// <returns>negative if rValue is greater and positive if lValue is greater </returns>
        private int DifferenceInMonth(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }

        private DateTime GetActualSelectedDate()
        {
            //id: cal_df_2014/11/7; class: s
            IWebElement actualDayElement = datePickerTable.FindElement(By.ClassName("s"));

            var id = actualDayElement.GetAttribute("id");
            var parsedDateStr = id.Substring(7).Split('/');

            int year = int.Parse(parsedDateStr[0]);
            int month = int.Parse(parsedDateStr[1]);
            int day = int.Parse(parsedDateStr[2]);


            return new DateTime(year, month, day);
        }


        private void ClickNext(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                datePickerNextMonthButton.Click();
            }
        }

        private void ClickPrevious(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                datePickerPreviousMonthButton.Click();
            }
        }

        private IWebElement GetYearMonthHeather()
        {
            IWebElement actualMonthField = datePickerTable.FindElement(By.ClassName("y"));
            return actualMonthField;
        }

    }
}
