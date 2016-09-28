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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/CareBuilder/Templates/MediaElementFormTemplate.cshtml")]
    public partial class _Areas_Customer_Views_CareBuilder_Templates_MediaElementFormTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_CareBuilder_Templates_MediaElementFormTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"mediaElementFormTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">
    <!-- Nav tabs -->
    <ul class=""nav nav-tabs nav-tabs-media-ci"" role=""tablist"">
        <li role=""presentation"">
            <a href=""#edit-existing-media"" aria-controls=""library"" role=""tab"" data-toggle=""tab"">Media Library</a>
        </li>
        <li role=""presentation"" class=""active"">
            <a href=""#create-new-media"" aria-controls=""createNew"" role=""tab"" data-toggle=""tab"">Create New</a>
        </li>
    </ul>
    
    <!-- Tab panes -->
    <div class=""tab-content"">
        <div role=""tabpanel"" class=""tab-pane"" id=""edit-existing-media"">

        </div>
        <div role=""tabpanel"" class=""tab-pane active"" id=""create-new-media"">
            <p>Creating a new file will add it to the Media Library for future use.</p>
            <div id=""media-file-container""></div>
        </div>
    </div>
</script>");

        }
    }
}
#pragma warning restore 1591