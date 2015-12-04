using System;
using System.Collections.Generic;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.DesignPattern.Structural.Composite;
using CSharpExtensions.Text;

namespace CSharpExtensions
{
    public static class StringParsingExtensions
    {
        public static bool IsParenthesisMatch(this string str)
        {
            if (str.IsNullOrEmpty())
                return true;

            var stack = new Stack<char>();

            for (var i = 0; i < str.Length; i++)
            {
                var c = str.CharAt(i);

                switch (c)
                {
                    case '(':
                        stack.Push(c);
                        break;
                    case '{':
                        stack.Push(c);
                        break;
                    case ')':
                        if (stack.None())
                            return false;
                        if (stack.Peek() == '(')
                            stack.Pop();
                        else
                            return false;
                        break;
                    case '}':
                        if (stack.None())
                            return false;
                        if (stack.Peek() == '{')
                            stack.Pop();
                        else
                            return false;
                        break;
                }
            }
            return stack.None();
        }

        public static CompositeList<string> ParseBrackets(this string @string)
        {
            var result = new CompositeList<string>();

            var stack = new Stack<Tuple<char, string>>();

            var collected = "";

            var currentComposite = result;

            for (var i = 0; i < @string.Length; i++)
            {
                var c = @string.CharAt(i);

                switch (c)
                {
                    case '(':
                        stack.Push(c.Pair(collected));
                        collected = "";
                        var child = new CompositeList<string>();
                        currentComposite.AddChild(child);
                        currentComposite = child;
                        break;
                    case '{':
                        stack.Push(c.Pair(collected));
                        collected = "";
                        var child2 = new CompositeList<string>();
                        currentComposite.AddChild(child2);
                        currentComposite = child2;
                        break;
                    case ')':
                        if (stack.None())
                            return result;
                        if (stack.Peek().Item1 == '(')
                        {
                            currentComposite.Content = collected;
                            collected = stack.Pop().Item2;
                            currentComposite = (CompositeList<string>)currentComposite.Parent;
                        }
                        else
                            return result;
                        break;
                    case '}':
                        if (stack.None())
                            return result;
                        if (stack.Peek().Item1 == '{')
                        {
                            currentComposite.Content = collected;
                            collected = stack.Pop().Item2;
                            currentComposite = (CompositeList<string>)currentComposite.Parent;
                        }
                        else
                            return result;
                        break;
                    default:
                        collected += c;
                        break;
                }
            }
            currentComposite.Content = collected;
            return result;
        }
    }
}
