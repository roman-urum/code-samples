using System.Linq;
using System.Web.Mvc;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// ModelStateExtensions.
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Gets the error messages.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <returns></returns>
        public static string GetErrorMessages(this ModelStateDictionary modelState)
        {
            var errors = modelState.Values.Where(v => v.Errors.Any()).SelectMany(v => v.Errors).Select(er => er.ErrorMessage);
            var errMessages = errors.Aggregate((s1, s2) => string.Format("{0} <> {1}", s1, s2));

            return errMessages;
        }
    }
}