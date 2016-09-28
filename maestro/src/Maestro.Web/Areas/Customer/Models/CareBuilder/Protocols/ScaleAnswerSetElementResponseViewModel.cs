using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    public class ScaleAnswerSetElementResponseViewModel : AnswerSetElementResponseViewModel
    {/// <summary>
        /// Gets or sets the low label.
        /// </summary>
        public LocalizedStringResponseDto LowLabel { get; set; }

        /// <summary>
        /// Gets or sets the mid label.
        /// </summary>
        public LocalizedStringResponseDto MidLabel { get; set; }

        /// <summary>
        /// Gets or sets the high label.
        /// </summary>
        public LocalizedStringResponseDto HighLabel { get; set; }

        /// <summary>
        /// Gets or sets the low value.
        /// </summary>
        /// <value>
        /// The low value.
        /// </value>
        public int LowValue { get; set; }

        /// <summary>
        /// Gets or sets the high value.
        /// </summary>
        /// <value>
        /// The high value.
        /// </value>
        public int HighValue { get; set; }

        /// <summary>
        /// Answer set tags.
        /// </summary>
        public IList<string> Tags { get; set; }
    }
}