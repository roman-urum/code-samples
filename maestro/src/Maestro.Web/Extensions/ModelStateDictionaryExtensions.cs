using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// Extensions methods for ModelStateDictionary instances.
    /// </summary>
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Generates string with description of errors in request.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string GenerateErrorMessageDetails(this ModelStateDictionary modelState)
        {
            var errorMessages = new List<string>();

            foreach (KeyValuePair<string, ModelState> keyModelStatePair in modelState)
            {
                ModelErrorCollection errors = keyModelStatePair.Value.Errors;

                if (errors != null && errors.Count > 0)
                {
                    errorMessages.AddRange(
                        errors
                        .Select(error =>
                        {
                            if (!string.IsNullOrEmpty(error.ErrorMessage))
                            {
                                return error.ErrorMessage;
                            }

                            if (error.Exception != null && !string.IsNullOrEmpty(error.Exception.Message))
                            {
                                return error.Exception.Message;
                            }

                            return string.Empty;
                        })
                        .ToArray()
                    );
                }
            }

            return string.Join(" <> ", errorMessages);
        }
    }
}