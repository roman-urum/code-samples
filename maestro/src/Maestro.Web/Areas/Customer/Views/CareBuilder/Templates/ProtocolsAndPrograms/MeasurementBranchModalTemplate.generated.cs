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
    using Maestro.Common.Extensions;
    using Maestro.Web;
    using Maestro.Web.Areas.Customer;
    using Maestro.Web.Controls;
    using Maestro.Web.Resources;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/CareBuilder/Templates/ProtocolsAndPrograms/MeasurementBran" +
        "chModalTemplate.cshtml")]
    public partial class _Areas_Customer_Views_CareBuilder_Templates_ProtocolsAndPrograms_MeasurementBranchModalTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_CareBuilder_Templates_ProtocolsAndPrograms_MeasurementBranchModalTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"measurementBranchModalTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n    <p>");

            
            #line 2 "..\..\Areas\Customer\Views\CareBuilder\Templates\ProtocolsAndPrograms\MeasurementBranchModalTemplate.cshtml"
  Write(GlobalStrings.Edit_Customer_CareBuilder_ProtocolsBranchInstuctionsText);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n    <div>\r\n        <span class=\"help-block-error js-value-not-selected hide" +
"\">\r\n");

WriteLiteral("            ");

            
            #line 5 "..\..\Areas\Customer\Views\CareBuilder\Templates\ProtocolsAndPrograms\MeasurementBranchModalTemplate.cshtml"
       Write(GlobalStrings.Edit_Customer_CareBuilder_ProtocolsBranchError);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </span>\r\n    </div>\r\n    <div>\r\n        <span class=\"help-block-error j" +
"s-branch-exists hide\">\r\n");

WriteLiteral("            ");

            
            #line 10 "..\..\Areas\Customer\Views\CareBuilder\Templates\ProtocolsAndPrograms\MeasurementBranchModalTemplate.cshtml"
       Write(GlobalStrings.Edit_Customer_CareBuilder_ProtocolsBranchExists);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </span>\r\n    </div>\r\n    \r\n    <div class=\"row js-alert-severities\">\r\n " +
"       <div class=\"col-md-6\">\r\n            <h4>");

            
            #line 16 "..\..\Areas\Customer\Views\CareBuilder\Templates\ProtocolsAndPrograms\MeasurementBranchModalTemplate.cshtml"
           Write(GlobalStrings.Edit_Customer_CareBuilder_ThresholdSeverityLabel);

            
            #line default
            #line hidden
WriteLiteral(@"</h4>
            <select id=""thresholdAlertSeverityId"" 
                    name=""thresholdAlertSeverityId"" 
                    data-name=""thresholdAlertSeverityId""
                    class=""form-control"">
            </select>
        </div>
    </div>
    
    <div class=""js-thresholds"">

    </div>
</script>
");

        }
    }
}
#pragma warning restore 1591
