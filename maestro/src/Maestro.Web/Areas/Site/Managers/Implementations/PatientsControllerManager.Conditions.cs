using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Domain.Dtos.VitalsService.PatientConditions;
using Maestro.Web.Areas.Site.Managers.Interfaces;
using Maestro.Web.Areas.Site.Models.Patients.PatientsConditions;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
	public partial class PatientsControllerManager
	{
	    public Task<IList<ConditionResponseDto>> GetPatientConditions(Guid patientId)
	    {
	        var token = authDataStorage.GetToken();
	        return vitalsService.GetPatientConditions(CustomerContext.Current.Customer.Id, patientId, token);
	    }

	    /// <summary>
	    /// Creates the patient conditions.
	    /// </summary>
	    /// <param name="request">The request.</param>
	    /// <returns></returns>
	    public Task CreatePatientConditions(PatiensConditionsRequestViewModel request)	        
	    {
            var token = authDataStorage.GetToken();

	        return vitalsService.CreatePatientConditions(
	            CustomerContext.Current.Customer.Id,
	            request.PatientId,
	            token,
	            new PatientConditionsRequestDto() { PatientConditionsIds = request.PatientConditionsIds });
	    }
    }
}