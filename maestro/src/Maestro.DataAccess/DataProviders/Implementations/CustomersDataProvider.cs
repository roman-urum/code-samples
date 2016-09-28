using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Common;
using Maestro.Common.Exceptions;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.CustomerService;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// CustomersDataProvider.
    /// </summary>
    public class CustomersDataProvider : ICustomersDataProvider
    {
        private readonly IRestApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersDataProvider"/> class.
        /// </summary>
        /// <param name="apiClientFactory">The API client factory.</param>
        public CustomersDataProvider(IRestApiClientFactory apiClientFactory)
        {
            _apiClient = apiClientFactory.Create(Settings.CustomerServiceUrl);
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="createCustomerData">The create customer data.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<int>> CreateCustomer(CreateCustomerRequestDto createCustomerData, string bearerToken)
        {
            return await _apiClient.SendRequestAsync<PostResponseDto<int>>("api/customers", createCustomerData, Method.POST, null, bearerToken);
        }

        /// <summary>
        /// Returns list of all existed customers.
        /// </summary>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<CustomerResponseDto>> GetCustomers(string bearerToken)
        {
            var requestUrl = string.Format("api/customers?isBrief=false&take={0}", int.MaxValue);
            var pagedResult = await _apiClient.SendRequestAsync<PagedResult<CustomerResponseDto>>(
                requestUrl,
                null, Method.GET,
                null, 
                bearerToken
            );

            return pagedResult.Results;
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<CustomerResponseDto> GetCustomer(int customerId, string bearerToken)
        {
            try
            {
                return await _apiClient.SendRequestAsync<CustomerResponseDto>("api/customers/" + customerId + "?isBrief=false", null, Method.GET, null, bearerToken);
            }
            catch (ServiceNotFoundException ex)
            {
                // Returning empty customer below
            }

            return await Task.FromResult<CustomerResponseDto>(null);
        }

        /// <summary>
        /// Return customer by customer subdomain.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="subdomain">Subdomain of customer site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public CustomerResponseDto GetCustomerBySubdomain(int customerId, string subdomain, string bearerToken)
        {
            var requestUrl = string.Format("/api/{0}/customers/subdomain/{1}?isBrief=false", customerId, subdomain);

            return _apiClient.SendRequest<CustomerResponseDto>(requestUrl, null, Method.GET, null, bearerToken);
        }

        /// <summary>
        /// Sends request to customer service to update customer data
        /// by specified id.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task UpdateCustomer(UpdateCustomerRequestDto customer, string bearerToken)
        {
            return _apiClient.SendRequestAsync(string.Format("api/customers/{0}", customer.Id), customer, Method.PUT, null, bearerToken);
        }

        /// <summary>
        /// Uploads customer logo using customer service.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns>
        /// Path to logo
        /// </returns>
        public async Task<string> UploadLogo(FileDto file, string bearerToken)
        {
            var response = await _apiClient.SendRequestAsync<UploadFileResponse>("api/FileUpload", file, Method.POST, null, bearerToken);

            return response.FilePath;
        }

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="newSite">The new site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateSite(int customerId, SiteRequestDto newSite, string bearerToken)
        {
            var requestUrl = string.Format("api/{0}/sites", customerId);

            return _apiClient.SendRequestAsync<PostResponseDto<Guid>>(requestUrl, newSite, Method.POST, null, bearerToken);
        }

        /// <summary>
        /// Updates the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="site">The site.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task UpdateSite(int customerId, Guid siteId, SiteRequestDto site, string bearerToken)
        {
            var requestUrl = string.Format("api/{0}/sites/{1}", customerId, siteId);

            return _apiClient.SendRequestAsync(requestUrl, site, Method.PUT, null, bearerToken);
        }

        /// <summary>
        /// Deletes the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task DeleteSite(int customerId, Guid siteId, string bearerToken)
        {
            var requestUrl = string.Format("api/{0}/sites/{1}", customerId, siteId);

            return _apiClient.SendRequestAsync(requestUrl, null, Method.DELETE, null, bearerToken);
        }

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateOrganization(int customerId, OrganizationRequestDto organization, string bearerToken)
        {
            var requestUrl = string.Format("api/{0}/organizations", customerId);

            return _apiClient.SendRequestAsync<PostResponseDto<Guid>>(requestUrl, organization, Method.POST, null, bearerToken);
        }

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task UpdateOrganization(int customerId, Guid organizationId, OrganizationRequestDto organization, string bearerToken)
        {
            var requestUrl = string.Format("api/{0}/organizations/{1}", customerId, organizationId);

            return _apiClient.SendRequestAsync(requestUrl, organization, Method.PUT, null, bearerToken);
        }

        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task DeleteOrganization(int customerId, Guid organizationId, string bearerToken)
        {
            var requestUrl = string.Format("api/{0}/organizations/{1}", customerId, organizationId);

            return _apiClient.SendRequestAsync(requestUrl, null, Method.DELETE, null, bearerToken);
        }

        /// <summary>
        /// Gets the sites.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<SiteResponseDto>> GetSites(int customerId, SiteSearchDto request, string bearerToken)
        {
            var requestUrl = string.Format("api/{0}/sites", customerId);

            var pagedResult = await _apiClient
                .SendRequestAsync<PagedResult<SiteResponseDto>>(
                    requestUrl,
                    request,
                    Method.GET,
                    null,
                    bearerToken
                );

            return pagedResult.Results;
        }
    }
}