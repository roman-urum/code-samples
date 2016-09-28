using System;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Alert.
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Gets unique identifier for entity.
        /// </summary>
        /// IMPORTANT: We are NOT using base class Entity because in this case Id is PK and FK at the same time
        /// and we don't wanna to generate this value automatically by <see cref="T:VitalsService.DataAccess.EF.Interceptors.PrimaryKeyInterceptor"/>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AlertType? Type { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Alert"/> is acknowledged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if acknowledged; otherwise, <c>false</c>.
        /// </value>
        public bool Acknowledged { get; set; }

        /// <summary>
        /// Gets or sets the acknowledged UTC.
        /// </summary>
        /// <value>
        /// The acknowledged UTC.
        /// </value>
        public DateTime? AcknowledgedUtc { get; set; }

        /// <summary>
        /// Gets or sets the acknowledged by.
        /// </summary>
        /// <value>
        /// The acknowledged by.
        /// </value>
        public Guid? AcknowledgedBy { get; set; }

        /// <summary>
        /// Gets or sets the occurred UTC.
        /// </summary>
        /// <value>
        /// The occurred UTC.
        /// </value>
        public DateTime OccurredUtc { get; set; }

        /// <summary>
        /// Gets or sets the expires UTC.
        /// </summary>
        /// <value>
        /// The expires UTC.
        /// </value>
        public DateTime? ExpiresUtc { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets the alert severity identifier
        /// </summary>
        /// <value>
        /// The alert severity identifier
        /// </value>
        public Guid? AlertSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the alert severity
        /// </summary>
        /// <value>
        /// The alert severity
        /// </value>
        public virtual AlertSeverity AlertSeverity { get; set; }
    }
}