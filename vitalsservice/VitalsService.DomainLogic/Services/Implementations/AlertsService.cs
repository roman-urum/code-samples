using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using EntityFramework.Extensions;

using Microsoft.Practices.ServiceLocation;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.DomainObjects;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// AlertsService.
    /// </summary>
    public class AlertsService : IAlertsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Alert> alertRepository;
        private readonly IRepository<HealthSession> healthSessionRepository;
        private readonly IRepository<AlertSeverity> alertSeverityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public AlertsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.alertRepository = unitOfWork.CreateRepository<Alert>();
            this.healthSessionRepository = unitOfWork.CreateRepository<HealthSession>();
            this.alertSeverityRepository = unitOfWork.CreateRepository<AlertSeverity>();
        }

        /// <summary>
        /// Creates the alert.
        /// </summary>
        /// <param name="alert">The alert.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateUpdateAlertStatus>> CreateAlert(Alert alert)
        {
            if (alert.AlertSeverityId.HasValue)
            {
                var existingAlertSeverity = 
                    (await alertSeverityRepository.FindAsync(
                            s => s.CustomerId == alert.CustomerId && s.Id == alert.AlertSeverityId
                        )
                    )
                    .FirstOrDefault();

                if (existingAlertSeverity == null)
                {
                    return await Task.FromResult(
                        new OperationResultDto<Guid, CreateUpdateAlertStatus>()
                        {
                            Status = CreateUpdateAlertStatus.AlertSeverityWithSuchIdDoesNotExist
                        }
                    );
                }
            }
            else
            {
                var highestAlertSeverity = await alertSeverityRepository
                    .FirstOrDefaultAsync(
                        s => s.CustomerId == alert.CustomerId,
                        s => s.OrderByDescending(e => e.Severity)
                    );

                if (highestAlertSeverity != null)
                {
                    alert.AlertSeverity = highestAlertSeverity;
                }
            }

            alertRepository.Insert(alert);
            await unitOfWork.SaveAsync();

            return await Task.FromResult(
                new OperationResultDto<Guid, CreateUpdateAlertStatus>()
                {
                    Status = CreateUpdateAlertStatus.Success,
                    Content = alert.Id
                }
            );
        }

        /// <summary>
        /// Acknowledges the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="acknowledgedBy">The acknowledged by.</param>
        /// <param name="alertsIds">The alerts ids.</param>
        /// <returns></returns>
        public async Task<CreateUpdateAlertStatus> AcknowledgeAlerts(int customerId, Guid acknowledgedBy, IList<Guid> alertsIds)
        {
            var existingAlerts = await alertRepository.FindAsync(
                a => a.CustomerId == customerId && alertsIds.Contains(a.Id)
            );

            if (existingAlerts.Count != alertsIds.Count || existingAlerts.Any(a => a.Acknowledged))
            {
                return CreateUpdateAlertStatus.OneOfProvidedAlertsDoesNotExistOrAlreadyAcknowledged;
            }

            foreach (var existingAlert in existingAlerts)
            {
                existingAlert.Acknowledged = true;
                existingAlert.AcknowledgedBy = acknowledgedBy;
                existingAlert.AcknowledgedUtc = DateTime.UtcNow;
                alertRepository.Update(existingAlert);
            }

            await unitOfWork.SaveAsync();

            return CreateUpdateAlertStatus.Success;
        }

        /// <summary>
        /// Gets the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public PagedResult<PatientAlerts> GetAlerts(int customerId, AlertsSearchDto request)
        {
            Expression<Func<Alert, bool>> expression = a => a.CustomerId == customerId;

            if (request != null)
            {
                expression = expression.And(a => a.Acknowledged == request.Acknowledged);

                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(a => a.Title.Contains(term));
                    }
                }

                if (request.PatientIds != null && request.PatientIds.Any())
                {
                    expression = expression.And(a => request.PatientIds.Contains(a.PatientId));
                }

                if (request.Types != null && request.Types.Any())
                {
                    expression = expression.And(a => a.Type.HasValue && request.Types.Contains(a.Type.Value));
                }

                if (request.AcknowledgedFrom.HasValue)
                {
                    expression = expression.And(a => a.AcknowledgedUtc >= request.AcknowledgedFrom.Value);
                }

                if (request.AcknowledgedTo.HasValue)
                {
                    expression = expression.And(a => a.AcknowledgedUtc <= request.AcknowledgedTo.Value);
                }

                if (request.SeverityIds != null && request.SeverityIds.Any())
                {
                    expression = expression.And(a => a.AlertSeverityId.HasValue && request.SeverityIds.Contains(a.AlertSeverityId.Value));
                }
            }
            else
            {
                expression = expression.And(a => !a.Acknowledged);
            }

            var dbContext = ServiceLocator.Current.GetInstance<DbContext>();

            var alertsGroupedByPatientQuery = dbContext
                .Set<Alert>()
                .Where(expression)
                .GroupBy(a => a.PatientId)
                .Select(g => new PatientAlerts() {PatientId = g.Key, Alerts = g.ToList()})
                .OrderBy(e => e.PatientId)
                .AsQueryable();

            var alertsGroupedByPatient = alertsGroupedByPatientQuery.Future();

            //This was added to avoid getting measurements, thresholds, vitals and severities from db one by one (by id) 
            //which used to be happening during mapping.
            //This is intended to optimize getting alerts.
            var severities = alertsGroupedByPatientQuery.SelectMany(a => a.Alerts.Select(va => va.AlertSeverity)).Future();

            if (request != null && !request.IsBrief)
            {
                var measurements = alertsGroupedByPatientQuery
                    .SelectMany(a => a.Alerts.OfType<VitalAlert>().Select(va => va.Vital.Measurement))
                    .Include(m => m.Vitals)
                    .Future();
                var thresholds =
                    alertsGroupedByPatientQuery.SelectMany(a => a.Alerts.OfType<VitalAlert>().Select(va => va.Threshold))
                        .Future();
                var vitals =
                    alertsGroupedByPatientQuery.SelectMany(a => a.Alerts.OfType<VitalAlert>().Select(va => va.Vital))
                        .Future();
                var healthSessionElements =
                    alertsGroupedByPatientQuery.SelectMany(
                        pa => pa.Alerts.OfType<HealthSessionElementAlert>().Select(a => a.HealthSessionElement))
                        .Include(el => el.Values)
                        .Include(el => el.Notes)
                        .Include(el => el.HealthSession)
                        .Include(el => el.HealthSessionElementAlert)
                        .Future();
                var healthSessionElementsValues =
                    alertsGroupedByPatientQuery.SelectMany(
                        pa =>
                            pa.Alerts.OfType<HealthSessionElementAlert>().SelectMany(a => a.HealthSessionElement.Values))
                        .Include(val => val.HealthSessionElement)
                        .Future();
            }

            var result = new PagedResult<PatientAlerts>()
            {
                Total = alertsGroupedByPatient.LongCount()
            };

            if (request != null)
            {
                result.Results = alertsGroupedByPatient.Skip(request.Skip).Take(request.Take).ToList();
            }
            else
            {
                result.Results = alertsGroupedByPatient.ToList();
            }

            return result;
        }

        /// <summary>
        /// Create alerts assign them to to measurement.Vitals in case of vital violation
        /// </summary>
        /// <param name="measurement">The measurement</param>
        /// <param name="aggregatedThresholds">The aggregated thresholds</param>
        /// <returns></returns>
        public void CreateViolationAlerts(Measurement measurement, IList<Threshold> aggregatedThresholds)
        {
            if (aggregatedThresholds.Any())
            {
                var vitals = measurement.Vitals;

                foreach (var vital in vitals)
                {
                    var maxViolatedThresholds =
                        aggregatedThresholds.Where(t => t.Name.ToString() == vital.Name && vital.Value > t.MaxValue).ToList();

                    var minViolatedThresholds =
                        aggregatedThresholds.Where(t => t.Name.ToString() == vital.Name && vital.Value < t.MinValue).ToList();

                    if (maxViolatedThresholds.All(t => t.AlertSeverity != null))
                    {
                        maxViolatedThresholds = maxViolatedThresholds.OrderByDescending(t => t.AlertSeverity.Severity).ToList();
                    }

                    if (minViolatedThresholds.All(t => t.AlertSeverity != null))
                    {
                        minViolatedThresholds = minViolatedThresholds.OrderByDescending(t => t.AlertSeverity.Severity).ToList();
                    }

                    //we take FirstOrDefault because maxViolatedThresholds is descending ordered by AlertSeverityId (if there are severities), so the violated threshold with max severity will be on firt place.
                    var maxViolatedThreshold = maxViolatedThresholds.FirstOrDefault();
                    //the same as maxViolatedThreshold
                    var minViolatedThreshold = minViolatedThresholds.FirstOrDefault();

                    if (maxViolatedThreshold == null && minViolatedThreshold == null)
                    {
                        continue;
                    }

                    var violatedThresholdValue =
                        minViolatedThreshold != null ?
                        minViolatedThreshold.MinValue :
                        maxViolatedThreshold.MaxValue;

                    var violatedThreshold = minViolatedThreshold ?? maxViolatedThreshold;

                    var alertTitle = string.Format(
                        "{0}: {1} {2} violated threshold of {3} {4} on {5}",
                        vital.Name,
                        vital.Value,
                        vital.Unit,
                        violatedThresholdValue,
                        violatedThreshold.Unit,
                        measurement.ObservedUtc
                    );

                    vital.VitalAlert = new VitalAlert
                    {
                        Id = SequentialGuidGenerator.Generate(),
                        Threshold = violatedThreshold,
                        CustomerId = measurement.CustomerId,
                        PatientId = measurement.PatientId,
                        Type = AlertType.VitalsViolation,
                        Title = alertTitle,
                        Body = null,
                        OccurredUtc = measurement.ObservedUtc,
                        ExpiresUtc = null,
                        Weight = 0,
                        AlertSeverity = violatedThreshold.AlertSeverity
                    };
                }
            }
        }
    }
}