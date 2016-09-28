using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Domain.Dtos.HealthLibraryService
{
    /// <summary>
    /// SearchEntryDto.
    /// </summary>
    [KnownType(typeof(SearchProgramResultDto))]
    [KnownType(typeof(SearchTextAndMediaDto))]
    [KnownType(typeof(SearchEntryDto))]
    public class SearchEntryDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public SearchCategoryType Type { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public IList<string> Tags { get; set; }
    }
}