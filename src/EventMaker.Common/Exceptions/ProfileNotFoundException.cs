using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EventMaker.Common.Exceptions
{
    public class ProfileNotFoundException : Exception
    {
        public ProfileNotFoundException() : base()
        {
        }

        public ProfileNotFoundException(string message) : base(message)
        {
        }

        public ProfileNotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
