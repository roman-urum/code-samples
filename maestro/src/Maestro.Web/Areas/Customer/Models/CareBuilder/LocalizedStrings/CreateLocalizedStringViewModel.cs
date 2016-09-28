using Maestro.Web.Areas.Customer.Models.CareBuilder.Medias;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings
{
    public class CreateLocalizedStringViewModel : BaseLocalizedStringViewModel
    {
        /// <summary>
        /// Info about media file for current localized string.
        /// </summary>
        public AudioFileMediaViewModel AudioFileMedia { get; set; }
    }
}