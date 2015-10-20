using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    /// <summary>
    /// "should"-style syntax for tests, using generic extensions 
    /// </summary>
    public static class TestUtil
    {
        #region object comparison

        /// <summary> 
        /// generic extension which
        /// uses Assert.AreEqual to assert the the operand should equal the supplied object,
        /// the expected value being the supplied object and the observed value being the operand
        /// </summary>
        /// <typeparam name="T">type of operand and supplied object</typeparam>
        /// <param name="right">operand</param>
        /// <param name="left">supplied object</param>
        public static void ShouldEqual<T>(this T right, T left)
        {
            Assert.AreEqual(left, right);
        }

        #endregion

        #region enumerables

        /// <summary>
        /// asserts that a given enumerable should enumerate a given number of elements
        /// </summary>
        /// <typeparam name="T">the type which the enumerable enumerates</typeparam>
        /// <param name="iEnumerable">the given enumerable</param>
        /// <param name="count">the expected number of elements </param>
        public static void ShouldNumber<T>(this IEnumerable<T> iEnumerable, int count)
        {
            iEnumerable.Count().ShouldEqual(count);
        }

        /// <summary>
        /// asserts that a given enumerable should enumerate no elements at all
        /// </summary>
        /// <typeparam name="T">the type which the enumerable enumerates</typeparam>
        /// <param name="iEnumerable">the given enumerable</param>
        public static void ShouldBeEmpty<T>(this IEnumerable<T> iEnumerable)
        {
            iEnumerable.ShouldNumber(0);
        }

        /// <summary>
        /// asserts that the elements enumerated by a given enumerable should include a given element
        /// </summary>
        /// <typeparam name="T">the type enumerated by the enumerable</typeparam>
        /// <param name="iEnumerable">the given enumerable</param>
        /// <param name="t">the element which should be included</param>
        public static void ShouldContain<T>(this IEnumerable<T> iEnumerable, T t)
        {
            iEnumerable.Contains(t).ShouldBeTrue();
        }

        #endregion

        #region boolean handling

        /// <summary>
        /// generic extension which uses Assert.IsTrue to assert that a condition
        /// should be true
        /// </summary>
        /// <param name="condition"></param>
        public static void ShouldBeTrue(this bool condition)
        {
            Assert.IsTrue(condition);
        }

        /// <summary>
        /// generic extension which uses Assert.IsFalse to assert that a condition
        /// should be false
        /// </summary>
        /// <param name="condition"></param>
        public static void ShouldBeFalse(this bool condition)
        {
            Assert.IsFalse(condition);
        }

        #endregion
    }
}