using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maestro.Web.DataAnnotations
{
    public class DateGreaterThan1752 : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is DateTime)) return false;

            DateTime dateValue = (DateTime)value;

            return dateValue.Year >= 1752;
        }
    }
}