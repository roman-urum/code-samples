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
    
    #line 1 "..\..\Views\Customers\_CustomersList.cshtml"
    using Maestro.Web.Models.Customers;
    
    #line default
    #line hidden
    using Maestro.Web.Resources;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Customers/_CustomersList.cshtml")]
    public partial class _Views_Customers__CustomersList_cshtml : Maestro.Web.BaseViewPage<CustomerListViewModel>
    {
        public _Views_Customers__CustomersList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-xs-12 ci-user-list\"");

WriteLiteral(">\r\n        \r\n");

            
            #line 7 "..\..\Views\Customers\_CustomersList.cshtml"
        
            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Customers\_CustomersList.cshtml"
         foreach (var customer in Model.Customers)
        {
            if (customer is SingleSiteViewModel)
            {
                
            
            #line default
            #line hidden
            
            #line 11 "..\..\Views\Customers\_CustomersList.cshtml"
           Write(Html.Partial("_SingleSiteRow", customer as SingleSiteViewModel));

            
            #line default
            #line hidden
            
            #line 11 "..\..\Views\Customers\_CustomersList.cshtml"
                                                                                
            }
            else if (customer is MultipleSitesViewModel)
            {
                
            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\Customers\_CustomersList.cshtml"
           Write(Html.Partial("_MultipleSitesList", customer as MultipleSitesViewModel));

            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\Customers\_CustomersList.cshtml"
                                                                                       
            }
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" class=\"user-list-counter small\"");

WriteLiteral(" id=\"search-result-sites-count\"");

WriteLiteral(">");

            
            #line 22 "..\..\Views\Customers\_CustomersList.cshtml"
                                                               Write(Model.SitesTotalCount);

            
            #line default
            #line hidden
WriteLiteral(" Sites Shown</div>");

        }
    }
}
#pragma warning restore 1591
