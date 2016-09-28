using System;
using System.Collections.Generic;

using Maestro.Domain.Dtos.PatientsService;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// CareManagerViewModel.
    /// </summary>
    public class CareManagerViewModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
    }
}