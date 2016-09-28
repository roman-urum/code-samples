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
    
    #line 1 "..\..\Areas\Customer\Views\CareBuilder\Templates\QuestionModalScaleAnswerChoiceTemplate.cshtml"
    using Roles = Maestro.Domain.Constants.Roles;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/CareBuilder/Templates/QuestionModalScaleAnswerChoiceTempla" +
        "te.cshtml")]
    public partial class _Areas_Customer_Views_CareBuilder_Templates_QuestionModalScaleAnswerChoiceTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_CareBuilder_Templates_QuestionModalScaleAnswerChoiceTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"questionModalScaleAnswerChoiceTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">
<div class=""row"">
    <div class=""col-sm-12"">
        <strong>Value:</strong> <%=value%>    
        <div class=""row row-value-ci form-horizontal"">
            <div class=""col-sm-6"">
                <div class=""form-group form-group-sm"">
                    <label class=""col-sm-3 control-label text-left"">ID:</label>
                    <div class=""col-sm-9"">
                        <input class=""form-control""
                               type=""text"" 
                               name=""externalId""
                               value=""<%=externalId%>""
                               placeholder=""Enter ID here""
                               data-name=""externalId"" />
                    </div>
                </div>
            </div>
            <div class=""col-sm-6"">
");

            
            #line 21 "..\..\Areas\Customer\Views\CareBuilder\Templates\QuestionModalScaleAnswerChoiceTemplate.cshtml"
                
            
            #line default
            #line hidden
            
            #line 21 "..\..\Areas\Customer\Views\CareBuilder\Templates\QuestionModalScaleAnswerChoiceTemplate.cshtml"
                 if (User.IsInRole(Roles.SuperAdmin))
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"form-group form-group-sm\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-sm-3 control-label offset-left-0 text-left\"");

WriteLiteral(">CI ID:</label>\r\n                    <div");

WriteLiteral(" class=\"col-sm-9\"");

WriteLiteral(">\r\n                        <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral("\r\n                               type=\"text\"");

WriteLiteral(" \r\n                               name=\"internalId\"");

WriteLiteral("\r\n                               value=\"<%=internalId%>\"");

WriteLiteral("\r\n                               placeholder=\"Enter CI ID here\"");

WriteLiteral("\r\n                               data-name=\"internalId\"");

WriteLiteral(" />\r\n                    </div>\r\n                </div>\r\n");

            
            #line 34 "..\..\Areas\Customer\Views\CareBuilder\Templates\QuestionModalScaleAnswerChoiceTemplate.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral(@"            </div>
        </div>

        <div class=""row form-horizontal"">
            <div class=""col-sm-6"">
                <div class=""form-group form-group-sm"">
                    <label class=""col-sm-3 control-label text-left"">Score:</label>
                    <div class=""col-sm-9"">
                        <input class=""form-control""
                               type=""text"" 
                               name=""externalScore""
                               value=""<%=externalScore%>""
                               placeholder=""Enter Score Here"" 
                               data-name=""externalScore""
                               id=""external-score_<%=value%>"" />
                        <span class=""help-block hidden"" data-validate-for=""external-score_<%=value%>""></span>
                    </div>
                </div>
            </div>
            <div class=""col-sm-6"">
");

            
            #line 55 "..\..\Areas\Customer\Views\CareBuilder\Templates\QuestionModalScaleAnswerChoiceTemplate.cshtml"
                
            
            #line default
            #line hidden
            
            #line 55 "..\..\Areas\Customer\Views\CareBuilder\Templates\QuestionModalScaleAnswerChoiceTemplate.cshtml"
                 if (User.IsInRole(Roles.SuperAdmin))
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"form-group form-group-sm\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-sm-3 control-label offset-left-0 text-left\"");

WriteLiteral(">CI Score:</label>\r\n                    <div");

WriteLiteral(" class=\"col-sm-9\"");

WriteLiteral(">\r\n                        <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral("\r\n                               type=\"text\"");

WriteLiteral(" \r\n                               name=\"internalScore\"");

WriteLiteral("\r\n                               value=\"<%=internalScore%>\"");

WriteLiteral("\r\n                               placeholder=\"Enter Score Here\"");

WriteLiteral("\r\n                               data-name=\"internalScore\"");

WriteLiteral("\r\n                               id=\"internal-score_<%=value%>\"");

WriteLiteral(" />\r\n                        <span");

WriteLiteral(" class=\"help-block hidden\"");

WriteLiteral(" data-validate-for=\"external-score_<%=value%>\"");

WriteLiteral("></span>\r\n                    </div>\r\n                </div>\r\n");

            
            #line 70 "..\..\Areas\Customer\Views\CareBuilder\Templates\QuestionModalScaleAnswerChoiceTemplate.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n</script>");

        }
    }
}
#pragma warning restore 1591
