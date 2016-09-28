using System;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.ScaleAnsweSet
{
    public class ScaleAnswerSetResponseViewModel : BaseScaleAnswerSetViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the low label localized.
        /// </summary>
        /// <value>
        /// The low label localized.
        /// </value>
        public BaseLocalizedStringViewModel LowLabel { get; set; }

        /// <summary>
        /// Gets or sets the mid label localized.
        /// </summary>
        /// <value>
        /// The mid label localized.
        /// </value>
        public BaseLocalizedStringViewModel MidLabel { get; set; }

        /// <summary>
        /// Gets or sets the highd label localized.
        /// </summary>
        /// <value>
        /// The highd label localized.
        /// </value>
        public BaseLocalizedStringViewModel HighLabel { get; set; }
    }
}