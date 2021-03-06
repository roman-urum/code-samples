﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.Conditions
{
    /// <summary>
    /// ConditionRequestDto.
    /// </summary>
    [JsonObject]
    public class ConditionRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual IList<string> Tags { get; set; }
    }
}