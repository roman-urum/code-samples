using System.ComponentModel.DataAnnotations;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.ScaleAnsweSet
{
    public class ScaleAnswerSetRequestViewModel : BaseScaleAnswerSetViewModel
    {
        /// <summary>
        /// Gets or sets the low label localized.
        /// </summary>
        /// <value>
        /// The low label localized.
        /// </value>
        [Required]
        [MaxLength(1000)]
        public string LowLabel { get; set; }

        /// <summary>
        /// Gets or sets the mid label localized.
        /// </summary>
        /// <value>
        /// The mid label localized.
        /// </value>
        [MaxLength(1000)]
        public string MidLabel { get; set; }

        /// <summary>
        /// Gets or sets the highd label localized.
        /// </summary>
        /// <value>
        /// The highd label localized.
        /// </value>
        [Required]
        [MaxLength(1000)]
        public string HighLabel { get; set; }
    }
}