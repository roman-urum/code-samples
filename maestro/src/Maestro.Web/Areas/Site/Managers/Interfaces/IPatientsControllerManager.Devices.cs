using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.DevicesService;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.Charts;
using Maestro.Web.Areas.Site.Models.Patients.Notes;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.CareManagers
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<IList<DeviceDto>> GetDevices(Guid patientId);

        /// <summary>
        /// Creates note
        /// </summary>
        /// <param name="request">The note request</param>
        /// <returns></returns>
        Task<BaseNoteResponseViewModel> CreateNote(CreateNoteViewModel request);

        /// <summary>
        /// Generates the device qr code.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        Task<FileViewModel> GenerateDeviceQRCode(Guid deviceId);

        /// <summary>
        /// Clears the measurements cache
        /// </summary>
        /// <returns></returns>
        void ClearMeasurementsCache(Guid patientId);

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateDevice(CreateDeviceRequestDto request);

        /// <summary>
        /// Removes the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        Task DeleteDevice(Guid deviceId);

        /// <summary>
        /// Updated list of devices
        /// </summary>
        /// <param name="devices"></param>
        /// <returns></returns>
        Task UpdateDevices(List<DeviceDto> devices);

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="deviceDto">The device dto.</param>
        /// <returns></returns>
        Task UpdateDevice(DeviceDto deviceDto);

        /// <summary>
        /// Requests the device decomission.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        Task RequestDeviceDecomission(Guid deviceId);

        /// <summary>
        /// Gets the assessment chart questions.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<IList<QuestionInfoViewModel>> GetAssessmentChartQuestions(
            GetAssessmentChartQuestionsViewModel request
        );

        /// <summary>
        /// Checks connection.
        /// </summary>
        /// <returns></returns>
        Task CheckConnection(Guid patientId);
    }
}