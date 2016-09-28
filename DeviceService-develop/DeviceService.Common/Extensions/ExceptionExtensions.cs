using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string InnerMessages(this Exception ex)
        {
            List<string> messages = new List<string>();

            while (ex != null)
            {
                messages.Add(ex.Message);
                ex = ex.InnerException;
            }

            string result = messages.Aggregate((s1, s2) => string.Format("{0} <> {1}", s1, s2));

            return result;
        }
    }
}
