using AutoMapper;
using Maestro.Domain.Dtos.VitalsService.PatientNotes;
using Maestro.Web.Areas.Site.Models.Patients.Notes;

namespace Maestro.Web.Models.Mappings.Converters
{
    /// <summary>
    /// NoteConverter.
    /// </summary>
    public class NoteConverter : ITypeConverter<BaseNoteResponseDto, BaseNoteResponseViewModel>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        public BaseNoteResponseViewModel Convert(ResolutionContext context)
        {
            if (context.SourceValue is NoteDetailedResponseDto)
            {
                return Mapper.Map<NoteDetailedResponseViewModel>(context.SourceValue);
            }

            return Mapper.Map<NoteBriefResponseViewModel>(context.SourceValue);
        }
    }
}