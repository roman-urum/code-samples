using System.Collections.Generic;
using Maestro.Domain.Dtos;

namespace Maestro.Web.Areas.Site.Models.Dashboard
{
    /// <summary>
    /// GetPatientsCardsResultViewModel class.
    /// </summary>
    public class GetPatientsCardsResultViewModel
    {
        /// <summary>
        /// Gets or sets the cards counts.
        /// </summary>
        public IList<AlertCountViewModel> Counts { get; set; }

        /// <summary>
        /// Gets or sets the patient cards.
        /// </summary>
        public PagedResult<BriefPatientCardViewModel> PatientCards { get; set; }

        /// <summary>
        /// Initializes default values.
        /// </summary>
        public GetPatientsCardsResultViewModel()
        {
            Counts = new List<AlertCountViewModel>();
            PatientCards = new PagedResult<BriefPatientCardViewModel>
            {
                Total = 0,
                Results = new List<BriefPatientCardViewModel>()
            };
        }
    }
}