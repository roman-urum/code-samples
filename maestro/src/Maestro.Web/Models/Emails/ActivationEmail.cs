using Postal;

namespace Maestro.Web.Models.Emails
{
    /// <summary>
    /// ActivationEmailModel.
    /// </summary>
    public class ActivationEmail : Email
    {
        public string To { get; set; }
        public string UserName { get; set; }
        public string ConfirmationLink { get; set; }
    }
}