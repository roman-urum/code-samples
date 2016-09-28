using System;

namespace CustomerService.ApiAccess.Exceptions
{
    // TODO: Class should be inherited from base service exception
    /// <summary>
    /// Raise this exception when api returns error
    /// because of credentials are invalid.
    /// </summary>
    public class AuthorizationException : Exception
    {
        // TODO: Exception should contains detailed info about request.
    }
}
