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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Dashboard/Templates/AlertSeverityWidgetView.cshtml")]
    public partial class _Areas_Site_Views_Dashboard_Templates_AlertSeverityWidgetView_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Dashboard_Templates_AlertSeverityWidgetView_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"alertSeverityWidgetView\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">
    <header class=""panel-heading stylized-panel"" style=""cursor: pointer;"" data-toggle=""collapse"" aria-controls=""alert-severity-body"" data-target=""#alert-severity-body"" aria-expanded=""true"">
        <div class=""row"">
            <div class=""col-sm-10"">
                Alert Severities
                <!--<sup>
                    <i style=""font-size: 80%;"" class=""fa fa-question-circle"" data-toggle=""tooltip"" data-placement=""right"" title=""Tooltip on right""></i>
                </sup>-->
                &nbsp;&nbsp;&nbsp;<a role=""button"" class=""btn btn-xs btn-primary select-all not-active"">Select All</a>
            </div>
            <div class=""col-sm-2"">
                <span class=""caret-icon pull-right""></span>
            </div>
        </div>
    </header>
    <div id=""alert-severity-body"" class=""panel-collapse collapse in"" aria-expanded=""true"">
        <div class=""panel-body no-padding"" style=""padding: 0;"">
            <div class=""dashboard-selector-body""></div>
        </div>
    </div>
</script>");

        }
    }
}
#pragma warning restore 1591
