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
    
    #line 1 "..\..\Areas\Customer\Views\CareBuilder\Templates\ContentTypeTemplate.cshtml"
    using Maestro.Domain.Enums;
    
    #line default
    #line hidden
    using Maestro.Web;
    using Maestro.Web.Areas.Customer;
    using Maestro.Web.Controls;
    using Maestro.Web.Resources;
    
    #line 2 "..\..\Areas\Customer\Views\CareBuilder\Templates\ContentTypeTemplate.cshtml"
    using Roles = Maestro.Domain.Constants.Roles;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/CareBuilder/Templates/ContentTypeTemplate.cshtml")]
    public partial class _Areas_Customer_Views_CareBuilder_Templates_ContentTypeTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_CareBuilder_Templates_ContentTypeTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 4 "..\..\Areas\Customer\Views\CareBuilder\Templates\ContentTypeTemplate.cshtml"
  
    bool hasManageCareElementsPermissions = User.IsInRoles(Roles.SuperAdmin) || User.HasPermissions(CustomerContext.Current.Customer.Id, CustomerUserRolePermissions.ManageCareElements);
    bool hasManageHealthProtocolsPermissions = User.IsInRoles(Roles.SuperAdmin) || User.HasPermissions(CustomerContext.Current.Customer.Id, CustomerUserRolePermissions.ManageHealthProtocols);
    bool hasManageHealthProgramsPermissions = User.IsInRoles(Roles.SuperAdmin) || User.HasPermissions(CustomerContext.Current.Customer.Id, CustomerUserRolePermissions.ManageHealthPrograms);

    string careElementsButtonClass = hasManageCareElementsPermissions ? string.Empty : "disabled";
    string healthProtocolsButtonClass = hasManageHealthProtocolsPermissions ? string.Empty : "disabled";
    string healthProgramsButtonClass = hasManageHealthProgramsPermissions ? string.Empty : "disabled";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<script");

WriteLiteral(" id=\"contentTypeTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">
    <div class=""alert alert-info"" role=""alert"">
        Please, select the type of content you want to add.
    </div>

    <h4>Care Element</h4>

    <div class=""btn-group btn-group-justified"">
        <a role=""button"" href=""#"" class=""btn btn-default js-add-question ");

            
            #line 22 "..\..\Areas\Customer\Views\CareBuilder\Templates\ContentTypeTemplate.cshtml"
                                                                    Write(careElementsButtonClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n            <span class=\"glyphicon glyphicon-question-sign\"></span><br>\r\n    " +
"        Question\r\n        </a>\r\n\r\n        <a role=\"button\" href=\"#\" class=\"btn b" +
"tn-default js-add-answer-set ");

            
            #line 27 "..\..\Areas\Customer\Views\CareBuilder\Templates\ContentTypeTemplate.cshtml"
                                                                      Write(careElementsButtonClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n            <span class=\"glyphicon glyphicon-info-sign\"></span><br>\r\n        " +
"    Answer Set\r\n        </a>\r\n\r\n        <a role=\"button\" href=\"#\" class=\"btn btn" +
"-default js-add-text-and-media ");

            
            #line 32 "..\..\Areas\Customer\Views\CareBuilder\Templates\ContentTypeTemplate.cshtml"
                                                                          Write(careElementsButtonClass);

            
            #line default
            #line hidden
WriteLiteral(@""">
            <span class=""glyphicon glyphicon-edit""></span><br>
            Text &amp; Media
        </a>
    </div>

    <hr>

    <h4>Protocols and Programs</h4>

    <div class=""btn-group btn-group-justified"">
        <a role=""button"" href=""CreateProtocol"" class=""btn btn-default js-add-protocols-and-programs ");

            
            #line 43 "..\..\Areas\Customer\Views\CareBuilder\Templates\ContentTypeTemplate.cshtml"
                                                                                               Write(healthProtocolsButtonClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n            <span class=\"glyphicon glyphicon-check\"></span><br>\r\n            " +
"Protocol\r\n        </a>\r\n\r\n        <a role=\"button\" href=\"CreateProgram\" class=\"b" +
"tn btn-default js-add-protocols-and-programs ");

            
            #line 48 "..\..\Areas\Customer\Views\CareBuilder\Templates\ContentTypeTemplate.cshtml"
                                                                                              Write(healthProgramsButtonClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n            <span class=\"glyphicon glyphicon-list\"></span><br>\r\n            P" +
"rogram\r\n        </a>\r\n    </div>\r\n</script>");

        }
    }
}
#pragma warning restore 1591