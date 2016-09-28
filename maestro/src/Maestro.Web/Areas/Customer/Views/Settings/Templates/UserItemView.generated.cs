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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/Settings/Templates/UserItemView.cshtml")]
    public partial class _Areas_Customer_Views_Settings_Templates_UserItemView_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_Settings_Templates_UserItemView_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"userItemView\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n\r\n    <td class=\"col-xs-4 ci-user-name highlightable\">        \r\n        <a hre" +
"f=\"/Settings/Users/<%= id %>/Details\" class=\"js-link\">\r\n            <% var first" +
"NameStartHighlightIndex = firstName.toLowerCase().indexOf(searchStr.toLowerCase(" +
")); %>            \r\n            <% var firstNameEndHighlightIndex = firstNameSta" +
"rtHighlightIndex >= 0 ? firstNameStartHighlightIndex + searchStr.length : -1; %>" +
"            \r\n            <span id=\"user-first-name\" class=\"user-first-name\"><%=" +
" firstName.substring(0, firstNameStartHighlightIndex) %><span class=\"highlight\">" +
"<%= firstName.substring(firstNameStartHighlightIndex, firstNameEndHighlightIndex" +
") %></span><%= firstName.substring(firstNameEndHighlightIndex, firstName.length)" +
" %></span>\r\n            \r\n            <% var lastNameStartHighlightIndex = lastN" +
"ame.toLowerCase().indexOf(searchStr.toLowerCase()); %>\r\n            <% var lastN" +
"ameEndHighlightIndex = lastNameStartHighlightIndex >= 0 ? lastNameStartHighlight" +
"Index + searchStr.length : -1; %>                        \r\n            <span id=" +
"\"user-first-name\" class=\"user-first-name\"><%= lastName.substring(0, lastNameStar" +
"tHighlightIndex) %><span class=\"highlight\"><%= lastName.substring(lastNameStartH" +
"ighlightIndex, lastNameEndHighlightIndex) %></span><%= lastName.substring(lastNa" +
"meEndHighlightIndex, lastName.length) %></span>\r\n        </a>\r\n        <br />\r\n " +
"       <% var emailStartHighlightIndex = email.toLowerCase().indexOf(searchStr.t" +
"oLowerCase()); %>\r\n        <% var emailEndHighlightIndex = emailStartHighlightIn" +
"dex >= 0 ? emailStartHighlightIndex + searchStr.length : -1; %>        \r\n       " +
" <a href=\"mailto:user.Email\"><%= email.substring(0, emailStartHighlightIndex) %>" +
"<span class=\"highlight\"><%= email.substring(emailStartHighlightIndex, emailEndHi" +
"ghlightIndex) %></span><%= email.substring(emailEndHighlightIndex, email.length)" +
" %></a>\r\n        <br />\r\n        <span id=\"user-phone\"><%= phone %></span>\r\n    " +
"</td>\r\n\r\n    <td class=\"col-xs-2 ci-user-site-block highlightable\">\r\n        <ul" +
" class=\"list-unstyled\">\r\n            <% _.each(sites, function (site) { %>\r\n    " +
"            <li><%= site.name %></li>\r\n            <% }); %>\r\n        </ul>\r\n   " +
" </td>\r\n\r\n    <td class=\"col-xs-2 highlightable\">\r\n        <div class=\"label lab" +
"el-success\"><%= customerUserRole.name %></div>\r\n    </td>\r\n\r\n    <td class=\"col-" +
"xs-3 highlightable\">\r\n        <input type=\"checkbox\" class=\"basic-checkbox js-to" +
"ggle-user\" <%= JSON.parse(isEnabled) ? \'checked\' : \'\' %>>\r\n    </td>\r\n\r\n    <td " +
"class=\"col-xs-1\">\r\n        <a href=\"/Settings/Users/<%= id %>/Edit\" class=\"js-li" +
"nk\">");

            
            #line 38 "..\..\Areas\Customer\Views\Settings\Templates\UserItemView.cshtml"
                                                            Write(GlobalStrings.Customer_Users_List_EditExistingLink);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n    </td>\r\n\r\n</script>");

        }
    }
}
#pragma warning restore 1591
