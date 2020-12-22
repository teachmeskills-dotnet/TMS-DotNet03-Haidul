using System.Threading.Tasks;

namespace EventMaker.BLL.Interfaces
{
    /// <summary>
    /// Email service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="message">Message.</param>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
