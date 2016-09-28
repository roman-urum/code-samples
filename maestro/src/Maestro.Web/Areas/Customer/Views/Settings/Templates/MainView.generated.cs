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
    
    #line 1 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
    using Maestro.Domain.Enums;
    
    #line default
    #line hidden
    using Maestro.Web;
    using Maestro.Web.Areas.Customer;
    using Maestro.Web.Controls;
    using Maestro.Web.Resources;
    
    #line 2 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
    using Roles = Maestro.Domain.Constants.Roles;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/Settings/Templates/MainView.cshtml")]
    public partial class _Areas_Customer_Views_Settings_Templates_MainView_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_Settings_Templates_MainView_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" id=\"mainView\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n    <div>\r\n        <ul class=\"nav nav-tabs main-settings-sections\">\r\n");

            
            #line 7 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            
            
            #line default
            #line hidden
            
            #line 7 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
             if (User.IsInRoles(Roles.SuperAdmin) ||
                User.HasPermissions(
                    CustomerContext.Current.Customer.Id,
                    CustomerUserRolePermissions.ViewCustomerSettings,
                    CustomerUserRolePermissions.ManageCustomerSettings
                )
            )
            {

            
            #line default
            #line hidden
WriteLiteral("                <li>\r\n                    <a");

WriteLiteral(" href=\"#content-general\"");

WriteLiteral(" id=\"tab-general\"");

WriteLiteral(" data-href=\"");

            
            #line 16 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                      Write(Url.Action("General", "Settings"));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(" class=\"js-link js-tab-link\"");

WriteLiteral(">");

            
            #line 16 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                                                                                      Write(GlobalStrings.Edit_Customer_CustomerSettingsTabTitle);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n                </li>\r\n");

            
            #line 18 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 20 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            
            
            #line default
            #line hidden
            
            #line 20 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
             if (User.IsInRoles(Roles.SuperAdmin) ||
                User.HasPermissions(
                    CustomerContext.Current.Customer.Id,
                    CustomerUserRolePermissions.ManageCustomerSites
                )
            )
            {

            
            #line default
            #line hidden
WriteLiteral("                <li>\r\n                    <a");

WriteLiteral(" href=\"#content-sites\"");

WriteLiteral(" id=\"tab-sites\"");

WriteLiteral(" data-href=\"");

            
            #line 28 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                  Write(Url.Action("Sites", "Settings"));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(" class=\"js-link js-tab-link\"");

WriteLiteral(">");

            
            #line 28 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                                                                                Write(GlobalStrings.Edit_Customer_SitesTabTitle);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n                </li>\r\n");

            
            #line 30 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 32 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            
            
            #line default
            #line hidden
            
            #line 32 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
             if (User.IsInRoles(Roles.SuperAdmin) ||
                User.HasPermissions(
                    CustomerContext.Current.Customer.Id,
                    CustomerUserRolePermissions.CreateCustomerUsers,
                    CustomerUserRolePermissions.ViewCustomerUsers,
                    CustomerUserRolePermissions.ManageCustomerUserDetails,
                    CustomerUserRolePermissions.ManageCustomerUserPassword,
                    CustomerUserRolePermissions.ManageCustomerUserPermissions
                )
            )
            {

            
            #line default
            #line hidden
WriteLiteral("                <li>\r\n                    <a");

WriteLiteral(" href=\"#content-users\"");

WriteLiteral(" id=\"tab-users\"");

WriteLiteral(" data-href=\"");

            
            #line 44 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                  Write(Url.Action("Users", "Settings"));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(" class=\"js-link js-tab-link\"");

WriteLiteral(">");

            
            #line 44 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                                                                                Write(GlobalStrings.Edit_Customer_UsersTabTitle);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n                </li>\r\n");

            
            #line 46 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 48 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            
            
            #line default
            #line hidden
            
            #line 48 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
             if (User.IsInRoles(Roles.SuperAdmin) ||
                User.HasPermissions(
                    CustomerContext.Current.Customer.Id,
                    CustomerUserRolePermissions.ManageCustomerThresholds
                )
            )
            {

            
            #line default
            #line hidden
WriteLiteral("                <li>\r\n                    <a");

WriteLiteral(" href=\"#content-thresholds\"");

WriteLiteral(" id=\"tab-thresholds\"");

WriteLiteral(" data-href=\"");

            
            #line 56 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                            Write(Url.Action("Thresholds", "Settings"));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(" class=\"js-link js-tab-link\"");

WriteLiteral(">");

            
            #line 56 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                                                                                               Write(GlobalStrings.Edit_Customer_ThresholdsTabTitle);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n                </li>\r\n");

            
            #line 58 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            \r\n");

            
            #line 60 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            
            
            #line default
            #line hidden
            
            #line 60 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
             if (User.IsInRoles(Roles.SuperAdmin) ||
                User.HasPermissions(
                    CustomerContext.Current.Customer.Id,
                    CustomerUserRolePermissions.ManageCustomerSettings // ToDo: Add separate permissions
                )
            )
            {

            
            #line default
            #line hidden
WriteLiteral("                <li>\r\n                    <a");

WriteLiteral(" href=\"#content-conditions\"");

WriteLiteral(" id=\"tab-conditions\"");

WriteLiteral(" data-href=\"");

            
            #line 68 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                            Write(Url.Action("Conditions", "Settings"));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(" class=\"js-link js-tab-link\"");

WriteLiteral(">");

            
            #line 68 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
                                                                                                                                               Write(GlobalStrings.Edit_Customer_ConditionsTabTitle);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n                </li>\r\n");

            
            #line 70 "..\..\Areas\Customer\Views\Settings\Templates\MainView.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral(@"        </ul>
        
        <div class=""tab-content"">
            <div id=""content-general"" class=""tab-pane""></div>
            <div id=""content-sites"" class=""tab-pane""></div>
            <div id=""content-users"" class=""tab-pane""></div>
            <div id=""content-thresholds"" class=""tab-pane""></div>
            <div id=""content-conditions"" class=""tab-pane""></div>
        </div>
    </div>
</script>");

        }
    }
}
#pragma warning restore 1591