using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Maestro.Common;
using Maestro.Domain.DbEntities;
using Maestro.Web.Helpers;
using Maestro.Web.Managers.Interfaces;
using Maestro.Web.Models.Emails;
using Postal;

namespace Maestro.Web.Managers.Implementations
{
    /// <summary>
    /// EmailManager.
    /// </summary>
    public class EmailManager : IEmailManager
    {
        private readonly IEmailService emailService;

        public EmailManager(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        /// <summary>
        /// Sends the admin activation email.
        /// </summary>
        /// <param name="user">The created user.</param>
        /// <param name="passwordExpiration"></param>
        public Task SendActivationEmail(User user, int? passwordExpiration)
        {
            var email = SetActivationEmailFields<ActivationEmail>(user, passwordExpiration, "ActivateAccount");

            return emailService.SendAsync(email);
        }

        /// <summary>
        /// Sends the reset password email.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="passwordExpiration"></param>
        public Task SendResetPasswordEmail(User user, int? passwordExpiration)
        {
            try
            {
                var email = SetActivationEmailFields<ResetPasswordEmail>(user, passwordExpiration, "ResetPassword");

                return emailService.SendAsync(email);
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        #region Private Methods

        private ActivationEmail SetActivationEmailFields<T>(User user, int? passwordExpiration,
            string controllerActionName) where T : ActivationEmail
        {
            var email = Activator.CreateInstance<T>();
            email.UserName = string.Format("{0} {1}", user.FirstName, user.LastName);
            email.To = user.Email;
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            var expirationTime = (Int32)(DateTime.UtcNow
                .AddHours(Settings.ActivationLinkExpirationHours)
                .Subtract(new DateTime(1970, 1, 1)))
                .TotalSeconds;

            var keys = string.Format("{0}{1}{2}", user.Email, expirationTime, passwordExpiration);

            var absoluteLink = url.Action(controllerActionName, "Account", new
            {
                area = string.Empty,
                email = user.Email,
                expires = expirationTime,
                passwordExpiration = passwordExpiration,
                token = HmacGenerator.GetHash(keys),
            }, HttpContext.Current.Request.Url.Scheme);
            email.ConfirmationLink = absoluteLink;

            return email;
        }

        #endregion
    }
}