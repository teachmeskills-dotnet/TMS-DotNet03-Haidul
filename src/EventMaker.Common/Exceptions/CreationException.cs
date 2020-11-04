using System;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    public class OtherException : Exception
    {
        public OtherException() : base()
        {
        }

        public OtherException(string message) : base(message)
        {
        }

        public OtherException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
