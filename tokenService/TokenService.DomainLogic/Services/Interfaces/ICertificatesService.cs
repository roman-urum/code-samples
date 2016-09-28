using System;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Contains business logic for devices' certificates.
    /// </summary>
    public interface ICertificatesService
    {
        /// <summary>
        /// Returns certificate with specified thumbprint.
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns></returns>
        Task<DeviceCertificate> GetCertificate(string thumbprint);

        /// <summary>
        /// Creates record in db with info about certificate.
        /// </summary>
        /// <param name="deviceCertificate"></param>
        /// <returns></returns>
        Task CreateCertificate(DeviceCertificate deviceCertificate);

        /// <summary>
        /// Deletes certificate with specified thumbprint.
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns>False in case if certificate not found.</returns>
        Task<bool> DeleteCertificate(string thumbprint);

        /// <summary>
        /// Verifies the signature.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="nonce">The nonce.</param>
        /// <param name="signature">The signature.</param>
        /// <returns></returns>
        Task<bool> VerifySignature(int customerId, Guid patientId, byte[] nonce, byte[] signature);
    }
}
