﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using Maestro.Web;
    using Maestro.Web.Areas.Site;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/PatientEditTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates_PatientEditTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_PatientEditTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"patientEditTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">
	<div class=""page-header page-header-ci page-header-primary-ci"">
		<h1>
			<%=firstName%>
			<%=lastName%>
			<% if( idMain ){ %><small>(<%=idMain.value%>)</small><% } %>
		</h1>
	</div>
	<div class=""page-content-ci"">
		<ul class=""nav nav-tabs"">
			<li><a href=""#patient-info"" id=""PatientInfo"" data-href=""EditPatient/<%=id%>/PatientInfo/"" class=""js-edit-patient-tab"">Patient Info</a></li>
			<li><a href=""#conditions"" id=""Conditions"" data-href=""EditPatient/<%=id%>/Conditions/"" class=""js-edit-patient-tab"">Conditions</a></li>
			<li><a href=""#care-managers"" id=""CareManagers"" data-href=""EditPatient/<%=id%>/CareManagers/"" class=""js-edit-patient-tab"">Care Managers</a></li>
			<li><a href=""#measurements"" id=""Measurements"" data-href=""EditPatient/<%=id%>/Measurements/"" class=""js-edit-patient-tab"">Measurements</a></li>
			<li><a href=""#devices"" id=""Devices"" data-href=""EditPatient/<%=id%>/Devices/"" class=""js-edit-patient-tab"">Devices</a></li>
		</ul>

		<div class=""tab-content"">
			<div id=""patient-info"" class=""tab-pane"">

");

WriteLiteral("\t\t        ");

            
            #line 21 "..\..\Areas\Site\Views\Patients\Templates\PatientEditTemplate.cshtml"
           Write(Html.Partial("Templates/_PatientEditInfoTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\t\t    </div>\r\n\t\t\t<div id=\"conditions\" class=\"tab-pane\">\r\n\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 26 "..\..\Areas\Site\Views\Patients\Templates\PatientEditTemplate.cshtml"
           Write(Html.Partial("Templates/_PatientEditConditionsTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\t\t\t</div>\r\n\t\t\t<div id=\"care-managers\" class=\"tab-pane\">\r\n\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 31 "..\..\Areas\Site\Views\Patients\Templates\PatientEditTemplate.cshtml"
           Write(Html.Partial("Templates/_PatientEditCareManagersTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\t\t\t</div>\r\n\t\t\t<div id=\"measurements\" class=\"tab-pane\">\r\n\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 36 "..\..\Areas\Site\Views\Patients\Templates\PatientEditTemplate.cshtml"
           Write(Html.Partial("Templates/_PatientEditMeasurementsTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t\t\r\n\t\t\t</div>\r\n\t\t\t<div id=\"devices\" class=\"tab-pane\">\r\n\t\t\t\t\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 41 "..\..\Areas\Site\Views\Patients\Templates\PatientEditTemplate.cshtml"
           Write(Html.Partial("Templates/_PatientEditDevicesTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t\t\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n</script>");

        }
    }
}
#pragma warning restore 1591