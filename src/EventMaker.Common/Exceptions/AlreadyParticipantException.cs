using System;
using System.Globalization;

namespace EventMaker.Common.Exceptions
{
    public class AlreadyParticipantException : Exception
    {
        public AlreadyParticipantException() : base()
        {
        }

        public AlreadyParticipantException(string message) : base(message)
        {
        }

        public AlreadyParticipantException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

    }
}
