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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Dashboard/Templates/PatientViewHeaderThreshold.cshtml")]
    public partial class _Areas_Site_Views_Dashboard_Templates_PatientViewHeaderThreshold_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Dashboard_Templates_PatientViewHeaderThreshold_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"patientCardViewTemplateHeaderTHRESHOLD\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">
    <div class=""col-sm-9"">
        <%- obj.reading.name %>:
        <strong style=""color: <%- obj.color %>"">
            <% if(!obj.reading.isAutomated) { %>*<% } %><%- obj.reading.value %> <%- obj.reading.unit %>
        </strong>
        on <%- obj.reading.formattedDate %>
    </div>
    <div class=""col-sm-3 text-right"">
        <strong><%- obj.reading.alert.type.NAME %></strong>
    </div>
</script>");

        }
    }
}
#pragma warning restore 1591
