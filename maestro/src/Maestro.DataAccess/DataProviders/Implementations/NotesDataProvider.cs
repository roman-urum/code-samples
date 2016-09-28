using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Common;
using RestSharp;
using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService.PatientNotes;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// NotesDataProvider.
    /// </summary>
    public class NotesDataProvider : INotesDataProvider
    {
        private readonly IRestApiClient apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesDataProvider" /> class.
        /// </summary>
        /// <param name="apiClientFactory">The API client factory.</param>
        public NotesDataProvider(IRestApiClientFactory apiClientFactory)
        {
            this.apiClient = apiClientFactory.Create(Settings.VitalsServiceUrl);
        }

        /// <summary>
        /// Creates note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="createNote">The note model.</param>
        /// <param name="token">The security token.</param>
        /// <returns></returns>
        public async Task<NoteDetailedResponseDto> CreateNote(
            int customerId,
            Guid patientId,
            BaseNoteDto createNote,
            string token)
        {
            string endpointUrl = string.Format("api/{0}/notes/{1}", customerId, patientId);

            var response =
                await
                apiClient.SendRequestAsync<NoteDetailedResponseDto>(endpointUrl, createNote, Method.POST, null, token);

            return response;
        }

        /// <summary>
        /// Get notables.
        /// </summary>
        /// <param name="customerId">The customer identifier</param>
        /// <param name="patientId">The patient identifier</param>
        /// <param name="token">The authorization token</param>
        /// <returns>The list of notables</returns>
        public async Task<List<string>> GetNotables(int customerId, Guid patientId, string token)
        {
            string endpointUrl = string.Format("api/{0}/notes/{1}/notables", customerId, patientId);

            return await apiClient.SendRequestAsync<List<string>>(endpointUrl, null, Method.GET, null, token);
        }

        /// <summary>
        /// Get notes.
        /// </summary>
        /// <param name="searchRequest">The search parameters</param>
        /// <param name="token">The authorization token</param>
        /// <returns>The list of notes</returns>
        public async Task<PagedResult<BaseNoteResponseDto>> GetNotes(SearchNotesDto searchRequest, string token)
        {
            string endpointUrl = string.Format("api/{0}/notes/{1}", searchRequest.CustomerId, searchRequest.PatientId);

            var queryStringBuilder = new StringBuilder();

            queryStringBuilder.AppendFormat("?skip={0}", searchRequest.Skip);
            queryStringBuilder.AppendFormat("&take={0}", searchRequest.Take);
            queryStringBuilder.AppendFormat("&isbrief={0}", searchRequest.IsBrief);

            if (!string.IsNullOrEmpty(searchRequest.Q))
            {
                queryStringBuilder.AppendFormat("&q={0}", searchRequest.Q);

            }

            if (searchRequest.Notables != null && searchRequest.Notables.Any())
            {
                var notablesquesryStringBuilder = new StringBuilder();
                searchRequest.Notables.ToList()
                    .ForEach(n => notablesquesryStringBuilder.AppendFormat("&notables={0}", n));

                queryStringBuilder.AppendFormat("{0}", notablesquesryStringBuilder);
            }

            endpointUrl += queryStringBuilder.ToString();

            var response =
                await
                apiClient.SendRequestAsync<PagedResult<BaseNoteResponseDto>>(endpointUrl, null, Method.GET, null, token);

            return response;
        }

        public async Task<PagedResult<SuggestedNotableDto>> GetSuggestedNotables(
            BaseSearchDto getNotablesRequest,
            int customerId,
            string token)
        {
            string endpointUrl = string.Format("api/{0}/notables/", customerId);

            var queryStringBuilder = new StringBuilder();

            if (getNotablesRequest != null)
            {
                queryStringBuilder.AppendFormat("?skip={0}", getNotablesRequest.Skip);
                queryStringBuilder.AppendFormat("&take={0}", getNotablesRequest.Take);

                if (!string.IsNullOrEmpty(getNotablesRequest.Q))
                {
                    queryStringBuilder.AppendFormat("&q={0}", getNotablesRequest.Q);

                }
            }

            endpointUrl += queryStringBuilder.ToString();

            var response =
                await
                apiClient.SendRequestAsync<PagedResult<SuggestedNotableDto>>(endpointUrl, null, Method.GET, null, token);

            return response;
        }
    }
}
