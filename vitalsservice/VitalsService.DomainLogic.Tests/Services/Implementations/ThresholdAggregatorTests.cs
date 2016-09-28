using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Implementations;
using VitalsService.DomainLogic.Services.Interfaces;

namespace VitalsService.DomainLogic.Tests.Services.Implementations
{
    [TestClass]
    public class ThresholdAggregatorTests
    {
        private IThresholdAggregator sut;

        private AlertSeverity customer3001AlertSeverity1;
        private AlertSeverity customer3001AlertSeverity2;
        private AlertSeverity customer3002AlertSeverity1;
        private DefaultThreshold customer3001DefaultThreshold1;
        private DefaultThreshold customer3001DefaultThreshold2;
        private DefaultThreshold customer3002DefaultThreshold1;
        private PatientThreshold customer3001PatientThreshold1;
        private PatientThreshold customer3001PatientThreshold2;
        private PatientThreshold customer3002PatientThreshold1;

        [TestInitialize]
        public void Initialize()
        {
            sut = new ThresholdAggregator();

            customer3001AlertSeverity1 = new AlertSeverity()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001,
                ColorCode = "#ff0000",
                Name = "Red alert severity for customer 3001",
                Severity = 2
            };

            customer3001AlertSeverity2 = new AlertSeverity()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001,
                ColorCode = "#00ff00",
                Name = "Green alert severity for customer 3002",
                Severity = 1
            };

            customer3002AlertSeverity1 = new AlertSeverity()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3002,
                ColorCode = "#ff0000",
                Name = "Red alert severity for customer 3002",
                Severity = 1
            };

            customer3001DefaultThreshold1 = new DefaultThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001,
                Name = VitalType.Weight.ToString(),
                Unit = UnitType.kg.ToString(),
                DefaultType = ThresholdDefaultType.Customer.ToString(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = customer3001AlertSeverity1,
                AlertSeverityId = customer3001AlertSeverity1.Id
            };

            customer3001DefaultThreshold2 = new DefaultThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001,
                Name = VitalType.Weight.ToString(),
                Unit = UnitType.kg.ToString(),
                DefaultType = ThresholdDefaultType.Customer.ToString(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = customer3001AlertSeverity2,
                AlertSeverityId = customer3001AlertSeverity2.Id
            };

            customer3002DefaultThreshold1 = new DefaultThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3002,
                Name = VitalType.Weight.ToString(),
                Unit = UnitType.kg.ToString(),
                DefaultType = ThresholdDefaultType.Customer.ToString(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = customer3002AlertSeverity1,
                AlertSeverityId = customer3002AlertSeverity1.Id
            };

            customer3001PatientThreshold1 = new PatientThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001,
                Name = VitalType.Weight.ToString(),
                Unit = UnitType.kg.ToString(),
                PatientId = Guid.NewGuid(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = customer3001AlertSeverity1,
                AlertSeverityId = customer3001AlertSeverity1.Id
            };

            customer3001PatientThreshold2 = new PatientThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001,
                Name = VitalType.Weight.ToString(),
                Unit = UnitType.kg.ToString(),
                PatientId = Guid.NewGuid(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = customer3001AlertSeverity2,
                AlertSeverityId = customer3001AlertSeverity2.Id
            };

            customer3002PatientThreshold1 = new PatientThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3002,
                Name = VitalType.Weight.ToString(),
                Unit = UnitType.kg.ToString(),
                PatientId = Guid.NewGuid(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = customer3002AlertSeverity1,
                AlertSeverityId = customer3002AlertSeverity1.Id
            };
        }

        [TestMethod]
        public void AggregateThresholds_ResultContainsAllThresholds_IfProvidedPatientAndDefaultThresholdsAreNotIntersectByNameAlertSeverityAndCustomerId()
        {
            // Arrange
            var defaultThresholds = new List<DefaultThreshold>()
            {
                customer3001DefaultThreshold1,
                customer3002DefaultThreshold1
            };

            var patientThresholds = new List<PatientThreshold>()
            {
                customer3001PatientThreshold2
            };

            // Act
            var actual = sut.AggregateThresholds(defaultThresholds, patientThresholds).ToList();
            var expected = defaultThresholds.Cast<Threshold>().Concat(patientThresholds).ToList();

            // Asssert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AggregateThresholds_PatientThresholdsOverrideDefaultThresholds_IfProvidedPatientAndDefaultThresholdsIntersectByNameAlertSeverityAndCustomerId()
        {
            // Arrange
            var defaultThresholds = new List<DefaultThreshold>()
            {
                customer3001DefaultThreshold1,
                customer3001DefaultThreshold2,
                customer3002DefaultThreshold1
            };

            var patientThresholds = new List<PatientThreshold>()
            {
                customer3001PatientThreshold1,
                customer3001PatientThreshold2,
                customer3002PatientThreshold1
            };

            // Act
            var actual = sut.AggregateThresholds(defaultThresholds, patientThresholds).ToList();
            var expected = patientThresholds;

            // Asssert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AggregateThresholds_ResultDoesNotContainThresholdsWithNullAlertSeverity_IfProvidedPatientAndDefaultThresholdsHaveAtLeastOneElementWithAlertSeverity()
        {
            // Arrange
            var defaultThresholdWithNullAlertSeverity = new DefaultThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001,
                Name = VitalType.HeartRate.ToString(),
                Unit = UnitType.BpM.ToString(),
                DefaultType = ThresholdDefaultType.Customer.ToString(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = null,
                AlertSeverityId = null
            };

            var defaultThresholds = new List<DefaultThreshold>()
            {
                customer3001DefaultThreshold1,
                customer3001DefaultThreshold2,
                customer3002DefaultThreshold1,
                defaultThresholdWithNullAlertSeverity
            };

            var patientThresholdWithNullAlertSeverity = new PatientThreshold()
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001,
                Name = VitalType.HeartRate.ToString(),
                Unit = UnitType.BpM.ToString(),
                PatientId = Guid.NewGuid(),
                Type = ThresholdType.Basic.ToString(),
                MinValue = 1,
                MaxValue = 100,
                AlertSeverity = null,
                AlertSeverityId = null
            };

            var patientThresholds = new List<PatientThreshold>()
            {
                customer3001PatientThreshold1,
                customer3001PatientThreshold2,
                customer3002PatientThreshold1,
                patientThresholdWithNullAlertSeverity
            };

            // Act
            var actual = sut.AggregateThresholds(defaultThresholds, patientThresholds).ToList();
            var expected = patientThresholds.Where(t => t.AlertSeverityId.HasValue).ToList();

            // Asssert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}