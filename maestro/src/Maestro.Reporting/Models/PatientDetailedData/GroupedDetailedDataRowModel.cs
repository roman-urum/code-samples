using System.Collections.Generic;

namespace Maestro.Reporting.Models.PatientDetailedData
{
    /// <summary>
    /// GroupedDetailedDataRowModel.
    /// </summary>
    public class GroupedDetailedDataRowModel : DetailedDataRowModel
    {
        /// <summary>
        /// Gets or sets the group elements.
        /// </summary>
        /// <value>
        /// The group elements.
        /// </value>
        public IList<DetailedDataRowModel> GroupElements { get; set; }
    }
}