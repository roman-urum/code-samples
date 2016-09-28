using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// MeasurementViewModel.
    /// </summary>
    public class MeasurementViewModel : MeasurementBriefViewModel
    {
        /// <summary>
        /// Contains id of assigned health session.
        /// </summary>
        public Guid? HealthSessionId { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public IList<MeasurementNoteDto> Notes { get; set; }

        /// <summary>
        /// Gets or sets the raw json.
        /// </summary>
        /// <value>
        /// The raw json.
        /// </value>
        public object RawJson { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public MeasurementDeviceDto Device { get; set; }

        /// <summary>
        /// Gets or sets the type of the processing.
        /// </summary>
        /// <value>
        /// The type of the processing.
        /// </value>
        public ProcessingType ProcessingType { get; set; }
    }
}