using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Exceptions;
using Maestro.Web.Security;

using Newtonsoft.Json;

namespace Maestro.Web.Areas.Site
{
    /// <summary>
    /// Container for context of current patient. 
    /// </summary>
    public static class PatientContext
    {
        /// <summary>
        /// Returns instance for current customer context.
        /// </summary>
        public static IPatientContext Current
        {
            get { return DependencyResolver.Current.GetService<IPatientContext>(); }
        }
    }

    /// <summary>
    /// Defines required interface for patient context.
    /// </summary>
    public interface IPatientContext
    {
        /// <summary>
        /// Provides info about requested patient.
        /// </summary>
        PatientDto Patient { get; }
    }

    /// <summary>
    /// Context initialized by "patientId" value in url query string.
    /// Context returns exception if "patientId" not specified in query string.
    /// </summary>
    public class DefaultPatientContext : IPatientContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPatientContext"/> class.
        /// </summary>
        /// <param name="patientsService">The patients service.</param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        /// <exception cref="ContextUsageException">Patient context is require patientId specified in request query string.</exception>
        public DefaultPatientContext(
            IPatientsService patientsService,
            IAuthDataStorage authDataStorage
        )
        {
            var queryStringValue = HttpContext.Current.Request.QueryString["patientId"];
            
            Guid patientId;
            
            if (queryStringValue == null || !Guid.TryParse(queryStringValue, out patientId))
            {
                if (HttpContext.Current.Request.ContentType.Contains("application/json"))
                {
                    HttpContext.Current.Request.InputStream.Position = 0;
                    StreamReader sReader = new StreamReader(HttpContext.Current.Request.InputStream);
                    var content = sReader.ReadToEnd();
                    var contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    if (string.IsNullOrEmpty(contentDictionary["PatientId"])
                        || !Guid.TryParse(contentDictionary["PatientId"], out patientId))
                    {
                        throw new ContextUsageException("Patient context requires patientId specified in request query string.");
                    }
                }
                else if (HttpContext.Current.Request.ContentType.Contains("application/x-www-form-urlencoded"))
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.Form["PatientId"])
                        || !Guid.TryParse(HttpContext.Current.Request.Form["PatientId"], out patientId))
                    {
                        throw new ContextUsageException("Patient context requires patientId specified in request query string.");
                    }
                }
                else
                {
                    throw new ContextUsageException("Patient context requires patientId specified in request query string.");
                }

                
            }

            Patient = patientsService.GetPatient(
                CustomerContext.Current.Customer.Id,
                patientId,
                false,
                authDataStorage.GetToken()
            );
        }

        /// <summary>
        /// Provides info about requested patient.
        /// </summary>
        public PatientDto Patient { get; }
    }
}