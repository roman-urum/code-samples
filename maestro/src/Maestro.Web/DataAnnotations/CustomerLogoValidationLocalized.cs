using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maestro.Web.DataAnnotations
{
    using System.ComponentModel.DataAnnotations;

    using Maestro.Web.Resources;

    public class CustomerLogoValidationLocalized:ValidationAttribute
    {
        public CustomerLogoValidationLocalized(string resourceId)
        {
            ErrorMessage = GlobalStrings.ResourceManager.GetString(resourceId);
        }

        public override bool IsValid(object value)
        {
            return false;
        }
    }
}