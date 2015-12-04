using System;
using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    public enum TestEnum
    {
        Value1, Value2, Value3
    }

    [TestClass]
    public class EnumExtensionsTestsBase 
    {
        [TestMethod]
        public void EnumToList_should_contain_all_enum_values()
        {
            EnumUtilities.EnumToList<TestEnum>().ShouldContain(TestEnum.Value1, TestEnum.Value2, TestEnum.Value3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnumToList_should_throw_argument_exception_when_applied_to_type_not_derived_from_enum()
        {
            EnumUtilities.EnumToList<int>();
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        public void EnumToStrings_should_contain_all_enum_values_as_strings_without_enum_prefix()
        {
            EnumUtilities.ToStrings<TestEnum>().ShouldContain("Value1", "Value2", "Value3");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnumToStringsshould_throw_argument_exception_when_applied_to_type_not_derived_from_enum()
        {
            EnumUtilities.ToStrings<int>();
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        public void Enum_parse_should_return_correct_values()
        {
            var value = EnumUtilities.Parse<TestEnum>("Value1");
            value.ShouldEqual(TestEnum.Value1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Enum_parse_should_throw_argument_exception_if_enum_value_does_not_exist()
        {
            EnumUtilities.Parse<TestEnum>("Nonexistent");
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        public void Enum_parse_should_default_if_argument_is_null()
        {
            var value = EnumUtilities.Parse<TestEnum>(null);
            value.ShouldEqual(TestEnum.Value1);
        }
    }
}
