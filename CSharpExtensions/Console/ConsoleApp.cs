using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.DependencyInjection.Interfaces;
using CSharpExtensions.Logging;

namespace CSharpExtensions.Console
{
    /// <summary>Wrapper console app class to provide logging, error trapping, Console.ReadKey and coloured writing</summary>
    public class ConsoleApp
    {
        public static ILoggingService Logger;

        public static void MainOuter(Action action)
        {
            System.Console.OutputEncoding = Encoding.Unicode;

            System.Console.ForegroundColor = ConsoleColor.Green;
            if (Logger != null)
                Logger.Initialise();

            try
            {
                action();
            }
            catch (Exception ex)
            {
                WriteError(ex.ToLogString());
            }
            System.Console.ReadKey(false);
        }

        public static void WriteLine(object o)
        {
            if (o.GetType().IsGenericType && o.GetType().GetGenericTypeDefinition() == typeof(List<>))
                foreach (var i in (IEnumerable)o)
                    System.Console.WriteLine(i);
            else
                System.Console.WriteLine(o);
        }

        public static void WriteLines(params object[] lines)
        {
            lines.Each(WriteLine);
        }

        public static void WriteError(object error)
        {
            WriteLine(error, ConsoleColor.Red);
        }

        public static void Write(object o, ConsoleColor colour)
        {
            System.Console.ForegroundColor = colour;
            System.Console.Write(o);
            System.Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void WriteLine(object o, ConsoleColor colour)
        {
            System.Console.ForegroundColor = colour;
            System.Console.WriteLine(o);
            System.Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void WriteErrors(params object[] errors)
        {
            errors.Each(WriteError);
        }

        public static void WriteInfo(params object[] info)
        {
            info.Each(WriteInfo);
        }

        public static void WriteInfo(object info)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(info);
        }

        public static void Write(object o)
        {
            System.Console.Write(o);
        }

        public static void Write(object o, ConsoleColor backgroundColour, ConsoleColor foregroundColour)
        {
            var currentBackgroundColour = System.Console.BackgroundColor;
            var currentForegroundColour = System.Console.ForegroundColor;
            System.Console.BackgroundColor = backgroundColour;
            System.Console.ForegroundColor = foregroundColour;
            System.Console.Write(o);
            System.Console.BackgroundColor = currentBackgroundColour;
            System.Console.ForegroundColor = currentForegroundColour;
        }

        public static void WriteLine(object o, ConsoleColor backgroundColour, ConsoleColor foregroundColour)
        {
            var currentBackgroundColour = System.Console.BackgroundColor;
            var currentForegroundColour = System.Console.ForegroundColor;
            System.Console.BackgroundColor = backgroundColour;
            System.Console.ForegroundColor = foregroundColour;
            W(o);
            System.Console.BackgroundColor = currentBackgroundColour;
            System.Console.ForegroundColor = currentForegroundColour;
        }

        public static void W(object o)
        {
            WriteLine(o);
        }
    }
}