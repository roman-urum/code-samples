using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Maestro.Domain.Dtos.PatientsService;

namespace Maestro.Web.Areas.Site.Models.Patients.SearchPatients
{
    public class SuggestionSearchPatientResultViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<PatientIdentifierDto> Identifiers { get; set; }
    }
}