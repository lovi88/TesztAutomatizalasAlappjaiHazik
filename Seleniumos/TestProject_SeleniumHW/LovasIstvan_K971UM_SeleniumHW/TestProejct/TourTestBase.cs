using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestFixture]
    class TourTestBase : TestBase
    {
        protected SearchWidget searchWidget;
        protected DateWidget dateWidget;
        protected ResultWidget resultPage;

        protected void AssertPriceForTrip(string departureTime, string departureStation, string price)
        {
            var result = resultPage.CheckIfPriceIsCorrect(departureTime, departureStation, price);
            Assert.True(result);
        }


        protected void StartSearching()
        {
            resultPage = searchWidget.clickTimetableButton();
        }


        protected void FillDate(int y, int m, int d)
        {
            dateWidget.SetDate(y, m, d);
        }

        protected void FillSearchOptions(SearchWidget.searchOptions searchOptions)
        {
            searchWidget.setSearchOptionTo(searchOptions);
        }

        
        protected void FillRoute(String fromCity, String toCity, String viaCity)
        {
            searchWidget.searchForRoute(fromCity, toCity, viaCity);
        }

        protected void SetReductionByValue(string val)
        {
            searchWidget.SetReductionByValue(val);
        }

        protected override void Setup()
        {
            base.Setup();

            searchWidget = new SearchWidget(Driver);
            dateWidget = new DateWidget(Driver);

        }
    }
}
