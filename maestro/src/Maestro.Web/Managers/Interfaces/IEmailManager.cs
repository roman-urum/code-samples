using System.Threading.Tasks;
using Maestro.Domain.DbEntities;

namespace Maestro.Web.Managers.Interfaces
{
    public interface IEmailManager
    {
        /// <summary>
        /// Sends the activation email.
        /// </summary>
        /// <param name="user">The created user.</param>
        /// <param name="passwordExpiration"></param>
        Task SendActivationEmail(User user, int? passwordExpiration);

        /// <summary>
        /// Sends the reset password email.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="passwordExpiration"></param>
        Task SendResetPasswordEmail(User user, int? passwordExpiration);
    }
}