using System;
using System.Text;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.DependencyInjection.Interfaces;
using CSharpExtensions.Logging;

namespace CSharpExtensions.Console
{
    public abstract class ConsoleApplication
    {
        public static ILoggingService Logger;

        public void Clear()
        {
            System.Console.Clear();
        }

        public void WriteLine(object o)
        {
            System.Console.WriteLine(o);
        }

        public void W(object o)
        {
            WriteLine(o);
        }

        public void WriteLines(params object[] os)
        {
            os.Each(WriteLine);
        }

        public void W(params object[] os)
        {
            os.Each(WriteLine);
        }

        public void Write(object o)
        {
            System.Console.Write(o);
        }

        public void NewLine()
        {
            W(string.Empty);
        }

        public void Write(object o, ConsoleColor backgroundColour, ConsoleColor foregroundColour)
        {
            var currentBackgroundColour = System.Console.BackgroundColor;
            var currentForegroundColour = System.Console.ForegroundColor;
            System.Console.BackgroundColor = backgroundColour;
            System.Console.ForegroundColor = foregroundColour;
            System.Console.Write(o);
            System.Console.BackgroundColor = currentBackgroundColour;
            System.Console.ForegroundColor = currentForegroundColour;
        }

        public void W(object o, ConsoleColor backgroundColour, ConsoleColor foregroundColour)
        {
            var currentBackgroundColour = System.Console.BackgroundColor;
            var currentForegroundColour = System.Console.ForegroundColor;
            System.Console.BackgroundColor = backgroundColour;
            System.Console.ForegroundColor = foregroundColour;
            W(o);
            System.Console.BackgroundColor = currentBackgroundColour;
            System.Console.ForegroundColor = currentForegroundColour;
        }

        public void WriteError(string error)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(error);
            System.Console.ForegroundColor = ConsoleColor.Green;
        }

        public void WriteErrors(params string[] errors)
        {
            errors.Each(WriteError);
        }

        public void WriteInfo(params string[] info)
        {
            info.Each(System.Console.WriteLine);
        }

        public void WriteInfo(string info)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(info);
        }

        public abstract void Run();

        public void MainOuter(Action action)
        {
            System.Console.OutputEncoding = Encoding.UTF8;

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
    }
}