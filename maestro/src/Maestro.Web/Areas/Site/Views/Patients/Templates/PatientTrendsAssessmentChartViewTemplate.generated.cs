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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/PatientTrendsAssessmentChartViewTemplate.cs" +
        "html")]
    public partial class _Areas_Site_Views_Patients_Templates_PatientTrendsAssessmentChartViewTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_PatientTrendsAssessmentChartViewTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"patientTrendsAssessmentChartViewTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">
    <li class=""chart-container col-sm-6"">
        <div class=""pull-right"">
            <a class=""js-reorder"">
                <i class=""fa fa-bars fa-invert-ci""></i>
                Reorder
            </a>
            &nbsp;
            <a class=""js-remove"">
                <i class=""fa fa-times fa-invert-ci""></i>
                Remove
            </a>
            <!--span><small>Move chart</small></span>
            <button class=""js-move-up"">&#8593;</button>
            <button class=""js-move-down"">&#8595;</button-->
        </div>
        <h3><%=chartLabel%></h3>
        <div class=""clearfix""></div>
        <div class=""chart""></div>
        <div class=""spinner-container"" style=""display: none;"">
            <img src=""/Content/img/spinner.gif"" class=""spinner"" />
        </div>
    </li>
</script>");

        }
    }
}
#pragma warning restore 1591
