using System;
using System.Collections.Generic;
using System.Text;

namespace EventMaker.Common.Interface
{
    /// <summary>
    /// Interface for implement identity.
    /// </summary>
    public interface IHasDbIdentity
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        int Id { get; set; }
    }
}
