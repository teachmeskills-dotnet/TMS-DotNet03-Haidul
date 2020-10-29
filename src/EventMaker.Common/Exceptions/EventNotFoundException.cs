using System;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException() : base()
        {
        }

        public EventNotFoundException(string message) : base(message)
        {
        }

        public EventNotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}