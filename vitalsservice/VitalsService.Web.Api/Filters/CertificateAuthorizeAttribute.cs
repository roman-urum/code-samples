﻿using System;

namespace VitalsService.Web.Api.Filters
{
    /// <summary>
    /// Verifies access rights by provided certificate.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CertificateAuthorizeAttribute : Attribute
    {
    }
}