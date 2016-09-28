using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.DataAccess;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Implementations;
using CustomerService.DomainLogic.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CustomerService.DomainLogic.Tests.Services.Implementations
{
    [TestClass]
    public class SitesServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private DefaultTestRepository<Site> siteRepository;
        private DefaultTestRepository<Organization> organizationRepository;
        private DefaultTestRepository<CategoryOfCare> categoriesOfCareRepository; 
        private CustomersTestRepository customersRepository;

        private ISiteService sut;

        [TestInitialize]
        public void Initialize()
        {
            // Setup
            unitOfWorkMock = new Mock<IUnitOfWork>();
            siteRepository = new DefaultTestRepository<Site>();
            organizationRepository = new DefaultTestRepository<Organization>();
            categoriesOfCareRepository = new DefaultTestRepository<CategoryOfCare>();
            customersRepository = new CustomersTestRepository();

            unitOfWorkMock.Setup(s => s.CreateGenericRepository<Site>())
                .Returns(() => siteRepository);
            unitOfWorkMock.Setup(s => s.CreateGenericRepository<Organization>())
                .Returns(() => organizationRepository);
            unitOfWorkMock.Setup(s=> s.CreateGenericRepository<Customer>())
                .Returns(() => customersRepository);
            unitOfWorkMock.Setup(s => s.CreateGenericRepository<CategoryOfCare>())
                .Returns(() => categoriesOfCareRepository);

            sut = new SitesService(unitOfWorkMock.Object);
        }

        #region GetSites

        [TestMethod]
        public async Task GetSites_DataSourceContainsSitesForDifferentCustomers_ReturnsCorrectSitesList()
        {
            // Arrange
            const int expectedSitesCount = 1;

            var site1 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000
            };
            var site2 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = 3001
            };

            this.siteRepository.Refresh(new List<Site> {site1, site2});

            // Act
            var actual = await this.sut.GetSites(site2.CustomerId);

            // Assert
            Assert.AreEqual(expectedSitesCount, actual.Results.Count);
            Assert.AreEqual(site2.Id, actual.Results.First().Id);
        }

        [TestMethod]
        public async Task GetSites_GetSitesForOrganization_ReturnsCorrectSitesList()
        {
            // Arrange
            const int testCustomerId = 3001;
            const int expectedSitesCount = 2;
            
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                ChildOrganizations = new List<Organization>(),
                Sites = new List<Site>()
            };
            var site1 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000
            };
            var site2 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                ParentOrganizationId = testOrganization.Id
            };
            var site3 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                ParentOrganizationId = testOrganization.Id
            };
            var searchDto = new SiteSearchDto
            {
                OrganizationId = testOrganization.Id
            };

            testOrganization.Sites.Add(site2);
            testOrganization.Sites.Add(site3);

            this.siteRepository.Refresh(new List<Site> {site1, site2, site3});
            this.organizationRepository.Refresh(new List<Organization> {testOrganization});

            // Act
            var actual = await this.sut.GetSites(testCustomerId, searchDto);

            // Assert
            Assert.AreEqual(expectedSitesCount, actual.Results.Count);
            Assert.IsTrue(actual.Results.All(s => s.Id == site2.Id || s.Id == site3.Id));
        }

        [TestMethod]
        public async Task GetSites_GetSitesForOrganisationWithSubOrganizations_ReturnsCorrectSitesList()
        {
            // Arrange
            const int testCustomerId = 3001;
            const int expectedSitesCount = 2;

            var testSubOrganuzation = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                ChildOrganizations = new List<Organization>(),
                Sites = new List<Site>()
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                ChildOrganizations = new List<Organization>
                {
                    testSubOrganuzation
                },
                Sites = new List<Site>()
            };
            var testSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                ParentOrganizationId = Guid.NewGuid()
            };
            var orgSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                ParentOrganizationId = testOrganization.Id
            };
            var subOrgSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId,
                ParentOrganizationId = testOrganization.Id
            };
            var searchDto = new SiteSearchDto
            {
                OrganizationId = testOrganization.Id
            };

            testOrganization.Sites.Add(orgSite);
            testSubOrganuzation.Sites.Add(subOrgSite);

            this.siteRepository.Refresh(new List<Site> { orgSite, subOrgSite, testSite });
            this.organizationRepository.Refresh(new List<Organization> { testOrganization });

            // Act
            var actual = await this.sut.GetSites(testCustomerId, searchDto);

            // Assert
            Assert.AreEqual(expectedSitesCount, actual.Results.Count);
            Assert.IsTrue(actual.Results.Any(s => s.Id == subOrgSite.Id));
        }

        #endregion

        #region GetSiteById

        [TestMethod]
        public async Task GetSiteById_SpecifiedSideExists_ReturnsCorrectSite()
        {
            // Arrange
            const int testCustomerId = 3000;

            var site1 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId
            };
            var site2 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId
            };

            this.siteRepository.Refresh(new List<Site> {site1, site2});

            // Act
            var actual = await this.sut.GetSiteById(testCustomerId, site2.Id);

            // Arrange
            Assert.IsNotNull(actual);
            Assert.AreSame(site2, actual);
        }

        [TestMethod]
        public async Task GetSiteById_CustomerIdIsMaestroCustomerId_ReturnsSiteWithoutCustomerIdCheck()
        {
            // Arrange
            const int testCustomerId = 3000;
            const int maestroCustomerId = 1;

            var site1 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId
            };
            var site2 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomerId
            };

            this.siteRepository.Refresh(new List<Site> { site1, site2 });

            // Act
            var actual = await this.sut.GetSiteById(maestroCustomerId, site2.Id);

            // Arrange
            Assert.IsNotNull(actual);
            Assert.AreSame(site2, actual);
        }

        #endregion

        #region CreateSite

        [TestMethod]
        public async Task CreateSite_CustomerNotExists_ReturnsError()
        {
            // Arrange
            const int notExistingCustomerId = 3000;

            var testSite = new Site
            {
                CustomerId = notExistingCustomerId
            };
        
            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.AreEqual(SiteStatus.CustomerNotFound, actual.Status);
        }

        [TestMethod]
        public async Task CreateSite_OrganizationNotExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testSite = new Site
            {
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid()
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer});

            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.AreEqual(SiteStatus.OrganizationNotFound, actual.Status);
        }

        [TestMethod]
        public async Task CreateSite_SiteWithSameNameExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3001
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var existingSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid(),
                Name = "testSite"
            };
            var testSite = new Site
            {
                CustomerId = testCustomer.Id,
                ParentOrganizationId = testOrganization.Id,
                Name = "testSite"
            };

            this.organizationRepository.Refresh(new List<Organization> {testOrganization});
            this.customersRepository.Refresh(new List<Customer> {testCustomer});
            this.siteRepository.Refresh(new List<Site> {existingSite});

            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.AreEqual(SiteStatus.NameConflict, actual.Status);
        }

        [TestMethod]
        public async Task CreateSite_SiteWithSameNPIExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var existingSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid(),
                NationalProviderIdentificator = "testIdentificator"
            };
            var testSite = new Site
            {
                CustomerId = testCustomer.Id,
                ParentOrganizationId = testOrganization.Id,
                NationalProviderIdentificator = "testIdentificator"
            };

            this.organizationRepository.Refresh(new List<Organization> { testOrganization });
            this.customersRepository.Refresh(new List<Customer> { testCustomer });
            this.siteRepository.Refresh(new List<Site> { existingSite });

            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.AreEqual(SiteStatus.NPIConflict, actual.Status);
        }

        [TestMethod]
        public async Task CreateSite_SiteWithSameCustomerSiteIdExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var existingSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid(),
                CustomerSiteId = "testSiteId"
            };
            var testSite = new Site
            {
                CustomerId = testCustomer.Id,
                ParentOrganizationId = testOrganization.Id,
                CustomerSiteId = "testSiteId"
            };

            this.organizationRepository.Refresh(new List<Organization> { testOrganization });
            this.customersRepository.Refresh(new List<Customer> { testCustomer });
            this.siteRepository.Refresh(new List<Site> { existingSite });

            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.AreEqual(SiteStatus.CustomerSiteIdConflict, actual.Status);
        }

        [TestMethod]
        public async Task CreateSite_CategoriesOfCareNotExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var testSite = new Site
            {
                CustomerId = testCustomer.Id,
                ParentOrganizationId = testOrganization.Id,
                CategoriesOfCare = new List<CategoryOfCare>
                {
                    new CategoryOfCare
                    {
                        Id = Guid.NewGuid()
                    }
                }
            };

            this.organizationRepository.Refresh(new List<Organization> { testOrganization });
            this.customersRepository.Refresh(new List<Customer> { testCustomer });

            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.AreEqual(SiteStatus.CategoryOfCareConflict, actual.Status);
        }

        [TestMethod]
        public async Task CreateSite_SiteDataIsValid_SiteCreated()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var testSite = new Site
            {
                CustomerId = testCustomer.Id,
                ParentOrganizationId = testOrganization.Id
            };

            this.organizationRepository.Refresh(new List<Organization> { testOrganization });
            this.customersRepository.Refresh(new List<Customer> { testCustomer });

            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.IsNotNull(actual.Content);
            Assert.AreEqual(SiteStatus.Success, actual.Status);
        }

        #endregion

        #region UpdateSite

        [TestMethod]
        public async Task UpdateSite_SiteNotExists_ReturnsError()
        {
            // Arrange
            var testSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000
            };

            // Act
            var actual = await this.sut.UpdateSite(testSite, false);

            // Assert
            Assert.AreEqual(SiteStatus.NotFound, actual);
        }

        [TestMethod]
        public async Task UpdateSite_OrganizationNotExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var existingSite = new Site
            {
                CustomerId = testCustomer.Id,
                Id = Guid.NewGuid()
            };
            var testSite = new Site
            {
                Id = existingSite.Id,
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid()
            };

            this.siteRepository.Refresh(new List<Site> {existingSite});
            this.customersRepository.Refresh(new List<Customer> { testCustomer });

            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.AreEqual(SiteStatus.OrganizationNotFound, actual.Status);
        }

        [TestMethod]
        public async Task UpdateSite_SiteWithSameNameExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var existingSite1 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                Name = "testSite1"
            };
            var existingSite2 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                Name = "testSite2"
            };
            var testSite = new Site
            {
                Id = existingSite2.Id,
                CustomerId = existingSite2.CustomerId,
                Name = existingSite1.Name
            };
            
            this.customersRepository.Refresh(new List<Customer> { testCustomer });
            this.siteRepository.Refresh(new List<Site> { existingSite1, existingSite2 });

            // Act
            var actual = await this.sut.CreateSite(testSite);

            // Assert
            Assert.AreEqual(SiteStatus.NameConflict, actual.Status);
        }

        [TestMethod]
        public async Task UpdateSite_SiteWithSameNPIExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var existingSite1 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid(),
                NationalProviderIdentificator = "testIdentificator1"
            };
            var existingSite2 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid(),
                NationalProviderIdentificator = "testIdentificator2"
            };
            var testSite = new Site
            {
                Id = existingSite2.Id,
                CustomerId = testCustomer.Id,
                ParentOrganizationId = testOrganization.Id,
                NationalProviderIdentificator = existingSite1.NationalProviderIdentificator
            };

            this.organizationRepository.Refresh(new List<Organization> { testOrganization });
            this.customersRepository.Refresh(new List<Customer> { testCustomer });
            this.siteRepository.Refresh(new List<Site> { existingSite1, existingSite2 });

            // Act
            var actual = await this.sut.UpdateSite(testSite, false);

            // Assert
            Assert.AreEqual(SiteStatus.NPIConflict, actual);
        }

        [TestMethod]
        public async Task UpdateSite_SiteWithSameCustomerSiteIdExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var existingSite1 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid(),
                CustomerSiteId = "testSiteId1"
            };
            var existingSite2 = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                ParentOrganizationId = Guid.NewGuid(),
                CustomerSiteId = "testSiteId2"
            };
            var testSite = new Site
            {
                Id = existingSite2.Id,
                CustomerId = testCustomer.Id,
                ParentOrganizationId = testOrganization.Id,
                CustomerSiteId = existingSite1.CustomerSiteId
            };

            this.organizationRepository.Refresh(new List<Organization> { testOrganization });
            this.customersRepository.Refresh(new List<Customer> { testCustomer });
            this.siteRepository.Refresh(new List<Site> { existingSite1, existingSite2 });

            // Act
            var actual = await this.sut.UpdateSite(testSite, false);

            // Assert
            Assert.AreEqual(SiteStatus.CustomerSiteIdConflict, actual);
        }

        [TestMethod]
        public async Task UpdateSite_CategoriesOfCareNotExists_ReturnsError()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var existingSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var testSite = new Site
            {
                Id = existingSite.Id,
                CustomerId = testCustomer.Id,
                ParentOrganizationId = testOrganization.Id,
                CategoriesOfCare = new List<CategoryOfCare>
                {
                    new CategoryOfCare
                    {
                        Id = Guid.NewGuid()
                    }
                }
            };

            this.siteRepository.Refresh(new List<Site> {existingSite});
            this.organizationRepository.Refresh(new List<Organization> {testOrganization});
            this.customersRepository.Refresh(new List<Customer> {testCustomer});

            // Act
            var actual = await this.sut.UpdateSite(testSite, false);

            // Assert
            Assert.AreEqual(SiteStatus.CategoryOfCareConflict, actual);
        }

        [TestMethod]
        public async Task UpdateSite_CorrectDataProvided_SiteUpdated()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000
            };
            var testOrganization = new Organization
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id
            };
            var existingSite = new Site()
            {
                Id = Guid.NewGuid(),
                CustomerId = testCustomer.Id,
                Name = "siteName",
                CategoriesOfCare = new List<CategoryOfCare>()
            };
            var testSite = new Site
            {
                Id = existingSite.Id,
                CustomerId = testCustomer.Id,
                Address1 = "testAddress1",
                Address2 = "testAddress2",
                Address3 = "testAddress3",
                City = "testCity",
                ContactPhone = "12345789",
                IsActive = true,
                IsPublished = true,
                Name = "testSiteName",
                NationalProviderIdentificator = "testId",
                State = "testState",
                ZipCode = "testZipCode",
                CategoriesOfCare = new List<CategoryOfCare>()
            };

            this.siteRepository.Refresh(new List<Site> { existingSite });
            this.organizationRepository.Refresh(new List<Organization> { testOrganization });
            this.customersRepository.Refresh(new List<Customer> { testCustomer });

            // Act
            var result = await this.sut.UpdateSite(testSite, false);
            var actual = await this.sut.GetSiteById(existingSite.CustomerId, existingSite.Id);

            // Assert
            Assert.AreEqual(SiteStatus.Success, result);
            Assert.AreEqual(testSite.Address1, actual.Address1);
            Assert.AreEqual(testSite.Address2, actual.Address2);
            Assert.AreEqual(testSite.Address3, actual.Address3);
            Assert.AreEqual(testSite.City, actual.City);
            Assert.AreEqual(testSite.ContactPhone, actual.ContactPhone);
            Assert.AreEqual(testSite.IsActive, actual.IsActive);
            Assert.AreEqual(testSite.IsPublished, actual.IsPublished);
            Assert.AreEqual(testSite.Name, actual.Name);
            Assert.AreEqual(testSite.NationalProviderIdentificator, actual.NationalProviderIdentificator);
            Assert.AreEqual(testSite.State, actual.State);
            Assert.AreEqual(testSite.ZipCode, actual.ZipCode);
        }

        #endregion

        #region Delete Site

        [TestMethod]
        public async Task DeleteSite_SiteNotExists_ReturnsFalse()
        {
            // Arrange
            const int testCustomerId = 3000;

            var notExistingSiteId = Guid.NewGuid();
            
            // Act
            var actual = await this.sut.DeleteSite(testCustomerId, notExistingSiteId);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task DeleteSite_SiteWasDeleted_ReturnsTrue()
        {
            // Arrage
            var existingSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                IsDeleted = false
            };

            this.siteRepository.Refresh(new List<Site> { existingSite });

            // Act
            var actual = await this.sut.DeleteSite(existingSite.CustomerId, existingSite.Id);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task DeleteSite_SiteWasDeleted_SiteAccessibleAfterDeletion()
        {
            // Arrage
            var existingSite = new Site
            {
                Id = Guid.NewGuid(),
                CustomerId = 3000,
                IsDeleted = false
            };

            this.siteRepository.Refresh(new List<Site> {existingSite});

            // Act
            await this.sut.DeleteSite(existingSite.CustomerId, existingSite.Id);
            var actual = await this.sut.GetSiteById(existingSite.CustomerId, existingSite.Id);

            // Assert
            Assert.IsNotNull(actual);
        }

        #endregion
    }
}