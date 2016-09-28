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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/DevicesListItemTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates_DevicesListItemTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_DevicesListItemTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"devicesListItemTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n    <td><span class=\"glyphicon glyphicon-phone\"></span></td>\r\n    <td><%= devi" +
"ceType %> Device</td>\r\n    <td><%= deviceStatusesStrings[status]%></td>\r\n    <td" +
">\r\n        <% if (status == deviceStatuses.NOT_ACTIVATED) { %>\r\n            <spa" +
"n>Activation code: <b><%=activationCode%></b></span>\r\n        <% } else if (devi" +
"ceId) { %>\r\n            <span>Serial number: <%=deviceId%></span>\r\n        <% } " +
"%>\r\n    </td>\r\n    <td>\r\n        \r\n        <% if (status == deviceStatuses.NOT_A" +
"CTIVATED\r\n               || status == deviceStatuses.DECOMISSION_COMPLETED\r\n    " +
"           || status == deviceStatuses.ACTIVATED\r\n               || status == de" +
"viceStatuses.DECOMISSION_REQUESTED) { %>           \r\n            <div class=\"btn" +
"-group\">\r\n                <button type=\"button\" \r\n                        class=" +
"\"btn btn-default dropdown-toggle\"\r\n                        data-toggle=\"dropdown" +
"\"\r\n                        aria-haspopup=\"true\"\r\n                        aria-ex" +
"panded=\"false\">\r\n                    <span class=\"glyphicon glyphicon-cog\"></spa" +
"n>\r\n                </button>\r\n            \r\n         \r\n                <ul clas" +
"s=\"device-actions dropdown-menu dropdown-menu-right\">\r\n                    <% if" +
" (status == deviceStatuses.NOT_ACTIVATED || status == deviceStatuses.DECOMISSION" +
"_COMPLETED) { %>\r\n                        <li><a href=\"#\" id=\"remove-device-btn\"" +
">Remove <span class=\"glyphicon glyphicon-trash pull-right\"></span></a></li>\r\n   " +
"                 <% } else if (status == deviceStatuses.ACTIVATED) { %>\r\n       " +
"                 <li><a href=\"#\" id=\"request-decomission-btn\">Decommission<span " +
"class=\"glyphicon glyphicon-ban-circle pull-right\"></span></a></li>\r\n            " +
"        <% } else if (status == deviceStatuses.DECOMISSION_REQUESTED) { %>\r\n    " +
"                    <li><a href=\"#\" id=\"request-decomission-btn\">Re-request deco" +
"mmission<span class=\"glyphicon glyphicon-refresh pull-right\"></span></a></li>\r\n " +
"                   <% } %>\r\n                    <% if (status == deviceStatuses." +
"NOT_ACTIVATED || status == deviceStatuses.ACTIVATED) { %>\r\n                     " +
"   <li><a href=\"#\" id=\"manage-pin-btn\">Manage Pin <span class=\"glyphicon glyphic" +
"on-lock pull-right\"></span></a></li>\r\n                    <% } %>\r\n             " +
"   </ul>\r\n            </div>\r\n        <% } %>\r\n    </td>\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591