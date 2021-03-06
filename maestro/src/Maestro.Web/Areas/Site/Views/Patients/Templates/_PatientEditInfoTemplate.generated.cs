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
    
    #line 1 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
    using Maestro.Web.Resources;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
    using NodaTime;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/_PatientEditInfoTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates__PatientEditInfoTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates__PatientEditInfoTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <h3>Status</h3>\r\n        <div");

WriteLiteral(" data-toggle=\"buttons\"");

WriteLiteral(" class=\"btn-group btn-group-block-ci clearfix\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" class=\"btn btn-default<%=(status == 1 ? \' active\' : \'\') %>\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" value=\"1\"");

WriteLiteral(" name=\"status\"");

WriteLiteral(">\r\n                Active\r\n            </label>\r\n            <label");

WriteLiteral(" class=\"btn btn-default<%=(status == 2 ? \' active\' : \'\') %>\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" value=\"2\"");

WriteLiteral(" name=\"status\"");

WriteLiteral(">\r\n                In Training\r\n            </label>\r\n            <label");

WriteLiteral(" class=\"btn btn-default<%=(status == 0 ? \' active\' : \'\') %>\"");

WriteLiteral(" >\r\n                <input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" name=\"status\"");

WriteLiteral(">\r\n                Inactive\r\n            </label>\r\n        </div>\r\n    </div>\r\n  " +
"  <div");

WriteLiteral(" class=\"col-sm-8\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"form-group-categories-of-care\"");

WriteLiteral(" class=\"form-group form-group-categories-of-care-ci\"");

WriteLiteral(">\r\n            <h3>Category Of Care</h3>\r\n            <select");

WriteLiteral(" id=\"categoriesOfCare\"");

WriteLiteral(" data-placeholder=\"Select Categories Of Care\"");

WriteLiteral(" name=\"categoriesOfCare\"");

WriteLiteral(" data-name=\"categoriesOfCare\"");

WriteLiteral(" class=\"form-control chosen-select categories-of-care\"");

WriteLiteral("></select>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<h3>");

            
            #line 30 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
Write(GlobalStrings.Patient_KeyIdentifiersText);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"firstName\"");

WriteLiteral(">");

            
            #line 35 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                              Write(GlobalStrings.Patient_FirstName);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"firstName\"");

WriteLiteral(" name=\"firstName\"");

WriteLiteral(" data-name=\"firstName\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"middleInitial\"");

WriteLiteral(">");

            
            #line 42 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                                  Write(GlobalStrings.Patient_MiddleInitial);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"middleInitial\"");

WriteLiteral(" name=\"middleInitial\"");

WriteLiteral(" data-name=\"middleInitial\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"lastName\"");

WriteLiteral(">");

            
            #line 49 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                             Write(GlobalStrings.Patient_LastName);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"lastName\"");

WriteLiteral(" name=\"lastName\"");

WriteLiteral(" data-name=\"lastName\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"gender\"");

WriteLiteral(">");

            
            #line 59 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                           Write(GlobalStrings.Patient_Gender);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <div");

WriteLiteral(" data-toggle=\"buttons\"");

WriteLiteral(" class=\"btn-group btn-group-block-ci clearfix\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"btn btn-default<%=(gender == 1 ? \' active\' : \'\') %>\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" id=\"male\"");

WriteLiteral(" name=\"gender\"");

WriteLiteral(" value=\"1\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 63 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
               Write(GlobalStrings.Gender_Male);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </label>\r\n                <label");

WriteLiteral(" class=\"btn btn-default<%=(gender == 2 ? \' active\' : \'\') %>\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" id=\"female\"");

WriteLiteral(" name=\"gender\"");

WriteLiteral(" value=\"2\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 67 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                   Write(GlobalStrings.Gender_Female);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </label>\r\n            </div>\r\n            <span");

WriteLiteral(" class=\"help-block help-block-error help-block-gender hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"birthDate\"");

WriteLiteral(">");

            
            #line 76 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                              Write(GlobalStrings.Patient_DateOfBirth);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <div");

WriteLiteral(" id=\"birthDate-datetimepicker\"");

WriteLiteral(" class=\"input-group date\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"birthDate-dp\"");

WriteLiteral(" name=\"birthDate-dp\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"glyphicon glyphicon-calendar\"");

WriteLiteral("></span>\r\n                </span>\r\n            </div>\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"birthDate\"");

WriteLiteral(" name=\"birthDate\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block help-block-error help-block-birthDate hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"age\"");

WriteLiteral(">");

            
            #line 89 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                        Write(GlobalStrings.Patient_Age);

            
            #line default
            #line hidden
WriteLiteral("</label>           \r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"age\"");

WriteLiteral(" name=\"age\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" readonly=\"readonly\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" id=\"patient-identifiers\"");

WriteLiteral("></div>\r\n\r\n<h3>");

            
            #line 98 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
Write(GlobalStrings.Patient_PatientDemographicsText);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"address1\"");

WriteLiteral(">");

            
            #line 103 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                             Write(GlobalStrings.Patient_Address1);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"address1\"");

WriteLiteral(" name=\"address1\"");

WriteLiteral(" data-name=\"address1\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"address2\"");

WriteLiteral(">");

            
            #line 110 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                             Write(GlobalStrings.Patient_Address2);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"address2\"");

WriteLiteral(" name=\"address2\"");

WriteLiteral(" data-name=\"address2\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"address3\"");

WriteLiteral(">");

            
            #line 117 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                             Write(GlobalStrings.Patient_Address3);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"address3\"");

WriteLiteral(" name=\"address3\"");

WriteLiteral(" data-name=\"address3\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"city\"");

WriteLiteral(">");

            
            #line 127 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                         Write(GlobalStrings.Patient_City);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"city\"");

WriteLiteral(" name=\"city\"");

WriteLiteral(" data-name=\"city\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"state\"");

WriteLiteral(">");

            
            #line 134 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                          Write(GlobalStrings.Patient_State);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"state\"");

WriteLiteral(" name=\"state\"");

WriteLiteral(" data-name=\"state\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"zipCode\"");

WriteLiteral(">");

            
            #line 141 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                            Write(GlobalStrings.Patient_Zip);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"zipCode\"");

WriteLiteral(" name=\"zipCode\"");

WriteLiteral(" data-name=\"zipCode\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"phoneHome\"");

WriteLiteral(">");

            
            #line 151 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                              Write(GlobalStrings.Patient_HomePhoneNumber);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"phoneHome\"");

WriteLiteral(" name=\"phoneHome\"");

WriteLiteral(" data-name=\"phoneHome\"");

WriteLiteral(" placeholder=\"(###) ###-####\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"phoneWork\"");

WriteLiteral(">");

            
            #line 158 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                              Write(GlobalStrings.Patient_WorkPhoneNumber);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"phoneWork\"");

WriteLiteral(" name=\"phoneWork\"");

WriteLiteral(" data-name=\"phoneWork\"");

WriteLiteral(" placeholder=\"(###) ###-####\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"email\"");

WriteLiteral(">");

            
            #line 165 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                          Write(GlobalStrings.Patient_Email);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            <input");

WriteLiteral(" type=\"email\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"email\"");

WriteLiteral(" name=\"email\"");

WriteLiteral(" data-name=\"email\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"language\"");

WriteLiteral(">*Language:</label>\r\n            <select");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"language\"");

WriteLiteral(" name=\"language\"");

WriteLiteral(" data-name=\"language\"");

WriteLiteral(">\r\n                <option");

WriteLiteral(" value=\"en\"");

WriteLiteral(" selected=\"selected\"");

WriteLiteral(">English</option>\r\n                <option");

WriteLiteral(" value=\"es\"");

WriteLiteral(">Spanish</option>\r\n            </select>\r\n            <span");

WriteLiteral(" class=\"help-block help-block-error help-block-language hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"timeZone\"");

WriteLiteral(">*Time Zone:</label>\r\n            <select");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"timeZone\"");

WriteLiteral(" name=\"timeZone\"");

WriteLiteral(" data-name=\"timeZone\"");

WriteLiteral(">\r\n");

            
            #line 187 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                
            
            #line default
            #line hidden
            
            #line 187 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                  
                    var now = SystemClock.Instance.Now;

                    foreach (var timeZoneId in DateTimeZoneProviders.Tzdb.Ids)
                    {
                        var tz = DateTimeZoneProviders.Tzdb[timeZoneId];
                        var offset = tz.GetZoneInterval(now).StandardOffset;


            
            #line default
            #line hidden
WriteLiteral("                        <option");

WriteAttribute("value", Tuple.Create(" value=\"", 8467), Tuple.Create("\"", 8486)
            
            #line 195 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
, Tuple.Create(Tuple.Create("", 8475), Tuple.Create<System.Object, System.Int32>(timeZoneId
            
            #line default
            #line hidden
, 8475), false)
);

WriteLiteral(">");

            
            #line 195 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                                               Write(string.Format("(UTC{0:+HH:mm}) {1}", offset, tz));

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 196 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
                    }
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </select>\r\n            <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" for=\"preferredSessionTime\"");

WriteLiteral(">*Preferred Session Time:</label>\r\n            <div");

WriteLiteral(" class=\"input-group preferred-session-time\"");

WriteLiteral(" id=\"preferred-session-time\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"preferredSessionTime-dp\"");

WriteLiteral(" name=\"preferredSessionTime-dp\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"glyphicon glyphicon-time\"");

WriteLiteral("></span>\r\n                </span>\r\n            </div>\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"preferredSessionTime\"");

WriteLiteral(" name=\"preferredSessionTime\"");

WriteLiteral(" data-name=\"preferredSessionTime\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"help-block help-block-error help-block-preferred-session-time hidden\"");

WriteLiteral("></span>\r\n        </div>\r\n\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" class=\"well\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"EditPatient<% if ( typeof id !== \"");

WriteLiteral("undefined\" ) { %>/<%=id%><% }else{ %>/id<% } %>/Conditions/\" class=\"btn btn-dange" +
"r js-patient-info-save\">\r\n");

WriteLiteral("                ");

            
            #line 222 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
           Write(GlobalStrings.Patient_SaveAndNextTabText);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </a>\r\n            <a");

WriteLiteral(" href=\"PatientDetails<% if ( typeof id !== \"");

WriteLiteral("undefined\" ) { %>/<%=id%><% }else{ %>/id<% } %>/\" class=\"btn btn-primary js-patie" +
"nt-info-save\">\r\n");

WriteLiteral("                ");

            
            #line 225 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
           Write(GlobalStrings.Patient_SaveAndExitText);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </a>\r\n            <a");

WriteLiteral(" href=\"<% if ( typeof id !== \"");

WriteLiteral("undefined\" ) { %>PatientDetails/<%=id%><% } %>/\" class=\"btn btn-link js-patient-i" +
"nfo-cancel\">\r\n");

WriteLiteral("                ");

            
            #line 228 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditInfoTemplate.cshtml"
           Write(GlobalStrings.Edit_Customer_CancelButtonText);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </a>\r\n        </div>\r\n    </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591
