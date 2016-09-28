using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Maestro.Domain.Dtos.PatientsService;

namespace Maestro.Web.Areas.Site.Models.Patients.CareManagers
{
    /// <summary>
    /// SearchCareManagersViewModel.
    /// </summary>
    public class SearchCareManagersViewModel
    {
        /// <summary>
        /// Gets or sets the indicating whether [return only care managers with assigned patients].
        /// </summary>
        /// <value><c>true</c>if [return only care managers with assigned patients]; otherwise, false.</value>
        public bool? OnlyCareManagersWithAssignedPatients { get; set; }
    }
}