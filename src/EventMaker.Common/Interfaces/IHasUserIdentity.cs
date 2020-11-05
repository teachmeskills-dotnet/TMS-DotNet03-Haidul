namespace EventMaker.Common.Interfaces
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