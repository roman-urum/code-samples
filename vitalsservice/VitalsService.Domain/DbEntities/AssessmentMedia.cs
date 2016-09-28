using System;
using System.Collections.Generic;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Db entity of assessment media.
    /// </summary>
    public class AssessmentMedia : Entity, IAuditable
    {
        /// <summary>
        /// Id of patient's customer.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Id of patient for which media is created.
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
        /// String representation of type of uploaded meadia.
        /// </summary>
        public string MediaTypeString
        {
            get { return MediaType.ToString(); }
            private set { MediaType = (MediaType) Enum.Parse(typeof (MediaType), value, true); }
        }

        /// <summary>
        /// Type of uploaded meadia.
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// Key to retrieve file from storage.
        /// </summary>
        public string StorageKey { get; set; }

        /// <summary>
        /// Date when record was created in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Date when record was updated last time in UTC.
        /// </summary>
        public DateTime UpdatedUtc { get; set; }

        /// <summary>
        /// Assessment values related with this media.
        /// </summary>
        public virtual ICollection<AssessmentValue> AssessmentValues { get; set; } 
    }
}
