using System;
using System.Collections.Generic;
using System.Text;

namespace EventMaker.Common.Interface
{
    /// <summary>
    /// Interface for implement User identity.
    /// </summary>
    public interface IHasUserIdentity
    {
        /// <summary>
        /// User Identifier.
        /// </summary>
        public string UserId { get; set; }
    }
}
