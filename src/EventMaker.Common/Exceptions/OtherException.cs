using System;
using System.Collections.Generic;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    /// <summary>
    /// Other exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OtherException<T> : Exception
    {
        /// <summary>
        /// Errors.
        /// </summary>
        public IEnumerable<T> ErrorCollection { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OtherException() : base()
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message"></param>
        public OtherException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        public OtherException(IEnumerable<T> message)
        {
            ErrorCollection = message;
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public OtherException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public OtherException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
