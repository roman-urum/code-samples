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
    
    #line 1 "..\..\Areas\Site\Views\Patients\Templates\PatientsListTemplate.cshtml"
    using Maestro.Web.Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/PatientsListTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates_PatientsListTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_PatientsListTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" id=\"patientsListTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n    <div class=\"page-header page-header-ci\">\r\n        <h1>\r\n");

WriteLiteral("            ");

            
            #line 6 "..\..\Areas\Site\Views\Patients\Templates\PatientsListTemplate.cshtml"
       Write(GlobalStrings.Patients_PatientsText);

            
            #line default
            #line hidden
WriteLiteral("\r\n            <small>(");

            
            #line 7 "..\..\Areas\Site\Views\Patients\Templates\PatientsListTemplate.cshtml"
               Write(SiteContext.Current.Site.Name);

            
            #line default
            #line hidden
WriteLiteral(")</small>\r\n        </h1>\r\n    </div>\r\n    <div class=\"page-content-ci\">\r\n        " +
"<div id=\"patient-search-box\"></div>\r\n        <div id=\"patients-list-container\"><" +
"/div>\r\n    </div>\r\n</script>");

        }
    }
}
#pragma warning restore 1591
