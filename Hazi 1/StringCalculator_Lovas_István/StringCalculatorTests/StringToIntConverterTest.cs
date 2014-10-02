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
    class StringToIntConverterTest
    {
        StringToIntConverter Converter;

        //[TestFixtureSetUp]
        //[TestFixtureTeardown]
        [SetUp]
        public void SetUp()
        {
            Arrange();
        }

        //[TearDown]


        [TestCase("", 0)]
        [TestCase("apple", 0)]
        public void Test_Convert_Should_ReturnZero_When_IllegalNumberIsGiven(string input, int expected)
        {
            // Arrange

            // Act
            var res = Converter.Convert(input);
            
            // Assert
            AssertEqual(expected, res);
        }


        [TestCase("15", 15)]
        [TestCase("-15", -15)]
        public void Test_Convert_Should_ReturnTheConvertedInteger_When_ValidIntegerIstGiven(string input, int expected)
        {
            // Arrange

            // Act
            var res = Converter.Convert(input);
           
            // Assert
            AssertEqual(expected, res);
        }


        [TestCase("-15", ExpectedException = typeof(ArgumentException), ExpectedMessage = "negatives not allowed")]
        public void Test_Convert_Should_ThrowArgumentException_When_NegativeValueIsGivenAndDisalowNegativesOptionIsSet(string input)
        {
            // Arrange
            Converter.Options.Add(StringToIntConverter.ConvertOptions.DisallowNegatives);

            // Act
            var res = Converter.Convert(input);

            // Assert
        }



        void Arrange()
        {
            Converter = new StringToIntConverter();
        }

        private void AssertEqual(object expected, object result)
        {
            Assert.AreEqual(expected, result);
        }
    }
}
