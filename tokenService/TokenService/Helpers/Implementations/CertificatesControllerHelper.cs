using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Implementations
{
    /// <summary>
    /// Helper for CertificatesController.
    /// </summary>
    public class CertificatesControllerHelper : ICertificatesControllerHelper
    {
        private readonly ICertificatesService certificatesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificatesControllerHelper"/> class.
        /// </summary>
        /// <param name="certificatesService">The certificates service.</param>
        public CertificatesControllerHelper(ICertificatesService certificatesService)
        {
            this.certificatesService = certificatesService;
        }


        /// <summary>
        /// Verifies that thumbprint has access to specified patient and customer.
        /// </summary>
        /// <param name="thumbprint">The thumbprint.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<CertificateResponseModel> VerifyAccess(string thumbprint, int customerId, Guid? patientId)
        {
            var certificate = await this.certificatesService.GetCertificate(thumbprint);

            if (certificate == null)
            {
                return null;
            }

            bool result;

            if (patientId.HasValue)
            {
                result =  certificate.CustomerId == customerId && certificate.PatientId == patientId;
            }
            else
            {
                result = certificate.CustomerId == customerId;
            }

            return new CertificateResponseModel
            {
                IsAllowed = result
            };
        }

        /// <summary>
        /// Creates new certificate.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True if certificate successfully created.</returns>
        public async Task<HttpStatusCode> CreateCertificate(CreateCertificateModel model)
        {
            var existedEntity = await this.certificatesService.GetCertificate(model.Thumbprint);

            if (existedEntity != null)
            {
                return HttpStatusCode.Conflict;
            }

            var entity = Mapper.Map<DeviceCertificate>(model);

            await this.certificatesService.CreateCertificate(entity);

            return HttpStatusCode.NoContent;
        }

        /// <summary>
        /// Deletes certificate with specified thumbprint.
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns>False in case if certificate not found.</returns>
        public async Task<bool> DeleteCertificate(string thumbprint)
        {
            return await this.certificatesService.DeleteCertificate(thumbprint);
        }
    }
}