using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.PatientNotes;
using Maestro.Web.Areas.Site.Models.Patients.Notes;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.Devices
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Creates note.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<BaseNoteResponseViewModel> CreateNote(CreateNoteViewModel request)
        {
            var createNote = Mapper.Map<CreateNoteRequestDto>(request);
            var authData = authDataStorage.GetUserAuthData();

            createNote.CreatedBy = authData.FullName;

            var createNoteResult = await notesService.CreateNote(
                CustomerContext.Current.Customer.Id,
                request.PatientId,
                createNote,
                authDataStorage.GetToken()
            );

            return Mapper.Map<NoteDetailedResponseViewModel>(createNoteResult);
        }

        public async Task<IList<string>> GetSuggestedNotables()
        {
            var getNotablesRequest = new BaseSearchDto() { Skip = 0, Take = int.MaxValue };

            var getSuggestedNotablesResponse = await notesService.GetSuggestedNotables(getNotablesRequest, CustomerContext.Current.Customer.Id, authDataStorage.GetToken());

            return getSuggestedNotablesResponse.Results.Select(s => s.Name).ToList();
        }


        public async Task<PagedResult<BaseNoteResponseViewModel>> GetNotes(SearchNotesViewModel searchModel)
        {
            var searchNotesRequest = Mapper.Map<SearchNotesDto>(searchModel);
            searchNotesRequest.CustomerId = CustomerContext.Current.Customer.Id;
            searchNotesRequest.IsBrief = false;

            var getNotesResult = await notesService.GetNotes(searchNotesRequest, authDataStorage.GetToken());

            
            return new PagedResult<BaseNoteResponseViewModel>()
            {
                Results = Mapper.Map<IList<BaseNoteResponseViewModel>>(getNotesResult.Results),
                Total = getNotesResult.Total
            };
        }

        public async Task<List<string>> GetNotables( Guid patientId)
        {
            var notables = await notesService.GetNotables(CustomerContext.Current.Customer.Id, patientId, authDataStorage.GetToken());

            return notables;
        }
    }
}