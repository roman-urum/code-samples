using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.SearchPatients;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Gets the brief patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<BriefPatientViewModel> GetBriefPatient(Guid patientId);

        /// <summary>
        /// Gets the full patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<FullPatientViewModel> GetFullPatient(Guid patientId);

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreatePatient(CreatePatientRequestDto request);

        /// <summary>
        /// Updates the patient.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Task.
        /// </returns>
        Task UpdatePatient(UpdatePatientRequestDto request);

        /// <summary>
        /// Gets the identifiers within customer scope.
        /// </summary>
        /// <returns></returns>
        Task<IList<IdentifierViewModel>> GetIdentifiersWithinCustomerScope();

        /// <summary>
        /// Suggestions the search.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<IList<SuggestionSearchPatientResultViewModel>> SuggestionSearch(SearchPatientsViewModel searchRequest);

        /// <summary>
        /// Searches the specified search request.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<PagedResult<FullPatientViewModel>> Search(SearchPatientsViewModel searchRequest);

        /// <summary>
        /// Gets the details of patient which was found.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<SearchPatientDetailsViewModel> GetPatientSearchDetails(Guid patientId);
    }
}