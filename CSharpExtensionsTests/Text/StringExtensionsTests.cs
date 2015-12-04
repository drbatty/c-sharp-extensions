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
        #region splitting and joining

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Split_over_length_one_string_should_throw_exception_if_string_does_not_have_length_one()
        {
            "abcbe".SplitOverLength1String("aa");
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

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

        [TestMethod]
        public void Test_explode()
        {
            "explode".Explode().ShouldContainExactly("e", "x", "p", "l", "o", "d", "e");
            "".Explode().ShouldBeEmpty();
        }

        [TestMethod]
        public void TestRemoveTail()
        {
            "abc".RemoveTail().ShouldEqual("ab");
            "".RemoveTail().ShouldEqual("");
            "abcd".RemoveTail(2).ShouldEqual("ab");
            "abcd".RemoveTail(4).ShouldEqual("");
            "abcd".RemoveTail(5).ShouldEqual("");
            "abcd".RemoveTail(-1).ShouldEqual("");
        }

        #endregion

        #region wrapping tests

        [TestMethod]
        public void TestWrap()
        {
            "a".Wrap("b").ShouldEqual("bab");
        }

        [TestMethod]
        public void TestWrapInSingleQuotes()
        {
            "a".WrapInSingleQuotes().ShouldEqual("'a'");
        }

        [TestMethod]
        public void TestWrapInRoundBrackets()
        {
            "a".WrapInRoundBrackets().ShouldEqual("(a)");
        }

        #endregion

        #region escaping and sanitizing

        [TestMethod]
        public static void TestEscapeSingleQuotes()
        {
            "'a'".EscapeSingleQuotes().ShouldEqual("\\'a\\'");
        }

        #endregion

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

        [TestMethod]
        public static void TestStringFormat()
        {
            "{0} a {1}".StringFormat("b", "c").ShouldEqual("b a c");
        }

        [TestMethod]
        public static void RemoveDoubleQuotesTest()
        {
            @"""a""".RemoveDoubleQuotes().ShouldEqual("a");
        }

        [TestMethod]
        public static void RemoveNonAlphabeticTest()
        {
            "1%7££aa££55bb".RemoveNonalphabetic().ShouldEqual("aabb");
            "ЖЯabzΨABXÉèçäüß".RemoveNonalphabetic().ShouldEqual("abzABX");
            "ЖЯabzΨABXÉèçäüß".RemoveNonalphabeticInternational().ShouldEqual("abzABX");
            "ЖЯabzABXÉèçäüß".RemoveNonalphanumericInternational().ShouldEqual("ЖЯabzABXÉèçäüß");
        }

        [TestMethod]
        public static void TestRemoveExtraWhiteSpace()
        {
            (" a b c " + Environment.NewLine + "d   ").RemoveExtraWhitespace().ShouldEqual("a b c d");
        }

        [TestMethod]
        public static void TestRemoveHtmlTags()
        {
            "<a href='http://a.com'>abc</a>def".RemoveHtmlTags().ShouldEqual("abcdef");
        }
        #endregion

        #region regex handling

        [TestMethod]
        public void TestIsMatch()
        {
            "cat".IsMatch("cat").ShouldBeTrue();
        }

        #endregion

        [TestMethod]
        public static void TestToHashInt()
        {
            "hello".ToHashInt();
        }

        [TestMethod]
        public static void IsStrongPassword()
        {
            "password".IsStrongPassword().ShouldBeFalse();
            "z13%u".IsStrongPassword().ShouldBeFalse();
            "z123$g4G4".IsStrongPassword().ShouldBeTrue();
            "bababababab".IsStrongPassword().ShouldBeFalse();
            "999999999999".IsStrongPassword().ShouldBeFalse();
            "BBBBBBBBBBBfffa".IsStrongPassword().ShouldBeFalse();
        }

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

        #region parsing

        //examples from
        //http://codefool.tumblr.com/post/15288874550/list-of-valid-and-invalid-email-addresses
        [TestMethod]
        public void TestValidEmailAddress()
        {
            "a@b.com".IsValidEmailAddress().ShouldBeTrue();
            "email@example.com".IsValidEmailAddress().ShouldBeTrue();
            "firstname.lastname@example.com".IsValidEmailAddress().ShouldBeTrue();
            "email@subdomain.example.com".IsValidEmailAddress().ShouldBeTrue();
            //"firstname+lastname@example.com".IsValidEmailAddress().ShouldBeTrue();
            "email@123.123.123.123".IsValidEmailAddress().ShouldBeTrue();
            //"email@[123.123.123.123]".IsValidEmailAddress().ShouldBeTrue();
            //"“email”@example.com".IsValidEmailAddress().ShouldBeTrue();
            "1234567890@example.com".IsValidEmailAddress().ShouldBeTrue();
            "email@example-one.com".IsValidEmailAddress().ShouldBeTrue();
            "_______@example.com".IsValidEmailAddress().ShouldBeTrue();
            "email@example.name".IsValidEmailAddress().ShouldBeTrue();
            //"email@example.museum".IsValidEmailAddress().ShouldBeTrue();
            "email@example.co.jp".IsValidEmailAddress().ShouldBeTrue();
            "firstname-lastname@example.com".IsValidEmailAddress().ShouldBeTrue();

            "plainaddress".IsValidEmailAddress().ShouldBeFalse();
            "#@%^%#$@#$@#.com".IsValidEmailAddress().ShouldBeFalse();
            "@example.com".IsValidEmailAddress().ShouldBeFalse();
            "Joe Smith <email@example.com>".IsValidEmailAddress().ShouldBeFalse();
            "email.example.com".IsValidEmailAddress().ShouldBeFalse();
            "email@example@example.com".IsValidEmailAddress().ShouldBeFalse();
            //".email@example.com".IsValidEmailAddress().ShouldBeFalse();
            //"email.@example.com".IsValidEmailAddress().ShouldBeFalse();
            //"email..email@example.com".IsValidEmailAddress().ShouldBeFalse();
            //"あいうえお@example.com".IsValidEmailAddress().ShouldBeFalse();
            "email@example.com (Joe Smith)".IsValidEmailAddress().ShouldBeFalse();
            "email@example".IsValidEmailAddress().ShouldBeFalse();
            //"email@-example.com".IsValidEmailAddress().ShouldBeFalse();
            //"email@example.web".IsValidEmailAddress().ShouldBeFalse();
            "email@111.222.333.44444".IsValidEmailAddress().ShouldBeFalse();
            "email@example..com".IsValidEmailAddress().ShouldBeFalse();
            //"Abc..123@example.com".IsValidEmailAddress().ShouldBeFalse();
        }

        //examples from
        //http://journals.cambridge.org/action/faq_answer?selectedTopicID=40&selectedAnswerID=99
        [TestMethod]
        public void TestValidIpAddress()
        {
            "121.18.19.20".IsValidIpAddress().ShouldBeTrue();
            //"121.18.19.*".IsValidIpAddress().ShouldBeTrue();
            "121.18.19.0-255".IsValidIpAddress().ShouldBeTrue();
            //"121.18.*".IsValidIpAddress().ShouldBeTrue();
            //"121.18.0-255.".IsValidIpAddress().ShouldBeTrue();
            //"121.*".IsValidIpAddress().ShouldBeTrue();
            //"121.0-255.0-255.0-255".IsValidIpAddress().ShouldBeTrue();
            /*
            
             = all IP addresses in the range 121.18.0-255 (ie all addresses beginning 121.18)
            121.18.0-255.0-255 = all IP addresses beginning 121.18
             = all IP addresses beginning 121
             = all IP addresses beginning 121*/

            //"0.42.42.42".IsValidIpAddress().ShouldBeFalse();
        }

        //examples from
        //https://mathiasbynens.be/demo/url-regex
        [TestMethod]
        public void TestValidUrl()
        {
            //"http://foo.com/blah_blah".IsValidUrl().ShouldBeTrue();
            //"http://foo.com/blah_blah/".IsValidUrl().ShouldBeTrue();
            /*
            http://foo.com/blah_blah_(wikipedia)	
            http://foo.com/blah_blah_(wikipedia)_(again)	
            http://www.example.com/wpstyle/?p=364	
            https://www.example.com/foo/?bar=baz&inga=42&quux	
            http://✪df.ws/123	
            http://userid:password@example.com:8080	
            http://userid:password@example.com:8080/	
            http://userid@example.com	
            http://userid@example.com/	
            http://userid@example.com:8080	
            http://userid@example.com:8080/	
            http://userid:password@example.com	
            http://userid:password@example.com/	
            http://142.42.1.1/	
            http://142.42.1.1:8080/	
            http://➡.ws/䨹	
            http://⌘.ws	
            http://⌘.ws/	
            http://foo.com/blah_(wikipedia)#cite-1	
            http://foo.com/blah_(wikipedia)_blah#cite-1	
            http://foo.com/unicode_(✪)_in_parens	
            http://foo.com/(something)?after=parens	
            http://☺.damowmow.com/	
            http://code.google.com/events/#&product=browser
            http://j.mp
            ftp://foo.bar/baz	
            http://foo.bar/?q=Test%20URL-encoded%20stuff	
            http://مثال.إختبار	
            http://उदाहरण.परीक्षा	
            http://1337.net	
            http://a.b-c.de	
            http://223.255.255.254  
             */
        }

        [TestMethod]
        public void TestIsTwoDigitNumber()
        {
            "0".IsTwoDigitNumber(99).ShouldBeTrue();
            "1".IsTwoDigitNumber(99).ShouldBeTrue();
            "9".IsTwoDigitNumber(99).ShouldBeTrue();
            "10".IsTwoDigitNumber(99).ShouldBeTrue();
            "99".IsTwoDigitNumber(99).ShouldBeTrue();
            "000".IsTwoDigitNumber(99).ShouldBeFalse();
            "999".IsTwoDigitNumber(99).ShouldBeFalse();
            "100".IsTwoDigitNumber(99).ShouldBeFalse();
            "50".IsTwoDigitNumber(49).ShouldBeFalse();
        }

        [TestMethod]
        public void TestIsLowerCaseAlphabetic()
        {
            "a".IsLowerCaseAlphabetic().ShouldBeTrue();
            "z".IsLowerCaseAlphabetic().ShouldBeTrue();
            "A".IsLowerCaseAlphabetic().ShouldBeFalse();
            "Z".IsLowerCaseAlphabetic().ShouldBeFalse();
            "4".IsLowerCaseAlphabetic().ShouldBeFalse();
            "!".IsLowerCaseAlphabetic().ShouldBeFalse();
            "HELLO".IsLowerCaseAlphabetic().ShouldBeFalse();
            "hello".IsLowerCaseAlphabetic().ShouldBeFalse();
            "j".IsLowerCaseAlphabetic(10).ShouldBeTrue();
            "k".IsLowerCaseAlphabetic(10).ShouldBeFalse();
        }

        [TestMethod]
        public void TestIsUpperCaseAlphabetic()
        {
            "a".IsUpperCaseAlphabetic().ShouldBeFalse();
            "z".IsUpperCaseAlphabetic().ShouldBeFalse();
            "A".IsUpperCaseAlphabetic().ShouldBeTrue();
            "Z".IsUpperCaseAlphabetic().ShouldBeTrue();
            "4".IsUpperCaseAlphabetic().ShouldBeFalse();
            "!".IsUpperCaseAlphabetic().ShouldBeFalse();
            "HELLO".IsUpperCaseAlphabetic().ShouldBeFalse();
            "hello".IsUpperCaseAlphabetic().ShouldBeFalse();
            "J".IsUpperCaseAlphabetic(10).ShouldBeTrue();
            "K".IsUpperCaseAlphabetic(10).ShouldBeFalse();
        }

        [TestMethod]
        public void TestIsAlphabetic()
        {
            "a".IsAlphabetic().ShouldBeTrue();
            "z".IsAlphabetic().ShouldBeTrue();
            "A".IsAlphabetic().ShouldBeTrue();
            "Z".IsAlphabetic().ShouldBeTrue();
            "4".IsAlphabetic().ShouldBeFalse();
            "!".IsAlphabetic().ShouldBeFalse();
            "HELLO".IsAlphabetic().ShouldBeFalse();
            "hello".IsAlphabetic().ShouldBeFalse();
        }

        [TestMethod]
        public void TestIsInteger()
        {
            "1".IsInteger().ShouldBeTrue();
            "-1".IsInteger().ShouldBeTrue();
            "+1".IsInteger().ShouldBeTrue();
            "999999999".IsInteger().ShouldBeTrue();
            "9999999999".IsInteger().ShouldBeFalse();
            "adsdfgsdfg".IsInteger().ShouldBeFalse();
        }

        [TestMethod]
        public void TestIsDecimal()
        {
            "0".IsDecimal().ShouldBeTrue();
            "0.0".IsDecimal().ShouldBeTrue();
            "1.5555555".IsDecimal().ShouldBeTrue();
            "-1.555".IsDecimal().ShouldBeTrue();
            "+1".IsDecimal().ShouldBeTrue();
        }

        [TestMethod]
        public void TestLastToken()
        {
            "a b c".LastToken(' ').ShouldEqual("c");
        }

        [TestMethod]
        public void TestFromTwoDigitString()
        {
            "99".FromTwoDigitString().ShouldEqual(99);
            "00".FromTwoDigitString().ShouldEqual(0);
            "01".FromTwoDigitString().ShouldEqual(1);
        }

        [TestMethod]
        public void TestReplaceFirst()
        {
            "a bc d bc d".ReplaceFirst("d", "xy").ShouldEqual("a bc xy bc d");
            "xyz".ReplaceFirst("xy", "a").ShouldEqual("xyz");
            "v".ReplaceFirst("xy", "a").ShouldEqual("v");
            "x".ReplaceFirst(null, "b").ShouldEqual("x");
            "x".ReplaceFirst("y", "z").ShouldEqual("x");
        }

        [TestMethod]
        public void TestTruncate()
        {
            "abcdefghijklmnopqrstuvwxyz".Truncate(10).ShouldEqual("abcdefg...");
            "".Truncate(10).ShouldEqual("");
            "abcdefghij".Truncate(10).ShouldEqual("abcdefghij");
        }

        [TestMethod]
        public void TestRepresentsMonth()
        {
            "17/09/1970".RepresentsMonth(new DateTime(1970, 9, 17)).ShouldBeTrue();
            "17/09".RepresentsMonth(new DateTime(1970, 9, 17)).ShouldBeFalse(); 
        }

        [TestMethod]
        public void TestRepresentsYear()
        {
            "17/09/1970".RepresentsYear(1970).ShouldBeTrue();
            "17/09".RepresentsYear(1970).ShouldBeFalse();
        }

        [TestMethod]
        public void TestIsDate()
        {
            DateTime.Now.ToString().IsDate().ShouldBeTrue();
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

        [TestMethod]
        public void TestConvertToPascal()
        {
            "michael batty".ConvertToPascal().ShouldEqual("Michael Batty");
            //"AEE".ConvertToPascal().ShouldEqual("Aee");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConvertToPascalNull()
        {
            string nullString = null;
            nullString.ConvertToPascal();
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        public void TestConvertToPascalEmpty()
        {
            "".ConvertToPascal().ShouldEqual("");
        }

        [TestMethod]
        public void TestRepeat()
        {
            "abc".Repeat(3).ShouldEqual("abcabcabc");
            "".Repeat(3).ShouldEqual("");
        }

        #endregion

        [TestMethod]
        public static void TestCommaSeparatedToList()
        {
            "a,b,c".CommaSeparatedToList().ShouldContainExactly("a", "b", "c");
        }

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