using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;
using Swashbuckle.Swagger.Annotations;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Controllers
{
    /// <summary>
    /// Provides API methods to manage devices cetificates subjects.
    /// </summary>
    public class CertificatesController : ServiceController
    {
        private readonly ICertificatesControllerHelper certificatesControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificatesController"/> class.
        /// </summary>
        /// <param name="certificatesControllerHelper">The certificates controller helper.</param>
        public CertificatesController(ICertificatesControllerHelper certificatesControllerHelper)
        {
            this.certificatesControllerHelper = certificatesControllerHelper;
        }

        /// <summary>
        /// Returns info if access to patient is allowed with specified certificate.
        /// </summary>
        /// <returns></returns>
        [Route("api/Certificates/{thumbprint}")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Specified certificate subject not exists")]
        [SwaggerResponse(HttpStatusCode.OK, "Certificate data provided in response", typeof(CertificateResponseModel))]
        public async Task<IHttpActionResult> Get(string thumbprint, int customerId, Guid? patientId = null)
        {
            var result = await this.certificatesControllerHelper.VerifyAccess(thumbprint, customerId, patientId);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        /// <summary>
        /// Creates record to allow access patient data by certificate with specified subject.
        /// </summary>
        /// <returns></returns>
        [Route("api/Certificates")]
        [SwaggerResponse(HttpStatusCode.Conflict, "Patient certificate already exists")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrect data provided")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Certificate subject successfully saved")]
        public async Task<IHttpActionResult> Post([FromBody]CreateCertificateModel model)
        {
            return StatusCode(await this.certificatesControllerHelper.CreateCertificate(model));
        }

        /// <summary>
        /// Handles requests to delete device certificate.
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns></returns>
        [System.Web.Http.Route("api/Certificates/{thumbprint}")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Certificate subject not exists")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Certificate of was deleted")]
        public async Task<IHttpActionResult> Delete(string thumbprint)
        {
            if (!await this.certificatesControllerHelper.DeleteCertificate(thumbprint))
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}