using System;
using FoolproofWebApi;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Medias
{
    /// <summary>
    /// Model for audio files in localized strings.
    /// </summary>
    public class AudioFileMediaViewModel
    {
        public Guid? Id { get; set; }

        [RequiredIfEmpty("Id")]
        public string FileName { get; set; }

        [RequiredIfEmpty("Id")]
        public string ContentType { get; set; }
    }
}