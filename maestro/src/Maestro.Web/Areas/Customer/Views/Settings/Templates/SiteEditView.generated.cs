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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/Settings/Templates/SiteEditView.cshtml")]
    public partial class _Areas_Customer_Views_Settings_Templates_SiteEditView_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_Settings_Templates_SiteEditView_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"siteEditView\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n\r\n    <form>\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-4\">\r\n " +
"               <label>Organization:</label>\r\n            </div>\r\n            <di" +
"v class=\"col-sm-5\">\r\n                <span class=\"orgs-path\"><%= getOrgsPath() %" +
"></span>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n     " +
"       <div class=\"col-sm-4\">\r\n                <label>Move site:</label>\r\n      " +
"      </div>\r\n            <div class=\"col-sm-5\">\r\n                <select name=\"" +
"parentOrganizationId\">\r\n                    <option value=\"\"><%= rootOrgName %><" +
"/option>\r\n                    <% _.each(orgs, function (org) { %>\r\n             " +
"           <option value=\"<%= org.id %>\">\r\n                            <% var i " +
"= 0, indentStr = \'\'; %>\r\n                            <% while (i < org.indentLev" +
"el) { %>\r\n                            <% indentStr += \'\\u2013\'; %>\r\n            " +
"                <% i += 1; %>\r\n                            <% } %>\r\n            " +
"                <%= indentStr %>\r\n                            <%= org.name %>\r\n " +
"                       </option>\r\n                    <% } ); %>\r\n              " +
"  </select>\r\n            </div>\r\n        </div>\r\n\r\n        <hr>\r\n\r\n        <div " +
"class=\"row\">\r\n            <div class=\"col-sm-4\">\r\n                <label>Site Av" +
"ailability:</label>\r\n            </div>\r\n            <div class=\"col-sm-5\">\r\n   " +
"             <input type=\"checkbox\" class=\"basic-checkbox js-site-toggle\" <%= is" +
"Active ? \'checked\' : \'\' %>>\r\n            </div>\r\n        </div>\r\n\r\n        <div " +
"class=\"row\">\r\n            <div class=\"col-sm-4\">\r\n                <label>Display" +
" name:*</label>\r\n            </div>\r\n            <div class=\"col-sm-5\">\r\n       " +
"         <input class=\"form-control input-sm\" type=\"text\" name=\"name\" data-name=" +
"\"name\" value=\"\">\r\n                <span class=\"help-block hidden\"></span>\r\n     " +
"       </div>\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n            <div cla" +
"ss=\"col-sm-4\">\r\n                <label>Contact Phone:</label>\r\n            </div" +
">\r\n            <div class=\"col-sm-5\">\r\n                <input class=\"form-contro" +
"l input-sm\" type=\"text\" placeholder=\"###-###-####\" name=\"contactPhone\" value=\"\">" +
"\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n            <" +
"div class=\"col-sm-4\">\r\n                <label>State:</label>\r\n            </div>" +
"\r\n            <div class=\"col-sm-5\">\r\n                <input class=\"form-control" +
" input-sm\" type=\"text\" name=\"state\" value=\"\">\r\n            </div>\r\n        </div" +
">\r\n\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-4\">\r\n            " +
"    <label>City:</label>\r\n            </div>\r\n            <div class=\"col-sm-5\">" +
"\r\n                <input class=\"form-control input-sm\" type=\"text\" name=\"city\" v" +
"alue=\"\">\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n     " +
"       <div class=\"col-sm-4\">\r\n                <label>ZipCode:</label>\r\n        " +
"    </div>\r\n            <div class=\"col-sm-5\">\r\n                <input class=\"fo" +
"rm-control input-sm\" type=\"text\" name=\"zipCode\" value=\"\">\r\n            </div>\r\n " +
"       </div>\r\n\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-4\">\r\n" +
"                <label>Address1:</label>\r\n            </div>\r\n            <div c" +
"lass=\"col-sm-5\">\r\n                <input class=\"form-control input-sm\" type=\"tex" +
"t\" name=\"address1\" value=\"\">\r\n            </div>\r\n        </div>\r\n\r\n        <div" +
" class=\"row\">\r\n            <div class=\"col-sm-4\">\r\n                <label>Addres" +
"s2:</label>\r\n            </div>\r\n            <div class=\"col-sm-5\">\r\n           " +
"     <input class=\"form-control input-sm\" type=\"text\" name=\"address2\" value=\"\">\r" +
"\n            </div>\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n            <d" +
"iv class=\"col-sm-4\">\r\n                <label>Address3:</label>\r\n            </di" +
"v>\r\n            <div class=\"col-sm-5\">\r\n                <input class=\"form-contr" +
"ol input-sm\" type=\"text\" name=\"address3\" value=\"\">\r\n            </div>\r\n        " +
"</div>\r\n\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-4\">\r\n       " +
"         <label>NPI:</label>\r\n            </div>\r\n            <div class=\"col-sm" +
"-5\">\r\n                <input class=\"form-control input-sm\" type=\"text\" name=\"nat" +
"ionalProviderIdentificator\" value=\"\">\r\n            </div>\r\n        </div>\r\n\r\n   " +
"     <div class=\"row\">\r\n            <div class=\"col-sm-4\">\r\n                <lab" +
"el>Customer Site ID:</label>\r\n            </div>\r\n            <div class=\"col-sm" +
"-5\">\r\n                <input class=\"form-control input-sm\" type=\"text\" name=\"cus" +
"tomerSiteId\" value=\"\">\r\n            </div>\r\n        </div>\r\n\r\n        <a class=\"" +
"btn btn-danger site-remove js-site-remove\">Remove</a>\r\n    </form>\r\n\r\n</script>");

        }
    }
}
#pragma warning restore 1591
