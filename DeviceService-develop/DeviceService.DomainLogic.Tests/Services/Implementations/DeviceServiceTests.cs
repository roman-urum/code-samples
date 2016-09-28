using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceService.ApiAccess;
using DeviceService.ApiAccess.DataProviders;
using DeviceService.Common;
using DeviceService.Common.Extensions;
using DeviceService.DataAccess;
using DeviceService.Domain.Dtos;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.Domain.Dtos.iHealth;
using DeviceService.Domain.Dtos.MessagingHub;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Entities.Enums;
using DeviceService.DomainLogic.Services.Implementations;
using DeviceService.DomainLogic.Services.Interfaces;
using DeviceService.DomainLogic.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DeviceService.DomainLogic.Tests.Services.Implementations
{
    [TestClass]
    public class DeviceServiceTests
    {
        private const string TestProfanityString = "coon";

        private readonly List<string> testProfanityList = new List<string> {TestProfanityString};

        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IMessagingHubDataProvider> messagingHubDataProviderMock;
        private Mock<IUsersDataProvider> usersDataProviderMock;
        private Mock<IiHealthDataProvider> iHealthDataProviderMock;
        private Mock<IAppSettings> appSettingsMock;
        private Mock<IiHealthSettings> iHealthSettingsMock;

        private TestRepository<Device> devicesRepository;

        private IDevicesService sut;

        [TestInitialize]
        public void Initialize()
        {
            // Setup
            unitOfWorkMock = new Mock<IUnitOfWork>();
            messagingHubDataProviderMock = new Mock<IMessagingHubDataProvider>();
            usersDataProviderMock = new Mock<IUsersDataProvider>();
            iHealthDataProviderMock = new Mock<IiHealthDataProvider>();
            appSettingsMock = new Mock<IAppSettings>();
            devicesRepository = new TestRepository<Device>();
            iHealthSettingsMock = new Mock<IiHealthSettings>();

            unitOfWorkMock.Setup(s => s.CreateGenericRepository<Device>())
                .Returns(devicesRepository);
            appSettingsMock.SetupGet(s => s.ProfanityList)
                .Returns(testProfanityList);

            sut = new DevicesService(unitOfWorkMock.Object, messagingHubDataProviderMock.Object,
                usersDataProviderMock.Object, iHealthDataProviderMock.Object, appSettingsMock.Object, iHealthSettingsMock.Object);
        }

        #region CreateDevice

        [TestMethod]
        public async Task CreateDevice_ActivatedIVRDeviceAlreadyExists_ReturnsError()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                DeviceType = DeviceType.IVR,
                Status = Status.Activated
            };
            var testDevice = new Device
            {
                DeviceType = DeviceType.IVR
            };

            this.devicesRepository.Refresh(new List<Device> {existingDevice});

            // Act
            var actual = await this.sut.CreateDevice(testDevice);

            // Assert
            Assert.AreEqual(CreateDeviceStatus.IVRAlreadyExists, actual.Status);
        }

        [TestMethod]
        public async Task CreateDevice_CreateIVRDevice_DeviceCreatedWithActivatedStatus()
        {
            // Arrage
            var testDevice = new Device
            {
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };

            // Act
            var actual = await this.sut.CreateDevice(testDevice);

            // Assert
            Assert.AreEqual(Status.Activated, actual.Content.Status);
        }

        [TestMethod]
        public async Task CreateDevice_WithOtherDeviceType_DeviceCreatedWithActivationCodeAndMarkedAsNotActivated()
        {
            // Arrage
            var testiHealthResponseDto = new iHealthUserResponseDto();
            var testDevice = new Device
            {
                DeviceType = DeviceType.Other,
                Settings = new DeviceSettings()
            };

            this.iHealthDataProviderMock
                .Setup(s => s.RegisterUser(It.IsAny<CreateiHealthUserRequestDto>()))
                .ReturnsAsync(testiHealthResponseDto);

            // Act
            var actual = await this.sut.CreateDevice(testDevice);

            // Assert
            Assert.AreEqual(Status.NotActivated, actual.Content.Status);
            Assert.IsNotNull(actual.Content.ActivationCode);
        }

        [TestMethod]
        public async Task CreateDevice_OtherDeviceTypeWithoutiHealthAccount_iHealthAccountCreated()
        {
            // Arrage
            var testiHealthResponseDto = new iHealthUserResponseDto();
            var testDevice = new Device
            {
                DeviceType = DeviceType.Other,
                Settings = new DeviceSettings()
            };

            this.iHealthDataProviderMock
                .Setup(s => s.RegisterUser(It.IsAny<CreateiHealthUserRequestDto>()))
                .ReturnsAsync(testiHealthResponseDto);

            // Act
            var actual = await this.sut.CreateDevice(testDevice);

            // Assert
            Assert.IsNotNull(actual.Content.Settings.iHealthAccount);
        }

        [TestMethod]
        public async Task CreateDevice_iHealthAccountSpecified_DeviceCreatedWithSpecifiediHealthAccount()
        {
            // Arrage
            const string testiHealthAccountName = "testiHealthAccount";

            var testDevice = new Device
            {
                DeviceType = DeviceType.Other,
                Settings = new DeviceSettings
                {
                    iHealthAccount = testiHealthAccountName
                }
            };

            // Act
            var actual = await this.sut.CreateDevice(testDevice);

            // Assert
            Assert.AreEqual(testiHealthAccountName, actual.Content.Settings.iHealthAccount);
        }

        [TestMethod]
        public async Task CreateDevice_DeviceHasIVRType_DeviceCreatedWithEmptyiHealthAccount()
        {
            // Arrage
            var testDevice = new Device
            {
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };

            // Act
            var actual = await this.sut.CreateDevice(testDevice);

            // Assert
            Assert.IsNull(actual.Content.Settings.iHealthAccount);
        }

        #endregion

        #region UpdateDevice

        [TestMethod]
        public async Task UpdateDevice_DeviceNotExists_ReturnsError()
        {
            // Arrange
            var testDevice = new Device
            {
                Id = Guid.NewGuid()
            };

            // Act
            var actual = await this.sut.UpdateDevice(testDevice);

            // Assert
            Assert.AreEqual(UpdateDeviceStatus.DeviceNotFound, actual);
        }

        [TestMethod]
        public async Task UpdateDevice_PinCodeRequiredButEmpty_ReturnsError()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                Settings = new DeviceSettings
                {
                    IsPinCodeRequired = true
                }
            };

            this.devicesRepository.Refresh(new List<Device> {existingDevice});

            // Act
            var actual = await this.sut.UpdateDevice(testDevice);

            // Assert
            Assert.AreEqual(UpdateDeviceStatus.PinCodeRequired, actual);
        }

        [TestMethod]
        public async Task UpdateDevice_PinCodeRequiredAndExistsInSavedData_DeviceUpdated()
        {
            // Arrange
            const string devicePinCode = "1234";

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings
                {
                    PinCode = devicePinCode
                }
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                Settings = new DeviceSettings
                {
                    IsPinCodeRequired = true
                }
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.UpdateDevice(testDevice);

            // Assert
            Assert.AreEqual(UpdateDeviceStatus.Success, actual);
        }

        [TestMethod]
        public async Task UpdateDevice_PinCodeRequiredAndExistsInSavedData_PinCodeIsNotChanged()
        {
            // Arrange
            const string devicePinCode = "1234";
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings
                {
                    PinCode = devicePinCode
                }
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                CustomerId = testCustomerId,
                Settings = new DeviceSettings
                {
                    IsPinCodeRequired = true
                }
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.UpdateDevice(testDevice);
            var actual = await this.sut.GetDevice(testCustomerId, existingDevice.Id);

            // Assert
            Assert.AreEqual(devicePinCode, actual.Settings.PinCode);
        }

        [TestMethod]
        public async Task UpdateDevice_iHealthAccountNotCreated_CreatesiHealthAccount()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                DeviceType = DeviceType.Other,
                Settings = new DeviceSettings()
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                CustomerId = testCustomerId,
                Settings = new DeviceSettings()
            };
            var testiHealthResponseDto = new iHealthUserResponseDto();

            this.iHealthDataProviderMock
                .Setup(s => s.RegisterUser(It.IsAny<CreateiHealthUserRequestDto>()))
                .ReturnsAsync(testiHealthResponseDto);
            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.UpdateDevice(testDevice);
            var actual = await this.sut.GetDevice(testCustomerId, testDevice.Id);

            // Assert
            Assert.IsNotNull(actual.Settings.iHealthAccount);
        }

        [TestMethod]
        public async Task UpdateDevice_ArgumentHasChangedDeviceTypeFlag_DeviceTypeFieldNotChanged()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                DeviceType = DeviceType.Other,
                Settings = new DeviceSettings
                {
                    iHealthAccount = "testAccount"
                }
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                CustomerId = testCustomerId,
                Settings = new DeviceSettings(),
                DeviceType = DeviceType.Android
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.UpdateDevice(testDevice);
            var actual = await this.sut.GetDevice(testCustomerId, testDevice.Id);

            // Assert
            Assert.AreEqual(DeviceType.Other, actual.DeviceType);
        }

        [TestMethod]
        public async Task UpdateDevice_ArgumentHasChangedActivationCodeFlag_ActivationCodeFieldNotChanged()
        {
            // Arrange
            const string existingActivationCode = "testActivationCode1";
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings(),
                ActivationCode = existingActivationCode
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                CustomerId = testCustomerId,
                Settings = new DeviceSettings(),
                ActivationCode = "testActivationCode2"
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.UpdateDevice(testDevice);
            var actual = await this.sut.GetDevice(testCustomerId, testDevice.Id);

            // Assert
            Assert.AreEqual(existingActivationCode, actual.ActivationCode);
        }

        [TestMethod]
        public async Task UpdateDevice_ArgumentHasChangedIsDeletedFlag_IsDeletedFieldNotChanged()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                DeviceType = DeviceType.IVR,
                IsDeleted = false,
                Settings = new DeviceSettings()
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                CustomerId = testCustomerId,
                Settings = new DeviceSettings(),
                IsDeleted = true
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.UpdateDevice(testDevice);
            var actual = await this.sut.GetDevice(testCustomerId, testDevice.Id);

            // Assert
            Assert.AreEqual(false, actual.IsDeleted);
        }

        [TestMethod]
        public async Task UpdateDevice_ArgumentHasChangedDeviceStatus_DeviceStatusNotChanged()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                DeviceType = DeviceType.IVR,
                Status = Status.Activated,
                Settings = new DeviceSettings()
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                CustomerId = testCustomerId,
                Settings = new DeviceSettings(),
                Status = Status.DecommissionAcknowledged
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.UpdateDevice(testDevice);
            var actual = await this.sut.GetDevice(testCustomerId, testDevice.Id);

            // Assert
            Assert.AreEqual(Status.Activated, actual.Status);
        }

        [TestMethod]
        public async Task UpdateDevice_UpdatedDeviceHasValidData_DeviceUpdatedSuccessfully()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                CustomerId = testCustomerId,
                Settings = new DeviceSettings
                {
                    IsWeightAutomated = true,
                    IsWeightManual = true,
                    IsBloodPressureAutomated = true,
                    IsBloodPressureManual = true,
                    IsPulseOxAutomated = true,
                    IsPulseOxManual = true,
                    IsBloodGlucoseAutomated = true,
                    IsBloodGlucoseManual = true,
                    IsPeakFlowAutomated = true,
                    IsPeakFlowManual = true,
                    IsTemperatureAutomated = true,
                    IsTemperatureManual = true,
                    IsPedometerAutomated = true,
                    IsPedometerManual = true,
                    IsPinCodeRequired = true,
                    PinCode = "1234",
                    BloodGlucosePeripheral = BloodGlucosePeripheral.diabeto
                },
                BirthDate = new DateTime(1990, 1, 1),
                DeviceModel = "testModel",
                DeviceTz = "Africa/Abidjan"
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.UpdateDevice(testDevice);
            var actual = await this.sut.GetDevice(testCustomerId, testDevice.Id);

            // Assert
            Assert.AreEqual(testDevice.BirthDate, actual.BirthDate);
            Assert.AreEqual(testDevice.DeviceModel, actual.DeviceModel);
            Assert.AreEqual(testDevice.DeviceTz, actual.DeviceTz);
            Assert.AreEqual(testDevice.Settings.PinCode, actual.Settings.PinCode);
            Assert.AreEqual(testDevice.Settings.IsWeightAutomated, actual.Settings.IsWeightAutomated);
            Assert.AreEqual(testDevice.Settings.IsWeightManual, actual.Settings.IsWeightManual);
            Assert.AreEqual(testDevice.Settings.IsBloodPressureAutomated, actual.Settings.IsBloodPressureAutomated);
            Assert.AreEqual(testDevice.Settings.IsBloodPressureManual, actual.Settings.IsBloodPressureManual);
            Assert.AreEqual(testDevice.Settings.IsPulseOxAutomated, actual.Settings.IsPulseOxAutomated);
            Assert.AreEqual(testDevice.Settings.IsPulseOxManual, actual.Settings.IsPulseOxManual);
            Assert.AreEqual(testDevice.Settings.IsBloodGlucoseAutomated, actual.Settings.IsBloodGlucoseAutomated);
            Assert.AreEqual(testDevice.Settings.IsBloodGlucoseManual, actual.Settings.IsBloodGlucoseManual);
            Assert.AreEqual(testDevice.Settings.IsPeakFlowAutomated, actual.Settings.IsPeakFlowAutomated);
            Assert.AreEqual(testDevice.Settings.IsPeakFlowManual, actual.Settings.IsPeakFlowManual);
            Assert.AreEqual(testDevice.Settings.IsTemperatureAutomated, actual.Settings.IsTemperatureAutomated);
            Assert.AreEqual(testDevice.Settings.IsTemperatureManual, actual.Settings.IsTemperatureManual);
            Assert.AreEqual(testDevice.Settings.IsPedometerAutomated, actual.Settings.IsPedometerAutomated);
            Assert.AreEqual(testDevice.Settings.IsPedometerManual, actual.Settings.IsPedometerManual);
            Assert.AreEqual(testDevice.Settings.IsPinCodeRequired, actual.Settings.IsPinCodeRequired);
            Assert.AreEqual(testDevice.Settings.BloodGlucosePeripheral, actual.Settings.BloodGlucosePeripheral);
        }

        [TestMethod]
        public async Task UpdateDevice_DeviceWasUpdated_NotificationIsSend()
        {
            // Arrange
            const int testCustomerId = 3000;
            const string expectedSubject = "PatientDeviceSettingsChanged";

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                CustomerId = testCustomerId,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };
            var testDevice = new Device
            {
                Id = existingDevice.Id,
                CustomerId = testCustomerId,
                Settings = new DeviceSettings(),
                PatientId = existingDevice.PatientId
            };
            var expectedTags = new List<string>
            {
                "maestro-patientId-{0}".FormatWith(existingDevice.PatientId),
                "maestro-device-{0}".FormatWith(existingDevice.Id),
                "maestro-customer-{0}".FormatWith(testCustomerId)
            };
            var expectedTypes = new[]
            {
                RegistrationType.APN,
                RegistrationType.GCM
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            NotificationDto actual = null;
            this.messagingHubDataProviderMock
                .Setup(s => s.SendNotification(It.IsAny<NotificationDto>()))
                .Callback<NotificationDto>((notificationDto) => { actual = notificationDto; })
                .ReturnsAsync(new NotificationResponseDto());

            await this.sut.UpdateDevice(testDevice);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedSubject,
                actual.Data.GetPropertyValue("PatientDeviceNotification").GetPropertyValue("Action"));
            Assert.AreEqual(existingDevice.CustomerId,
                actual.Data.GetPropertyValue("PatientDeviceNotification").GetPropertyValue("CustmerId"));
            Assert.AreEqual(existingDevice.PatientId,
                actual.Data.GetPropertyValue("PatientDeviceNotification").GetPropertyValue("PatientId"));
            Assert.AreEqual("maestro-customer-{0}".FormatWith(testCustomerId), actual.Sender);
            Assert.IsTrue(actual.AllTags);
            Assert.IsTrue(expectedTags.All(t => actual.Tags.Any(et => et.Equals(t))));
            Assert.IsTrue(expectedTypes.All(t => actual.Types.Any(et => et.Equals(t))));
        }

        #endregion

        #region DeleteDevice

        [TestMethod]
        public async Task DeleteDevice_DeviceNotExists_ReturnsError()
        {
            // Arrange
            const int testCustomerId = 3000;

            var notExistingDeviceId = Guid.NewGuid();

            // Act
            var actual = await this.sut.DeleteDevice(testCustomerId, notExistingDeviceId);

            // Assert
            Assert.AreEqual(DeleteDeviceStatus.DeviceNotFound, actual);
        }

        [TestMethod]
        public async Task DeleteDevice_DeviceAlreadyWasDeleted_ReturnsError()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                IsDeleted = true,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.DeleteDevice(testCustomerId, existingDevice.Id);

            // Assert
            Assert.AreEqual(DeleteDeviceStatus.DeviceNotFound, actual);
        }

        [TestMethod]
        public async Task DeleteDevice_DeviceWasActivated_ReturnsError()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                Status = Status.Activated,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.DeleteDevice(testCustomerId, existingDevice.Id);

            // Assert
            Assert.AreEqual(DeleteDeviceStatus.InvalidDeviceStatus, actual);
        }

        [TestMethod]
        public async Task DeleteDevice_DeviceNotActivated_ReturnsSuccess()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                Status = Status.NotActivated,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.DeleteDevice(testCustomerId, existingDevice.Id);

            // Assert
            Assert.AreEqual(DeleteDeviceStatus.Success, actual);
        }

        [TestMethod]
        public async Task DeleteDevice_DeviceNotActivated_DeletedDeviceNotAvailable()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                Status = Status.NotActivated,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.DeleteDevice(testCustomerId, existingDevice.Id);
            var actual = await this.sut.GetDevice(testCustomerId, existingDevice.Id);

            // Assert
            Assert.IsNull(actual);
        }

        #endregion

        #region GetDevices

        [TestMethod]
        public async Task GetDevices_DeletedDevicesExists_DeletedDevicesNotIncludedInResult()
        {
            // Arrange
            const int expectedDevicesCount = 2;
            const int testCustomerId = 3000;

            var existingDevices = new List<Device>
            {
                new Device
                {
                    Id = Guid.NewGuid(),
                    DeviceType = DeviceType.IVR,
                    Status = Status.Activated,
                    IsDeleted = false,
                    CustomerId = testCustomerId
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    DeviceType = DeviceType.Other,
                    Status = Status.NotActivated,
                    IsDeleted = false,
                    CustomerId = testCustomerId
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    DeviceType = DeviceType.Other,
                    Status = Status.NotActivated,
                    IsDeleted = true,
                    CustomerId = testCustomerId
                }
            };

            this.devicesRepository.Refresh(existingDevices);

            // Act
            var actual = await this.sut.GetDevices(testCustomerId, null);

            // Assert
            Assert.AreEqual(expectedDevicesCount, actual.Results.Count);
            Assert.IsFalse(actual.Results.Any(d => d.IsDeleted));
        }

        [TestMethod]
        public async Task GetDevices_SearchDtoSpecified_FiltersWorksCorrect()
        {
            // Arrange
            const int testCustomerId = 3000;
            const int expectedDevicesCount = 1;
            const string testModel = "model1";

            var existingDevices = new List<Device>
            {
                new Device
                {
                    Id = Guid.NewGuid(),
                    DeviceType = DeviceType.IVR,
                    Status = Status.Activated,
                    DeviceModel = testModel,
                    CustomerId = testCustomerId,
                    LastConnectedUtc = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    DeviceType = DeviceType.IVR,
                    Status = Status.DecommissionAcknowledged,
                    DeviceModel = testModel,
                    CustomerId = testCustomerId,
                    LastConnectedUtc = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    DeviceType = DeviceType.IVR,
                    Status = Status.Activated,
                    DeviceModel = testModel,
                    CustomerId = testCustomerId,
                    LastConnectedUtc = DateTime.UtcNow.AddDays(-3)
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    DeviceType = DeviceType.Other,
                    Status = Status.NotActivated,
                    DeviceModel = "model2",
                    CustomerId = testCustomerId,
                    LastConnectedUtc = DateTime.UtcNow
                }
            };
            var searchDto = new DevicesSearchDto
            {
                Q = testModel,
                LastConnectedUtcAfter = DateTime.UtcNow.AddDays(-1),
                LastConnectedUtcBefore = DateTime.UtcNow.AddDays(1),
                Status = Status.Activated
            };

            this.devicesRepository.Refresh(existingDevices);

            // Act
            var actual = await this.sut.GetDevices(testCustomerId, searchDto);

            // Assert
            Assert.AreEqual(expectedDevicesCount, actual.Results.Count);
        }

        #endregion

        #region SetDecomissionStatus

        [TestMethod]
        public async Task SetDecomissionStatus_InvalidStatusProvided_ReturnsError()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                Settings = new DeviceSettings()
            };

            this.devicesRepository.Refresh(new List<Device> {existingDevice});

            // Act
            var actual = await this.sut.SetDecomissionStatus(testCustomerId, existingDevice.Id, Status.Activated);

            // Assert
            Assert.AreEqual(SetDecomissionStatusOperationStatus.InvalidDecomissionStatus, actual.Status);
        }

        [TestMethod]
        public async Task SetDecomissionStatus_DeviceNotExists_ReturnsError()
        {
            // Arrange
            const int testCustomerId = 3000;

            var notExistingDeviceId = Guid.NewGuid();

            // Act
            var actual = await this.sut.SetDecomissionStatus(testCustomerId, notExistingDeviceId, Status.DecommissionStarted);

            // Assert
            Assert.AreEqual(SetDecomissionStatusOperationStatus.DeviceNotFound, actual.Status);
        }

        [TestMethod]
        public async Task SetDecomissionStatus_DeviceWasDeleted_ReturnsError()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                IsDeleted = true
            };

            this.devicesRepository.Refresh(new List<Device> {existingDevice});

            // Act
            var actual = await this.sut.SetDecomissionStatus(testCustomerId, existingDevice.Id, Status.DecommissionStarted);

            // Assert
            Assert.AreEqual(SetDecomissionStatusOperationStatus.DeviceNotFound, actual.Status);
        }

        [TestMethod]
        public async Task SetDecomissionStatus_DecomissionAlreadyCompleted_ReturnsError()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                Status = Status.DecommissionCompleted,
                Settings = new DeviceSettings
                {
                    iHealthAccount = "testiHealthAccount"
                }
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.SetDecomissionStatus(testCustomerId, existingDevice.Id, Status.DecommissionStarted);

            // Assert
            Assert.AreEqual(SetDecomissionStatusOperationStatus.InvalidDeviceStatus, actual.Status);
        }

        [TestMethod]
        public async Task SetDecommisionStatus_DeviceHasIVRDeviceType_StatusChangedToDecommissionCompleted()
        {
            // Arrange
            const int testCustomerId = 3000;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                Status = Status.Activated,
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettings()
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.SetDecomissionStatus(testCustomerId, existingDevice.Id, Status.DecommissionStarted);

            // Assert
            Assert.AreEqual(Status.DecommissionCompleted, actual.Content.Status);
        }

        [TestMethod]
        public async Task SetDecommisionStatus_DeviceIsNotIVR_StatusChangedToSpecified()
        {
            // Arrange
            const int testCustomerId = 3000;

            var newStatus = Status.DecommissionStarted;
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                Status = Status.Activated,
                DeviceType = DeviceType.Android,
                Settings = new DeviceSettings
                {
                    iHealthAccount = "testiHealthAccount"
                }
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.SetDecomissionStatus(testCustomerId, existingDevice.Id, newStatus);

            // Assert
            Assert.AreEqual(newStatus, actual.Content.Status);
        }

        [TestMethod]
        public async Task SetDecommissionStatus_DecommissionRequested_NotificationIsSend()
        {
            // Arrange
            const int testCustomerId = 3000;
            const string expectedSubject = "PatientDeviceDecommissionRequested";

            var newStatus = Status.DecommissionRequested;
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                Status = Status.Activated,
                DeviceType = DeviceType.Android,
                Settings = new DeviceSettings
                {
                    iHealthAccount = "testiHealthAccount"
                }
            };
            var expectedTags = new List<string>
            {
                "maestro-patientId-{0}".FormatWith(existingDevice.PatientId),
                "maestro-device-{0}".FormatWith(existingDevice.Id),
                "maestro-customer-{0}".FormatWith(testCustomerId)
            };
            var expectedTypes = new[]
            {
                RegistrationType.APN,
                RegistrationType.GCM
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            NotificationDto actual = null;
            this.messagingHubDataProviderMock
                .Setup(s => s.SendNotification(It.IsAny<NotificationDto>()))
                .Callback<NotificationDto>((notificationDto) => { actual = notificationDto; })
                .ReturnsAsync(new NotificationResponseDto());

            await this.sut.SetDecomissionStatus(testCustomerId, existingDevice.Id, newStatus);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedSubject,
                actual.Data.GetPropertyValue("PatientDeviceNotification").GetPropertyValue("Action"));
            Assert.AreEqual(existingDevice.CustomerId,
                actual.Data.GetPropertyValue("PatientDeviceNotification").GetPropertyValue("CustmerId"));
            Assert.AreEqual(existingDevice.PatientId,
                actual.Data.GetPropertyValue("PatientDeviceNotification").GetPropertyValue("PatientId"));
            Assert.AreEqual("maestro-customer-{0}".FormatWith(testCustomerId), actual.Sender);
            Assert.IsTrue(actual.AllTags);
            Assert.IsTrue(expectedTags.All(t => actual.Tags.Any(et => et.Equals(t))));
            Assert.IsTrue(expectedTypes.All(t => actual.Types.Any(et => et.Equals(t))));
        }

        #endregion

        #region ActivateDevice

        [TestMethod]
        public async Task ActivateDevice_ActivationCodeIncorrect_ReturnsError()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                ActivationCode = "actCode",
                BirthDate = new DateTime(1990, 1, 1),
                Settings = new DeviceSettings()
            };
            var activationData = new Activation
            {
                ActivationCode = "incorrectActCode",
                BirthDate = existingDevice.BirthDate.Value
            };

            this.devicesRepository.Refresh(new List<Device> {existingDevice});

            // Act
            var actual = await this.sut.ActivateDevice(activationData);

            // Assert
            Assert.AreEqual(ActivateDeviceStatus.DeviceNotFound, actual.Status);
        }

        [TestMethod]
        public async Task ActivateDevice_BirthDateIncorrect_ReturnsError()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                ActivationCode = "actCode",
                BirthDate = new DateTime(1990, 1, 1),
                Settings = new DeviceSettings()
            };
            var activationData = new Activation
            {
                ActivationCode = existingDevice.ActivationCode,
                BirthDate = new DateTime(1990, 1, 2)
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.ActivateDevice(activationData);

            // Assert
            Assert.AreEqual(ActivateDeviceStatus.DeviceNotFound, actual.Status);
        }

        [TestMethod]
        public async Task ActivateDevice_DeviceAlreadyActivated_ReturnsError()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                ActivationCode = "actCode",
                BirthDate = new DateTime(1990, 1, 1),
                Settings = new DeviceSettings(),
                Status = Status.Activated
            };
            var activationData = new Activation
            {
                ActivationCode = existingDevice.ActivationCode,
                BirthDate = existingDevice.BirthDate.Value
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.ActivateDevice(activationData);

            // Assert
            Assert.AreEqual(ActivateDeviceStatus.InvalidaDeviceStatus, actual.Status);
        }

        [TestMethod]
        public async Task ActivateDevice_CorrectActivationDataProvided_DeviceSuccessfullyActivated()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                ActivationCode = "actCode",
                BirthDate = new DateTime(1990, 1, 1),
                Settings = new DeviceSettings(),
                Status = Status.NotActivated,
                DeviceType = DeviceType.Other
            };
            var activationData = new Activation
            {
                ActivationCode = existingDevice.ActivationCode,
                BirthDate = existingDevice.BirthDate.Value,
                DeviceType = DeviceType.iOS,
                DeviceIdType = DeviceIdType.IMEI,
                DeviceId = "990000862471854",
                DeviceModel = "testModel"
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.ActivateDevice(activationData);

            // Assert
            Assert.IsNull(actual.Content.ActivationCode);
            Assert.AreEqual(Status.Activated, actual.Content.Status);
            Assert.AreEqual(activationData.DeviceType, actual.Content.DeviceType);
            Assert.AreEqual(activationData.DeviceIdType, actual.Content.DeviceIdType);
            Assert.AreEqual(activationData.DeviceId, actual.Content.DeviceId);
            Assert.AreEqual(activationData.DeviceModel, actual.Content.DeviceModel);
        }

        #endregion

        #region GetDevice

        [TestMethod]
        public async Task GetDevice_IncorrectCustomerId_ReturnsNull()
        {
            // Arrange
            const int incorrectCustomerId = 3001;

            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                DeviceType = DeviceType.IVR,
                IsDeleted = false
            };

            this.devicesRepository.Refresh(new List<Device> {existingDevice});

            // Act
            var actual = await this.sut.GetDevice(incorrectCustomerId, existingDevice.Id);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetDevice_DeviceWasDeleted_ReturnsNull()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                DeviceType = DeviceType.IVR,
                IsDeleted = true
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var actual = await this.sut.GetDevice(existingDevice.CustomerId, existingDevice.Id);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetDevice_DeviceIsAndroidWithoutiHealthAccount_iHealthAccountCreated()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                DeviceType = DeviceType.Android,
                IsDeleted = false,
                Settings = new DeviceSettings()
            };
            var iHealthResponse = new iHealthUserResponseDto();

            this.devicesRepository.Refresh(new List<Device> { existingDevice });
            this.iHealthDataProviderMock
                .Setup(s => s.RegisterUser(It.IsAny<CreateiHealthUserRequestDto>()))
                .ReturnsAsync(iHealthResponse);

            // Act
            var actual = await this.sut.GetDevice(existingDevice.CustomerId, existingDevice.Id);

            // Assert
            Assert.IsNotNull(actual.Settings.iHealthAccount);
        }

        #endregion

        #region UpdateLastConnectedUtc

        [TestMethod]
        public async Task UpdateLastConnectedUtc_DeviceNotExists_ReturnsFalse()
        {
            // Arrange
            const int testCustomerId = 3000;

            var notExistingDeviceId = Guid.NewGuid();

            // Act
            var actual = await this.sut.UpdateLastConnectedUtc(testCustomerId, notExistingDeviceId);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task UpdateLastConnectedUtc_IncorrectCustomerId_LastConnectedUtcIsNotChanged()
        {
            // Arrange
            const int incorrectCustomerId = 3001;

            var prevLastConnectedUtc = DateTime.UtcNow.AddMonths(-1);
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                DeviceType = DeviceType.IVR,
                Status = Status.NotActivated,
                Settings = new DeviceSettings(),
                LastConnectedUtc = prevLastConnectedUtc
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var result = await this.sut.UpdateLastConnectedUtc(incorrectCustomerId, existingDevice.Id);
            var actual = await this.sut.GetDevice(existingDevice.CustomerId, existingDevice.Id);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(prevLastConnectedUtc, actual.LastConnectedUtc);
        }

        [TestMethod]
        public async Task UpdateLastConnectedUtc_DeviceIsNotActivated_LastConnectedUtcIsNotChanged()
        {
            // Arrange
            var prevLastConnectedUtc = DateTime.UtcNow.AddMonths(-1);
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                DeviceType = DeviceType.IVR,
                Status = Status.NotActivated,
                Settings = new DeviceSettings(),
                LastConnectedUtc = prevLastConnectedUtc
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var result = await this.sut.UpdateLastConnectedUtc(existingDevice.CustomerId, existingDevice.Id);
            var actual = await this.sut.GetDevice(existingDevice.CustomerId, existingDevice.Id);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(prevLastConnectedUtc, actual.LastConnectedUtc);
        }

        [TestMethod]
        public async Task UpdateLastConnectedUtc_DeviceWasUpdated_DataSavedCorrect()
        {
            // Arrange
            const string newDeviceTz = "Africa/Abidjan";

            var prevLastConnectedUtc = DateTime.UtcNow.AddHours(-1);
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                DeviceType = DeviceType.IVR,
                Status = Status.Activated,
                Settings = new DeviceSettings(),
                LastConnectedUtc = prevLastConnectedUtc
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.UpdateLastConnectedUtc(existingDevice.CustomerId, existingDevice.Id, newDeviceTz);
            var actual = await this.sut.GetDevice(existingDevice.CustomerId, existingDevice.Id);

            // Assert
            Assert.IsTrue(actual.LastConnectedUtc > prevLastConnectedUtc);
            Assert.AreEqual(newDeviceTz, actual.DeviceTz);
        }

        #endregion

        #region ClearDevices

        [TestMethod]
        public async Task ClearDevices_PatientHasNotActivatedDevice_ResultContainsDeletedDevices()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                CustomerId = 3000,
                Status = Status.NotActivated,
                DeviceType = DeviceType.Android,
                Settings = new DeviceSettings { iHealthAccount = "testiHealthAccount" }
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var result = await this.sut.ClearDevices(existingDevice.CustomerId, existingDevice.PatientId);
            var actual = result.FirstOrDefault();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(existingDevice, actual);
        }

        [TestMethod]
        public async Task ClearDevices_PatientHasActivatedDevice_DecommisionRequested()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                CustomerId = 3000,
                Status = Status.Activated,
                DeviceType = DeviceType.Android,
                Settings = new DeviceSettings {iHealthAccount = "testiHealthAccount"}
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            var result = await this.sut.ClearDevices(existingDevice.CustomerId, existingDevice.PatientId);
            var actual = result.FirstOrDefault();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Status.DecommissionRequested, actual.Status);
        }

        [TestMethod]
        public async Task ClearDevices_PatientHasNotActivatedDevice_DeviceWasDeleted()
        {
            // Arrange
            var existingDevice = new Device
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                CustomerId = 3000,
                Status = Status.NotActivated,
                DeviceType = DeviceType.Android,
                Settings = new DeviceSettings { iHealthAccount = "testiHealthAccount" }
            };

            this.devicesRepository.Refresh(new List<Device> { existingDevice });

            // Act
            await this.sut.ClearDevices(existingDevice.CustomerId, existingDevice.PatientId);
            var actual = await this.sut.GetDevice(existingDevice.CustomerId, existingDevice.Id);

            // Assert
            Assert.IsNull(actual);
        }

        #endregion
    }
}