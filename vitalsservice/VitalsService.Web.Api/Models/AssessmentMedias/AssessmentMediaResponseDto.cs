using System;
using VitalsService.Domain.Enums;

namespace VitalsService.Web.Api.Models.AssessmentMedias
{
    /// <summary>
    /// Model for data of assessment media in response.
    /// </summary>
    public class AssessmentMediaResponseDto
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
        /// Date when record was created in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Date when record was updated last time in UTC.
        /// </summary>
        public DateTime UpdatedUtc { get; set; }
    }
}