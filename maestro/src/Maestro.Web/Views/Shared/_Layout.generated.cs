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
    using Maestro.Web.Controls;
    using Maestro.Web.Resources;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_Layout.cshtml")]
    public partial class _Views_Shared__Layout_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Views_Shared__Layout_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Shared\_Layout.cshtml"
  
    Layout = null;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<!DOCTYPE html>\r\n<html");

WriteLiteral(" lang=\"en\"");

WriteLiteral(">\r\n<head>\r\n    <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" http-equiv=\"X-UA-Compatible\"");

WriteLiteral(" content=\"IE=edge\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1\"");

WriteLiteral(">\r\n    <title>");

            
            #line 11 "..\..\Views\Shared\_Layout.cshtml"
      Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n");

WriteLiteral("    ");

            
            #line 12 "..\..\Views\Shared\_Layout.cshtml"
Write(Styles.Render("~/Content/css/fonts.css"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 13 "..\..\Views\Shared\_Layout.cshtml"
Write(Styles.Render("~/bundles/css"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <!--[if lt IE 9]>\r\n    <script src=\"https://oss.maxcdn.com/html5shiv/3.7.2/" +
"html5shiv.min.js\"></script>\r\n    <script src=\"https://oss.maxcdn.com/respond/1.4" +
".2/respond.min.js\"></script>\r\n    <![endif]-->\r\n</head>\r\n<body>\r\n");

            
            #line 20 "..\..\Views\Shared\_Layout.cshtml"
    
            
            #line default
            #line hidden
            
            #line 20 "..\..\Views\Shared\_Layout.cshtml"
     if (User.Identity.IsAuthenticated)
    {
        
            
            #line default
            #line hidden
            
            #line 22 "..\..\Views\Shared\_Layout.cshtml"
   Write(Html.Action("PasswordExpirationModal", "Account", new { area = string.Empty }));

            
            #line default
            #line hidden
            
            #line 22 "..\..\Views\Shared\_Layout.cshtml"
                                                                                       
        
            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\Shared\_Layout.cshtml"
   Write(Html.Partial("_SessionExpirationModal"));

            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\Shared\_Layout.cshtml"
                                                
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 26 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("    ");

            
            #line 28 "..\..\Views\Shared\_Layout.cshtml"
Write(Html.Partial("_GlobalStrings"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 29 "..\..\Views\Shared\_Layout.cshtml"
    
            
            #line default
            #line hidden
            
            #line 29 "..\..\Views\Shared\_Layout.cshtml"
     if (ViewBag.IsRequireJsEnabled == null || !ViewBag.IsRequireJsEnabled)
    {
        
            
            #line default
            #line hidden
            
            #line 31 "..\..\Views\Shared\_Layout.cshtml"
   Write(Scripts.Render("~/bundles/js/common"));

            
            #line default
            #line hidden
            
            #line 31 "..\..\Views\Shared\_Layout.cshtml"
                                              
    }
    else
    {

            
            #line default
            #line hidden
WriteLiteral("        <script");

WriteLiteral(" src=\"/Content/js/libs/require.min.js\"");

WriteLiteral("></script>\r\n");

WriteLiteral("        <script");

WriteLiteral(" src=\"/Content/js/config.js\"");

WriteLiteral("></script>\r\n");

            
            #line 37 "..\..\Views\Shared\_Layout.cshtml"

        if (!string.IsNullOrEmpty(ViewBag.AppId))
        {
            string appId = HttpContext.Current.IsDebuggingEnabled ?
                string.Format("{0}.js?ver={1}", ViewBag.AppId, DateTime.Now.ToString("HH:mm:ss.ffff")) :
                string.Format("{0}.min.js?ver={1}", ViewBag.AppId, typeof(MvcApplication).Assembly.GetName().Version);


            
            #line default
            #line hidden
WriteLiteral("            <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1486), Tuple.Create("\"", 1532)
            
            #line 44 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1492), Tuple.Create<System.Object, System.Int32>(string.Format("/Content/js/{0}", appId)
            
            #line default
            #line hidden
, 1492), false)
);

WriteLiteral("></script>\r\n");

            
            #line 45 "..\..\Views\Shared\_Layout.cshtml"
        }
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 48 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderSection("scripts", required: false));

            
            #line default
            #line hidden
WriteLiteral("\r\n</body>\r\n</html>");

        }
    }
}
#pragma warning restore 1591
