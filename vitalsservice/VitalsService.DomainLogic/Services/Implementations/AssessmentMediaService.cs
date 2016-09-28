using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VitalsService.ContentStorage.Azure.Services.Interfaces;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business logic for assessment media entities.
    /// </summary>
    public class AssessmentMediaService : IAssessmentMediaService
    {
        private readonly IContentStorage contentStorage;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<AssessmentMedia> assessmentMediaRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="contentStorage"></param>
        public AssessmentMediaService(IUnitOfWork unitOfWork, IContentStorage contentStorage)
        {
            this.unitOfWork = unitOfWork;
            this.assessmentMediaRepository = unitOfWork.CreateRepository<AssessmentMedia>();
            this.contentStorage = contentStorage;
        }

        /// <summary>
        /// Creates new instance of assessment media in db.
        /// </summary>
        /// <param name="assessmentMedia"></param>
        /// <returns></returns>
        public async Task<AssessmentMedia> Create(AssessmentMedia assessmentMedia)
        {
            this.assessmentMediaRepository.Insert(assessmentMedia);
            await this.unitOfWork.SaveAsync();

            return assessmentMedia;
        }

        /// <summary>
        /// Updates storage key for assessment media if storage key is not set yet.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<OperationResultDto<AssessmentMedia, UpdateAssessmentMediaStatus>> Update(int customerId,
            Guid patientId, Guid id, byte[] file)
        {
            var existingMedia =
                await this.GetById(customerId, patientId, id);

            if (existingMedia == null)
            {
                return new OperationResultDto<AssessmentMedia, UpdateAssessmentMediaStatus>(
                    UpdateAssessmentMediaStatus.NotFound);
            }

            if (!string.IsNullOrEmpty(existingMedia.StorageKey))
            {
                return new OperationResultDto<AssessmentMedia, UpdateAssessmentMediaStatus>(
                    UpdateAssessmentMediaStatus.FileAlreadyUploaded);
            }

            if (existingMedia.ContentLength != file.Length)
            {
                return new OperationResultDto<AssessmentMedia, UpdateAssessmentMediaStatus>(
                    UpdateAssessmentMediaStatus.InvalidContentProvided);
            }

            existingMedia.StorageKey = await this.contentStorage.UploadContent(file, existingMedia.ContentType);

            await this.unitOfWork.SaveAsync();

            return new OperationResultDto<AssessmentMedia, UpdateAssessmentMediaStatus>(
                UpdateAssessmentMediaStatus.Success, existingMedia);
        }

        /// <summary>
        /// Returns required assessment media by identifier.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AssessmentMedia> GetById(int customerId, Guid patientId, Guid id)
        {
            var result = await this.assessmentMediaRepository.FindAsync(
                m => m.Id == id && m.CustomerId == customerId && m.PatientId == patientId);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns all records matches to specified criteria.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        public async Task<PagedResult<AssessmentMedia>> GetAll(int customerId, Guid patientId,
            AssessmentMediaSearchDto searchDto)
        {
            Expression<Func<AssessmentMedia, bool>> expression =
                m => m.CustomerId == customerId && m.PatientId == patientId;

            if (searchDto != null)
            {
                if (!string.IsNullOrEmpty(searchDto.Q))
                {
                    var terms = searchDto.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(m => m.OriginalFileName.Contains(term));
                    }
                }

                if (searchDto.CreatedAfter.HasValue)
                {
                    expression = expression.And(m => m.CreatedUtc > searchDto.CreatedAfter.Value);
                }

                if (searchDto.CreatedBefore.HasValue)
                {
                    expression = expression.And(m => m.CreatedUtc < searchDto.CreatedBefore.Value);
                }
            }

            return await this.assessmentMediaRepository.FindPagedAsync(
                expression,
                m => m.OrderByDescending(e => e.CreatedUtc),
                null,
                searchDto != null ? searchDto.Skip : (int?) null,
                searchDto != null ? searchDto.Take : (int?) null
                );
        }

    }
}
