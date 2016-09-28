using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Extensions;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// AlertSeveritiesService.
    /// </summary>
    public class AlertSeveritiesService : IAlertSeveritiesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<AlertSeverity> alertSeverityRepository;
        private readonly IRepository<Threshold> thresholdRepository;
        private readonly IRepository<Alert> alertRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertSeveritiesService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public AlertSeveritiesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.alertSeverityRepository = unitOfWork.CreateRepository<AlertSeverity>();
            this.thresholdRepository = unitOfWork.CreateRepository<Threshold>();
            this.alertRepository = unitOfWork.CreateRepository<Alert>();
        }

        /// <summary>
        /// Creates the alert severity.
        /// </summary>
        /// <param name="alertSeverity">The alert severity.</param>
        /// <returns></returns>
        public async Task<AlertSeverity> CreateAlertSeverity(AlertSeverity alertSeverity)
        {
            this.alertSeverityRepository.Insert(alertSeverity);

            await UpdateThresholdsWithoutSeverityWithNewOne(alertSeverity.CustomerId, alertSeverity);
            await UpdateAlertsWithoutSeverityWithNewOne(alertSeverity.CustomerId, alertSeverity);

            await unitOfWork.SaveAsync();

            return alertSeverity;
        }

        /// <summary>
        /// Updates the thresholds without severity with new one.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="newSeverity">The new severity.</param>
        /// <returns></returns>
        private async Task UpdateThresholdsWithoutSeverityWithNewOne(int customerId, AlertSeverity newSeverity)
        {
            var thresholdsWithNullSeverity = await thresholdRepository
                .FindAsync(
                    t => t.CustomerId == customerId &&
                    !t.AlertSeverityId.HasValue
                );

            foreach (var threshold in thresholdsWithNullSeverity)
            {
                threshold.AlertSeverity = newSeverity;
                thresholdRepository.Update(threshold);
            }
        }

        /// <summary>
        /// Updates the alerts without severity with new one.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="newSeverity">The new severity.</param>
        /// <returns></returns>
        private async Task UpdateAlertsWithoutSeverityWithNewOne(int customerId, AlertSeverity newSeverity)
        {
            var alertsWithNullSeverity = await alertRepository
                .FindAsync(
                    a => a.CustomerId == customerId &&
                    !a.AlertSeverityId.HasValue
                );

            foreach (var alert in alertsWithNullSeverity)
            {
                alert.AlertSeverity = newSeverity;
                alertRepository.Update(alert);
            }
        }

        /// <summary>
        /// Updates the alert severity.
        /// </summary>
        /// <param name="alertSeverity">The alert severity.</param>
        /// <returns></returns>
        public async Task<CreateUpdateDeleteAlertSeverityStatus> UpdateAlertSeverity(AlertSeverity alertSeverity)
        {
            var existedAlertSeverity = (await alertSeverityRepository
                .FindAsync(
                    aseverity => aseverity.Id == alertSeverity.Id &&
                    aseverity.CustomerId == alertSeverity.CustomerId
                )).FirstOrDefault();

            if (existedAlertSeverity == null)
            {
                return CreateUpdateDeleteAlertSeverityStatus.NotFound;
            }

            existedAlertSeverity.Name = alertSeverity.Name;
            existedAlertSeverity.ColorCode = alertSeverity.ColorCode;
            existedAlertSeverity.Severity = alertSeverity.Severity;

            await unitOfWork.SaveAsync();

            return CreateUpdateDeleteAlertSeverityStatus.Success;
        }

        public async Task<OperationResultDto<AlertSeverity, GetAlertSeverityStatus>> GetAlertSeverity(int customerId, Guid id)
        {
            var alertSeverity = (await alertSeverityRepository
                .FindAsync(
                    aseverity => aseverity.Id == id && 
                    aseverity.CustomerId == customerId
                )).FirstOrDefault();

            if (alertSeverity == null)
            {
                return new OperationResultDto<AlertSeverity, GetAlertSeverityStatus>()
                {
                    Status = GetAlertSeverityStatus.NotFound,
                    Content = null
                };
            }

            return new OperationResultDto<AlertSeverity, GetAlertSeverityStatus>()
            {
                Status = GetAlertSeverityStatus.Success,
                Content = alertSeverity
            };
        }

        /// <summary>
        /// Gets the alert severities.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<PagedResult<AlertSeverity>> GetAlertSeverities(int customerId, AlertSeveritiesSearchDto searchRequest)
        {
            Expression<Func<AlertSeverity, bool>> expression = s => s.CustomerId == customerId;

            if (searchRequest != null)
            {
                if (!string.IsNullOrEmpty(searchRequest.Q))
                {
                    var terms = searchRequest.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(a => a.Name.Contains(term));
                    }
                }

                if (searchRequest.Severity.HasValue)
                {
                    expression = expression.And(s => s.Severity == searchRequest.Severity);
                }
            }

            return await alertSeverityRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(s => s.Id),
                    null,
                    searchRequest != null ? searchRequest.Skip : (int?)null,
                    searchRequest != null ? searchRequest.Take : (int?)null
                );
        }

        /// <summary>
        /// Deletes the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="alertSeverityId">The identifier.</param>
        /// <returns></returns>
        public async Task<CreateUpdateDeleteAlertSeverityStatus> DeleteAlertSeverity(int customerId, Guid alertSeverityId)
        {
            var alertSeverity = (await alertSeverityRepository
                .FindAsync(
                    aseverity => aseverity.CustomerId == customerId &&
                    aseverity.Id == alertSeverityId
                )).FirstOrDefault();

            if (alertSeverity == null)
            {
                return CreateUpdateDeleteAlertSeverityStatus.NotFound;
            }

            await UpdateAlertsWithNewHighestSeverity(customerId, alertSeverity);

            if (alertSeverity.Thresholds != null && alertSeverity.Thresholds.Any())
            {
                thresholdRepository.DeleteRange(alertSeverity.Thresholds);
            }
            
            alertSeverityRepository.Delete(alertSeverity);

            await unitOfWork.SaveAsync();

            return CreateUpdateDeleteAlertSeverityStatus.Success;
        }

        /// <summary>
        /// Updates the alerts with new highest severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="oldAlertSeverity">The old alert severity.</param>
        /// <returns></returns>
        private async Task UpdateAlertsWithNewHighestSeverity(int customerId, AlertSeverity oldAlertSeverity)
        {
            var newhighestSeverity = await alertSeverityRepository
                .FirstOrDefaultAsync(
                    e => e.CustomerId == customerId &&
                        e.Id != oldAlertSeverity.Id,
                    o => o.OrderByDescending(s => s.Severity)
                );

            var alertsToBeUpdated = oldAlertSeverity.Alerts.ToList();

            if (newhighestSeverity != null)
            {
                newhighestSeverity.Alerts.AddRange(alertsToBeUpdated);
            }

            oldAlertSeverity.Alerts.RemoveRange(alertsToBeUpdated);
        }
    }
}