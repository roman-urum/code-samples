using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceService.Web.Api.Models.Dtos.Entities
{
    using System.ComponentModel.DataAnnotations;

    using DeviceService.Web.Api.Resources;

    public class CreateDeviceRequestDto
    {
        [Required(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "DeviceDto_PatientIdRequiredError")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "DeviceDto_CustomerIdRequiredError")]
        public int CustomerId { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "DeviceDto_DateOfBirthRequiredError")]
        public DateTime BirthDate { get; set; }

        public string DeviceModel { get; set; }
    }
}