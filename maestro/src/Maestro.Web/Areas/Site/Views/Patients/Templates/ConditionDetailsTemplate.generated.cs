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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/ConditionDetailsTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates_ConditionDetailsTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_ConditionDetailsTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"conditionDetailsTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">

    <div class=""condition-details"">
        <h2><%= name %></h2>
        <p><%= description %></p>

        <div class=""condition-details-content"">
            <ul class=""nav nav-tabs"" role=""tablist"">
                <li role=""presentation"" class=""active""><a href=""#condition-thresholds"" role=""tab"" data-toggle=""tab"">Thresholds</a></li>
                <li role=""presentation""><a href=""#condition-content"" role=""tab"" data-toggle=""tab"">Content</a></li>
            </ul>

            <div class=""tab-content"">
                <div role=""tabpanel"" class=""tab-pane active"" id=""condition-thresholds"">
                </div>
                <div role=""tabpanel"" class=""tab-pane"" id=""condition-content"">
                </div>
            </div>
        </div>

    </div>

</script>");

        }
    }
}
#pragma warning restore 1591
