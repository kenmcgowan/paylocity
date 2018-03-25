using System;
using System.Text;

namespace Paylocity.Benefits.Registration.Api.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Convert an exception and all its inner exceptions to a single string.
        /// </summary>
        /// <param name="exception">The exception to convert to a string.</param>
        /// <returns></returns>
        public static string ToStringRecursive(this Exception exception)
        {
            // Given the nature of exceptions, better to not let this method blow up
            // just because something went awry, but include some information that
            // hints at the fact that something odd is going on. This is definitely
            // an edge case, but it would be awful to diagnose without  :P
            if (exception == null)
            {
                return "(no exception provided)";
            }

            var buffer = new StringBuilder();

            for (var x = exception; x != null; x = x.InnerException)
            {
                buffer.AppendLine(x.ToString());
                buffer.AppendLine();
            }

            return buffer.ToString();
        }
    }
}
