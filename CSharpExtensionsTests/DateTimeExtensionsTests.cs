using System;
using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void SaturdayTest()
        {
            var lastSaturday = new DateTime(2014, 4, 26); // known Saturday
            lastSaturday.IsWeekend().ShouldBeTrue();
        }

        [TestMethod]
        public void SundayTest()
        {
            var lastSunday = new DateTime(2014, 4, 27); // known Sunday
            lastSunday.IsWeekend().ShouldBeTrue();
        }

        [TestMethod]
        public void MondayTest()
        {
            var today = new DateTime(2014, 4, 28); //known Monday
            today.IsWeekend().ShouldBeFalse();
        }

        [TestMethod]
        public void FirstDayOfMonthTest()
        {
            var today = new DateTime(2014, 4, 28);
            today.FirstDayOfMonth().ShouldEqual("01/04/2014");
        }

        [TestMethod]
        public void LastDayOfMonthTest()
        {
            var today = new DateTime(2014, 4, 28);
            today.LastDayOfMonth().ShouldEqual("30/04/2014");
        }

        /*
        private readonly Mock<ITimeService> _mockTimeService = new Mock<ITimeService>();

        [TestInitialize]
        public void Initialise()
        {
            Providers.TimeService = _mockTimeService.Object;
        }*/

        [TestMethod]
        public void IsFutureTest()
        {
            var today = new DateTime(2014, 4, 30);
            var tomorrow = new DateTime(2014, 5, 1);
            tomorrow.IsFuture(today).ShouldBeTrue();
        }

        [TestMethod]
        public void IsPastTest()
        {
            var today = new DateTime(2014, 4, 30);
            var tomorrow = new DateTime(2014, 5, 1);
            today.IsPast(tomorrow).ShouldBeTrue();
        }

        /*
        [TestMethod]
        public void TestAge()
        {
            _mockTimeService.Setup(x => x.Now()).Returns(new DateTime(2013, 5, 7));
            new DateTime(1970, 9, 17).Age().ShouldEqual(42);
        }

        [TestMethod]
        public void TestIsFuture()
        {
            _mockTimeService.Setup(x => x.Now()).Returns(new DateTime(2013, 5, 7));
            new DateTime(2013, 5, 8).IsFuture().ShouldBeTrue();
        }

        [TestMethod]
        public void TestIsPast()
        {
            _mockTimeService.Setup(x => x.Now()).Returns(new DateTime(2013, 5, 8));
            new DateTime(2013, 5, 7).IsPast().ShouldBeTrue();
        }
       
        [TestMethod]
        public void TestFriendlyDateString()
        {
            var today = new DateTime(2013, 5, 8);
            _mockTimeService.Setup(x => x.Today()).Returns(new DateTime(2013, 5, 8));
            today.AddHours(6).ToFriendlyDateString().ShouldEqual("Today @ 06:00");
            today.AddDays(-1).ToFriendlyDateString().ShouldEqual("Yesterday @ 00:00");
            today.AddDays(-2).AddHours(13).ToFriendlyDateString().ShouldEqual("Monday @ 13:00");
            today.AddDays(-7).AddHours(23).ToFriendlyDateString().ShouldEqual("May 01, 2013 @ 23:00");
        }
        */

        [TestMethod]
        public void TestMonthNames()
        {
            DateTimeExtensions.MonthName(1).ShouldEqual("January");
            DateTimeExtensions.MonthName(2).ShouldEqual("February");
            DateTimeExtensions.MonthName(3).ShouldEqual("March");
            DateTimeExtensions.MonthName(4).ShouldEqual("April");
            DateTimeExtensions.MonthName(5).ShouldEqual("May");
            DateTimeExtensions.MonthName(6).ShouldEqual("June");
            DateTimeExtensions.MonthName(7).ShouldEqual("July");
            DateTimeExtensions.MonthName(8).ShouldEqual("August");
            DateTimeExtensions.MonthName(9).ShouldEqual("September");
            DateTimeExtensions.MonthName(10).ShouldEqual("October");
            DateTimeExtensions.MonthName(11).ShouldEqual("November");
            DateTimeExtensions.MonthName(12).ShouldEqual("December");
            DateTimeExtensions.MonthName(0).ShouldEqual("");
            DateTimeExtensions.MonthName(13).ShouldEqual("");
        }
    }
}