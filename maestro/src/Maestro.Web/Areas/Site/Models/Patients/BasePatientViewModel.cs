using System;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Enums;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// BasePatientViewModel.
    /// </summary>
    public abstract class BasePatientViewModel
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
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public Guid SiteId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle initial.
        /// </summary>
        /// <value>
        /// The middle initial.
        /// </value>
        public string MiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public string BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public Gender Gender { get; set; }
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the address3.
        /// </summary>
        /// <value>
        /// The address3.
        /// </value>
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number work.
        /// </summary>
        /// <value>
        /// The phone number work.
        /// </value>
        public string PhoneWork { get; set; }

        /// <summary>
        /// Gets or sets the phone number home.
        /// </summary>
        /// <value>
        /// The phone number home.
        /// </value>
        public string PhoneHome { get; set; }

        /// <summary>
        /// Gets or sets the patient email.
        /// </summary>
        /// <value>
        /// The patient email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public PatientStatus? Status { get; set; }

        /// <summary>
        /// Indicates when new status becomes effective.
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the preferred session time.
        /// </summary>
        /// <value>
        /// The preferred session time.
        /// </value>
        public TimeSpan PreferredSessionTime { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the list of categories of care
        /// </summary>
        /// <value>
        /// The list of categories of care
        /// </value>
        public List<Guid> CategoriesOfCare { get; set; }

        /// <summary>
        /// Gets or sets the identifiers.
        /// </summary>
        /// <value>
        /// The identifiers.
        /// </value>
        public List<PatientIdentifierDto> Identifiers { get; set; }
    }
}