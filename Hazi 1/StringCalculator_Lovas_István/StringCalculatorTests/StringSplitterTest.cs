using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StringCalculatorKata;

namespace StringCalculatorTests
{
    [TestFixture]
    class StringSplitterTest
    {
        StringSplitter strSplitter;

        //[TestFixtureSetUp]
        //[TestFixtureTeardown]
        //[SetUp]
        //[TearDown]

        [Test]
        public void Test_GetStringParts_Should_ReturnEmptyList_When_EmptyStringIsGiven()
        {
            // Arrange
            Arrange("");

            // Act
            var res = strSplitter.GetStringParts();

            // Assert
            Assert.AreEqual(new List<string>(), res);
        }

        [TestCase("apple", "apple")]
        [TestCase("1", "1")]
        public void Test_GetStringParts_Should_ReturnListWithTheGivenString_When_NotSeparatorFoundInStringGiven(string input, string expected)
        {
            // Arrange
            Arrange(input);

            // Act
            var res = strSplitter.GetStringParts();

            // Assert
            AssertEqual(new List<string>() { expected }, res);
        }

        [TestCase("1,2,3", "1", "2", "3")]
        [TestCase("1\n2\n3", "1", "2", "3")]
        [TestCase("1,2\n3", "1", "2", "3")]
        public void Test_GetStringParts_Should_ReturnListWithTheGivenParts_When_SeparatedWithCommaOrNewLine(string input, string p1, string p2, string p3)
        {
            // Arrange
            Arrange(input);

            // Act
            var res = strSplitter.GetStringParts();

            // Assert
            AssertEqual(new List<string>() { p1, p2, p3 }, res);
        }



        [Test]
        [TestCase("//;1;2","1","2")]
        [TestCase("//!1!2", "1", "2")]
        public void Test_GetStringParts_Should_ReturnListWithTheGivenParts_When_CustomSeparatorIsGiven(string input, string p1, string p2)
        {
            // Arrange
            Arrange(input);

            // Act
            var res = strSplitter.GetStringParts();

            // Assert
            AssertEqual(new List<string>() { p1, p2}, res);
        }


        private void Arrange(string data)
        {
            strSplitter = new StringSplitter(data);
        }

        private void AssertEqual(object expected, object result)
        {
            Assert.AreEqual(expected, result);
        }
    }
}
