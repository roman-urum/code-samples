using System;
using System.Web;
using DeviceService.Domain.Entities;
using DeviceService.DomainLogic.Services.Interfaces;
using DeviceService.Web.Api.Extensions;
using Microsoft.Practices.ServiceLocation;

namespace DeviceService.Web.Api.Security
{
    /// <summary>
    /// Container for authorization certificate context.
    /// </summary>
    public class CertificateAuthContext
    {
        /// <summary>
        /// Returns instance for current authorization context.
        /// </summary>
        public static ICertificateAuthContext Current
        {
            get { return ServiceLocator.Current.GetInstance<ICertificateAuthContext>(); }
        }
    }

    /// <summary>
    /// Provides request auth data.
    /// </summary>
    public interface ICertificateAuthContext
    {
        /// <summary>
        /// Identifies if request contains client certificate.
        /// </summary>
        bool IsAuthorizedRequest { get; }

        /// <summary>
        /// Id of device entity in db. Null if certificate not provided in request.
        /// </summary>
        Guid DeviceId { get; }
    }

    /// <summary>
    /// Default implementation of ICertificateAuthRequest
    /// </summary>
    public class DefaultCertificateAuthContext : ICertificateAuthContext
    {
        /// <summary>
        /// Identifies if request contains client certificate.
        /// </summary>
        public bool IsAuthorizedRequest { get; private set; }

        /// <summary>
        /// Id of device entity in db. Null if certificate provided in request is not valid or 
        /// if device with this certificate not exists in db.
        /// </summary>
        public Guid DeviceId { get; private set; }

        public DefaultCertificateAuthContext(IDevicesService deviceService)
        {
            var clientCertificate = HttpContext.Current.Request.GetClientCertificate();

            if (clientCertificate == null)
            {
                return;
            }

            Device device = deviceService.GetDevice(clientCertificate.Thumbprint);

            if (device == null)
            {
                return;
            }

            DeviceId = device.Id;
            IsAuthorizedRequest = true;
        }
    }
}