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
    
    #line 1 "..\..\Views\Shared\_PublicHeader.cshtml"
    using Maestro.Web.Extensions;
    
    #line default
    #line hidden
    using Maestro.Web.Resources;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_PublicHeader.cshtml")]
    public partial class _Views_Shared__PublicHeader_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Views_Shared__PublicHeader_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<header");

WriteLiteral(" id=\"header\"");

WriteLiteral(">\r\n    <nav");

WriteLiteral(" class=\"navbar navbar-header-ci\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"navbar-header\"");

WriteLiteral(">\r\n                <a");

WriteLiteral(" class=\"navbar-header-logo-ci\"");

WriteAttribute("href", Tuple.Create(" href=\"", 218), Tuple.Create("\"", 327)
            
            #line 6 "..\..\Views\Shared\_PublicHeader.cshtml"
, Tuple.Create(Tuple.Create("", 225), Tuple.Create<System.Object, System.Int32>(User.Identity.IsAuthenticated ? Url.Action("Index", "Customers", new { area = string.Empty }) : "#"
            
            #line default
            #line hidden
, 225), false)
);

WriteLiteral("></a>\r\n            </div>\r\n\r\n");

            
            #line 9 "..\..\Views\Shared\_PublicHeader.cshtml"
            
            
            #line default
            #line hidden
            
            #line 9 "..\..\Views\Shared\_PublicHeader.cshtml"
             if (User.Identity.IsAuthenticated)
            {

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"navbar-collapse collapse\"");

WriteLiteral(">\r\n                    <!--ul class=\"nav navbar-nav navbar-nav-top-ci\">\r\n");

            
            #line 13 "..\..\Views\Shared\_PublicHeader.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 13 "..\..\Views\Shared\_PublicHeader.cshtml"
                          
                            string homeLinkClass = HttpContext.Current.Request.Url.IsRouteMatch("Index", "Customers") ? "active" : string.Empty;
                        
            
            #line default
            #line hidden
WriteLiteral("\r\n                        <li class=\"");

            
            #line 16 "..\..\Views\Shared\_PublicHeader.cshtml"
                              Write(homeLinkClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n                            <a href=\"");

            
            #line 17 "..\..\Views\Shared\_PublicHeader.cshtml"
                                Write(Url.Action("Index", "Customers", new { area = string.Empty} ));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"btn btn-link\">\r\n                                <i class=\"fa fa-home\"></" +
"i> Home\r\n                            </a>\r\n                        </li>\r\n      " +
"              </ul-->\r\n                    <ul");

WriteLiteral(" class=\"nav navbar-nav navbar-nav-user-ci navbar-right\"");

WriteLiteral(">\r\n                        <li");

WriteLiteral(" class=\"dropdown\"");

WriteLiteral(">\r\n                            <a");

WriteLiteral(" aria-expanded=\"false\"");

WriteLiteral(" aria-haspopup=\"true\"");

WriteLiteral(" role=\"button\"");

WriteLiteral(" data-toggle=\"dropdown\"");

WriteLiteral(" class=\"dropdown-toggle dropdown-toggle-user-ci\"");

WriteLiteral(" href=\"#\"");

WriteLiteral(">\r\n");

WriteLiteral("                                ");

            
            #line 25 "..\..\Views\Shared\_PublicHeader.cshtml"
                           Write(GlobalStrings.Layout_Header_Welcome);

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                                ");

            
            #line 26 "..\..\Views\Shared\_PublicHeader.cshtml"
                           Write(User.Identity.Name);

            
            #line default
            #line hidden
WriteLiteral("!\r\n                                <i");

WriteLiteral(" class=\"fa fa-angle-down\"");

WriteLiteral("></i>\r\n                            </a>\r\n                            <ul");

WriteLiteral(" class=\"dropdown-menu\"");

WriteLiteral(">\r\n                                <li>\r\n                                    <a");

WriteAttribute("href", Tuple.Create(" href=\"", 1738), Tuple.Create("\"", 1800)
            
            #line 31 "..\..\Views\Shared\_PublicHeader.cshtml"
, Tuple.Create(Tuple.Create("", 1745), Tuple.Create<System.Object, System.Int32>(Url.Action("Index", "Admins", new {area = "Settings"})
            
            #line default
            #line hidden
, 1745), false)
);

WriteLiteral(" class=\"btn btn-link\"");

WriteLiteral(">\r\n                                        Settings\r\n                            " +
"        </a>\r\n                                </li>\r\n                           " +
"     <li>\r\n");

WriteLiteral("                                    ");

            
            #line 36 "..\..\Views\Shared\_PublicHeader.cshtml"
                               Write(Html.ActionLink(GlobalStrings.Layout_Header_SignOutLink, "Logout", new { controller = "Account", area = string.Empty }, new { @class = "btn btn-link" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                </li>\r\n                            </ul>\r\n     " +
"                   </li>\r\n                        ");

WriteLiteral("\r\n                    </ul>\r\n                </div>\r\n");

            
            #line 47 "..\..\Views\Shared\_PublicHeader.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </nav>\r\n</header>");

        }
    }
}
#pragma warning restore 1591
