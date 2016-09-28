using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VitalsService.DataAccess.EF;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Implementations;
using VitalsService.DomainLogic.Services.Interfaces;

namespace VitalsService.DomainLogic.Tests.Services.Implementations
{
    [TestClass]
    public class DefaultThresholdsServiceTests
    {
        private IDefaultThresholdsService sut;

        private TestRepository<AlertSeverity> alertSeverityRepository;
        private TestRepository<DefaultThreshold> defaultThresholdRepository;

        private Mock<IUnitOfWork> unitOfWorkMock;
        private TestRepository<VitalAlert> vitalAlertRepository;

        private AlertSeverity testAlertSeverity1;
        private AlertSeverity testAlertSeverity2;
        private DefaultThreshold testDefaultThreshold1;
        private DefaultThreshold testDefaultThreshold2;

        private const int TestCustomerId = 3001;

        [TestInitialize]
        public void Initialize()
        {
            DomainLogicAutomapperConfig.RegisterRules();

            // Setup
            unitOfWorkMock = new Mock<IUnitOfWork>();

            alertSeverityRepository = new TestRepository<AlertSeverity>();

            defaultThresholdRepository = new TestRepository<DefaultThreshold>();
            vitalAlertRepository = new TestRepository<VitalAlert>();

            unitOfWorkMock.Setup(s => s.CreateRepository<DefaultThreshold>())
                .Returns(() => defaultThresholdRepository);
            unitOfWorkMock.Setup(s => s.CreateRepository<AlertSeverity>())
                .Returns(() => alertSeverityRepository);
            unitOfWorkMock.Setup(s => s.CreateRepository<VitalAlert>())
                .Returns(() => vitalAlertRepository);

            sut = new DefaultThresholdsService(unitOfWorkMock.Object);

            testAlertSeverity1 = new AlertSeverity()
            {
                Id = Guid.NewGuid(),
                CustomerId = TestCustomerId,
                ColorCode = "#ff0000",
                Name = "Red alert severity for customer 3001",
                Severity = 2
            };

            testAlertSeverity2 = new AlertSeverity()
            {
                Id = Guid.NewGuid(),
                CustomerId = TestCustomerId,
                ColorCode = "#00ff00",
                Name = "Green alert severity for customer 3001",
                Severity = 1
            };

            testDefaultThreshold1 = new DefaultThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = TestCustomerId,
                Name = VitalType.Weight.ToString(),
                Unit = UnitType.kg.ToString(),
                DefaultType = ThresholdDefaultType.Customer.ToString(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = testAlertSeverity1,
                AlertSeverityId = testAlertSeverity1.Id
            };

            testDefaultThreshold2 = new DefaultThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = TestCustomerId,
                Name = VitalType.HeartRate.ToString(),
                Unit = UnitType.BpM.ToString(),
                DefaultType = ThresholdDefaultType.Customer.ToString(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = testAlertSeverity2,
                AlertSeverityId = testAlertSeverity2.Id
            };

            this.alertSeverityRepository.Refresh(new List<AlertSeverity> { testAlertSeverity1, testAlertSeverity2 });
            this.defaultThresholdRepository.Refresh(new List<DefaultThreshold> { testDefaultThreshold1, testDefaultThreshold2 });
        }

        #region CreateDefaultThreshold

        [TestMethod]
        public async Task CreateDefaultThreshold_ReturnsError_IfProvidedAlertSeverityDoesNotExist()
        {
            // Arrange
            var invalidAlertSeverityId = Guid.NewGuid();

            var defaultThreshold = new DefaultThreshold()
            {
                AlertSeverityId = invalidAlertSeverityId
            };

            // Act
            var actual = await sut.CreateDefaultThreshold(defaultThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.AlertSeverityDoesNotExist, actual.Status);
        }

        [TestMethod]
        public async Task CreateDefaultThreshold_ReturnsError_IfExistingAlertSeverityIsNotProvided()
        {
            // Arrange
            var defaultThreshold = new DefaultThreshold()
            {
                CustomerId = TestCustomerId,
                AlertSeverity = null,
                AlertSeverityId = null
            };

            // Act
            var actual = await sut.CreateDefaultThreshold(defaultThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.ExistingAlertSeverityShouldBeUsed, actual.Status);
        }

        [TestMethod]
        public async Task CreateDefaultThreshold_ReturnsError_IfThresholdAlreadyExists()
        {
            // Act
            var actual = await sut.CreateDefaultThreshold(testDefaultThreshold1);

            // Asssert
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.VitalDefaultThresholdAlreadyExists, actual.Status);
        }

        [TestMethod]
        public async Task CreateDefaultThreshold_ReturnsSuccessStatusWithContent_IfCorrectDataProvided()
        {
            // Arrange
            var newDefaultThreshold = new DefaultThreshold()
            {
                CustomerId = TestCustomerId,
                Name = VitalType.HeartRate.ToString(),
                Unit = UnitType.BpM.ToString(),
                DefaultType = ThresholdDefaultType.Customer.ToString(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = testAlertSeverity1,
                AlertSeverityId = testAlertSeverity1.Id
            };

            // Act
            var actual = await sut.CreateDefaultThreshold(newDefaultThreshold);

            var savedResult = await sut.GetDefaultThreshold(newDefaultThreshold.CustomerId, actual.Content);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.Success, actual.Status);
            Assert.AreNotEqual(actual.Content, Guid.Empty);
            Assert.AreEqual(savedResult.Id, newDefaultThreshold.Id);
            Assert.AreEqual(savedResult.CustomerId, newDefaultThreshold.CustomerId);
            Assert.AreEqual(savedResult.DefaultType, newDefaultThreshold.DefaultType);
            Assert.AreEqual(savedResult.Name, newDefaultThreshold.Name);
            Assert.AreEqual(savedResult.Type, newDefaultThreshold.Type);
            Assert.AreEqual(savedResult.Unit, newDefaultThreshold.Unit);
            Assert.AreEqual(savedResult.MinValue, newDefaultThreshold.MinValue);
            Assert.AreEqual(savedResult.MaxValue, newDefaultThreshold.MaxValue);
            Assert.AreEqual(savedResult.AlertSeverity, newDefaultThreshold.AlertSeverity);
            Assert.AreEqual(savedResult.AlertSeverityId, newDefaultThreshold.AlertSeverityId);
        }

        #endregion

        #region UpdateDefaultThreshold

        [TestMethod]
        public async Task UpdateDefaultThreshold_ReturnsError_IfProvidedDefaultThresholdDoesNotExist()
        {
            // Arrange
            var defaultThreshold = new DefaultThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = TestCustomerId
            };

            // Act
            var actual = await sut.UpdateDefaultThreshold(defaultThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.VitalDefaultThresholdDoesNotExist, actual);
        }

        [TestMethod]
        public async Task UpdateDefaultThreshold_ReturnsError_IfProvidedAlertSeverityDoesNotExist()
        {
            // Arrange
            var invalidAlertSeverityId = Guid.NewGuid();

            var defaultThreshold = new DefaultThreshold()
            {
                Id = testDefaultThreshold1.Id,
                CustomerId = TestCustomerId,
                AlertSeverityId = invalidAlertSeverityId
            };

            // Act
            var actual = await sut.UpdateDefaultThreshold(defaultThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.AlertSeverityDoesNotExist, actual);
        }

        [TestMethod]
        public async Task UpdateDefaultThreshold_ReturnsError_IfExistingAlertSeverityIsNotProvided()
        {
            // Arrange
            var defaultThreshold = new DefaultThreshold()
            {
                Id = testDefaultThreshold1.Id,
                CustomerId = TestCustomerId,
                AlertSeverity = null,
                AlertSeverityId = null
            };

            // Act
            var actual = await sut.UpdateDefaultThreshold(defaultThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.ExistingAlertSeverityShouldBeUsed, actual);
        }

        [TestMethod]
        public async Task UpdateDefaultThreshold_ReturnsError_IfDefaultThresholdAlreadyExists()
        {
            // Arrange
            var updatedDefaultThreshold = new DefaultThreshold()
            {
                Id = testDefaultThreshold1.Id,
                CustomerId = testDefaultThreshold1.CustomerId,
                Name = testDefaultThreshold2.Name,
                AlertSeverity = testDefaultThreshold2.AlertSeverity,
                AlertSeverityId = testDefaultThreshold2.AlertSeverityId
            };

            // Act
            var actual = await sut.UpdateDefaultThreshold(updatedDefaultThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.VitalDefaultThresholdAlreadyExists, actual);
        }

        [TestMethod]
        public async Task UpdateDefaultThreshold_ReturnsSuccessStatusWithContent_IfCorrectDataProvided()
        {
            // Arrange
            var updatedDefaultThreshold = new DefaultThreshold()
            {
                Id = testDefaultThreshold1.Id,
                CustomerId = testDefaultThreshold1.CustomerId,
                Name = testDefaultThreshold2.Name,
                Type = testDefaultThreshold2.Type,
                Unit = testDefaultThreshold2.Unit,
                MinValue = -100500,
                MaxValue = 100500,
                AlertSeverity = testDefaultThreshold1.AlertSeverity,
                AlertSeverityId = testDefaultThreshold1.AlertSeverityId
            };

            // Act
            var actual = await sut.UpdateDefaultThreshold(updatedDefaultThreshold);

            var savedResult = await sut.GetDefaultThreshold(updatedDefaultThreshold.CustomerId, updatedDefaultThreshold.Id);

            // Asssert
            Assert.AreEqual(CreateUpdateDefaultThresholdStatus.Success, actual);
            Assert.AreEqual(savedResult.Id, updatedDefaultThreshold.Id);
            Assert.AreEqual(savedResult.CustomerId, updatedDefaultThreshold.CustomerId);
            Assert.AreEqual(savedResult.DefaultType, updatedDefaultThreshold.DefaultType);
            Assert.AreEqual(savedResult.Name, updatedDefaultThreshold.Name);
            Assert.AreEqual(savedResult.Type, updatedDefaultThreshold.Type);
            Assert.AreEqual(savedResult.Unit, updatedDefaultThreshold.Unit);
            Assert.AreEqual(savedResult.MinValue, updatedDefaultThreshold.MinValue);
            Assert.AreEqual(savedResult.MaxValue, updatedDefaultThreshold.MaxValue);
            Assert.AreEqual(savedResult.AlertSeverity, updatedDefaultThreshold.AlertSeverity);
            Assert.AreEqual(savedResult.AlertSeverityId, updatedDefaultThreshold.AlertSeverityId);
        }

        #endregion

        #region DeleteDefaultThreshold

        [TestMethod]
        public async Task DeleteDefaultThreshold_ReturnsError_IfDefaultThresholdDoesNotExist()
        {
            // Arrange
            var notExistingDefaultThresholdId = Guid.NewGuid();

            // Act
            var actual = await sut.DeleteDefaultThreshold(TestCustomerId, notExistingDefaultThresholdId);

            // Assert
            Assert.AreEqual(GetDeleteDefaultThresholdStatus.NotFound, actual);
        }

        [TestMethod]
        public async Task DeleteDefaultThreshold_ReturnsSuccessfulResult_IfCorrectDataProvided()
        {
            // Arrange
            var testVitalAlert1 = new VitalAlert()
            {
                Id = Guid.NewGuid(),
                ThresholdId = testDefaultThreshold1.Id,
                Threshold = testDefaultThreshold1
            };

            var testVitalAlert2 = new VitalAlert()
            {
                Id = Guid.NewGuid(),
                ThresholdId = testDefaultThreshold2.Id,
                Threshold = testDefaultThreshold2
            };

            var testVitalAlerts = new List<VitalAlert> { testVitalAlert1, testVitalAlert2 };

            vitalAlertRepository.Refresh(testVitalAlerts);

            testDefaultThreshold1.VitalAlerts = testVitalAlerts;

            // Act
            var actual = await sut.DeleteDefaultThreshold(testDefaultThreshold1.CustomerId, testDefaultThreshold1.Id);
            var deletedThreshold = await sut.GetDefaultThreshold(testDefaultThreshold1.CustomerId, testDefaultThreshold1.Id);
            var deletedVitalAlert1 = await vitalAlertRepository.FirstOrDefaultAsync(a => a.Id == testVitalAlert1.Id);
            var deletedVitalAlert2 = await vitalAlertRepository.FirstOrDefaultAsync(a => a.Id == testVitalAlert2.Id);

            // Assert
            Assert.AreEqual(GetDeleteDefaultThresholdStatus.Success, actual);
            Assert.IsNull(deletedThreshold);
            Assert.IsNull(deletedVitalAlert1);
            Assert.IsNull(deletedVitalAlert2);
        }

        #endregion
    }
}