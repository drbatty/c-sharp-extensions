using System;
using CSharpExtensions.Delegate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Delegate
{
    [TestClass]
    public class ActionExtensionsTests
    {
        [TestMethod]
        public void Project1Test()
        {
            var total = 0;

            Action<int, int> action = (x, y) => total += 2 * x + 5 * y;

            var projected = action.Project1(2);
            projected(3);

            total.ShouldEqual(19);
        }

        [TestMethod]
        public void Project2Test()
        {
            var total = 0;

            Action<int, int> action = (x, y) => total += 2 * x + 5 * y;

            var projected = action.Project2(2);
            projected(3);

            total.ShouldEqual(16);
        }

        public void ThrowsError()
        {
            throw new Exception();
        }

        public void DoesntThrowError()
        {

        }

        [TestMethod]
        public void SuppressTest()
        {
            Action action = ThrowsError;
            var doesntThrowError = action.Suppress();
            doesntThrowError();
        }

        [TestMethod]
        public void InvokeSuppressedTest()
        {
            Action action = ThrowsError;
            action.InvokeSuppressed();
        }

        [TestMethod]
        public void SuppressExceptionTest()
        {
            Action action = ThrowsError;
            var stillThrowsError = action.Suppress<NotImplementedException>();
            var thrown = false;
            try
            {
                stillThrowsError();
            }
            catch (Exception)
            {
                thrown = true;
            }
            thrown.ShouldBeTrue();
        }

        [TestMethod]
        public void SuppressExceptionTestNoException()
        {
            Action action = DoesntThrowError;
            action.InvokeSuppressed();
        }

        [TestMethod]
        public void ToFuncTest()
        {
            Action<int> action = Console.WriteLine;
            var func = action.ToFunc<int, string>();
            Assert.IsNull(func(0));
        }
    }
}
