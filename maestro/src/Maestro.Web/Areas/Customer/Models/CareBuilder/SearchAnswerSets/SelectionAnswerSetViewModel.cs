using System;
using System.Collections.Generic;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerChoices;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SearchAnswerSets
{
    /// <summary>
    /// Model for data from API responses with selection answer sets.
    /// </summary>
    public class SelectionAnswerSetViewModel : BaseSelectionAnswerSetViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        public IEnumerable<SelectionAnswerChoiceViewModel> SelectionAnswerChoices { get; set; }
    }
}