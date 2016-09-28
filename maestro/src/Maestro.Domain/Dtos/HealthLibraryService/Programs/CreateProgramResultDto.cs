using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    public class CreateProgramResultDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the protocol elements ids.
        /// </summary>
        /// <value>
        /// The protocol elements ids.
        /// </value>
        public ICollection<Guid> ProtocolElementsIds { get; set; }
    }
}
