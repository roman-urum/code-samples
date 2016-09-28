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
    public class ThresholdsServiceTests
    {
        private IThresholdsService sut;

        private TestRepository<AlertSeverity> alertSeverityRepository;
        private TestRepository<PatientThreshold> patientThresholdRepository;

        private Mock<IUnitOfWork> unitOfWorkMock;
        private TestRepository<VitalAlert> vitalAlertRepository;

        private AlertSeverity testAlertSeverity1;
        private AlertSeverity testAlertSeverity2;
        private PatientThreshold testPatientThreshold1;
        private PatientThreshold testPatientThreshold2;

        private const int TestCustomerId = 3001;
        private Guid testPatientId;

        [TestInitialize]
        public void Initialize()
        {
            DomainLogicAutomapperConfig.RegisterRules();

            // Setup
            unitOfWorkMock = new Mock<IUnitOfWork>();

            alertSeverityRepository = new TestRepository<AlertSeverity>();

            patientThresholdRepository = new TestRepository<PatientThreshold>();
            vitalAlertRepository = new TestRepository<VitalAlert>();

            unitOfWorkMock.Setup(s => s.CreateRepository<PatientThreshold>())
                .Returns(() => patientThresholdRepository);
            unitOfWorkMock.Setup(s => s.CreateRepository<AlertSeverity>())
                .Returns(() => alertSeverityRepository);
            unitOfWorkMock.Setup(s => s.CreateRepository<VitalAlert>())
                .Returns(() => vitalAlertRepository);

            sut = new ThresholdsService(unitOfWorkMock.Object);

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

            testPatientId = Guid.NewGuid();

            testPatientThreshold1 = new PatientThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = TestCustomerId,
                Name = VitalType.Weight.ToString(),
                Unit = UnitType.kg.ToString(),
                PatientId = testPatientId,
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = testAlertSeverity1,
                AlertSeverityId = testAlertSeverity1.Id
            };

            testPatientThreshold2 = new PatientThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = TestCustomerId,
                Name = VitalType.HeartRate.ToString(),
                Unit = UnitType.BpM.ToString(),
                PatientId = testPatientId,
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = testAlertSeverity2,
                AlertSeverityId = testAlertSeverity2.Id
            };

            this.alertSeverityRepository.Refresh(new List<AlertSeverity> { testAlertSeverity1, testAlertSeverity2 });
            this.patientThresholdRepository.Refresh(new List<PatientThreshold> { testPatientThreshold1, testPatientThreshold2 });
        }

        #region CreateThreshold

        [TestMethod]
        public async Task CreateThreshold_ReturnsError_IfProvidedAlertSeverityDoesNotExist()
        {
            // Arrange
            var invalidAlertSeverityId = Guid.NewGuid();

            var patientThreshold = new PatientThreshold()
            {
                AlertSeverityId = invalidAlertSeverityId
            };

            // Act
            var actual = await sut.CreateThreshold(patientThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateThresholdStatus.AlertSeverityDoesNotExist, actual.Status);
        }

        [TestMethod]
        public async Task CreateThreshold_ReturnsError_IfExistingAlertSeverityIsNotProvided()
        {
            // Arrange
            var patientThreshold = new PatientThreshold()
            {
                CustomerId = TestCustomerId,
                AlertSeverity = null,
                AlertSeverityId = null
            };

            // Act
            var actual = await sut.CreateThreshold(patientThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateThresholdStatus.ExistingAlertSeverityShouldBeUsed, actual.Status);
        }

        [TestMethod]
        public async Task CreateThreshold_ReturnsError_IfThresholdAlreadyExists()
        {
            // Act
            var actual = await sut.CreateThreshold(testPatientThreshold1);

            // Asssert
            Assert.AreEqual(CreateUpdateThresholdStatus.VitalThresholdAlreadyExists, actual.Status);
        }

        [TestMethod]
        public async Task CreateThreshold_ReturnsSuccessStatusWithContent_IfCorrectDataProvided()
        {
            // Arrange
            var newPatientThreshold = new PatientThreshold()
            {
                CustomerId = TestCustomerId,
                Name = VitalType.HeartRate.ToString(),
                Unit = UnitType.BpM.ToString(),
                PatientId = Guid.NewGuid(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = testAlertSeverity1,
                AlertSeverityId = testAlertSeverity1.Id
            };

            // Act
            var actual = await sut.CreateThreshold(newPatientThreshold);

            var savedResult = await sut.GetThreshold(newPatientThreshold.CustomerId, newPatientThreshold.PatientId, actual.Content);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(CreateUpdateThresholdStatus.Success, actual.Status);
            Assert.AreNotEqual(actual.Content, Guid.Empty);
            Assert.AreEqual(savedResult.Id, newPatientThreshold.Id);
            Assert.AreEqual(savedResult.CustomerId, newPatientThreshold.CustomerId);
            Assert.AreEqual(savedResult.PatientId, newPatientThreshold.PatientId);
            Assert.AreEqual(savedResult.Name, newPatientThreshold.Name);
            Assert.AreEqual(savedResult.Type, newPatientThreshold.Type);
            Assert.AreEqual(savedResult.Unit, newPatientThreshold.Unit);
            Assert.AreEqual(savedResult.MinValue, newPatientThreshold.MinValue);
            Assert.AreEqual(savedResult.MaxValue, newPatientThreshold.MaxValue);
            Assert.AreEqual(savedResult.AlertSeverity, newPatientThreshold.AlertSeverity);
            Assert.AreEqual(savedResult.AlertSeverityId, newPatientThreshold.AlertSeverityId);
        }

        #endregion

        #region UpdateThreshold

        [TestMethod]
        public async Task UpdateThreshold_ReturnsError_IfProvidedPatientThresholdDoesNotExist()
        {
            // Arrange
            var patientThreshold = new PatientThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = TestCustomerId,
                PatientId = testPatientThreshold1.PatientId
            };

            // Act
            var actual = await sut.UpdateThreshold(patientThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateThresholdStatus.VitalThresholdDoesNotExist, actual);
        }

        [TestMethod]
        public async Task UpdateThreshold_ReturnsError_IfProvidedAlertSeverityDoesNotExist()
        {
            // Arrange
            var invalidAlertSeverityId = Guid.NewGuid();

            var patientThreshold = new PatientThreshold()
            {
                Id = testPatientThreshold1.Id,
                CustomerId = TestCustomerId,
                PatientId = testPatientThreshold1.PatientId,
                AlertSeverityId = invalidAlertSeverityId
            };

            // Act
            var actual = await sut.UpdateThreshold(patientThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateThresholdStatus.AlertSeverityDoesNotExist, actual);
        }

        [TestMethod]
        public async Task UpdateThreshold_ReturnsError_IfExistingAlertSeverityIsNotProvided()
        {
            // Arrange
            var patientThreshold = new PatientThreshold()
            {
                Id = testPatientThreshold1.Id,
                CustomerId = TestCustomerId,
                PatientId = testPatientThreshold1.PatientId,
                AlertSeverity = null,
                AlertSeverityId = null
            };

            // Act
            var actual = await sut.UpdateThreshold(patientThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateThresholdStatus.ExistingAlertSeverityShouldBeUsed, actual);
        }

        [TestMethod]
        public async Task UpdateThreshold_ReturnsError_IfThresholdAlreadyExists()
        {
            // Arrange
            var updatedPatientThreshold = new PatientThreshold()
            {
                Id = testPatientThreshold1.Id,
                CustomerId = testPatientThreshold1.CustomerId,
                PatientId = testPatientThreshold1.PatientId,
                Name = testPatientThreshold2.Name,
                AlertSeverity = testPatientThreshold2.AlertSeverity,
                AlertSeverityId = testPatientThreshold2.AlertSeverityId
            };

            // Act
            var actual = await sut.UpdateThreshold(updatedPatientThreshold);

            // Asssert
            Assert.AreEqual(CreateUpdateThresholdStatus.VitalThresholdAlreadyExists, actual);
        }

        [TestMethod]
        public async Task UpdateThreshold_ReturnsSuccessStatusWithContent_IfCorrectDataProvided()
        {
            // Arrange
            var updatedPatientThreshold = new PatientThreshold()
            {
                Id = testPatientThreshold1.Id,
                CustomerId = testPatientThreshold1.CustomerId,
                PatientId = testPatientThreshold1.PatientId,
                Name = testPatientThreshold2.Name,
                Type = testPatientThreshold2.Type,
                Unit = testPatientThreshold2.Unit,
                MinValue = -100500,
                MaxValue = 100500,
                AlertSeverity = testPatientThreshold1.AlertSeverity,
                AlertSeverityId = testPatientThreshold1.AlertSeverityId
            };

            // Act
            var actual = await sut.UpdateThreshold(updatedPatientThreshold);

            var savedResult = await sut.GetThreshold(updatedPatientThreshold.CustomerId, updatedPatientThreshold.PatientId, updatedPatientThreshold.Id);

            // Asssert
            Assert.AreEqual(CreateUpdateThresholdStatus.Success, actual);
            Assert.AreEqual(savedResult.Id, updatedPatientThreshold.Id);
            Assert.AreEqual(savedResult.CustomerId, updatedPatientThreshold.CustomerId);
            Assert.AreEqual(savedResult.PatientId, updatedPatientThreshold.PatientId);
            Assert.AreEqual(savedResult.Name, updatedPatientThreshold.Name);
            Assert.AreEqual(savedResult.Type, updatedPatientThreshold.Type);
            Assert.AreEqual(savedResult.Unit, updatedPatientThreshold.Unit);
            Assert.AreEqual(savedResult.MinValue, updatedPatientThreshold.MinValue);
            Assert.AreEqual(savedResult.MaxValue, updatedPatientThreshold.MaxValue);
            Assert.AreEqual(savedResult.AlertSeverity, updatedPatientThreshold.AlertSeverity);
            Assert.AreEqual(savedResult.AlertSeverityId, updatedPatientThreshold.AlertSeverityId);
        }

        #endregion

        #region DeleteThreshold

        [TestMethod]
        public async Task DeleteThreshold_ReturnsError_IfThresholdDoesNotExist()
        {
            // Arrange
            var notExistingPatientThresholdId = Guid.NewGuid();

            // Act
            var actual = await sut.DeleteThreshold(TestCustomerId, testPatientId, notExistingPatientThresholdId);

            // Assert
            Assert.AreEqual(GetDeleteThresholdStatus.NotFound, actual);
        }

        [TestMethod]
        public async Task DeleteThreshold_ReturnsSuccessfulResult_IfCorrectDataProvided()
        {
            // Arrange
            var testVitalAlert1 = new VitalAlert()
            {
                Id = Guid.NewGuid(),
                ThresholdId = testPatientThreshold1.Id,
                Threshold = testPatientThreshold1
            };

            var testVitalAlert2 = new VitalAlert()
            {
                Id = Guid.NewGuid(),
                ThresholdId = testPatientThreshold2.Id,
                Threshold = testPatientThreshold2
            };

            var testVitalAlerts = new List<VitalAlert> { testVitalAlert1, testVitalAlert2 };

            vitalAlertRepository.Refresh(testVitalAlerts);

            testPatientThreshold1.VitalAlerts = testVitalAlerts;

            // Act
            var actual = await sut.DeleteThreshold(testPatientThreshold1.CustomerId, testPatientThreshold1.PatientId, testPatientThreshold1.Id);
            var deletedThreshold = await sut.GetThreshold(testPatientThreshold1.CustomerId, testPatientThreshold1.PatientId, testPatientThreshold1.Id);
            var deletedVitalAlert1 = await vitalAlertRepository.FirstOrDefaultAsync(a => a.Id == testVitalAlert1.Id);
            var deletedVitalAlert2 = await vitalAlertRepository.FirstOrDefaultAsync(a => a.Id == testVitalAlert2.Id);

            // Assert
            Assert.AreEqual(GetDeleteThresholdStatus.Success, actual);
            Assert.IsNull(deletedThreshold);
            Assert.IsNull(deletedVitalAlert1);
            Assert.IsNull(deletedVitalAlert2);
        }

        #endregion
    }
}