using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Maestro.Common;
using Maestro.Common.Exceptions;
using Maestro.Domain.Constants;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Extensions;
using Maestro.Web.Filters;
using Maestro.Web.Models.Customers;
using Maestro.Web.Security;

namespace Maestro.Web.Controllers
{
    /// <summary>
    /// CustomersController.
    /// </summary>
    [MaestroAuthorize(Roles.SuperAdmin)]
    public class CustomersController : BaseController
    {
        private readonly ICustomersService customersService;
        private readonly IAuthDataStorage authDataStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="customersService">The customers service.</param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        public CustomersController(
            ICustomersService customersService,
            IAuthDataStorage authDataStorage
        )
        {
            this.customersService = customersService;
            this.authDataStorage = authDataStorage;
        }

        /// <summary>
        /// Returns view with list of customers and ability
        /// to search customers and sites.
        /// </summary>
        public async Task<ActionResult> Index()
        {
            IList<CustomerResponseDto> customers;
            
            if (User.IsInRole(Roles.SuperAdmin))
            {
                customers = await customersService.GetCustomers(authDataStorage.GetToken());
            }
            else if (User.IsInRole(Roles.CustomerUser))
            {
                customers = await customersService.GetCustomers(User.Id, authDataStorage.GetToken());
            }
            else
            {
                customers = new List<CustomerResponseDto>();
            }

            var model = new CustomerListViewModel(customers);

            return View(model);
        }

        /// <summary>
        /// Handles requests to create new customer.
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns></returns>
        public async Task<JsonResult> Create(CreateCustomerViewModel newCustomer)
        {
            if (!ModelState.IsValid)
            {
                string validationError = ModelState
                    .Where(item => item.Value.Errors.Any())
                    .SelectMany(item => item.Value.Errors)
                    .Select(item => item.ErrorMessage)
                    .Aggregate((s1, s2) => s1 + ";" + s2);

                return Json(new { message = validationError, success = false });
            }

            try
            {
                var newCustomerDto = Mapper.Map<CreateCustomerRequestDto>(newCustomer);
                newCustomerDto.LogoPath = Settings.DefaultCustomerLogoPath;
                newCustomerDto.PasswordExpirationDays = Settings.DefaultCustomerPasswordExpirationDays;
                newCustomerDto.IddleSessionTimeout = Settings.DefaultCustomerIddleSessionTimeout;

                // Creating new customer
                var customerResult = await this.customersService.CreateCustomer(newCustomerDto, authDataStorage.GetToken());

                // Creating new site for new customer
                var newSite = Mapper.Map<CreateCustomerViewModel, SiteRequestDto>(newCustomer);

                await this.customersService.CreateSite(customerResult.Id, newSite, authDataStorage.GetToken());

                var redirectUrl = Url.CustomerAction(newCustomer.SubdomainName, "General", "Settings", null);

                return Json(new { message = "Success", success = true, redirectUrl = redirectUrl });
            }
            catch (ServiceException ex)
            {
                return Json(new { message = ex.ServiceMessage, success = false });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false });
            }
        }
    }
}