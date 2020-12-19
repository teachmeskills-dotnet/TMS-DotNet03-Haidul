using System;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
