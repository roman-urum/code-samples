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
    
    #line 1 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditDevicesTemplate.cshtml"
    using Maestro.Web.Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/_PatientEditDevicesTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates__PatientEditDevicesTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates__PatientEditDevicesTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"row row-patient-title\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"btn-group\"");

WriteLiteral(">\r\n            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-primary dropdown-toggle\"");

WriteLiteral("\r\n                    data-toggle=\"dropdown\"");

WriteLiteral("\r\n                    aria-haspopup=\"true\"");

WriteLiteral(" \r\n                    aria-expanded=\"false\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 10 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditDevicesTemplate.cshtml"
           Write(GlobalStrings.Devices_NewActivationText);

            
            #line default
            #line hidden
WriteLiteral(" \r\n                <span");

WriteLiteral(" class=\"caret\"");

WriteLiteral("></span>\r\n            </button>\r\n            <ul");

WriteLiteral(" class=\"dropdown-menu\"");

WriteLiteral(">\r\n                <li><a");

WriteLiteral(" class=\"js-new-activation\"");

WriteLiteral(" data-deviceType=\"Other\"");

WriteLiteral(">");

            
            #line 14 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditDevicesTemplate.cshtml"
                                                                    Write(GlobalStrings.Devices_MobileDeviceTypeText);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n                <li><a");

WriteLiteral(" class=\"js-new-activation\"");

WriteLiteral(" data-deviceType=\"IVR\"");

WriteLiteral(">");

            
            #line 15 "..\..\Areas\Site\Views\Patients\Templates\_PatientEditDevicesTemplate.cshtml"
                                                                  Write(GlobalStrings.Devices_IVRTypeText);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</div>\r\n<div");

WriteLiteral(" id=\"devices-list-container\"");

WriteLiteral("></div>\r\n\r\n<div");

WriteLiteral(" class=\"well\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"PatientDetails/<%=id%>/\"");

WriteLiteral(" class=\"btn btn-primary js-patient-device-exit\"");

WriteLiteral(">\r\n                Exit\r\n            </a>\r\n        </div>\r\n    </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591
