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
    
    #line 1 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
    using System.Web.UI.WebControls;
    
    #line default
    #line hidden
    using System.Web.WebPages;
    
    #line 2 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
    using Maestro.Domain.Constants;
    
    #line default
    #line hidden
    using Maestro.Web;
    using Maestro.Web.Areas.Site;
    
    #line 3 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
    using Newtonsoft.Json;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Dashboard/Index.cshtml")]
    public partial class _Areas_Site_Views_Dashboard_Index_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Dashboard_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 6 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
  
    ViewBag.AppId = "appDashboard";

    var constants = Model != null ? JsonConvert.SerializeObject(Model) : string.Empty;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<input");

WriteLiteral(" id=\"constants\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 280), Tuple.Create("\"", 298)
            
            #line 12 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
, Tuple.Create(Tuple.Create("", 288), Tuple.Create<System.Object, System.Int32>(constants
            
            #line default
            #line hidden
, 288), false)
);

WriteLiteral("/>\r\n\r\n<h1");

WriteLiteral(" class=\"page-header page-header-ci\"");

WriteLiteral(">\r\n    Dashboard\r\n    <small>(");

            
            #line 16 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
       Write(SiteContext.Current.Site.Name);

            
            #line default
            #line hidden
WriteLiteral(")</small>\r\n</h1>\r\n\r\n<div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral(" style=\"margin: 0 -15px;\"");

WriteLiteral(">\r\n\r\n");

            
            #line 21 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 21 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
     if (User.IsInCustomerRoles(CustomerUserRoles.CustomerAdmin) || @User.IsInCustomerRoles(CustomerUserRoles.ManageAllPatients))
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-lg-3\"");

WriteLiteral(">\r\n\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-lg-9 form-horizontal\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" for=\"care-managers-filter-dropdown\"");

WriteLiteral(" class=\"col-sm-2 control-label\"");

WriteLiteral(">View Patients:</label>\r\n                    <div");

WriteLiteral(" class=\"col-sm-5\"");

WriteLiteral(">\r\n                        <select");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" id=\"care-managers-filter-dropdown\"");

WriteLiteral(" disabled>\r\n                            <option");

WriteLiteral(" value=\"\"");

WriteLiteral(">Loading...</option>\r\n                        </select>\r\n                    </di" +
"v>\r\n                </div>\r\n\r\n\r\n            </div>\r\n        </div>\r\n");

            
            #line 40 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n    <div");

WriteLiteral(" class=\"col-lg-3\"");

WriteLiteral(" id=\"dashboard-widgets\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"sites-list\"");

WriteLiteral(" class=\"panel panel-default visible\"");

WriteLiteral(">\r\n            <header");

WriteLiteral(" class=\"panel-heading stylized-panel collapsed\"");

WriteLiteral(" style=\"cursor: pointer;\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" aria-controls=\"sites-list-body\"");

WriteLiteral(" data-target=\"#sites-list-body\"");

WriteLiteral(" aria-expanded=\"false\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-sm-10\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 47 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
                   Write(SiteContext.Current.Site.Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n                        <a");

WriteLiteral(" role=\"button\"");

WriteLiteral(" class=\"btn btn-xs btn-primary select-all not-active\"");

WriteLiteral(">Select All</a>\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"col-sm-2\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"caret-icon pull-right\"");

WriteLiteral("></span>\r\n                    </div>\r\n                </div>\r\n            </heade" +
"r>\r\n            <div");

WriteLiteral(" id=\"sites-list-body\"");

WriteLiteral(" class=\"panel-collapse collapse\"");

WriteLiteral(" aria-expanded=\"false\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"panel-body no-padding\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" id=\"sites-list-body-content\"");

WriteLiteral("></div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div" +
"");

WriteLiteral(" id=\"severity-filter\"");

WriteLiteral(" class=\"panel panel-default\"");

WriteLiteral(">\r\n            <header");

WriteLiteral(" class=\"panel-heading stylized-panel\"");

WriteLiteral(" style=\"cursor: pointer;\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" aria-controls=\"severity-filter-body\"");

WriteLiteral(" data-target=\"#severity-filter-body\"");

WriteLiteral(" aria-expanded=\"true\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-sm-10\"");

WriteLiteral(">\r\n                        Alert severities\r\n                        <sup>\r\n     " +
"                       <i");

WriteLiteral(" style=\"font-size: 80%;\"");

WriteLiteral(" class=\"fa fa-question-circle\"");

WriteLiteral(" data-toggle=\"tooltip\"");

WriteLiteral(" data-placement=\"right\"");

WriteLiteral(" title=\"Tooltip on right\"");

WriteLiteral("></i>\r\n                        </sup>\r\n                    </div>\r\n              " +
"      <div");

WriteLiteral(" class=\"col-sm-2\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"caret-icon pull-right\"");

WriteLiteral("></span>\r\n                    </div>\r\n                </div>\r\n            </heade" +
"r>\r\n            <div");

WriteLiteral(" id=\"severity-filter-body\"");

WriteLiteral(" class=\"panel-collapse collapse in\"");

WriteLiteral(" aria-expanded=\"true\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"panel-body no-padding\"");

WriteLiteral(" style=\"padding: 0;\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"dashboard-selector-body\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"range-item\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"scale-width-detector\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"scale-range-width-detector\"");

WriteLiteral(">25/50</span>\r\n                                <span");

WriteLiteral(" class=\"scale-name-width-detector\"");

WriteLiteral(">Name</span>\r\n                            </div>\r\n                            <di" +
"v");

WriteLiteral(" class=\"scale-item-wrapper\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"scale-item\"");

WriteLiteral(" style=\"height: 55%;\"");

WriteLiteral(">\r\n                                    <span");

WriteLiteral(" class=\"scale-range\"");

WriteLiteral(">25/50</span>\r\n                                    <span");

WriteLiteral(" class=\"scale-name\"");

WriteLiteral(">Name</span>\r\n                                </div>\r\n                           " +
" </div>\r\n                        </div>\r\n                        <div");

WriteLiteral(" class=\"range-item disabled\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"scale-width-detector\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"scale-range-width-detector\"");

WriteLiteral(">25/50</span>\r\n                                <span");

WriteLiteral(" class=\"scale-name-width-detector\"");

WriteLiteral(">This is very long name</span>\r\n                            </div>\r\n             " +
"               <div");

WriteLiteral(" class=\"scale-item-wrapper\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"scale-item\"");

WriteLiteral(" style=\"height: 15%;\"");

WriteLiteral(">\r\n                                    <span");

WriteLiteral(" class=\"scale-range\"");

WriteLiteral(">25/50</span>\r\n                                    <span");

WriteLiteral(" class=\"scale-name\"");

WriteLiteral(">This is very long name</span>\r\n                                </div>\r\n         " +
"                   </div>\r\n                        </div>\r\n                     " +
"   <div");

WriteLiteral(" class=\"range-item\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"scale-width-detector\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"scale-range-width-detector\"");

WriteLiteral(">50</span>\r\n                                <span");

WriteLiteral(" class=\"scale-name-width-detector\"");

WriteLiteral(">Simple name</span>\r\n                            </div>\r\n                        " +
"    <div");

WriteLiteral(" class=\"scale-item-wrapper\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"scale-item\"");

WriteLiteral(" style=\"height: 100%;\"");

WriteLiteral(">\r\n                                    <span");

WriteLiteral(" class=\"scale-range\"");

WriteLiteral(">50</span>\r\n                                    <span");

WriteLiteral(" class=\"scale-name\"");

WriteLiteral(">Simple name</span>\r\n                                </div>\r\n                    " +
"        </div>\r\n                        </div>\r\n                    </div>\r\n    " +
"            </div>\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" id=\"alert-type-filter\"");

WriteLiteral(" class=\"panel panel-default\"");

WriteLiteral(">\r\n            <header");

WriteLiteral(" class=\"panel-heading stylized-panel\"");

WriteLiteral(" style=\"cursor: pointer;\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" aria-controls=\"alert-type-filter-body\"");

WriteLiteral(" data-target=\"#alert-type-filter-body\"");

WriteLiteral(" aria-expanded=\"true\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-sm-10\"");

WriteLiteral(">\r\n                        Alert types\r\n                        <sup>\r\n          " +
"                  <i");

WriteLiteral(" style=\"font-size: 80%;\"");

WriteLiteral(" class=\"fa fa-question-circle\"");

WriteLiteral(" data-toggle=\"tooltip\"");

WriteLiteral(" data-placement=\"right\"");

WriteLiteral(" title=\"Tooltip on right\"");

WriteLiteral("></i>\r\n                        </sup>\r\n                    </div>\r\n              " +
"      <div");

WriteLiteral(" class=\"col-sm-2\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"caret-icon pull-right\"");

WriteLiteral("></span>\r\n                    </div>\r\n                </div>\r\n            </heade" +
"r>\r\n            <div");

WriteLiteral(" id=\"alert-type-filter-body\"");

WriteLiteral(" class=\"panel-collapse collapse in\"");

WriteLiteral(" aria-expanded=\"true\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"panel-body no-padding\"");

WriteLiteral(">\r\n                    this is body\r\n                </div>\r\n            </div>\r\n" +
"        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-lg-9\"");

WriteLiteral(">\r\n        <header");

WriteLiteral(" class=\"stylized-panel panel-heading\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">Care Needed</div>\r\n            </div>\r\n        </header>\r\n        <section");

WriteLiteral(" style=\"margin-top: 20px\"");

WriteLiteral(" id=\"patient-list-container\"");

WriteLiteral(">\r\n            <img");

WriteLiteral(" src=\"/Content/img/spinner.gif\"");

WriteLiteral(" class=\"spinner\"");

WriteLiteral(">\r\n        </section>\r\n    </div>\r\n</div>\r\n\r\n");

            
            #line 151 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/WaitTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 152 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewNoCards"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 153 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewWatchPatient"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 155 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 156 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewHeader"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 157 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewHeaderItem"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 158 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewHeaderBehavior"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 159 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewHeaderThreshold"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 160 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewHeaderBloodPressure"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 161 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewDetailsBloodPressure"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 162 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewHeaderAdherence"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 163 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewDetailsAdherence"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 164 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewDetailsBehavior"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 165 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewDetailsThreshold"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 166 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewDetailsFooter"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 167 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/PatientViewIgnoreReadingModal"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 168 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/AlertTypeItemWidgetView"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 169 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/AlertTypeWidgetView"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 170 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/AlertSeverityItemWidgetView"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 171 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/AlertSeverityWidgetView"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 172 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/CareManagersFilterTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 173 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/SitesView"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 174 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("Templates/OrgItemTreeView"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 175 "..\..\Areas\Site\Views\Dashboard\Index.cshtml"
Write(Html.Partial("~/Areas/Site/Views/Patients/Templates/OneWayChatTemplate.cshtml"));

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
