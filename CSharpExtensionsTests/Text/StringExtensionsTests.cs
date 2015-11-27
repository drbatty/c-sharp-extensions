using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Text
{
    [TestClass]
    public class StringExtensionsTests
    {
        #region string formatting tests

        #region comma separation tests

        [TestMethod]
        public void SpacedCommaSeparateTest4()
        {
            1.Upto(3).SpacedCommaSeparate().ShouldEqual("1 , 2 , 3");
        }

        [TestMethod]
        public void CommaSeparateTest()
        {
            1.Upto(3).CommaSeparate().ShouldEqual("1,2,3");
        }

        [TestMethod]
        public void SpacedAfterCommaSeparateTest()
        {
            1.Upto(3).SpacedAfterCommaSeparate().ShouldEqual("1, 2, 3");
        }

        [TestMethod]
        public void CommaSeparate_with_prefix_should_have_correct_format()
        {
            new List<string> {"1", "2", "3"}.CommaSeparate("v").ShouldEqual("v1,v2,v3");
        }

        [TestMethod]
        public void CommaSeparate_with_prefix_should_have_correct_format_for_empty_list()
        {
            new List<string>().CommaSeparate("v").ShouldEqual(string.Empty);
        }

        [TestMethod]
        public void CommaSeparatePrefixedWithNullTest()
        {
            IEnumerable<string> stringList = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            stringList.CommaSeparate("").ShouldEqual("");
        }

        #endregion

        #endregion

        #region culture invariance

        [TestMethod]
        public void ToS_should_use_invariant_culture()
        {
            1.ToS().ShouldEqual(1.ToString(CultureInfo.InvariantCulture));
            'a'.ToS().ShouldEqual('a'.ToString(CultureInfo.InvariantCulture));
            1.0.ToS().ShouldEqual(1.0.ToString(CultureInfo.InvariantCulture));
            var now = DateTime.Now;
            now.ToS().ShouldEqual(now.ToString(CultureInfo.InvariantCulture));
        }

        #endregion

        #region character handling

        [TestMethod]
        public void CharAtTest()
        {
            "abc".CharAt(0).ShouldEqual('a');
            "abc".CharAt(2).ShouldEqual('c');
        }

        #endregion



        #region splitting and joining

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Split_over_length_one_string_should_throw_exception_if_string_does_not_have_length_one()
        {
            "abcbe".SplitOverLength1String("aa");
        }

        [TestMethod]
        public void
            Split_abcbe_over_length_one_string_b_should_be_length_three_array_of_string_containing_a_c_e_in_order()
        {
            "abcbe".SplitOverLength1String("b").ShouldNumber(3);
            "abcbe".SplitOverLength1String("b")[0].ShouldEqual("a");
            "abcbe".SplitOverLength1String("b")[1].ShouldEqual("c");
            "abcbe".SplitOverLength1String("b")[2].ShouldEqual("e");
        }

        [TestMethod]
        public void
            Split_string_over_a_length_one_string_not_in_the_string_should_return_an_array_of_string_containing_only_the_original_string
            ()
        {
            "abcbe".SplitOverLength1String("d").ShouldNumber(1);
            "abcbe".SplitOverLength1String("d")[0].ShouldEqual("abcbe");
        }

        #endregion

        #region substrings

        [TestMethod]
        public void Tail_should_return_correct_result()
        {
            "abcdefgh".Tail(4).ShouldEqual("efgh");
            "".Tail(-1).ShouldEqual("");
            "abcdefgh".Tail(9).ShouldEqual("");
        }

        #endregion

        #region replacing

        [TestMethod]
        public void ReplaceCharacter_should_give_correct_results()
        {
            "Hello".ReplaceCharacter(0, "World").ShouldEqual("Worldello");
            "Hello".ReplaceCharacter(2, "World").ShouldEqual("HeWorldlo");
            "Hello".ReplaceCharacter(4, "World").ShouldEqual("HellWorld");
        }

        #endregion

        #region formatting

        [TestMethod]
        public void RemoveEndOfLines_should_remove_end_of_line_characters_from_string()
        {
            ("a" + Environment.NewLine + "b").RemoveEndOfLines().ShouldEqual("a b");
        }

        #endregion

        #region line handling

        [TestMethod]
        public void
            String_with_two_end_of_line_chars_between_a_b_c_should_give_three_element_list_with_a_b_c_after_ToLineList()
        {
            var list = ("a" + Environment.NewLine + "b" + Environment.NewLine + "c").ToLineList();
            var enumerable = list as IList<string> ?? list.ToList();
            enumerable.ShouldNumber(3);
            enumerable.ElementAt(0).ShouldEqual("a");
            enumerable.ElementAt(1).ShouldEqual("b");
            enumerable.ElementAt(2).ShouldEqual("c");
        }

        [TestMethod]
        public void Three_element_string_list_should_return_string_with_two_end_of_line_chars_after_JoinLineSeparated()
        {
            var list = new List<string> {"a", "b", "c"};
            list.JoinLineSeparated().ShouldEqual("a" + Environment.NewLine + "b" + Environment.NewLine + "c");
        }

        [TestMethod]
        public void ContainsLine_should_return_true_if_line_is_in_string()
        {
            ("a" + Environment.NewLine + "b" + Environment.NewLine + "c").ContainsLine("a").ShouldBeTrue();
            ("a" + Environment.NewLine + "b" + Environment.NewLine + "c").ContainsLine("b").ShouldBeTrue();
            ("a" + Environment.NewLine + "b" + Environment.NewLine + "c").ContainsLine("c").ShouldBeTrue();
        }

        [TestMethod]
        public void ContainsLine_should_return_false_if_line_is_not_in_string()
        {
            ("a" + Environment.NewLine + "b" + Environment.NewLine + "c").ContainsLine("d").ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveLine_should_return_correct_format_when_line_exists_once()
        {
            ("a" + Environment.NewLine + "b" + Environment.NewLine + "c").RemoveLine("b").ShouldEqual(
                "a" + Environment.NewLine + "c");
        }

        [TestMethod]
        public void RemoveLine_should_remove_all_matching_lines()
        {
            ("a" + Environment.NewLine + "b" + Environment.NewLine + "d" + Environment.NewLine + "b" +
             Environment.NewLine + "c").
                RemoveLine("b").ShouldEqual("a" + Environment.NewLine + "d" + Environment.NewLine + "c");
        }

        #endregion

        #region case handling

        [TestMethod]
        public void EqualsIgnoreCase_should_ignore_case()
        {
            "abc".EqualsIgnoreCase("abc").ShouldBeTrue();
            "AbC".EqualsIgnoreCase("aBc").ShouldBeTrue();
        }

        #endregion

        #region looping

        [TestMethod]
        public void EachToken_should_iterate_lambda_over_all_tokens_when_split_over_separator()
        {
            var result = "";
            "a b c".EachToken(' ', t => result += t);
            result.ShouldEqual("abc");
        }

        [TestMethod]
        public void Each_should_iterate_over_each_character()
        {
            var result = "";
            "abc".Each(c => result += c);
            result.ShouldEqual("abc");
        }

        [TestMethod]
        public void Test_times()
        {
            "a".Times(5).ShouldEqual("aaaaa");
        }

        #endregion
    }
}
