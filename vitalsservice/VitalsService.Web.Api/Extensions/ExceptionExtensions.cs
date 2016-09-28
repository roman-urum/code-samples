using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VitalsService.Web.Api.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetInnerExceptionsMessages(this Exception ex)
        {
            List<string> resultList = new List<string>();

            while (ex != null)
            {
                resultList.Add(ex.Message);

                ex = ex.InnerException;
            }

            return resultList.Aggregate((s1, s2) => s1 + " -> " + s2);
        }
    }
}