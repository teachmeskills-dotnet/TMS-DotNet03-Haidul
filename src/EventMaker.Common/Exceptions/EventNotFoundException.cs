using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EventMaker.Common.Exceptions
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException() : base() { }

        public EventNotFoundException(string message) : base(message) { }

        public EventNotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
