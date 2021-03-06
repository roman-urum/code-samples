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
    using Maestro.Web.Controls;
    using Maestro.Web.Resources;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Settings/Views/Admins/Create.cshtml")]
    public partial class _Areas_Settings_Views_Admins_Create_cshtml : System.Web.Mvc.WebViewPage<Maestro.Web.Models.Users.UserViewModel>
    {

#line 76 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
public System.Web.WebPages.HelperResult DisplayInnerExceptions(Exception exception)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 77 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
 
if (exception != null)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"error-state\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 80 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
WriteTo(__razor_helper_writer, exception.Message);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, ": ");


#line 80 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   WriteTo(__razor_helper_writer, exception.StackTrace);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</div>\r\n");


#line 81 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
    

#line default
#line hidden

#line 81 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
WriteTo(__razor_helper_writer, DisplayInnerExceptions(exception.InnerException));


#line default
#line hidden

#line 81 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                                                     
}



#line default
#line hidden
});

#line 84 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
}
#line default
#line hidden

        public _Areas_Settings_Views_Admins_Create_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
  
    ViewBag.Title = "Create";
    Layout = "~/Areas/Settings/Views/Shared/_AdminLayout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"tab-pane active\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" id=\"create-admin-user\"");

WriteLiteral(">\r\n");

            
            #line 10 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
        
            
            #line default
            #line hidden
            
            #line 10 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
         using (Html.BeginForm("Create", "Admins"))
        {

            
            #line default
            #line hidden
WriteLiteral("            <h3>User Information</h3>\r\n");

            
            #line 13 "..\..\Areas\Settings\Views\Admins\Create.cshtml"


            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 17 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.CheckBoxFor(model => model.IsEnabled, new { @class = "basic-checkbox", data_on_text = "ENABLED", data_off_text = "DISABLED" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");

            
            #line 21 "..\..\Areas\Settings\Views\Admins\Create.cshtml"


            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 25 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.LabelFor(model => model.FirstName));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 26 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.TextBoxFor(model => model.FirstName, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 27 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.ValidationMessageFor(model => model.FirstName));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 32 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.LabelFor(model => model.LastName));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 33 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.TextBoxFor(model => model.LastName, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 34 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.ValidationMessageFor(model => model.LastName));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");

            
            #line 38 "..\..\Areas\Settings\Views\Admins\Create.cshtml"


            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 42 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.LabelFor(model => model.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 43 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.TextBoxFor(model => model.Email, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 44 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.ValidationMessageFor(model => model.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 49 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.LabelFor(model => model.Phone));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 50 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.TextBoxFor(model => model.Phone, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 51 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.ValidationMessageFor(model => model.Phone));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");

            
            #line 55 "..\..\Areas\Settings\Views\Admins\Create.cshtml"


            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 59 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.LabelFor(model => model.Role));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 60 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                   Write(Html.ValueFor(model => model.Role));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");

            
            #line 65 "..\..\Areas\Settings\Views\Admins\Create.cshtml"


            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" id=\"save-new-admin\"");

WriteLiteral(">");

            
            #line 68 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                                                                                 Write(GlobalStrings.Edit_Customer_SaveButtonText);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-link\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(" id=\"cancel-new-admin\"");

WriteAttribute("href", Tuple.Create(" href=\"", 2917), Tuple.Create("\"", 2954)
            
            #line 69 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                                , Tuple.Create(Tuple.Create("", 2924), Tuple.Create<System.Object, System.Int32>(Url.Action("Index", "Admins")
            
            #line default
            #line hidden
, 2924), false)
);

WriteLiteral(">");

            
            #line 69 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
                                                                                                                                           Write(GlobalStrings.Edit_Customer_CancelButtonText);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n                </div>\r\n            </div>\r\n");

            
            #line 72 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </div>\r\n</div>\r\n\r\n");

WriteLiteral("\r\n");

DefineSection("Scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 87 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
Write(Scripts.Render("~/bundles/js/jqueryval"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 88 "..\..\Areas\Settings\Views\Admins\Create.cshtml"
Write(Scripts.Render("~/bundles/js/create-admin"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
