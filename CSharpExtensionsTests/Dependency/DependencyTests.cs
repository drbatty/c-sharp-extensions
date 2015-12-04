using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpExtensions.DependencyInjection;
using CSharpExtensions.DependencyInjection.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Dependency
{
    [TestClass]
    public class DependencyTests
    {
        /*[ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var mockTimeService = new Mock<ITimeService>();
            Providers.TimeService = mockTimeService.Object;
            var mockRandomisationService = new Mock<RandomisationService>();
            Providers.RandomisationService = mockRandomisationService.Object;
        }

        [TestMethod]
        public void TimeProviderTest()
        {
            (Providers.TimeService != null).ShouldBeTrue();
        }

        [TestMethod]
        public void RandomisationProviderTest()
        {
            (Providers.RandomisationService != null).ShouldBeTrue();
        }*/
    }
}