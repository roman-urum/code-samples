using System;

namespace Maestro.Domain.Dtos.VitalsService.AlertSeverities
{
    /// <summary>
    /// AlertSeverityResponseDto.
    /// </summary>
    public class AlertSeverityResponseDto : IEquatable<AlertSeverityResponseDto>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the colour code.
        /// </summary>
        /// <value>
        /// The colour code.
        /// </value>
        public string ColorCode { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public int Severity { get; set; }

        #region Implementation of IEquatable<AlertSeverityResponseDto>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(AlertSeverityResponseDto other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var alertSeverityObj = obj as AlertSeverityResponseDto;

            if (alertSeverityObj == null)
            {
                return false;
            }

            return Equals(alertSeverityObj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #region operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="alertSeverity1">The alert severity1.</param>
        /// <param name="alertSeverity2">The alert severity2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(AlertSeverityResponseDto alertSeverity1, AlertSeverityResponseDto alertSeverity2)
        {
            if ((object)alertSeverity1 == null || (object)alertSeverity2 == null)
            {
                return object.Equals(alertSeverity1, alertSeverity2);
            }

            return alertSeverity1.Equals(alertSeverity2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="alertSeverity1">The alert severity1.</param>
        /// <param name="alertSeverity2">The alert severity2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(AlertSeverityResponseDto alertSeverity1, AlertSeverityResponseDto alertSeverity2)
        {
            if ((object) alertSeverity1 == null || (object) alertSeverity2 == null)
            {
                return !object.Equals(alertSeverity1, alertSeverity2);
            }

            return !alertSeverity1.Equals(alertSeverity2);
        }

        #endregion
    }
}