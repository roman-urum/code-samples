using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Domain.Dtos.VitalsService.Thresholds
{
    /// <summary>
    /// ThresholdsSearchDto.
    /// </summary>
    public class ThresholdsSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the default type.
        /// </summary>
        /// <value>
        /// The default type.
        /// </value>
        public ThresholdSearchType? Mode { get; set; }

        /// <summary>
        /// Gets or sets the list of condition identifiers.
        /// </summary>
        /// <value>
        /// The list of condition identifiers.
        /// </value>
        public IList<Guid> ConditionIds { get; set; }
    }
}
