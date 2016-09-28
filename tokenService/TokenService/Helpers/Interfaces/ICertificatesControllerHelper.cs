using System;
using System.Net;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces
{
    /// <summary>
    /// Helper for CertificatesController.
    /// </summary>
    public interface ICertificatesControllerHelper
    {
        /// <summary>
        /// Verifies that thumbprint has access to specified patient and customer.
        /// </summary>
        /// <param name="thumbprint">The thumbprint.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<CertificateResponseModel> VerifyAccess(string thumbprint, int customerId, Guid? patientId);

        /// <summary>
        /// Creates new certificate.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True if certificate successfully created.</returns>
        Task<HttpStatusCode> CreateCertificate(CreateCertificateModel model);

        /// <summary>
        /// Deletes certificate with specified thumbprint.
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns>False in case if certificate not found.</returns>
        Task<bool> DeleteCertificate(string thumbprint);
    }
}
