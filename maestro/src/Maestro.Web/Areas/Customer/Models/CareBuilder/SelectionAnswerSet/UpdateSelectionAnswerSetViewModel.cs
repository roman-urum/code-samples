using System;
using System.Collections.Generic;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerChoices;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet
{
    /// <summary>
    /// UpdateSelectionAnswerSetDto.
    /// </summary>
    public class UpdateSelectionAnswerSetViewModel : BaseSelectionAnswerSetViewModel
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
        [NotEmptyList(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "NotEmptyList_ValidationError", ErrorMessage = null)]
        public IEnumerable<UpdateSelectionAnswerChoiceViewModel> SelectionAnswerChoices { get; set; }
    }
}