using FoolproofWebApi;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// NotRequiredIfNotEmptyAttribute.
    /// </summary>
    public class NotRequiredIfNotEmptyAttribute : ContingentValidationAttribute
    {
        /// <summary>
        /// Gets the default error message.
        /// </summary>
        /// <value>
        /// The default error message.
        /// </value>
        public override string DefaultErrorMessage
        {
            get
            {
                return "{0} is not required due to {1} is not empty";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotRequiredIfNotEmptyAttribute"/> class.
        /// </summary>
        /// <param name="dependentProperty">The dependent property.</param>
        public NotRequiredIfNotEmptyAttribute(string dependentProperty)
          : base(dependentProperty)
        {
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dependentValue">The dependent value.</param>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public override bool IsValid(object value, object dependentValue, object container)
        {
            if (!string.IsNullOrEmpty((string)dependentValue) && !string.IsNullOrEmpty((string)value))
            {
                return false;
            }

            return true;
        }
    }
}