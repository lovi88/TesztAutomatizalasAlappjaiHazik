using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestFixture]
    class TourTest : TourTestBase
    {
        
        [Test]
        public void TourTestHomeWork_Check4200Ft()
        {
            Driver.Navigate().GoToUrl("http://elvira.mav-start.hu/");

            FillRouteBudapestKeszthelyVeszprém();

            FillPotjegyNelkul();

            FillDate2015_02_01();

            StartSearching();

            //checking the time, the  road, the price 

            var departureTime = "13:27";
            var departureStation ="Kőbánya-Kispest";
            var price ="4.200 Ft";

            AssertPriceForTrip(departureTime, departureStation, price);
        }

        
        [Test]
        [ExpectedException(ExpectedException = typeof(TravelException), ExpectedMessage = "There is no train starts at 05:27")]
        public void TourTestHomeWork_Check2800Ft()
        {
            Driver.Navigate().GoToUrl("http://elvira.mav-start.hu/");

            FillRouteBudapestKeszthelyVeszprém();

            FillPotjegyNelkul();

            FillDate2015_02_01();

            SetYouthUnder26Reduction();

            StartSearching();

            //checking the time, the  road, the price 
            var departureTime = "05:27";
            var departureStation = "Kőbánya-Kispest";
            var price = "2.810 Ft";

            AssertPriceForTrip(departureTime, departureStation, price);
        }


        [Test]
        public void TourTestHomeWork_Check2100Ft()
        {
            Driver.Navigate().GoToUrl("http://elvira.mav-start.hu/");

            FillRouteBudapestKeszthelyVeszprém();

            FillPotjegyNelkul();

            FillDate2015_02_01();

            SetStudentsInFullTimeOrEveningCourseReduction();

            StartSearching();

            //checking the time, the  road, the price 
            var departureTime = "13:10";
            var departureStation = "Budapest-Déli";
            var price = "2.100 Ft";

            AssertPriceForTrip(departureTime, departureStation, price);
        }

        private void SetStudentsInFullTimeOrEveningCourseReduction()
        {
            SetReductionByValue("39");
        }


        private void SetYouthUnder26Reduction()
        {
            SetReductionByValue("15");
        }

        protected void FillPotjegyNelkul()
        {
            FillSearchOptions(SearchWidget.searchOptions.PotjegyNelkul);
        }
        
        private void FillDate2015_02_01()
        {
            FillDate(2015, 2, 1);
        }

        private void FillRouteBudapestKeszthelyVeszprém()
        {
            FillRoute("Budapest", "Keszthely", "Veszprém");
        }
    }
}
