using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.ContainerClasses
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        private static readonly int[] FirstThree = 1.ArrayUpto(3);

        [TestMethod]
        public void RemoveFirstEntry_applied_to_nonempty_array_should_return_array_of_one_less_element()
        {
            FirstThree.RemoveFirstEntry().ShouldNumber(2);
        }

        [TestMethod]
        public void
            RemoveFirstEntry_applied_to_array_of_length_2_or_more_should_have_the_old_second_element_as_its_first_element
            ()
        {
            FirstThree.RemoveFirstEntry().First().ShouldEqual(2);
        }

        [TestMethod]
        public void EachIndex_should_return_6_when_adding_elements_of_array_of_ints_1_to_3()
        {
            var total = 0;
            FirstThree.EachIndex(i => total += FirstThree[i]);
            total.ShouldEqual(6);
        }

        [TestMethod]
        public void EachIndex_should_not_throw_exception_applied_to_null_action()
        {
            FirstThree.EachIndex(null);
        }

        [TestMethod]
        public void Empty_array_should_not_have_index_0()
        {
            var strings = new string[0];
            strings.HasIndex(0).ShouldBeFalse();
        }

        [TestMethod]
        public void One_element_array_should_have_index_0()
        {
            var strings = new string[1];
            strings.HasIndex(0).ShouldBeTrue();
        }

        [TestMethod]
        public void One_element_array_should_not_have_index_1()
        {
            var strings = new string[1];
            strings.HasIndex(1).ShouldBeFalse();
        }
    }
}