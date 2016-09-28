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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/PatientTrendsViewTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates_PatientTrendsViewTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_PatientTrendsViewTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"patientTrendsViewTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n    <div class=\"patient-trends clearfix\">\r\n        ");

WriteLiteral(@"
        <div class=""row patients-trends-header"">
            <div class=""col-sm-2"">
                <button class=""btn btn-primary btn-lg patient-trends-add-charts"" <%= isSettingsFetched ? '' : 'disabled'%> >Add charts</button>
            </div>
            <div class=""col-sm-8 daterange-container"">
            </div>
            <div class=""col-sm-2 text-right"">
                <a href=""#"" class=""disabled js-patient-export-trends"">
                    <i class=""fa fa-external-link fa-invert-ci""></i>
                    Export all charts
                </a>
            </div>
        </div>

        <% if (isSettingsFetched) { %>
            <div class=""row"">
                <div class=""col-sm-12"">

                    <ul class=""chart-containers-list list-unstyled row"">

                    </ul>

                </div>
            </div>
        <% } else {%>
            <div class=""col-sm-12"">
                <img src=""/Content/img/spinner.gif"" class=""spinner"" />
            </div>
        <% }%>

    </div>
</script>");

        }
    }
}
#pragma warning restore 1591
