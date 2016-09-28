using System.Collections.Generic;
using AutoMapper;
using Maestro.Domain.Dtos.CustomerService;

namespace Maestro.Web.Models.Customers
{
    /// <summary>
    /// Model for customers list
    /// </summary>
    public class CustomerListViewModel
    {
        public IEnumerable<BaseCustomerViewModel> Customers { get; private set; }

        public int SitesTotalCount { get; private set; }

        /// <summary>
        /// Initializes model by list of existed customers
        /// </summary>
        /// <param name="customers"></param>
        public CustomerListViewModel(IEnumerable<CustomerResponseDto> customers)
        {
            var customersList = new List<BaseCustomerViewModel>();

            foreach (var customerDto in customers)
            {
                if (customerDto.Sites.Count > 1)
                {
                    customersList.Add(Mapper.Map<MultipleSitesViewModel>(customerDto));
                }
                else
                {
                    customersList.Add(Mapper.Map<SingleSiteViewModel>(customerDto));
                }

                SitesTotalCount += customerDto.Sites.Count;
            }

            Customers = customersList;
        }
    }
}