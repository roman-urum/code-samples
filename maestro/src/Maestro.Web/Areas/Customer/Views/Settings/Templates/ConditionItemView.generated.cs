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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/Settings/Templates/ConditionItemView.cshtml")]
    public partial class _Areas_Customer_Views_Settings_Templates_ConditionItemView_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_Settings_Templates_ConditionItemView_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"conditionItemView\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">

    <td class=""col-xs-4 ci-condition-name highlightable"">
        <%=name%>
    </td>

    <td class=""col-xs-5 highlightable"">
        <%=description%>
    </td>
   
    <td class=""col-xs-4 highlightable"">
        <%=tags%>
    </td>
   
    <td class=""col-xs-1"">
        <a href=""/Settings/Conditions/<%=id%>/Details"" class=""js-link"">Edit</a>
    </td>

</script>");

        }
    }
}
#pragma warning restore 1591
