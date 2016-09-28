using System;

namespace Maestro.Web.Areas.Site.Models.Patients.ChatMessages
{
    /// <summary>
    /// The model for posting chat messages.
    /// </summary>
    public class ChatMessageViewModel
    {
        /// <summary>
        /// Gets or sets the patient identifier. 
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }
}