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
    }
}