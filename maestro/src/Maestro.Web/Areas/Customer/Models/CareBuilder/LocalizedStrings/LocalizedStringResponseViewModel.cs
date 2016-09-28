using Maestro.Web.Areas.Customer.Models.CareBuilder.Medias;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings
{
    public class LocalizedStringViewModel : BaseLocalizedStringViewModel
    {
        /// <summary>
        /// Media info attached to current string.
        /// </summary>
        public MediaResponseViewModel AudioFileMedia { get; set; }
    }
}