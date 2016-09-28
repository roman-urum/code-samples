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
    
    #line 1 "..\..\Views\Customers\Index.cshtml"
    using Maestro.Web.Models.Customers;
    
    #line default
    #line hidden
    using Maestro.Web.Resources;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Customers/Index.cshtml")]
    public partial class _Views_Customers_Index_cshtml : Maestro.Web.BaseViewPage<CustomerListViewModel>
    {
        public _Views_Customers_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Customers\Index.cshtml"
  
    ViewBag.Title = "Administration";
    Layout = "~/Views/Shared/_PublicLayout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<!-- PAGE HEADER / BREADCRUMBS -->\r\n<h1");

WriteLiteral(" class=\"page-header-ci\"");

WriteLiteral(">");

            
            #line 10 "..\..\Views\Customers\Index.cshtml"
                      Write(GlobalStrings.Customers_Search_PageTitle);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n<!-- END PAGE HEADER / BREADCRUMBS -->\r\n\r\n<div");

WriteLiteral(" class=\"ci-content\"");

WriteLiteral(">\r\n");

            
            #line 14 "..\..\Views\Customers\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 14 "..\..\Views\Customers\Index.cshtml"
     if (User.IsInRole(Maestro.Domain.Constants.Roles.SuperAdmin))
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 18 "..\..\Views\Customers\Index.cshtml"
           Write(GlobalStrings.Customers_Search_ChangeSettingsAssumption);

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 19 "..\..\Views\Customers\Index.cshtml"
           Write(Html.ActionLink(@GlobalStrings.Customers_Search_ChangeSettingsLink, "Index", new {area = "Settings", controller = "Admins"}));

            
            #line default
            #line hidden
WriteLiteral(" \r\n            </div>\r\n        </div>\r\n");

            
            #line 22 "..\..\Views\Customers\Index.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    \r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-sm-3\"");

WriteLiteral(">\r\n            <h2");

WriteLiteral(" class=\"ci-page-subheader\"");

WriteLiteral(">");

            
            #line 26 "..\..\Views\Customers\Index.cshtml"
                                     Write(GlobalStrings.Customers_Search_SitesListTitle);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n        </div>\r\n        \r\n");

            
            #line 29 "..\..\Views\Customers\Index.cshtml"
        
            
            #line default
            #line hidden
            
            #line 29 "..\..\Views\Customers\Index.cshtml"
         if (User.IsInRole(Maestro.Domain.Constants.Roles.SuperAdmin))
        {

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"col-sm-9 subhead-add-link\"");

WriteLiteral(">\r\n                <a");

WriteLiteral(" class=\"add-cust\"");

WriteLiteral(" href=\"#\"");

WriteLiteral(" data-toggle=\"modal\"");

WriteLiteral(">");

            
            #line 32 "..\..\Views\Customers\Index.cshtml"
                                                            Write(GlobalStrings.Customers_Search_AddCustomerButton);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n            </div>\r\n");

            
            #line 34 "..\..\Views\Customers\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </div>\r\n    \r\n    <!-- SEARCH FILTER -->\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n            <p>");

            
            #line 40 "..\..\Views\Customers\Index.cshtml"
          Write(GlobalStrings.Customers_Search_SelectCustomerAssumption);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n            <form");

WriteLiteral(" class=\"site-filter form-inline\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(">\r\n                        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control filter-input\"");

WriteAttribute("placeholder", Tuple.Create(" placeholder=\"", 1613), Tuple.Create("\"", 1681)
            
            #line 44 "..\..\Views\Customers\Index.cshtml"
          , Tuple.Create(Tuple.Create("", 1627), Tuple.Create<System.Object, System.Int32>(GlobalStrings.Customers_Search_SearchFieldPlaceholder
            
            #line default
            #line hidden
, 1627), false)
);

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral("><a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"submit-site-filter\"");

WriteLiteral("><span");

WriteLiteral(" class=\"glyphicon glyphicon-search\"");

WriteLiteral("></span></a></div>\r\n                    </div>\r\n                </div>\r\n         " +
"   </form>\r\n        </div>\r\n    </div>\r\n    <!-- END SEARCH FILTER -->\r\n    \r\n  " +
"  <!-- USER LIST -->\r\n");

WriteLiteral("    ");

            
            #line 54 "..\..\Views\Customers\Index.cshtml"
Write(Html.Partial("_CustomersList", Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <!-- END USER LIST -->\r\n</div>\r\n\r\n");

            
            #line 58 "..\..\Views\Customers\Index.cshtml"
Write(Html.Partial("CreateCustomer", new CreateCustomerViewModel()));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 63 "..\..\Views\Customers\Index.cshtml"
Write(Scripts.Render("~/bundles/js/customerslist"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 64 "..\..\Views\Customers\Index.cshtml"
Write(Scripts.Render("~/bundles/js/jqueryval"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
