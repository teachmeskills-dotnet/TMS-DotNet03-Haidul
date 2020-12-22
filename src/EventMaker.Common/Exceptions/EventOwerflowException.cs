using System;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    /// <summary>
    /// Event overflow exception.
    /// </summary>
    public class EventOwerflowException : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EventOwerflowException() : base()
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        public EventOwerflowException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public EventOwerflowException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public EventOwerflowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
