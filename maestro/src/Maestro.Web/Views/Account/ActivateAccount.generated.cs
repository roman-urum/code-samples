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
    
    #line 1 "..\..\Views\Account\ActivateAccount.cshtml"
    using Maestro.Web.Models.Users;
    
    #line default
    #line hidden
    using Maestro.Web.Resources;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/ActivateAccount.cshtml")]
    public partial class _Views_Account_ActivateAccount_cshtml : Maestro.Web.BaseViewPage<ActivateAccountViewModel>
    {
        public _Views_Account_ActivateAccount_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Account\ActivateAccount.cshtml"
  
    ViewBag.Title = "Activate account";
    Layout = "~/Views/Shared/_PublicLayout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n        <!-- PAGE HEADER / BREADCRUMBS -->\r\n        <h1");

WriteLiteral(" class=\"page-header-ci\"");

WriteLiteral(">");

            
            #line 12 "..\..\Views\Account\ActivateAccount.cshtml"
                              Write(GlobalStrings.Users_Login_PageTitle);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n        <!-- END PAGE HEADER / BREADCRUMBS -->\r\n        <!-- MAIN CONTENT " +
"- SITE ADMIN -->\r\n        <div");

WriteLiteral(" class=\"ci-content\"");

WriteLiteral(">\r\n");

            
            #line 16 "..\..\Views\Account\ActivateAccount.cshtml"
            
            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\Account\ActivateAccount.cshtml"
             using (Html.BeginForm("ActivateAccount", "Account", FormMethod.Post, new { @class = "ci-activate-account-form" }))
            {
                
            
            #line default
            #line hidden
            
            #line 18 "..\..\Views\Account\ActivateAccount.cshtml"
           Write(Html.HiddenFor(m => m.Email));

            
            #line default
            #line hidden
            
            #line 18 "..\..\Views\Account\ActivateAccount.cshtml"
                                             
                
            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\Account\ActivateAccount.cshtml"
           Write(Html.HiddenFor(m => m.Token));

            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\Account\ActivateAccount.cshtml"
                                             
                
            
            #line default
            #line hidden
            
            #line 20 "..\..\Views\Account\ActivateAccount.cshtml"
           Write(Html.HiddenFor(m => m.Expires));

            
            #line default
            #line hidden
            
            #line 20 "..\..\Views\Account\ActivateAccount.cshtml"
                                               
                
            
            #line default
            #line hidden
            
            #line 21 "..\..\Views\Account\ActivateAccount.cshtml"
           Write(Html.HiddenFor(m => m.PasswordExpiration));

            
            #line default
            #line hidden
            
            #line 21 "..\..\Views\Account\ActivateAccount.cshtml"
                                                          

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-sm-2 ci-text\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 24 "..\..\Views\Account\ActivateAccount.cshtml"
                   Write(Html.LabelFor(model => model.Password, new { @class = "strong" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"col-xs-10\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 27 "..\..\Views\Account\ActivateAccount.cshtml"
                   Write(Html.PasswordFor(model => model.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        <div");

WriteLiteral(" class=\"input-sub-note x-small error-state\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 29 "..\..\Views\Account\ActivateAccount.cshtml"
                       Write(Html.ValidationMessageFor(model => model.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </d" +
"iv>\r\n");

            
            #line 33 "..\..\Views\Account\ActivateAccount.cshtml"


            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-sm-2 ci-text\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 36 "..\..\Views\Account\ActivateAccount.cshtml"
                   Write(Html.LabelFor(model => model.ConfirmPassword, new { @class = "strong" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"col-xs-10\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 39 "..\..\Views\Account\ActivateAccount.cshtml"
                   Write(Html.PasswordFor(model => model.ConfirmPassword));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                        <div");

WriteLiteral(" class=\"input-sub-note x-small error-state\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 42 "..\..\Views\Account\ActivateAccount.cshtml"
                       Write(Html.ValidationMessageFor(model => model.ConfirmPassword));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n\r\n                        <div");

WriteLiteral(" class=\"x-small error-state\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 46 "..\..\Views\Account\ActivateAccount.cshtml"
                       Write(Html.ValidationMessage(ActivateAccountViewModel.IncorrectCredentialsKey));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </d" +
"iv>\r\n");

            
            #line 50 "..\..\Views\Account\ActivateAccount.cshtml"


            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-5 text-right\"");

WriteLiteral(">\r\n                        <button");

WriteLiteral(" id=\"btn-sign-in\"");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(">");

            
            #line 53 "..\..\Views\Account\ActivateAccount.cshtml"
                                                                                  Write(GlobalStrings.Users_ActivateAccount_Reset);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n                    </div>\r\n                </div>\r\n");

            
            #line 56 "..\..\Views\Account\ActivateAccount.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n        <!-- END MAIN CONTENT -->\r\n    </div>\r\n</div>\r\n\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 64 "..\..\Views\Account\ActivateAccount.cshtml"
Write(Scripts.Render("~/bundles/js/jqueryval"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 65 "..\..\Views\Account\ActivateAccount.cshtml"
Write(Scripts.Render("~/bundles/js/activate-account"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591