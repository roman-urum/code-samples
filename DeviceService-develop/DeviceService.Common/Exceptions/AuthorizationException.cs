using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Common.Exceptions
{
    using System.Net;

    using DeviceService.Common.Extensions;

    /// <summary>
    /// Raise this exception when api returns error
    /// because of credentials are invalid.
    /// </summary>
    public class AuthorizationException : ServiceException
    {
        public AuthorizationException(string serviceUrl, string message)
            : base(serviceUrl, HttpStatusCode.Unauthorized, message)
        {
        }
    }
}
