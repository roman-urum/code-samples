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
    
    #line 1 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditConditionsTemplate.cshtml"
    using Maestro.Web.Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/_PatientEditConditionsTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates__PatientEditConditionsTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates__PatientEditConditionsTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral(@">
    <h3>Patient Conditions</h3>
    <!--<div class=""pull-left"">
            <span class=""glyphicon glyphicon-question-sign""
                  data-toggle=""popover""
                  data-content=""
                    <p>Use this form to specify which measurements you would like to collect from your patient.
                    </p>
                    <p>Automated Entry will allow the patient to collect readings through peripheral devices. Manual Entry will allow your patient to type in their measurements by hand.
                    </p>
                    <p>Turning on either Automated or Manual entry will allow you to configure thresholds in the below panels.
                    </p>""
                  data-original-title=""Collect Measurements""
                  title=""Collect Measurements"">
            </span>
    </div>-->
</div>

<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-6\"");

WriteLiteral("><a");

WriteLiteral(" class=\"js-assign-all-conditions\"");

WriteLiteral(">Assign All</a></div>\r\n    <div");

WriteLiteral(" class=\"col-sm-6\"");

WriteLiteral("><a");

WriteLiteral(" class=\"js-remove-all-conditions\"");

WriteLiteral(">Remove All</a></div>\r\n</div>\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-6 conditions-container available-conditions-container\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"available-conditions-container\"");

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" class=\"box-overlay\"");

WriteLiteral(" style=\"display: none;\"");

WriteLiteral("></div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-6 conditions-container assigned-conditions-container\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"assigned-conditions-container\"");

WriteLiteral("></div>\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" class=\"well\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"EditPatient<% if ( typeof id !== \"");

WriteLiteral("undefined\" ) { %>/<%=id%><% }else{ %>/id<% } %>/CareManagers/\" class=\"btn btn-dan" +
"ger js-patient-conditions-save\">\r\n");

WriteLiteral("                ");

            
            #line 38 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditConditionsTemplate.cshtml"
           Write(GlobalStrings.Patient_SaveAndNextTabText);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </a>\r\n            <a");

WriteLiteral(" href=\"PatientDetails<% if ( typeof id !== \"");

WriteLiteral("undefined\" ) { %>/<%=id%><% }else{ %>/id<% } %>/\" class=\"btn btn-primary js-patie" +
"nt-conditions-save\">\r\n");

WriteLiteral("                ");

            
            #line 41 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditConditionsTemplate.cshtml"
           Write(GlobalStrings.Patient_SaveAndExitText);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </a>\r\n            <a");

WriteLiteral(" href=\"<% if ( typeof id !== \"");

WriteLiteral("undefined\" ) { %>PatientDetails/<%=id%><% } %>/\" class=\"btn btn-link js-patient-c" +
"onditions-cancel\">\r\n");

WriteLiteral("                ");

            
            #line 44 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditConditionsTemplate.cshtml"
           Write(GlobalStrings.Edit_Customer_CancelButtonText);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </a>\r\n        </div>\r\n    </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591