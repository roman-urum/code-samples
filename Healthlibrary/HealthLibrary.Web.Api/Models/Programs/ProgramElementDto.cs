using System;
using System.Collections.Generic;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// ProgramElementDto.
    /// </summary>
    public class ProgramElementDto
    {
        /// <summary>
        /// Gets or sets the protocol identifier.
        /// </summary>
        /// <value>
        /// The protocol identifier.
        /// </value>
        public Guid ProtocolId { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public int Sort { get; set; }

        /// <summary>
        /// Gets or sets the program day elements.
        /// </summary>
        /// <value>
        /// The program day elements.
        /// </value>
        [NotEmptyList(
            ErrorMessageResourceName = "NotEmptyListAttribute_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings))]
        public IList<ProgramDayElementDto> ProgramDayElements { get; set; }
    }
}