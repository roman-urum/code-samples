using System;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// AssessmentMediaViewModel.
    /// </summary>
    public class AssessmentMediaViewModel
    {
        /// <summary>
        /// Identifier of assessment media.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of related customer.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Id of patient which created assessment media.
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Original name of media file.
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Type of file content.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Size of file content in bytes.
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Type of uploaded meadia.
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// Key to retrieve file from storage.
        /// </summary>
        public string AssessmentMediaUrl { get; set; }

        /// <summary>
        /// Date when record was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Date when record was updated last time.
        /// </summary>
        public DateTime Updated { get; set; }
    }
}