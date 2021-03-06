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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/_PasswordExpirationModal.cshtml")]
    public partial class _Views_Account__PasswordExpirationModal_cshtml : Maestro.Web.BaseViewPage<TimeSpan>
    {
        public _Views_Account__PasswordExpirationModal_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"modal fade\"");

WriteLiteral(" id=\"password-expiration-modal\"");

WriteLiteral(" tabindex=\"-1\"");

WriteLiteral(" role=\"dialog\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n");

            
            #line 7 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
                
            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
                 if (Model.TotalDays > 1 && Model.TotalDays < 7)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <p>\r\n                        Your password will be expired in" +
" ");

            
            #line 10 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
                                                    Write(Math.Ceiling(Model.TotalDays));

            
            #line default
            #line hidden
WriteLiteral(" days. Do you want to change it now?\r\n                    </p>\r\n");

            
            #line 12 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
                }
                else if (Model.TotalHours > 1 && Model.TotalHours < 24)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <p>\r\n                        Your password will be expired in" +
" ");

            
            #line 16 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
                                                    Write(Math.Ceiling(Model.TotalHours));

            
            #line default
            #line hidden
WriteLiteral(" hours. Do you want to change it now?\r\n                    </p>\r\n");

            
            #line 18 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
                }
                else if (Model.TotalMinutes < 60)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <p>\r\n                        Your password will be expired in" +
" ");

            
            #line 22 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
                                                    Write(Math.Ceiling(Model.TotalMinutes));

            
            #line default
            #line hidden
WriteLiteral(" minutes. Do you want to change it now?\r\n                    </p>\r\n");

            
            #line 24 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </div>\r\n            <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 1129), Tuple.Create("\"", 1205)
            
            #line 27 "..\..\Views\Account\_PasswordExpirationModal.cshtml"
, Tuple.Create(Tuple.Create("", 1136), Tuple.Create<System.Object, System.Int32>(Url.Action("ChangePassword", "Account", new { area = string.Empty })
            
            #line default
            #line hidden
, 1136), false)
);

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" id=\"submit-customer\"");

WriteLiteral(">\r\n                    Change\r\n                </a>\r\n                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(" id=\"cancel-button\"");

WriteLiteral(">\r\n                    Not now\r\n                </button>\r\n            </div>\r\n  " +
"      </div>\r\n    </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591
