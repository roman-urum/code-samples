using System;
using System.Threading.Tasks;
using DeviceService.Common;
using DeviceService.Domain.Dtos;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Entities.Enums;
using DeviceService.DomainLogic.Services.Interfaces;
using DeviceService.Web.Api.Helpers.Implementations;
using DeviceService.Web.Api.Helpers.Interfaces;
using DeviceService.Web.Api.Models.Dtos.Entities;
using DeviceService.Web.Api.Models.Dtos.RequestsResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DeviceService.Web.Api.Tests.Helpers.Implementations
{
    [TestClass]
    public class DevicesControllerHelperTests
    {
        const string testMessagingHubUrl = "http://localhost/";

        private Mock<IAppSettings> appSettingsMock;
        private Mock<IDevicesService> devicesServiceMock;

        private IDevicesControllerHelper sut;

        [TestInitialize]
        public void Initalize()
        {
            // Setup
            devicesServiceMock = new Mock<IDevicesService>();
            appSettingsMock = new Mock<IAppSettings>();
            
            this.appSettingsMock.SetupGet(s => s.MessagingHubUrl).Returns(testMessagingHubUrl);

            sut = new DevicesControllerHelper(devicesServiceMock.Object);

            AutomapperConfig.RegisterRules(appSettingsMock.Object);
        }

        [TestMethod]
        public async Task CreateDevice_DeviceWasCreated_ReturnsCorrectResult()
        {
            // Arrange
            const int testCustomerId = 3000;

            var deviceDto = new CreateDeviceRequestDto
            {
                BirthDate = "1990-01-01",
                PatientId = Guid.NewGuid(),
                DeviceModel = "TestModel",
                DeviceType = DeviceType.IVR,
                Settings = new DeviceSettingsDto
                {
                    iHealthAccount = "TestiHealthAccount",
                    BloodGlucosePeripheral = BloodGlucosePeripheral.glooko_metersync,
                    PinCode = "1234",
                    IsWeightAutomated = true,
                    IsPinCodeRequired = true,
                    IsBloodGlucoseAutomated = true,
                    IsBloodGlucoseManual = true,
                    IsBloodPressureAutomated = true,
                    IsBloodPressureManual = true,
                    IsPeakFlowAutomated = true,
                    IsPeakFlowManual = true,
                    IsPedometerAutomated = true,
                    IsPedometerManual = true,
                    IsPulseOxAutomated = true,
                    IsPulseOxManual = true,
                    IsTemperatureAutomated = true,
                    IsTemperatureManual = true,
                    IsWeightManual = true
                }
            };
            Device createdDevice = null;

            this.devicesServiceMock
                .Setup(s => s.CreateDevice(It.IsAny<Device>()))
                .Callback<Device>((deviceToSave) =>
                {
                    createdDevice = deviceToSave;
                    createdDevice.Id = Guid.NewGuid();
                    createdDevice.ActivationCode = "actCode";
                })
                .Returns(() => Task.FromResult(
                    new OperationResultDto<Device, CreateDeviceStatus>(
                        CreateDeviceStatus.Success,
                        createdDevice)));

            // Act
            var result = await this.sut.CreateDevice(testCustomerId, deviceDto);
            var actual = result.Content;

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(createdDevice.Id, actual.Id);
            Assert.AreEqual(createdDevice.ActivationCode, actual.ActivationCode);
            Assert.AreEqual(testCustomerId, actual.CustomerId);
            Assert.AreEqual(deviceDto.DeviceModel, actual.DeviceModel);
            Assert.AreEqual(deviceDto.DeviceType.ToString(), actual.DeviceType);
            Assert.AreEqual(deviceDto.BirthDate, actual.BirthDate);
            Assert.AreEqual(deviceDto.Settings.iHealthAccount, actual.Settings.iHealthAccount);
            Assert.AreEqual(deviceDto.Settings.BloodGlucosePeripheral, actual.Settings.BloodGlucosePeripheral);
            Assert.AreEqual(deviceDto.Settings.PinCode, actual.Settings.PinCode);
            Assert.AreEqual(deviceDto.Settings.IsWeightAutomated, actual.Settings.IsWeightAutomated);
            Assert.AreEqual(deviceDto.Settings.IsPinCodeRequired, actual.Settings.IsPinCodeRequired);
            Assert.AreEqual(deviceDto.Settings.IsBloodGlucoseAutomated, actual.Settings.IsBloodGlucoseAutomated);
            Assert.AreEqual(deviceDto.Settings.IsBloodGlucoseManual, actual.Settings.IsBloodGlucoseManual);
            Assert.AreEqual(deviceDto.Settings.IsBloodPressureAutomated, actual.Settings.IsBloodPressureAutomated);
            Assert.AreEqual(deviceDto.Settings.IsBloodPressureManual, actual.Settings.IsBloodPressureManual);
            Assert.AreEqual(deviceDto.Settings.IsPeakFlowAutomated, actual.Settings.IsPeakFlowAutomated);
            Assert.AreEqual(deviceDto.Settings.IsPeakFlowManual, actual.Settings.IsPeakFlowManual);
            Assert.AreEqual(deviceDto.Settings.IsPedometerAutomated, actual.Settings.IsPedometerAutomated);
            Assert.AreEqual(deviceDto.Settings.IsPedometerManual, actual.Settings.IsPedometerManual);
            Assert.AreEqual(deviceDto.Settings.IsPulseOxAutomated, actual.Settings.IsPulseOxAutomated);
            Assert.AreEqual(deviceDto.Settings.IsPulseOxManual, actual.Settings.IsPulseOxManual);
            Assert.AreEqual(deviceDto.Settings.IsTemperatureAutomated, actual.Settings.IsTemperatureAutomated);
            Assert.AreEqual(deviceDto.Settings.IsTemperatureManual, actual.Settings.IsTemperatureManual);
            Assert.AreEqual(deviceDto.Settings.IsWeightManual, actual.Settings.IsWeightManual);
            Assert.AreEqual(testMessagingHubUrl, actual.Settings.MessagingHubUrl);
        }
    }
}
