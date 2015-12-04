using System;
using System.Text;
using CSharpExtensions.Text;

namespace CSharpExtensions.Logging
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Extension to provide a loggable string representing all information about an exception and its inner exceptions.
        /// </summary>
        /// <param name="ex">The exception to log</param>
        /// <param name="additionalMessage"> An optional additional message to log</param>
        /// <returns> a loggable string representing all information about an exception and its inner exceptions.</returns>
        public static string ToLogString(this Exception ex, string additionalMessage = "")
        {
            var msg = new StringBuilder();

            if (!string.IsNullOrEmpty(additionalMessage))
                msg.AppendLine(additionalMessage);

            if (ex == null)
                return msg.ToString();

            var orgEx = ex;
            msg.AppendLine("Exception:");
            while (orgEx != null)
            {
                msg.AppendLine(orgEx.Message);
                orgEx = orgEx.InnerException;
            }

            foreach (var i in ex.Data)
            {
                msg.Append("Data :");
                msg.AppendLine(i.ToString());
            }

            if (ex.StackTrace != null)
                msg.AppendLines("StackTrace:", ex.StackTrace);

            if (ex.Source != null)
                msg.AppendLines("Source:", ex.Source);

            if (ex.TargetSite != null)
                msg.AppendLines("TargetSite:", ex.TargetSite.ToString());

            msg.AppendLine("BaseException:");
            msg.Append(ex.GetBaseException());
            return msg.ToString();
        }
    }
}
