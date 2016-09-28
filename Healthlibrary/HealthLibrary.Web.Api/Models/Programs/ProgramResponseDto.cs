using System.Collections.Generic;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// Includes additional info to brief response.
    /// </summary>
    public class ProgramResponseDto : ProgramBriefResponseDto
    {
        /// <summary>
        /// Collection of protocols assigned to program.
        /// </summary>
        public new ICollection<ProgramElementResponseDto> ProgramElements { get; set; }
    }
}