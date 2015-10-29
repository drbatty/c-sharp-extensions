using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.ContainerClasses;
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

        /// <summary> 
        /// generic extension which
        /// uses Assert.AreNotEqual to assert the the operand should not equal the supplied object,
        /// the expected value being the supplied object and the observed value being the operand
        /// </summary>
        /// <typeparam name="T">type of operand and supplied object</typeparam>
        /// <param name="right">operand</param>
        /// <param name="left">supplied object</param>
        public static void ShouldNotEqual<T>(this T right, T left)
        {
            Assert.AreNotEqual(left, right);
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

        /// <summary>
        /// asserts that a given enumerable should enumerate all of the elements enumerated by a second given enumerable
        /// </summary>
        /// <typeparam name="T">the type of object which the enumerable enumerates</typeparam>
        /// <param name="iEnumerable">the given enumerable</param>
        /// <param name="contained">the second given enumerable</param>
        public static void ShouldContain<T>(this IEnumerable<T> iEnumerable, IEnumerable<T> contained)
        {
            contained.Each(iEnumerable.ShouldContain);
        }

        /// <summary>
        /// asserts that a given enumerable should enumerate all of the supplied generic arguments
        /// </summary>
        /// <typeparam name="T">the type which the enumerable enumerates</typeparam>
        /// <param name="iEnumerable">the given enumerable</param>
        /// <param name="ts">the generic arguments supplied</param>
        public static void ShouldContain<T>(this IEnumerable<T> iEnumerable, params T[] ts)
        {
            ts.Each(iEnumerable.ShouldContain);
        }

        public static void ShouldContainExactly<T>(this IEnumerable<T> iEnumerable, params T[] ts)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            enumerable.ShouldContain(ts);
            enumerable.Count().ShouldEqual(ts.Length);
        }

        #endregion

        public static void ShouldEqual<T>(this IEnumerable<T> iEnumerable, params T[] ts)
        {
            var list = iEnumerable as List<T> ?? iEnumerable.ToList();
            list.Count().ShouldEqual(ts.Length);
            ts.EachIndex(i => list[i].ShouldEqual(ts[i]));
        }

        #region integer comparison

        public static void ShouldBeAtMost(this int @int, int max)
        {
            Assert.IsTrue(@int <= max);
        }

        public static void ShouldBeAtLeast(this int @int, int max)
        {
            Assert.IsTrue(@int >= max);
        }

        public static void ShouldBeLessThan(this int @int, int max)
        {
            Assert.IsTrue(@int < max);
        }

        public static void ShouldBeMoreThan(this int @int, int max)
        {
            Assert.IsTrue(@int < max);
        }

        public static void ShouldBeBetween(this int @int, int min, int max)
        {
            @int.ShouldBeAtLeast(min);
            @int.ShouldBeAtMost(max);
        }

        public static void ShouldBeStrictlyBetween(this int @int, int min, int max)
        {
            @int.ShouldBeMoreThan(min);
            @int.ShouldBeLessThan(max);
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