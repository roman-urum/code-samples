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
    using Maestro.Web.Areas.Site;
    
    #line 1 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
    using Maestro.Web.Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/PatientCalendarHistoryTemplates.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates_PatientCalendarHistoryTemplates_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_PatientCalendarHistoryTemplates_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" id=\"oneTimeEventScheduledTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 4 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_OneTimeEventScheduledTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"oneTimeEventRescheduledTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 8 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_OneTimeEventRescheduledTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"oneTimeEventDeletedTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 12 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_OneTimeEventDeletedTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"recurrentEventScheduledTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 16 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_RecurrentEventScheduledTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"recurrentEventRescheduledTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 20 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_RecurrentEventRescheduledTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"recurrentEventDeletedTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 24 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_RecurrentEventDeletedTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"recurrentEventTerminatedTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 28 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_RecurrentEventTerminatedTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"programScheduledTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 32 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_ProgramScheduledTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"programDeletedTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 36 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_ProgramDeletedTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"programTerminatedTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 40 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_ProgramTerminatedTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"defaultSessionCreatedTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 44 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_DefaultSessionCreatedTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"defaultSessionUpdatedTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 48 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_DefaultSessionUpdatedTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<script");

WriteLiteral(" id=\"defaultSessionDeletedTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 52 "..\..\Areas\Site\Views\Patients\Templates\PatientCalendarHistoryTemplates.cshtml"
Write(Html.Raw(GlobalStrings.Patient_CalendarHistory_DefaultSessionDeletedTemplate));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>");

        }
    }
}
#pragma warning restore 1591
