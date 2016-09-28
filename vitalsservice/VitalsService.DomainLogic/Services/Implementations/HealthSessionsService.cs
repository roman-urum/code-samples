using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Domain.Enums.Ordering;
using VitalsService.DomainLogic.Extensions;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Provides business logic to manage health sessions.
    /// </summary>
    public class HealthSessionsService : IHealthSessionsService
    {
        private readonly IUserContext userContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<HealthSession> healthSessionRepository;
        private readonly IRepository<AssessmentMedia> assessmentMediaRepository;

        private static readonly List<Expression<Func<HealthSession, object>>> PropertiesToInclude = new List
            <Expression<Func<HealthSession, object>>>
        {
            s => s.Elements,
            s => s.Elements.Select(e => e.Values),
            s => s.Elements.Select(e => e.HealthSessionElementAlert),
            s => s.Elements.Select(e => e.Notes)
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthSessionsService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="userContext">The user context.</param>
        public HealthSessionsService(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.healthSessionRepository = unitOfWork.CreateRepository<HealthSession>();
            this.assessmentMediaRepository = unitOfWork.CreateRepository<AssessmentMedia>();
            this.userContext = userContext;
        }

        /// <summary>
        /// Creates new health session record.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<OperationResultDto<HealthSession, CreateHealthSessionStatus>> Create(HealthSession entity)
        {
            if (!await this.IsAssessmentValuesValid(entity))
            {
                return new OperationResultDto<HealthSession, CreateHealthSessionStatus>(
                    CreateHealthSessionStatus.AssessmentMediaIsNotValid);
            }

            entity.SubmittedUtc = DateTime.UtcNow;

            if (!await this.userContext.IsCIUser())
            {
                foreach (var healthSessionElement in entity.Elements)
                {
                    ResetInternalValues(healthSessionElement);
                }
            }

            healthSessionRepository.Insert(entity);
            await unitOfWork.SaveAsync();

            return new OperationResultDto<HealthSession, CreateHealthSessionStatus>(
                CreateHealthSessionStatus.Success, entity);
        }

        /// <summary>
        /// Returns list of all health sessions for selected patient.
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResult<HealthSession>> Find(
            int customerId,
            Guid patientId,
            HealthSessionsSearchDto searchDto,
            string clientId = null
        )
        {
            Expression<Func<HealthSession, bool>> expression = hs => hs.CustomerId == customerId && hs.PatientId == patientId;
            Func<IQueryable<HealthSession>, IOrderedQueryable<HealthSession>> orderByFunc = q => q.OrderBy(e => e.SubmittedUtc);

            if (searchDto != null)
            {
                if (!string.IsNullOrEmpty(searchDto.Q))
                {
                    var terms = searchDto.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(hs => hs.ProtocolName.Contains(term));
                    }
                }

                if (searchDto.StartedFromUtc.HasValue)
                {
                    expression = expression
                        .And(hs => hs.StartedUtc >= searchDto.StartedFromUtc.Value);
                }

                if (searchDto.StartedToUtc.HasValue)
                {
                    expression = expression
                        .And(hs => hs.StartedUtc <= searchDto.StartedToUtc.Value);
                }

                if (searchDto.CompletedFromUtc.HasValue)
                {
                    expression = expression
                        .And(hs => hs.CompletedUtc >= searchDto.CompletedFromUtc.Value);
                }

                if (searchDto.CompletedToUtc.HasValue)
                {
                    expression = expression
                        .And(hs => hs.CompletedUtc <= searchDto.CompletedToUtc.Value);
                }

                if (searchDto.SubmittedFromUtc.HasValue)
                {
                    expression = expression
                        .And(hs => hs.SubmittedUtc >= searchDto.SubmittedFromUtc.Value);
                }

                if (searchDto.SubmittedToUtc.HasValue)
                {
                    expression = expression
                        .And(hs => hs.SubmittedUtc <= searchDto.SubmittedToUtc.Value);
                }

                if (!searchDto.IncludePrivate)
                {
                    expression = expression.And(hs => !hs.IsPrivate);
                }

                if (searchDto.ElementType.HasValue)
                {
                    expression = expression.And(hs => hs.Elements.Any(el => el.Type == searchDto.ElementType));
                }

                if (searchDto.CalendarItemId.HasValue)
                {
                    expression = expression.And(hs => hs.CalendarItemId == searchDto.CalendarItemId.Value);
                }

                Expression<Func<HealthSession, object>> orderExpression;

                switch (searchDto.OrderBy)
                {
                    case HealthSessionOrderBy.ScheduledUtc:
                        {
                            orderExpression = e => new { ScheduledUtc = e.ScheduledUtc };

                            break;
                        }
                    case HealthSessionOrderBy.StartedUtc:
                        {
                            orderExpression = e => new { StartedUtc = e.StartedUtc };

                            break;
                        }
                    case HealthSessionOrderBy.CompletedUtc:
                        {
                            orderExpression = e => new { CompletedUtc = e.CompletedUtc };

                            break;
                        }
                    case HealthSessionOrderBy.SubmittedUtc:
                        {
                            orderExpression = e => new { SubmittedUtc = e.SubmittedUtc };

                            break;
                        }
                    default:
                        {
                            orderExpression = e => new { SubmittedUtc = e.SubmittedUtc };

                            break;
                        }
                }

                orderByFunc = searchDto.SortDirection == SortDirection.Ascending ?
                    (Func<IQueryable<HealthSession>, IOrderedQueryable<HealthSession>>)(q => q.OrderBy(orderExpression)) :
                    (q => q.OrderByDescending(orderExpression));
            }
            else
            {
                expression = expression.And(hs => !hs.IsPrivate);
            }

            if (!string.IsNullOrEmpty(clientId))
            {
                expression = expression.And(hs => hs.ClientId.ToLower() == clientId.ToLower());
            }

            return await healthSessionRepository
                .FindPagedAsync(
                    expression,
                    orderByFunc,
                    PropertiesToInclude,
                    searchDto != null ? searchDto.Skip : (int?)null,
                    searchDto != null ? searchDto.Take : (int?)null
                );
        }

        /// <summary>
        /// Returns required health session by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HealthSession> GetById(int customerId, Guid patientId, Guid id)
        {
            var result = await
                this.healthSessionRepository.FindAsync(
                    s => s.CustomerId == customerId && s.PatientId == patientId && s.Id == id,
                    null, PropertiesToInclude);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Updates the health session instance.
        /// </summary>
        /// <param name="healthSession">The health session instance.</param>
        /// <returns></returns>
        public async Task Update(HealthSession healthSession)
        {
            this.healthSessionRepository.Update(healthSession);
            await unitOfWork.SaveAsync();
        }

        #region Private methods

        /// <summary>
        /// Updates internal ids for new values.
        /// </summary>
        /// <param name="healthSessionElement"></param>
        /// <returns></returns>
        private void ResetInternalValues(HealthSessionElement healthSessionElement)
        {
            healthSessionElement.InternalId = null;

            foreach (var healthSessionElementValue in healthSessionElement.Values)
            {
                var value = healthSessionElementValue as IAnalyticsEntity;

                if (value != null)
                {
                    value.ResetInternalValues();
                }
            }
        }

        /// <summary>
        /// Validates that assessments ids exists in db.
        /// </summary>
        /// <param name="healthSession"></param>
        /// <returns></returns>
        private async Task<bool> IsAssessmentValuesValid(HealthSession healthSession)
        {
            var assessmentsIds = new List<Guid>();

            foreach (var element in healthSession.Elements.Where(e => e.Type == HealthSessionElementType.Assessment))
            {
                assessmentsIds.AddRange(
                    element.Values.OfType<AssessmentValue>().Select(assessmentValue => assessmentValue.AssessmentMediaId));
            }

            var assessments = await this.assessmentMediaRepository.FindAsync(
                a => a.CustomerId == healthSession.CustomerId && a.PatientId == healthSession.PatientId &&
                     assessmentsIds.Any(aid => aid == a.Id));

            return assessments.Count == assessmentsIds.Count;
        }

        #endregion
    }
}
