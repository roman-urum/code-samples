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
    public class CustomerServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private CustomersTestRepository customersRepository;

        private ICustomerService sut;

        [TestInitialize]
        public void Initialize()
        {
            // Setup
            unitOfWorkMock = new Mock<IUnitOfWork>();
            customersRepository = new CustomersTestRepository();

            unitOfWorkMock.Setup(s => s.CreateGenericRepository<Customer>())
                .Returns(() => customersRepository);

            sut = new CustomersService(unitOfWorkMock.Object);
        }

        #region GetCustomer

        [TestMethod]
        public async Task GetCustomer_IncludeSoftDeleted_ReturnsDeletedRecordInResult()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000,
                IsDeleted = true
            };

            this.customersRepository.Refresh(new List<Customer> {testCustomer});

            // Act
            var actual = await this.sut.GetCustomer(testCustomer.Id, true);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(testCustomer, actual);
        }

        [TestMethod]
        public async Task GetCustomer_DontIncludeSoftDeleted_RerturnsNullForDeletedCustomers()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000,
                IsDeleted = true
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer });

            // Act
            var actual = await this.sut.GetCustomer(testCustomer.Id);

            // Assert
            Assert.IsNull(actual);
        }

        #endregion

        #region GetCustomerByName

        [TestMethod]
        public async Task GetCustomerByName_CustomerIdSpecified_ReturnsCustomerWithMatchParameters()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1"
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Name = "TestCustomer2"
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var actual = await this.sut.GetCustomerByName(testCustomer1.Name, testCustomer1.Id);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(testCustomer1, actual);
        }

        [TestMethod]
        public async Task GetCustomerByName_IncorrectCustomerIdSpecified_ReturnsNull()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1"
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Name = "TestCustomer2"
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var actual = await this.sut.GetCustomerByName(testCustomer1.Name, testCustomer2.Id);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetCustomerByName_CorrectNameSpecified_ReturnsAppropriateCustomer()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1"
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Name = "TestCustomer2"
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var actual = await this.sut.GetCustomerByName(testCustomer1.Name);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(testCustomer1, actual);
        }

        [TestMethod]
        public async Task GetCustomerByName_CustomerWasDeleted_ReturnsNull()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                IsDeleted = true
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1 });

            // Act
            var actual = await this.sut.GetCustomerByName(testCustomer1.Name);

            // Assert
            Assert.IsNull(actual);
        }

        #endregion

        #region GetCustomerBySubdomain

        [TestMethod]
        public async Task GetCustomerBySubdomain_CustomerIdSpecified_ReturnsCustomerWithMatchParameters()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Subdomain = "TestCustomer1"
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Subdomain = "TestCustomer2"
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var actual = await this.sut.GetCustomerBySubdomain(testCustomer1.Subdomain, testCustomer1.Id);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(testCustomer1, actual);
        }

        [TestMethod]
        public async Task GetCustomerBySubdomain_IncorrectCustomerIdSpecified_ReturnsNull()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Subdomain = "TestCustomer1"
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Subdomain = "TestCustomer2"
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var actual = await this.sut.GetCustomerBySubdomain(testCustomer1.Subdomain, testCustomer2.Id);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetCustomerBySubdomain_CorrectSubdomainSpecified_ReturnsAppropriateCustomer()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Subdomain = "TestCustomer1"
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Subdomain = "TestCustomer2"
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var actual = await this.sut.GetCustomerBySubdomain(testCustomer1.Subdomain);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(testCustomer1, actual);
        }

        [TestMethod]
        public async Task GetCustomerBySubdomain_CustomerWasDeleted_ReturnsNull()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Subdomain = "TestCustomer1",
                IsDeleted = true
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1 });

            // Act
            var actual = await this.sut.GetCustomerBySubdomain(testCustomer1.Subdomain);

            // Assert
            Assert.IsNull(actual);
        }

        #endregion

        #region DeleteCustomer

        [TestMethod]
        public async Task DeleteCustomer_CustomerNotExists_ReturnsFalse()
        {
            // Arrange
            var testCustomerId = 3000;

            // Act
            var actual = await this.sut.DeleteCustomer(testCustomerId);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task DeleteCustomer_CorrectCustomerIdSpecified_ReturnsTrue()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000,
                IsDeleted = false,
                Sites = new List<Site>(),
                Organizations = new List<Organization>()
            };

            this.customersRepository.Refresh(new List<Customer> {testCustomer});

            // Act
            var actual = await this.sut.DeleteCustomer(testCustomer.Id);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task DeleteCustomer_CorrectCustomerIdSpecified_CustomerDeleted()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Id = 3000,
                IsDeleted = false,
                Sites = new List<Site>(),
                Organizations = new List<Organization>()
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer });

            // Act
            await this.sut.DeleteCustomer(testCustomer.Id);
            var actual = await this.sut.GetCustomer(testCustomer.Id);

            // Assert
            Assert.IsNull(actual);
        }

        #endregion

        #region CreateCustomer

        [TestMethod]
        public async Task CreateCustomer_CustomerWithTheSameNameExists_ReturnsError()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                Subdomain = "TestCustomer1"
            };
            var testCustomer = new Customer
            {
                Name = existingCustomer.Name,
                Subdomain = "TestCustomer2"
            };

            this.customersRepository.Refresh(new List<Customer> {existingCustomer});

            // Act
            var actual = await this.sut.CreateCustomer(testCustomer);

            // Assert
            Assert.AreEqual(CustomerStatus.NameConflict, actual.Status);
        }

        [TestMethod]
        public async Task CreateCustomer_CustomerWithTheSameSubdomainExists_ReturnsError()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                Subdomain = "TestCustomer1"
            };
            var testCustomer = new Customer
            {
                Name = "TestCustomer2",
                Subdomain = existingCustomer.Subdomain
            };

            this.customersRepository.Refresh(new List<Customer> { existingCustomer });

            // Act
            var actual = await this.sut.CreateCustomer(testCustomer);

            // Assert
            Assert.AreEqual(CustomerStatus.SubdomainConflict, actual.Status);
        }

        [TestMethod]
        public async Task CreateCustomer_CustomerCreated_ReturnsSuccessResponse()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Name = "TestCustomer",
                Subdomain = "TestCustomer"
            };

            // Act
            var actual = await this.sut.CreateCustomer(testCustomer);

            // Assert
            Assert.AreEqual(CustomerStatus.Success, actual.Status);
            Assert.IsNotNull(actual.Content);
        }

        [TestMethod]
        public async Task CreateCustomer_CustomerCreated_CustomerIsAvailableAfterSaving()
        {
            // Arrange
            var testCustomer = new Customer
            {
                Name = "TestCustomer",
                Subdomain = "TestCustomer",
                IddleSessionTimeout = 20,
                PasswordExpirationDays = 5
            };

            // Act
            var result = await this.sut.CreateCustomer(testCustomer);
            var actual = await this.sut.GetCustomer(result.Content);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(testCustomer.Name, actual.Name);
            Assert.AreEqual(testCustomer.Subdomain, actual.Subdomain);
            Assert.AreEqual(testCustomer.IddleSessionTimeout, actual.IddleSessionTimeout);
            Assert.AreEqual(testCustomer.PasswordExpirationDays, actual.PasswordExpirationDays);
        }

        #endregion

        #region GetCustomers

        [TestMethod]
        public async Task GetCustomers_CustomerNameMatchToQuery_ReturnsMatchCustomer()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                Subdomain = "TestCustomerSubdomain1"
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Name = "TestCustomer2",
                Subdomain = "TestCustomerSubdomain2"
            };
            var searchRequest = new CustomersSearchDto
            {
                Q = testCustomer2.Name
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var result = await this.sut.GetCustomers(searchRequest);
            var actual = result.Results.FirstOrDefault();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(testCustomer2, actual);
        }

        [TestMethod]
        public async Task GetCustomers_CustomerSubdomainMatchToQuery_ReturnsMatchCustomer()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                Subdomain = "TestCustomerSubdomain1"
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Name = "TestCustomer2",
                Subdomain = "TestCustomerSubdomain2"
            };
            var searchRequest = new CustomersSearchDto
            {
                Q = testCustomer2.Subdomain
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var result = await this.sut.GetCustomers(searchRequest);
            var actual = result.Results.FirstOrDefault();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(testCustomer2, actual);
        }

        [TestMethod]
        public async Task GetCustomers_IncludeArchived_ReturnsDeletedCustomers()
        {
            // Arrange
            var testCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                Subdomain = "TestCustomerSubdomain1",
                IsDeleted = true
            };
            var testCustomer2 = new Customer
            {
                Id = 3001,
                Name = "TestCustomer2",
                Subdomain = "TestCustomerSubdomain2"
            };
            var searchRequest = new CustomersSearchDto
            {
                IncludeArchived = true
            };

            this.customersRepository.Refresh(new List<Customer> { testCustomer1, testCustomer2 });

            // Act
            var result = await this.sut.GetCustomers(searchRequest);
            var actual = result.Results.FirstOrDefault(c => c.Id == testCustomer1.Id);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreSame(testCustomer1, actual);
        }

        #endregion

        #region UpdateCustomer

        [TestMethod]
        public async Task UpdateCustomer_CustomerNotExists_ReturnsError()
        {
            // Arrange
            const int notExistingCustomerId = 3000;

            var customer = new Customer
            {
                Id = notExistingCustomerId
            };

            // Act
            var actual = await this.sut.UpdateCustomer(customer, false);

            // Assert
            Assert.AreEqual(CustomerStatus.NotFound, actual);
        }

        [TestMethod]
        public async Task UpdateCustomer_CustomerWithTheSameNameExists_ReturnsError()
        {
            // Arrange
            var existingCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                Subdomain = "TestCustomer1"
            };
            var existingCustomer2 = new Customer
            {
                Id = 3001,
                Name = "TestCustomer2",
                Subdomain = "TestCustomer2"
            };
            var testCustomer = new Customer
            {
                Id = existingCustomer2.Id,
                Name = existingCustomer1.Name,
                Subdomain = existingCustomer2.Subdomain
            };

            this.customersRepository.Refresh(new List<Customer> { existingCustomer1, existingCustomer2 });

            // Act
            var actual = await this.sut.UpdateCustomer(testCustomer, false);

            // Assert
            Assert.AreEqual(CustomerStatus.NameConflict, actual);
        }

        [TestMethod]
        public async Task UpdateCustomer_CustomerWithTheSameSubdomainExists_ReturnsError()
        {
            // Arrange
            var existingCustomer1 = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                Subdomain = "TestCustomer1"
            };
            var existingCustomer2 = new Customer
            {
                Id = 3001,
                Name = "TestCustomer2",
                Subdomain = "TestCustomer2"
            };
            var testCustomer = new Customer
            {
                Id = existingCustomer2.Id,
                Name = existingCustomer2.Name,
                Subdomain = existingCustomer1.Subdomain
            };

            this.customersRepository.Refresh(new List<Customer> {existingCustomer1, existingCustomer2});

            // Act
            var actual = await this.sut.UpdateCustomer(testCustomer, false);

            // Assert
            Assert.AreEqual(CustomerStatus.SubdomainConflict, actual);
        }

        [TestMethod]
        public async Task UpdateCustomer_CustomerWasUpdated_DataSavedCorrect()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                Id = 3000,
                Name = "TestCustomer1",
                Subdomain = "TestCustomerSubdomain1"
            };
            var testCustomer = new Customer
            {
                Id = existingCustomer.Id,
                Name = "TestCustomer2",
                Subdomain = "TestCustomerSubdomain2",
                IddleSessionTimeout = 10,
                LogoPath = "http://localhost/logo.img",
                PasswordExpirationDays = 10
            };

            this.customersRepository.Refresh(new List<Customer> {existingCustomer});

            // Act
            await this.sut.UpdateCustomer(testCustomer, true);
            var actual = await this.sut.GetCustomer(existingCustomer.Id, true);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.IsDeleted);
            Assert.AreEqual(testCustomer.Name, actual.Name);
            Assert.AreEqual(testCustomer.Subdomain, actual.Subdomain);
            Assert.AreEqual(testCustomer.IddleSessionTimeout, actual.IddleSessionTimeout);
            Assert.AreEqual(testCustomer.LogoPath, actual.LogoPath);
            Assert.AreEqual(testCustomer.PasswordExpirationDays, actual.PasswordExpirationDays);
        }

        #endregion
    }
}
