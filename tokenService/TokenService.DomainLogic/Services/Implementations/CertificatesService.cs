using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business logic for devices' certificates.
    /// </summary>
    public class CertificatesService : ICertificatesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<DeviceCertificate> devicesCertificatesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificatesService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CertificatesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.devicesCertificatesRepository = unitOfWork.CreateGenericRepository<DeviceCertificate>();
        }

        /// <summary>
        /// Returns certificate with specified thumbprint.
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns></returns>
        public async Task<DeviceCertificate> GetCertificate(string thumbprint)
        {
            var result = await devicesCertificatesRepository.FindAsync(d => d.Thumbprint.Equals(thumbprint));

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Creates record in db with info about certificate.
        /// </summary>
        /// <param name="deviceCertificate"></param>
        /// <returns></returns>
        public async Task CreateCertificate(DeviceCertificate deviceCertificate)
        {
            this.devicesCertificatesRepository.Insert(deviceCertificate);

            await unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Deletes certificate with specified thumbprint.
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns>False in case if certificate not found.</returns>
        public async Task<bool> DeleteCertificate(string thumbprint)
        {
            var instance = await this.GetCertificate(thumbprint);

            if (instance == null)
            {
                return false;
            }

            this.devicesCertificatesRepository.Delete(instance);

            await unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// Verify that a nonce was signed by one of the patient's certificate's corresponding keys.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="nonce">The nonce.</param>
        /// <param name="signature">The signature.</param>
        /// <returns></returns>
        public async Task<bool> VerifySignature(int customerId, Guid patientId, byte[] nonce, byte[] signature)
        {
            var deviceCertificates = await devicesCertificatesRepository.FindAsync(d => d.CustomerId == customerId && d.PatientId == patientId);

            if (deviceCertificates.Any())
            {
                var verifyMessage = string.Concat(
                    Convert.ToBase64String(nonce), 
                    '\n',
                    customerId, 
                    '\n',
                    patientId.ToString().ToLowerInvariant()
                );

                var verifyMessageBytes = Encoding.UTF8.GetBytes(verifyMessage);

                using (var hash = SHA256.Create())
                {
                    foreach (var deviceCertificate in deviceCertificates)
                    {
                        var verified = false;

                        try
                        {
                            var certificateBytes = Convert.FromBase64String(deviceCertificate.Certificate);
                            var certificate = new X509Certificate2(certificateBytes);
                            var publicKey = (RSACryptoServiceProvider)certificate.PublicKey.Key;

                            verified = publicKey.VerifyData(verifyMessageBytes, hash, signature);
                        }
                        catch (Exception)
                        {
                            // TODO: Add logging
                        }

                        if (verified)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}