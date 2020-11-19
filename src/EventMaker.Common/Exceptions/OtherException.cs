using System;
using System.Collections.Generic;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    public class OtherException<T> : Exception
    {
        public IEnumerable<T> ErrorCollection { get; set; }

        public OtherException() : base()
        {
        }

        public OtherException(string message) : base(message)
        {
        }

        public OtherException(IEnumerable<T> message)
        {
            ErrorCollection = message;
        }


        public OtherException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public OtherException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
