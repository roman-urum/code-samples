using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.Devices
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Removes existing default sessions and creates new.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateDefaultSession(Guid patientId, DefaultSessionDto defaultSessionDto)
        {
            return await this.patientsService.CreateDefaultSession(
                CustomerContext.Current.Customer.Id,
                PatientContext.Current.Patient.Id, 
                defaultSessionDto, 
                this.authDataStorage.GetToken());
        }

        /// <summary>
        /// Returns default session for specified patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<DefaultSessionResponseDto> GetDefaultSession(Guid patientId)
        {
            return await
                this.patientsService.GetDefaultSession(CustomerContext.Current.Customer.Id, patientId,
                    this.authDataStorage.GetToken());
        }

        /// <summary>
        /// Updates existing session.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <returns></returns>
        public async Task UpdateDefaultSession(Guid patientId, Guid defaultSessionId, DefaultSessionDto defaultSessionDto)
        {
            await this.patientsService.UpdateDefaultSession(
                CustomerContext.Current.Customer.Id,
                patientId,
                defaultSessionId,
                defaultSessionDto,
                this.authDataStorage.GetToken());
        }

        /// <summary>
        /// Deletes default session.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <returns></returns>
        public async Task DeleteDefaultSession(Guid patientId, Guid defaultSessionId)
        {
            await this.patientsService.DeleteDefaultSession(
                CustomerContext.Current.Customer.Id,
                patientId,
                defaultSessionId,
                this.authDataStorage.GetToken());
        }
    }
}