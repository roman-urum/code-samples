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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/Settings/Templates/SiteItemView.cshtml")]
    public partial class _Areas_Customer_Views_Settings_Templates_SiteItemView_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_Settings_Templates_SiteItemView_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"siteItemView\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">

    <div class=""org-head-overlay-container site-item"">
        <div class=""org-head"">
            <span class=""site-icon fa-stack fa-lg <%= isActive ? 'site-enabled' : 'site-disabled' %>"">
              <i class=""fa fa-circle fa-stack-2x""></i>
              <i class=""fa fa-laptop fa-stack-1x fa-inverse""></i>
            </span>
                <span class=""name""><%= name %></span>
            <span class=""actions"">
                <span class=""actions-item"">
                    <a href="""" class=""js-site-edit"">site properties</a>
                </span>
            </span>
        </div>
        <div class=""node-overlay"" style=""display: none;""></div>
    </div>

</script>");

        }
    }
}
#pragma warning restore 1591
