using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomerService.Common.Helpers;
using CustomerService.DataAccess;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Interfaces;

namespace CustomerService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// CategoriesOfCaresService.
    /// </summary>
    public class CategoriesOfCareService : ICategoriesOfCareService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<CategoryOfCare> categoryOfCareRepository;
        private readonly IRepository<Customer> customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesOfCareService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CategoriesOfCareService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.categoryOfCareRepository = this.unitOfWork.CreateGenericRepository<CategoryOfCare>();
            this.customerRepository = this.unitOfWork.CreateGenericRepository<Customer>();
        }

        /// <summary>
        /// Creates the category of care.
        /// </summary>
        /// <param name="categoryOfCare">The category of care.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<CategoryOfCare, CategoryOfCareStatus>> 
            CreateCategoryOfCare(CategoryOfCare categoryOfCare)
        {
            var validationResult = CategoryOfCareStatus.Success;

            var existingCustomer = (await customerRepository
                .FindAsync(
                    c => !c.IsDeleted && c.Id == categoryOfCare.CustomerId
                ))
                .SingleOrDefault();

            if (existingCustomer == null)
            {
                validationResult = CategoryOfCareStatus.CustomerDoesNotExist;
            }

            var existingCategoryOfCare = await GetCategoryOfCare(categoryOfCare.CustomerId, categoryOfCare.Name);

            if (existingCategoryOfCare != null)
            {
                if (validationResult.HasFlag(CategoryOfCareStatus.Success))
                {
                    validationResult = CategoryOfCareStatus.NameConflict;
                }
                else
                {
                    validationResult |= CategoryOfCareStatus.NameConflict;
                }
            }

            if (!validationResult.HasFlag(CategoryOfCareStatus.Success))
            {
                return new OperationResultDto<CategoryOfCare, CategoryOfCareStatus>()
                {
                    Status = validationResult
                };
            }

            categoryOfCareRepository.Insert(categoryOfCare);

            await unitOfWork.SaveAsync();

            return new OperationResultDto<CategoryOfCare, CategoryOfCareStatus>()
            {
                Content = categoryOfCare,
                Status = CategoryOfCareStatus.Success
            };
        }

        /// <summary>
        /// Updates the category of care.
        /// </summary>
        /// <param name="categoryOfCare">The category of care.</param>
        /// <returns></returns>
        public async Task<CategoryOfCareStatus> UpdateCategoryOfCare(CategoryOfCare categoryOfCare)
        {
            var existingCategoryOfCare = await GetCategoryOfCare(categoryOfCare.Id);

            if (existingCategoryOfCare == null)
            {
                return CategoryOfCareStatus.CategoryOfCareWithSuchIdDoesNotExist;
            }

            var existingCategoryOfCareWithConflictedName = 
                await GetCategoryOfCare(categoryOfCare.CustomerId, categoryOfCare.Name);

            if (existingCategoryOfCareWithConflictedName != null)
            {
                return CategoryOfCareStatus.NameConflict;
            }

            existingCategoryOfCare.Name = categoryOfCare.Name;

            categoryOfCareRepository.Update(existingCategoryOfCare);
            await unitOfWork.SaveAsync();

            return CategoryOfCareStatus.Success;
        }

        /// <summary>
        /// Gets the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        public async Task<CategoryOfCare> GetCategoryOfCare(Guid categoryOfCareId)
        {
            return (await categoryOfCareRepository
                .FindAsync(c => c.Id == categoryOfCareId))
                .SingleOrDefault();
        }

        /// <summary>
        /// Gets the category of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<CategoryOfCare> GetCategoryOfCare(int customerId, string name)
        {
            return (await categoryOfCareRepository.FindAsync(c => c.CustomerId == customerId &&c.Name == name))
                   .SingleOrDefault();
        }

        /// <summary>
        /// Deletes the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        public async Task<CategoryOfCareStatus> DeleteCategoryOfCare(Guid categoryOfCareId)
        {
            var existingCategoryOfCare = await GetCategoryOfCare(categoryOfCareId);

            if (existingCategoryOfCare != null)
            {
                categoryOfCareRepository.Delete(existingCategoryOfCare);

                await unitOfWork.SaveAsync();

                return await Task.FromResult(CategoryOfCareStatus.Success);
            }

            return await Task.FromResult(CategoryOfCareStatus.CategoryOfCareWithSuchIdDoesNotExist);
        }

        /// <summary>
        /// Gets the categories of cares.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<CategoryOfCare>> GetCategoriesOfCare(int customerId, BaseSearchDto request)
        {
            Expression<Func<CategoryOfCare, bool>> expression = c => c.CustomerId == customerId;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(c => c.Name.Contains(term));
                    }
                }
            }

            return await categoryOfCareRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Name),
                    null,
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }
    }
}