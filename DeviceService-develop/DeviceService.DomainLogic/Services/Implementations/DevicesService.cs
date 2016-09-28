using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrockfordBase32;
using DeviceService.ApiAccess;
using DeviceService.ApiAccess.DataProviders;
using DeviceService.Common;
using DeviceService.Common.Extensions;
using DeviceService.Common.Helpers;
using DeviceService.Domain.Dtos.MessagingHub;
using DeviceService.Domain.Dtos.TokenService;
using DeviceService.DomainLogic.Services.Interfaces;
using DeviceService.DataAccess;
using DeviceService.Domain.Dtos;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.Domain.Dtos.iHealth;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Entities.Enums;
using Newtonsoft.Json;

namespace DeviceService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// DeviceService.
    /// </summary>
    public class DevicesService : IDevicesService
    {
        private const int DuplicateUseriHealthErrorCode = 5010;
        private const string CustomerTagTemplate = "maestro-customer-{0}";
        private const string PatientTagTemplate = "maestro-patientId-{0}";
        private const string DeviceTagTemplate = "maestro-device-{0}";
        private const string CustomerSenderTemplate = "maestro-customer-{0}";

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Device> deviceRepository;
        private readonly IMessagingHubDataProvider notificationsDataProvider;
        private readonly IUsersDataProvider usersDataProvider;
        private readonly IiHealthDataProvider iHealthDataProvider;
        private readonly IAppSettings appSettings;
        private readonly IiHealthSettings iHealthSettings;

        private readonly NLog.Logger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="notificationsDataProvider">The notifications data provider.</param>
        /// <param name="usersDataProvider">The users data provider.</param>
        /// <param name="appSettings">The application settings.</param>
        public DevicesService(
            IUnitOfWork unitOfWork,
            IMessagingHubDataProvider notificationsDataProvider,
            IUsersDataProvider usersDataProvider,
            IiHealthDataProvider iHealthDataProvider,
            IAppSettings appSettings,
            IiHealthSettings iHealthSettings
        )
        {
            this.unitOfWork = unitOfWork;
            this.deviceRepository = this.unitOfWork.CreateGenericRepository<Device>();
            this.notificationsDataProvider = notificationsDataProvider;
            this.usersDataProvider = usersDataProvider;
            this.iHealthDataProvider = iHealthDataProvider;
            this.appSettings = appSettings;
            this.iHealthSettings = iHealthSettings;
            this.logger = NLog.LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Device, CreateDeviceStatus>> CreateDevice(Device device)
        {
            if (device.DeviceType == DeviceType.IVR)
            {
                if (await this.IsActiveIVRExists(device.CustomerId, device.PatientId))
                {
                    return new OperationResultDto<Device, CreateDeviceStatus>(CreateDeviceStatus.IVRAlreadyExists);
                }

                device.Status = Status.Activated;
            }
            else
            {
                device.Status = Status.NotActivated;
                device.ActivationCode = await GenerateActivationCode();
            }

            if (string.IsNullOrEmpty(device.Settings.iHealthAccount) && device.DeviceType != DeviceType.IVR)
            {
                device.Settings.iHealthAccount = await this.CreateiHealthAccount();
            }

            device.LastConnectedUtc = null;
            deviceRepository.Insert(device);

            await unitOfWork.SaveAsync();

            return new OperationResultDto<Device, CreateDeviceStatus>(CreateDeviceStatus.Success, device);
        }

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        public async Task<UpdateDeviceStatus> UpdateDevice(Device device)
        {
            var existingDevice = await GetDevice(device.CustomerId, device.Id);

            if (existingDevice == null)
            {
                return UpdateDeviceStatus.DeviceNotFound;
            }

            if (!string.IsNullOrEmpty(device.Settings.PinCode))
            {
                existingDevice.Settings.PinCode = device.Settings.PinCode;
            }
            else
            {
                if (device.Settings.IsPinCodeRequired && string.IsNullOrEmpty(existingDevice.Settings.PinCode))
                {
                    return UpdateDeviceStatus.PinCodeRequired;
                }
            }

            existingDevice.PatientId = device.PatientId;
            existingDevice.BirthDate = device.BirthDate;
            existingDevice.DeviceModel = device.DeviceModel;
            existingDevice.DeviceTz = device.DeviceTz;
            existingDevice.Settings.IsWeightAutomated = device.Settings.IsWeightAutomated;
            existingDevice.Settings.IsWeightManual = device.Settings.IsWeightManual;
            existingDevice.Settings.IsBloodPressureAutomated = device.Settings.IsBloodPressureAutomated;
            existingDevice.Settings.IsBloodPressureManual = device.Settings.IsBloodPressureManual;
            existingDevice.Settings.IsPulseOxAutomated = device.Settings.IsPulseOxAutomated;
            existingDevice.Settings.IsPulseOxManual = device.Settings.IsPulseOxManual;
            existingDevice.Settings.IsBloodGlucoseAutomated = device.Settings.IsBloodGlucoseAutomated;
            existingDevice.Settings.IsBloodGlucoseManual = device.Settings.IsBloodGlucoseManual;
            existingDevice.Settings.IsPeakFlowAutomated = device.Settings.IsPeakFlowAutomated;
            existingDevice.Settings.IsPeakFlowManual = device.Settings.IsPeakFlowManual;
            existingDevice.Settings.IsTemperatureAutomated = device.Settings.IsTemperatureAutomated;
            existingDevice.Settings.IsTemperatureManual = device.Settings.IsTemperatureManual;
            existingDevice.Settings.IsPedometerAutomated = device.Settings.IsPedometerAutomated;
            existingDevice.Settings.IsPedometerManual = device.Settings.IsPedometerManual;
            existingDevice.Settings.IsPinCodeRequired = device.Settings.IsPinCodeRequired;
            existingDevice.Settings.BloodGlucosePeripheral = device.Settings.BloodGlucosePeripheral;

            if (existingDevice.DeviceType != DeviceType.IVR)
            {
                if (!string.IsNullOrEmpty(device.Settings.iHealthAccount))
                {
                    existingDevice.Settings.iHealthAccount = device.Settings.iHealthAccount;
                }
                else if (string.IsNullOrEmpty(existingDevice.Settings.iHealthAccount))
                {
                    existingDevice.Settings.iHealthAccount = await this.CreateiHealthAccount();
                }
            }

            deviceRepository.Update(existingDevice);

            await unitOfWork.SaveAsync();
            await this.SendDeviceUpdateNotification(existingDevice);

            return UpdateDeviceStatus.Success;
        }

        /// <summary>
        /// Deletes the device.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">Device not found</exception>
        /// <exception cref="System.InvalidOperationException">Invalid device status.</exception>
        public async Task<DeleteDeviceStatus> DeleteDevice(int customerId, Guid deviceId)
        {
            var device = (await this.deviceRepository.FindAsync(
                d => d.Id == deviceId && d.CustomerId == customerId && !d.IsDeleted, o => o.OrderBy(d => d.Id)))
                .FirstOrDefault();

            if (device == null)
            {
                return DeleteDeviceStatus.DeviceNotFound;
            }

            if (device.Status != Status.NotActivated && device.Status != Status.DecommissionCompleted)
            {
                return DeleteDeviceStatus.InvalidDeviceStatus;
            }

            device.IsDeleted = true;

            await this.unitOfWork.SaveAsync();

            return DeleteDeviceStatus.Success;
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<Device>> GetDevices(int customerId, DevicesSearchDto request)
        {
            Expression<Func<Device, bool>> expression = d => d.CustomerId == customerId && !d.IsDeleted;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(d => d.DeviceModel.Contains(term));
                    }
                }

                if (request.PatientId.HasValue)
                {
                    expression = expression
                        .And(d => d.PatientId == request.PatientId.Value);
                }

                if (request.LastConnectedUtcBefore.HasValue)
                {
                    expression = expression.And(d =>
                        d.LastConnectedUtc.HasValue && d.LastConnectedUtc < request.LastConnectedUtcBefore.Value);
                }

                if (request.LastConnectedUtcAfter.HasValue)
                {
                    expression = expression.And(d =>
                        d.LastConnectedUtc.HasValue && d.LastConnectedUtc > request.LastConnectedUtcAfter.Value);
                }

                if (request.Status.HasValue)
                {
                    expression = expression.And(d =>
                        d.Status == request.Status.Value);
                }

                if (request.Type.HasValue)
                {
                    var typeString = request.Type.Value.ToString();

                    expression = expression.And(d =>
                        d.DeviceTypeString.Equals(typeString));
                }
            }

            return (await deviceRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Id),
                    null,
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                ));
        }

        /// <summary>
        /// Sets the decomission status.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">
        /// Invalid decomission status
        /// or
        /// Invalid device status.
        /// </exception>
        /// <exception cref="System.NullReferenceException">Device not found</exception>
        public async Task<OperationResultDto<Device, SetDecomissionStatusOperationStatus>> SetDecomissionStatus(
            int customerId, Guid deviceId, Status status)
        {
            if (status < Status.DecommissionRequested || status > Status.DecommissionCompleted)
            {
                return new OperationResultDto<Device, SetDecomissionStatusOperationStatus>()
                {
                    Status = SetDecomissionStatusOperationStatus.InvalidDecomissionStatus
                };
            }

            var device = await this.GetDevice(customerId, deviceId);

            if (device == null)
            {
                return new OperationResultDto<Device, SetDecomissionStatusOperationStatus>()
                {
                    Status = SetDecomissionStatusOperationStatus.DeviceNotFound
                };
            }

            var allowChangingDecomissionStatus = Status.Activated <= device.Status &&
                                                 device.Status < Status.DecommissionCompleted;

            if (!allowChangingDecomissionStatus)
            {
                return new OperationResultDto<Device, SetDecomissionStatusOperationStatus>()
                {
                    Status = SetDecomissionStatusOperationStatus.InvalidDeviceStatus
                };
            }

            if (device.DeviceType == DeviceType.IVR)
            {
                device.Status = Status.DecommissionCompleted;
            }
            else
            {
                device.Status = status;
            }

            await this.unitOfWork.SaveAsync();

            if (device.Status == Status.DecommissionCompleted && device.DeviceType != DeviceType.IVR)
            {
                await this.DeleteCertificate(device);
            }

            if (device.Status == Status.DecommissionRequested)
            {
                await this.SendDecommisionNotification(device);
            }

            return new OperationResultDto<Device, SetDecomissionStatusOperationStatus>()
            {
                Status = SetDecomissionStatusOperationStatus.Success,
                Content = device
            };
        }

        /// <summary>
        /// Activates the device.
        /// </summary>
        /// <param name="activation">The activation.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Device, ActivateDeviceStatus>> ActivateDevice(Activation activation)
        {
            var device = await this.GetDevice(activation.ActivationCode, activation.BirthDate);

            if (device == null)
            {
                return new OperationResultDto<Device, ActivateDeviceStatus>()
                           {
                               Status = ActivateDeviceStatus.DeviceNotFound,
                               Content = null
                           };
            }

            if (device.Status != Status.NotActivated)
            {
                return new OperationResultDto<Device, ActivateDeviceStatus>()
                           {
                               Status = ActivateDeviceStatus.InvalidaDeviceStatus,
                               Content = null
                           };
            }

            var certificateRequest = new CreateCertificateRequest
            {
                Certificate = activation.Certificate,
                Thumbprint = activation.Thumbprint,
                CustomerId = device.CustomerId,
                PatientId = device.PatientId
            };

            await this.usersDataProvider.CreateCertificate(certificateRequest);
            
            device.Status = Status.Activated;
            device.DeviceId = activation.DeviceId;
            device.DeviceType = activation.DeviceType;
            device.DeviceIdType = activation.DeviceIdType;
            device.Certificate = activation.Certificate;
            device.Thumbprint = activation.Thumbprint;
            device.DeviceModel = string.IsNullOrEmpty(activation.DeviceModel) ? device.DeviceModel : activation.DeviceModel;            
            device.ActivationCode = null;
            //This is commented because it causes an exception in case of updating activated device.
            //Note that BirthDate is required in UpdateDeviceRequestDto
            //device.BirthDate = null;
            
            await this.unitOfWork.SaveAsync();

            return new OperationResultDto<Device, ActivateDeviceStatus>()
                       {
                           Status = ActivateDeviceStatus.Success,
                           Content = device
                       };
        }

        /// <summary>
        /// Gets the device.
        /// Creates iHealth account for the device if it not exists yet.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Device> GetDevice(int customerId, Guid id)
        {
            var result = (await
                this.deviceRepository.FindAsync(d => d.Id == id && d.CustomerId == customerId && !d.IsDeleted,
                    o => o.OrderBy(d => d.Id)))
                .FirstOrDefault();

            if (result != null && 
                string.IsNullOrEmpty(result.Settings.iHealthAccount) &&
                result.DeviceType != DeviceType.IVR)
            {
                result.Settings.iHealthAccount = await this.CreateiHealthAccount();

                await this.unitOfWork.SaveAsync();
            }

            return result;
        }

        /// <summary>
        /// Returns device by certificate subject.
        /// </summary>
        /// <returns></returns>
        public Device GetDevice(string thumbprint)
        {
            return this.deviceRepository.Find(d => !d.IsDeleted && d.Thumbprint.Equals(thumbprint),
                o => o.OrderBy(d => d.Id)).FirstOrDefault();
        }

        /// <summary>
        /// Returns device by certificate subject.
        /// </summary>
        /// <returns></returns>
        public async Task<Device> GetDevice(string activationCode, DateTime birthDate)
        {
            var deviceRequest = await this.deviceRepository.FindAsync(item => item.ActivationCode ==
                activationCode && item.BirthDate == birthDate &&
                !item.IsDeleted, o => o.OrderBy(d => d.Id));

            return deviceRequest.FirstOrDefault();
        }

        /// <summary>
        /// Updates time of last device connection to current.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTz">The device tz.</param>
        /// <returns>
        /// False if active device with such id is not found.
        /// </returns>
        public async Task<bool> UpdateLastConnectedUtc(
            int customerId,
            Guid deviceId,
            string deviceTz = null
        )
        {
            var searchResult = await deviceRepository
                .FindAsync(d =>
                    d.CustomerId == customerId &&
                    d.Id == deviceId &&
                    d.Status == Status.Activated &&
                    !d.IsDeleted
                );

            var device = searchResult.FirstOrDefault();

            if (device == null)
            {
                return false;
            }

            device.LastConnectedUtc = DateTime.UtcNow;
            device.DeviceTz = deviceTz;

            deviceRepository.Update(device);

            await unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// Clears the devices. Decommission activated devices and delete decommissioned devices.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns>List of decomissioned/deleted devices.</returns>
        public async Task<IList<Device>> ClearDevices(int customerId, Guid patientId)
        {
            var devices = await this.GetDevices(
                customerId,
                new DevicesSearchDto()
                {
                    PatientId = patientId,
                    Take = int.MaxValue,
                    Skip = 0
                }
                );

            foreach (var device in devices.Results)
            {
                if (device.Status == Status.Activated)
                {
                    await this.SetDecomissionStatus(customerId, device.Id, Status.DecommissionRequested);
                }
                else if (device.Status == Status.NotActivated || device.Status == Status.DecommissionCompleted)
                {
                    await this.DeleteDevice(customerId, device.Id);
                }
            }

            if (devices.Results.Any())
            {
                await this.SendSettingsChangedNotification(customerId, patientId);
            }

            return devices.Results;
        }

        #region Private Methods

        /// <summary>
        /// Generates the activation code.
        /// </summary>
        /// <returns></returns>
        private async Task<string> GenerateActivationCode()
        {
            List<string> profanityList = appSettings.ProfanityList;

            string result;
            ushort deploymentNumber = appSettings.DeploymentNumber;
            var rand = new RngCspRandom();

            do
            {
                ulong token = deploymentNumber;
                token <<= 8 * sizeof(uint);
                uint nonce = rand.Next(100000000, 999999999);
                token |= nonce;

                var encoder = new CrockfordBase32Encoding();
                result = encoder.Encode(token, false);
            }
            while (result.ContainsAny(profanityList.ToArray()) || !(await IsActivationCodeUnique(result)));

            return result;
        }

        /// <summary>
        /// Determines whether [is activation code unique] [the specified activation code].
        /// </summary>
        /// <param name="activationCode">The activation code.</param>
        /// <returns></returns>
        private async Task<bool> IsActivationCodeUnique(string activationCode)
        {
            var devicesWithTheSameActivationCode =
                await deviceRepository
                    .FindAsync(d => d.ActivationCode == activationCode, o => o.OrderBy(d => d.Id));

            return await Task.FromResult(!devicesWithTheSameActivationCode.Any());
        }

        /// <summary>
        /// Creates new iHealthAccount with random username and password.
        /// </summary>
        /// <returns></returns>
        private async Task<string> CreateiHealthAccount()
        {
            iHealthUserResponseDto response = null;
            string username = null;

            // Try create new iHealth account again if generated username already reserved.
            while (response == null ||
                   response.ErrorCode.HasValue && response.ErrorCode.Value != DuplicateUseriHealthErrorCode)
            {
                var nickname = iHealthHelper.GenerateRandomiHealthUsernamePrefix();
                var request = new CreateiHealthUserRequestDto
                {
                    UserName = string.Format("{0}@{1}", nickname, this.iHealthSettings.iHealthAccountDomain),
                    UserPassword = iHealthHelper.GeneratePassword(20, 5),
                    Nickname = nickname
                };

                response = await this.iHealthDataProvider.RegisterUser(request);

                if (response != null && !response.ErrorCode.HasValue)
                {
                    username = request.UserName;
                }
            }

            return username;
        }

        /// <summary>
        /// Checks if patient already has IVR device with active status.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsActiveIVRExists(int customerId, Guid patientId)
        {
            var IVRDevicesSearchRequest = new DevicesSearchDto
            {
                PatientId = patientId,
                Type = DeviceType.IVR,
                Status = Status.Activated
            };
            var existingIVRDevices = await this.GetDevices(customerId, IVRDevicesSearchRequest);

            return existingIVRDevices.Results.Any();
        }

        /// <summary>
        /// Sends notification using messaging hub that device was updated.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        private async Task SendDeviceUpdateNotification(Device device)
        {
            await notificationsDataProvider.SendNotification(
                new NotificationDto()
                {
                    AllTags = true,
                    Data = new
                    {
                        PatientDeviceNotification = new
                        {
                            Action = "PatientDeviceSettingsChanged",
                            CustmerId = device.CustomerId,
                            PatientId = device.PatientId
                        }
                    },
                    Message = null,
                    Sender = string.Format(CustomerSenderTemplate, device.CustomerId),
                    Tags = new[]
                    {
                            string.Format(CustomerTagTemplate, device.CustomerId),
                            string.Format(PatientTagTemplate, device.PatientId),
                            string.Format(DeviceTagTemplate, device.Id)
                    },
                    Types = new[]
                    {
                            RegistrationType.APN,
                            RegistrationType.GCM
                    }
                }
            );
        }

        /// <summary>
        /// Sends notification that decommision was requested for specified device.
        /// </summary>
        /// <returns></returns>
        private async Task SendDecommisionNotification(Device device)
        {
            var notification = new NotificationDto()
            {
                AllTags = true,
                Data = new
                {
                    PatientDeviceNotification = new
                    {
                        Action = "PatientDeviceDecommissionRequested",
                        CustmerId = device.CustomerId,
                        PatientId = device.PatientId
                    }
                },
                Message = null,
                Sender = string.Format(CustomerSenderTemplate, device.CustomerId),
                Tags = new[]
                {
                        string.Format(CustomerTagTemplate, device.CustomerId),
                        string.Format(PatientTagTemplate, device.PatientId),
                        string.Format(DeviceTagTemplate, device.Id)
                    },
                Types = new[] { RegistrationType.APN, RegistrationType.GCM, }
            };

            logger.Debug("Push notification will be sent when device decomissioning : " +
                         JsonConvert.SerializeObject(notification));

            await notificationsDataProvider.SendNotification(notification);
        }

        /// <summary>
        /// Sends notification that settings of patient device were changed.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        private async Task SendSettingsChangedNotification(int customerId, Guid patientId)
        {
            await notificationsDataProvider
                .SendNotification(
                    new NotificationDto()
                    {
                        AllTags = true,
                        Data = new
                        {
                            PatientDeviceNotification =
                                new
                                {
                                    Action = "PatientDeviceSettingsChanged",
                                    CustmerId = customerId,
                                    PatientId = patientId
                                }
                        },
                        Message = null,
                        Sender = string.Format(CustomerSenderTemplate, customerId),
                        Tags = new List<string>()
                        {
                            string.Format(CustomerTagTemplate, customerId),
                            string.Format(PatientTagTemplate, patientId)
                        },
                        Types = new List<RegistrationType>()
                        {
                            RegistrationType.APN,
                            RegistrationType.GCM
                        }
                    }
                );
        }

        /// <summary>
        /// Deletes info about certificate from token service.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        private async Task DeleteCertificate(Device device)
        {
            var deleteCertificateRequest = new DeleteCertificateRequest
            {
                Thumbprint = device.Thumbprint
            };

            await this.usersDataProvider.DeleteCertificate(deleteCertificateRequest);
        }

        #endregion
    }
}