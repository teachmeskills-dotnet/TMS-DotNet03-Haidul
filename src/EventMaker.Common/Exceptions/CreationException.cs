using System;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    public class CreationException : Exception
    {
        public CreationException() : base()
        {
        }

        public CreationException(string message) : base(message)
        {
        }

        public CreationException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
