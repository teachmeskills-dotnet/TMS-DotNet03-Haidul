using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EventMaker.Common.Exceptions
{
    public class EventOwerflowException : Exception
    {
        public EventOwerflowException() : base()
        {
        }

        public EventOwerflowException(string message) : base(message)
        {
        }

        public EventOwerflowException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public EventOwerflowException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
