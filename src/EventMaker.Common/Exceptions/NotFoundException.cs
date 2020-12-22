using System;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    /// <summary>
    /// Not found exception.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NotFoundException() : base()
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public NotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
