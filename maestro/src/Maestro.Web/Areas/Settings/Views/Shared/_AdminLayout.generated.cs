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
    using Maestro.Web.Controls;
    using Maestro.Web.Resources;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Settings/Views/Shared/_AdminLayout.cshtml")]
    public partial class _Areas_Settings_Views_Shared__AdminLayout_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Settings_Views_Shared__AdminLayout_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Areas\Settings\Views\Shared\_AdminLayout.cshtml"
  
    Layout = "~/Views/Shared/_PublicLayout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h1");

WriteLiteral(" class=\"page-header-ci\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Areas\Settings\Views\Shared\_AdminLayout.cshtml"
Write(GlobalStrings.Edit_Customer_AdministrationText);

            
            #line default
            #line hidden
WriteLiteral("\r\n    <span");

WriteLiteral(" class=\"small\"");

WriteLiteral(">(Maestro Default Settings)</span>\r\n</h1>\r\n<ol");

WriteLiteral(" class=\"breadcrumb breadcrumb-ci\"");

WriteLiteral(">\r\n    <li><a");

WriteLiteral(" href=\"/\"");

WriteLiteral(">");

            
            #line 10 "..\..\Areas\Settings\Views\Shared\_AdminLayout.cshtml"
               Write(GlobalStrings.Edit_Customer_SiteManagementBreadcrumbText);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n    <li><a");

WriteLiteral(" class=\"active\"");

WriteLiteral(">Edit Maestro Default Settings</a></li>\r\n</ol>\r\n\r\n<!-- END PAGE HEADER / BREADCRU" +
"MBS -->\r\n\r\n<!-- MAIN CONTENT - MAESTRO DEFAULT SETTINGS -->\r\n<div");

WriteLiteral(" class=\"ci-content\"");

WriteLiteral(">\r\n\r\n    <!-- OUTER TAB SHELL FOR DEFAULT SETTINGS -->\r\n    <ul");

WriteLiteral(" class=\"nav nav-tabs\"");

WriteLiteral(">\r\n        <li");

WriteLiteral(" class=\"active\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"#ci-user-settings\"");

WriteLiteral(">");

            
            #line 22 "..\..\Areas\Settings\Views\Shared\_AdminLayout.cshtml"
                                   Write(GlobalStrings.Edit_Customer_UsersTabTitle);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n        </li>\r\n    </ul>\r\n    <div");

WriteLiteral(" class=\"tab-content\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 26 "..\..\Areas\Settings\Views\Shared\_AdminLayout.cshtml"
   Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <!-- END OUTER TAB SHELL FOR DEFAULT SETTINGS -->\r\n\r\n</div>\r\n<!" +
"-- END MAIN CONTENT -->\r\n\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 35 "..\..\Areas\Settings\Views\Shared\_AdminLayout.cshtml"
Write(RenderSection("scripts", required: false));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
