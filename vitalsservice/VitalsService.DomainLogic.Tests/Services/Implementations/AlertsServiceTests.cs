using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using EntityFramework.Future;

using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VitalsService.DataAccess.EF;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Implementations;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.DomainLogic.Tests.Helpers;

namespace VitalsService.DomainLogic.Tests.Services.Implementations
{
    [TestClass]
    public class AlertsServiceTests
    {
        private IAlertsService sut;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private TestRepository<AlertSeverity> alertSeveritiesRepository;
        private TestRepository<Alert> alertRepository;

        [TestInitialize]
        public void Initialize()
        {            
            alertSeveritiesRepository = new TestRepository<AlertSeverity>();
            alertRepository = new TestRepository<Alert>();

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(s => s.CreateRepository<AlertSeverity>()).Returns(() => alertSeveritiesRepository);
            unitOfWorkMock.Setup(s => s.CreateRepository<Alert>()).Returns(() => alertRepository);

            sut = new AlertsService(unitOfWorkMock.Object);

        }  

        [TestMethod]
        public async Task CreateAlert_AlertSeverityWithSuchIdDoesNotExist_ReturnsError()
        {         
            Alert createdAlert = new Alert()
            {
                Id = Guid.NewGuid(),
                Acknowledged = false,
                AlertSeverityId = Guid.NewGuid(),
                Body = "test body",
                CustomerId = 1,
                OccurredUtc = DateTime.Now,
                PatientId = Guid.NewGuid(),
                Title = "test tiyle",
                Weight = 10,              
            };

            alertSeveritiesRepository.Refresh(new List<AlertSeverity>()
            {
                new AlertSeverity()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = 1,
                    ColorCode = "red",
                    Name = "test severity",
                    Severity = 10
                }                                          
            });

            var result = await sut.CreateAlert(createdAlert);

            Assert.AreEqual(result.Status, CreateUpdateAlertStatus.AlertSeverityWithSuchIdDoesNotExist);

        }

        [TestMethod]
        public async Task CreateAlert_IfNoSeveritySpecified_AssignHighestSeverity()
        {
            Alert createdAlert = new Alert()
            {
                Id = Guid.NewGuid(),
                Acknowledged = false,
                Body = "test body",
                CustomerId = 1,
                OccurredUtc = DateTime.Now,
                PatientId = Guid.NewGuid(),
                Title = "test tiyle",
                Weight = 10,
            };

            var existedSeverities = new List<AlertSeverity>()
            {
                new AlertSeverity()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = 1,
                    ColorCode = "red",
                    Name = "test severity 1",
                    Severity = 10
                },
                new AlertSeverity()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = 1,
                    ColorCode = "black",
                    Name = "test severity 2",
                    Severity = 11
                },
            };
            alertSeveritiesRepository.Refresh(existedSeverities);

            var result = await sut.CreateAlert(createdAlert);

            Assert.AreEqual(createdAlert.AlertSeverity.Id, existedSeverities.Last().Id);

        }

        [TestMethod]
        public async Task CreateAlert_SuccessfulCreation()
        {
            Alert createdAlert = new Alert()
            {
                Id = Guid.NewGuid(),
                Acknowledged = false,
                Body = "test body",
                CustomerId = 1,
                OccurredUtc = DateTime.Now,
                PatientId = Guid.NewGuid(),
                Title = "test title",
                Weight = 10,
            };

            var result = await sut.CreateAlert(createdAlert);     

            Assert.AreEqual(result.Status, CreateUpdateAlertStatus.Success);
            var alertFromRepo = await alertRepository.FirstOrDefaultAsync(a => a.Id == createdAlert.Id);
            Assert.AreSame(alertFromRepo, createdAlert);
        }

        [TestMethod]
        public async Task AcknowledgeAlerts_TryToAcknowledgeNonExistedAlert_ReturnsError()
        {
            var alertIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            alertRepository.Refresh(new List<Alert>()
            {
                new Alert()
                    {
                        CustomerId = 1,
                        Acknowledged = false,
                        Id = alertIds[0],
                        Body = "test body 1",
                        OccurredUtc = DateTime.Now,
                        PatientId = Guid.NewGuid()
                    },
                new Alert()
                    {
                        CustomerId = 1,
                        Acknowledged = false,
                        Id = alertIds[0],
                        Body = "test body 2",
                        OccurredUtc = DateTime.Now,
                        PatientId = Guid.NewGuid()
                    }                               
            });

            var result = await sut.AcknowledgeAlerts(1, Guid.NewGuid(), alertIds);

            Assert.AreEqual(result, CreateUpdateAlertStatus.OneOfProvidedAlertsDoesNotExistOrAlreadyAcknowledged);
        }

        [TestMethod]
        public async Task AcknowledgeAlerts_TryToAcknowledgeAcknowkedgetAlerts_ReturnsError()
        {
            var alertIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
            alertRepository.Refresh(new List<Alert>()
            {
                new Alert()
                    {
                        CustomerId = 1,
                        Acknowledged = true,
                        Id = alertIds[0],
                        Body = "test body 1",
                        OccurredUtc = DateTime.Now,
                        PatientId = Guid.NewGuid()
                    },
                new Alert()
                    {
                        CustomerId = 1,
                        Acknowledged = true,
                        Id = alertIds[0],
                        Body = "test body 2",
                        OccurredUtc = DateTime.Now,
                        PatientId = Guid.NewGuid()
                    }                               
            });

            var result = await sut.AcknowledgeAlerts(1, Guid.NewGuid(), alertIds);

            Assert.AreEqual(result, CreateUpdateAlertStatus.OneOfProvidedAlertsDoesNotExistOrAlreadyAcknowledged);
        }

        [TestMethod]
        public async Task AcknowledgeAlerts_SuccessfulAcknowledge()
        {
            var alertIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
            
            var existedAlerts = new List<Alert>()
            {
                new Alert()
                    {
                        CustomerId = 1,
                        Acknowledged = false,
                        Id = alertIds[0],
                        Body = "test body 1",
                        OccurredUtc = DateTime.Now,
                        PatientId = Guid.NewGuid()
                    },
                new Alert()
                    {
                        CustomerId = 1,
                        Acknowledged = false,
                        Id = alertIds[0],
                        Body = "test body 2",
                        OccurredUtc = DateTime.Now,
                        PatientId = Guid.NewGuid()
                    }                               
            };

            alertRepository.Refresh(existedAlerts);

            Guid acknowledgedBy = Guid.NewGuid();
            var result = await sut.AcknowledgeAlerts(1, acknowledgedBy, alertIds);

            Assert.AreEqual(result, CreateUpdateAlertStatus.Success);
            Assert.IsTrue(existedAlerts.All(a => a.Acknowledged && a.AcknowledgedBy == acknowledgedBy));
        }

        //[TestMethod]
        //public async Task GetAlerts_SearchWorksCorrectly()
        //{
        //    try
        //    {
        //        var patientsIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
        //        var severityIds = new List<Guid>() {Guid.NewGuid(), Guid.NewGuid()};

        //        var measurementsData = new List<Measurement>().AsQueryable();
        //        var measurementsMock = new Mock<DbSet<Measurement>>();
        //        measurementsMock.As<IQueryable<Measurement>>().Setup(m => m.Provider).Returns(measurementsData.Provider);

        //        var vitalsData = new List<Vital>().AsQueryable();
        //        var vitalsMock = new Mock<DbSet<Vital>>();
        //        vitalsMock.As<IQueryable<Vital>>().Setup(m => m.Provider).Returns(vitalsData.Provider);

        //        var alertsData = new List<Alert>()
        //        {
        //            new Alert()
        //            {
        //                Id = Guid.NewGuid(),
        //                CustomerId = 1,
        //                Title = "ttt",
        //                Acknowledged = false,
        //                OccurredUtc = DateTime.Now,
        //                PatientId = Guid.NewGuid(),
        //                Weight = 10
        //            }                         
        //        }.AsQueryable();

        //        var futuredAlertsMock = new Mock<FutureQuery<Alert>>();
        //        //futuredAlertsMock.Setup(m => m).Returns(alertsData.Future());

        //        var alertsMock = new Mock<DbSet<Alert>>();
        //        alertsMock.As<IQueryable<Alert>>().Setup(m => m.Provider).Returns(alertsData.Provider);
        //        alertsMock.As<IQueryable<Alert>>().Setup(m => m.Expression).Returns(alertsData.Expression);
        //        alertsMock.As<IQueryable<Alert>>().Setup(m => m.ElementType).Returns(alertsData.ElementType);
        //        alertsMock.As<IQueryable<Alert>>().Setup(m => m.GetEnumerator()).Returns(() => alertsData.GetEnumerator());
        //        alertsMock.As<IQueryable<Alert>>().Setup(m => m.Future()).Returns(() => futuredAlertsMock.Object);
        //        //alertsMock.Setup(m => m.Future()).Returns(() => null);

        //        var dbContextMock = new Mock<DbContext>();
        //        dbContextMock.Setup(m => m.Set<Measurement>()).Returns(measurementsMock.Object);
        //        dbContextMock.Setup(m => m.Set<Vital>()).Returns(vitalsMock.Object);
        //        dbContextMock.Setup(m => m.Set<Alert>()).Returns(alertsMock.Object);

        //        var mockServiceLocator = new Mock<IServiceLocator>();
        //        mockServiceLocator.Setup(s => s.GetInstance<DbContext>()).Returns(dbContextMock.Object);
        //        ServiceLocator.SetLocatorProvider(() => mockServiceLocator.Object);


        //        var existedAlerts = new List<Alert>()
        //        {
        //            new Alert()
        //            {
        //                CustomerId = 1,
        //                Acknowledged = false,
        //                Id = Guid.NewGuid(),
        //                Body = "test body 1",
        //                OccurredUtc = DateTime.Now,
        //                PatientId = patientsIds[0],
        //                Title = "alert1",
        //                Type = AlertType.VitalsViolation,
        //                AlertSeverityId = severityIds[0],
        //                AlertSeverity = new AlertSeverity()
        //                {
        //                    Id = Guid.NewGuid(),
        //                    CustomerId = 1,
        //                    ColorCode = "red",
        //                    Name = "red severity",
        //                    Severity = 10,

        //                }
        //            },
        //            new Alert()
        //            {
        //                CustomerId = 1,
        //                Acknowledged = true,
        //                AcknowledgedBy = Guid.NewGuid(),
        //                AcknowledgedUtc = DateTime.Now.AddDays(-2),
        //                Id = Guid.NewGuid(),
        //                Body = "test body 2",
        //                OccurredUtc = DateTime.Now,
        //                PatientId = patientsIds[1],
        //                Title = "alert2",
        //                Type = AlertType.ResponseViolation,
        //                AlertSeverity = new AlertSeverity()
        //                {
        //                    Id = Guid.NewGuid(),
        //                    CustomerId = 1,
        //                    ColorCode = "yellow",
        //                    Name = "yellow severity",
        //                    Severity = 8,

        //                }
        //            }
        //        };


        //        var getAlertsResult = await sut.GetAlerts(1, new AlertsSearchDto());
        //    }
        //    catch (Exception ex)
        //    {                
        //        throw;
        //    }


        //    Assert.IsTrue(true);
        //}

        [TestMethod]
        public async Task CreateViolationAlerts_DoesNotAffectVitalsWhichDontViolateThresholds()
        {
            #region testData

            Measurement testMeasurement = new Measurement()
            {
                Id = Guid.NewGuid(),
                CustomerId = 1,
                PatientId = Guid.NewGuid(),
                CreatedUtc = DateTime.Now,
                ObservedTz = "test timezone",
                ObservedUtc = DateTime.Now,
                Vitals = new List<Vital>()
                {
                    new Vital()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Weight",
                        Unit = "kg",
                        Value = 105
                    },
                    new Vital()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Temperature",
                        Unit = "C",
                        Value = 35
                    },
                    new Vital()
                    {
                        Id = Guid.NewGuid(),
                        Name = "HeartRate",
                        Unit = "BpM",
                        Value = 90                        
                    }
                }
            };

            var weightThreshold = new DefaultThreshold()
            {
                Name = "Weight",
                Unit = "kg",
                Id = Guid.NewGuid(),
                CustomerId = 1,
                MaxValue = 100,
                MinValue = 80                    
            };

            var temperatureThreshold = new DefaultThreshold()
            {
                Name = "Temperature",
                Id = Guid.NewGuid(),
                CustomerId = 1,
                MaxValue = 38,
                MinValue = 36,
                Unit = "C"                    
            };

            var heartRateThreshold = new DefaultThreshold()
            {
                Name = "HeartRate",
                Id = Guid.NewGuid(),
                CustomerId = 1,
                MaxValue = 190,
                MinValue = 50,
                Unit = "BpM"
            };

            #endregion

            sut.CreateViolationAlerts(testMeasurement, new List<Threshold>() {weightThreshold, temperatureThreshold, heartRateThreshold });

            Assert.IsTrue(testMeasurement.Vitals.Where(v => v.Name == "WeightTemperature").All(v => v.VitalAlert != null
                                                                                         && v.VitalAlert.Threshold != null
                                                                                         && v.VitalAlert.Threshold.Id == weightThreshold.Id));

            Assert.IsTrue(testMeasurement.Vitals.Where(v => v.Name == "Temperature").All(v => v.VitalAlert != null
                                                                                         && v.VitalAlert.Threshold != null
                                                                                         && v.VitalAlert.Threshold.Id == temperatureThreshold.Id));

            Assert.IsTrue(testMeasurement.Vitals.Where(v => v.Name == "HeartRate").All(v => v.VitalAlert == null));
        }
    }
}
