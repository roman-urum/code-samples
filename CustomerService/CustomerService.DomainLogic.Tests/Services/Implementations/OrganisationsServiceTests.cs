using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.DataAccess;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Implementations;
using CustomerService.DomainLogic.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CustomerService.DomainLogic.Tests.Services.Implementations
{
    [TestClass]
    public class OrganisationsServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private DefaultTestRepository<Organization> organizationsRepository;
        private IOrganizationsService sut;

        [TestInitialize]
        public void Initialize()
        {
            // Setup
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.organizationsRepository = new DefaultTestRepository<Organization>();

            this.unitOfWorkMock.Setup(s => s.CreateGenericRepository<Organization>())
                .Returns(() => this.organizationsRepository);

            this.sut = new OrganizationsService(unitOfWorkMock.Object);
        }

        #region GetOrganizations

        [TestMethod]
        public async Task GetOrganizations_WithoutArchived_ResultNotContainsDeletedOrganizations()
        {
            // Arrange
            const int testCustomerId = 3000;

            var deletedOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                IsDeleted = true
            };
            var activeOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                IsDeleted = false
            };
            var searchDto = new OrganizationSearchDto
            {
                IncludeArchived = false
            };

            this.organizationsRepository.Refresh(new List<Organization> {deletedOrganization, activeOrganization});

            // Act
            var actual = await this.sut.GetOrganizations(testCustomerId, searchDto);

            // Assert
            Assert.IsFalse(actual.Results.Contains(deletedOrganization));
            Assert.IsTrue(actual.Results.Contains(activeOrganization));
        }

        [TestMethod]
        public async Task GetOrganizations_WithoutArchived_ResultContainsDeletedOrganizations()
        {
            // Arrange
            const int testCustomerId = 3000;

            var deletedOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                IsDeleted = true
            };
            var activeOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                IsDeleted = false
            };
            var searchDto = new OrganizationSearchDto
            {
                IncludeArchived = true
            };

            this.organizationsRepository.Refresh(new List<Organization> { deletedOrganization, activeOrganization });

            // Act
            var actual = await this.sut.GetOrganizations(testCustomerId, searchDto);

            // Assert
            Assert.IsTrue(actual.Results.Contains(deletedOrganization));
            Assert.IsTrue(actual.Results.Contains(activeOrganization));
        }

        #endregion

        [TestMethod]
        public async Task GetOrganization_OrganizationWasDeleted_ReturnsDeletedOrganization()
        {
            // Arrange
            const int testCustomerId = 3000;

            var deletedOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                IsDeleted = true
            };

            this.organizationsRepository.Refresh(new List<Organization> { deletedOrganization });

            // Act
            var actual = await this.sut.GetOrganization(testCustomerId, deletedOrganization.Id);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}