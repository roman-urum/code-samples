using System.Collections.Generic;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements
{
    /// <summary>
    /// BaseTextMediaElementModel.
    /// </summary>
    public class BaseTextMediaElementViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public CreateLocalizedStringViewModel Text { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }
    }
}