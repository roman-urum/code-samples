using System;
using AutoMapper;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;
using Maestro.Domain.Dtos.PatientsService.Enums;
using Maestro.Web.Areas.Site.Models.Patients.Calendar;

namespace Maestro.Web.Models.Mappings.Converters
{
    /// <summary>
    /// Converts responses' dtos to view models with appropriate type.
    /// </summary>
    public class CalendarChangesConverter : ITypeConverter<CalendarChangeResponseDto, CalendarChangeViewModel>
    {
        public CalendarChangeViewModel Convert(ResolutionContext context)
        {
            CalendarChangeResponseDto responseDto = context.SourceValue as CalendarChangeResponseDto;

            if (responseDto == null)
            {
                throw new ArgumentException("context.SourceValue");
            }

            switch (responseDto.ElementType)
            {
                case CalendarElementType.Event:
                    return Mapper.Map<CalendarItemChangeViewModel>(responseDto as CalendarItemChangeResponseDto);

                case CalendarElementType.Program:
                    return Mapper.Map<CalendarProgramChangeViewModel>(responseDto as CalendarProgramChangeResponseDto);

                case CalendarElementType.DefaultSession:
                    return Mapper.Map<DefaultSessionChangeViewModel>(responseDto as DefaultSessionChangeResponseDto);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}