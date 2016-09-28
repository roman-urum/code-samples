using System;
using System.Collections.Generic;
using System.Linq;
using HealthLibrary.Domain.Entities.Program;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// Model for event in response with program schedule.
    /// </summary>
    public class ProgramScheduleEventDto
    {
        /// <summary>
        /// Default constructor to build event dto.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="dayElements">The day elements.</param>
        /// <param name="dateIndex">Index of the date.</param>
        /// <param name="dayIndex">Index of the day.</param>
        public ProgramScheduleEventDto(
            ProgramScheduleRequestDto model,
            IList<ProgramElement> dayElements,
            DateTime dateIndex,
            int dayIndex
        )
        {
            DueUtc = dateIndex;
            ExpireMinutes = model.ExpireMinutes;
            Protocols = dayElements
                .Select(e => 
                    new ProgramScheduleElementDto
                    {
                        ProtocolId = e.ProtocolId,
                        Order = GetOrderForDay(e, dayIndex)
                    }
                ).ToList();
        }

        /// <summary>
        /// Date and time of the session
        /// </summary>
        public DateTime DueUtc { get; set; }

        /// <summary>
        /// Gets or sets the event timezone.
        /// </summary>
        /// <value>
        /// The event timezone.
        /// </value>
        public string EventTz { get; set; }

        /// <summary>
        /// The number of minutes after which the calendar event should expire
        /// </summary>
        public int? ExpireMinutes { get; set; }

        /// <summary>
        /// Gets or sets the program day.
        /// </summary>
        /// <value>
        /// The program day.
        /// </value>
        public int ProgramDay { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// List of protocols for this event.
        /// </summary>
        public IList<ProgramScheduleElementDto> Protocols { get; set; }

        /// <summary>
        /// Returns order for program element in specified day.
        /// </summary>
        /// <param name="programElement"></param>
        /// <param name="dayNumber"></param>
        /// <returns></returns>
        private static int GetOrderForDay(ProgramElement programElement, int dayNumber)
        {
            var day = programElement.ProgramDayElements.First(d => d.Day == dayNumber);

            return day.Sort ?? programElement.Sort;
        }
    }
}