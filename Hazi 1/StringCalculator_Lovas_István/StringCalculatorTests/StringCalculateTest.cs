using NUnit.Framework;
using StringCalculatorKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorTests
{
    [TestFixture]
    class TextCalculateTest
    {
        IStringCalculator strCalc;


        //[TestFixtureSetUp]

        //[TestFixtureTeardown]

        [SetUp]
        public void SetUp()
        {
            Arrange();
        }

        //[TearDown]



        [Test]
        public void Test_Add_Should_ReturnZero_When_EptyStringIsGiven()
        {
            // Arrange

            // Act
            var res = strCalc.Add("");

            // Assert
            AssertEqual(0, res);

        }



        [TestCase("1",1)]
        [TestCase("2", 2)]
        public void Test_Add_Should_ReturnTheGivenWalue_When_OnlyOneValueIsGiven(string input, int expected)
        {
            // Arrange

            // Act
            var res = strCalc.Add(input);
            // Assert
            AssertEqual(expected, res);

        }


        [Test]
        public void Test_Add_Should_ReturnTheSumOfTheColoumnSeparatedTwoStringNumbers()
        {
            // Arrange

            // Act
            var ret = strCalc.Add("1,2");
            // Assert

            Assert.IsTrue(ret == 3);
        }

        [Test]
        public void Test_Add_Should_ReturnTheSumOfTheColoumnSeparatedStringNumbers()
        {
            // Arrange

            // Act
            var ret = strCalc.Add("1,2,3");
            // Assert

            Assert.IsTrue(ret == 6);
        }

        [Test]
        public void Test_Add_Should_ReturnTheSumOfTheColoumnOrNewLineSeparatedStringNumbers()
        {
            // Arrange

            // Act
            var ret = strCalc.Add("1\n2,3");
            // Assert

            Assert.IsTrue(ret == 6);
        }

        [Test]
        public void Test_Add_Should_ReturnTheSumOfTheGivenSeparatorOrComaOrNewLineSeparatedStringNumbers()
        {
            // Arrange

            // Act
            var res = strCalc.Add("//;\n1;2");
            // Assert

            Assert.IsTrue(res == 3);
        }


        [Test]
        [ExpectedException(ExpectedException = typeof(ArgumentException), ExpectedMessage = "negatives not allowed - -3")]
        [TestCase("-3")]
        [TestCase("1,2,-3")]
        [TestCase("//;1;-3")]
        public void Test_Add_Should_ThrowArgumentException_When_NegativeValueIsGiven(string input)
        {
            // Act
            var res = strCalc.Add(input);
        }


        [TestCase("1,-2\n3", ExpectedException = typeof(ArgumentException), ExpectedMessage = "negatives not allowed - -2")]
        public void Test_Add_Should_ThrowExceptionAndTheNegativesGiven_When_NegativesPassed(string input)
        {
            // Act
            var res = strCalc.Add(input);
        }

        private void AssertEqual(object expected, object result)
        {
            Assert.AreEqual(expected, result);
        }

        private void Arrange()
        {
            strCalc = new StringCalculate();
        }
    }

}
