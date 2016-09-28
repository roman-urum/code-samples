using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using VitalsService.DataAccess.Document;
using VitalsService.DataAccess.Document.Contexts;
using VitalsService.DataAccess.Document.Repository;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.DataAccess.EF;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.DocumentDb;
using VitalsService.Domain.EsbEntities;
using VitalsService.DomainLogic.Services.Implementations;
using VitalsService.DomainLogic.Tests.Exceptions;

namespace VitalsService.DomainLogic.Tests.Services.Implementations
{
    [TestClass]
    public class MeasurmentsServiceTests
    {
        private IMeasurementsService sut;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private TestRepository<Measurement> measurementsRepository;    

        public MeasurmentsServiceTests()
        {
            measurementsRepository = new TestRepository<Measurement>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.CreateRepository<Measurement>()).Returns(measurementsRepository);            
        }

        [TestMethod]
        public async Task Create_AddMeasurementRepository_WorksCorrectly()
        {            
            //Mock<IDocumentDbRepository<RawMeasurement>> documentRepositoryMock = new Mock<IDocumentDbRepository<RawMeasurement>>();
            //documentRepositoryMock.Setup(m => m.CreateItemAsync(It.IsAny<RawMeasurement>())).ReturnsAsync(HttpStatusCode.OK);

            //Mock<IDocumentRepositoryFactory> documentRepositoryFactoryMock = new Mock<IDocumentRepositoryFactory>();
            //documentRepositoryFactoryMock
            //    .Setup(f => f.Create<RawMeasurement>(It.IsAny<VitalsServiceCollection>()))
            //    .Returns(documentRepositoryMock.Object);

            //Mock<IEsb> esbMock = new Mock<IEsb>();
            //esbMock.Setup(m => m.PublishMeasurement(It.IsAny<MeasurementMessage>())).Returns(() => null);

            //sut = new MeasurmentsService(unitOfWorkMock.Object, documentRepositoryFactoryMock.Object, esbMock.Object);

            //var testMeasurement = new Measurement()
            //{
            //    Id = Guid.NewGuid(),
            //    CustomerId = 1,
            //    PatientId = Guid.NewGuid(),
            //    CreatedUtc = DateTime.Now,
            //    ObservedTz = "test tz",
            //    ObservedUtc = DateTime.Now,
            //    Vitals = new List<Vital>() { new Vital() }
            //};

            //measurementsRepository.Refresh(new List<Measurement>());
            //await sut.Create(testMeasurement, null, false);
            
            //var actualAddedMeasurement = await measurementsRepository.FirstOrDefaultAsync();
            //Assert.AreEqual(actualAddedMeasurement, testMeasurement);
        }

        [TestMethod]
        [ExpectedException(typeof(AddDocumentDbItemException))]
        public async Task Create_AddRawMeasurementToDocumentDbRepository_WorksCorrectly()
        {
            //Mock<IDocumentDbRepository<RawMeasurement>> documentRepositoryMock = new Mock<IDocumentDbRepository<RawMeasurement>>();
            //documentRepositoryMock.Setup(m => m.CreateItemAsync(It.IsAny<RawMeasurement>())).Throws<AddDocumentDbItemException>();
                
            //Mock<IDocumentRepositoryFactory> documentRepositoryFactoryMock = new Mock<IDocumentRepositoryFactory>();
            //documentRepositoryFactoryMock
            //    .Setup(f => f.Create<RawMeasurement>(It.IsAny<VitalsServiceCollection>()))
            //    .Returns(documentRepositoryMock.Object);

            //Mock<IEsb> esbMock = new Mock<IEsb>();
            //esbMock.Setup(m => m.PublishMeasurement(It.IsAny<MeasurementMessage>())).Returns(() => null);

            //sut = new MeasurmentsService(unitOfWorkMock.Object, documentRepositoryFactoryMock.Object, esbMock.Object);

            //var testMeasurement = new Measurement()
            //{
            //    Id = Guid.NewGuid(),
            //    CustomerId = 1,
            //    PatientId = Guid.NewGuid(),
            //    CreatedUtc = DateTime.Now,
            //    ObservedTz = "test tz",
            //    ObservedUtc = DateTime.Now,
            //    Vitals = new List<Vital>() { new Vital() }
            //};
            //await sut.Create(testMeasurement, null, false);
        }

        [TestMethod]
        public async Task Create_SentMeasurementToEsb_WorksCorrectly()
        {
            
        }

        [TestMethod]
        public async Task Create_DoNotSentMeasurementToEsb_WorksCorrectly()
        {
            
        }
    }
}
