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
    
    #line 1 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
    using Newtonsoft.Json;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/CareBuilder/Templates.cshtml")]
    public partial class _Areas_Customer_Views_CareBuilder_Templates_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_CareBuilder_Templates_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
  
    ViewBag.AppId = "appCareBuilder";

    var constants = Model != null ? JsonConvert.SerializeObject(Model) : string.Empty;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<input");

WriteLiteral(" id=\"constants\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 215), Tuple.Create("\"", 233)
            
            #line 10 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
, Tuple.Create(Tuple.Create("", 223), Tuple.Create<System.Object, System.Int32>(constants
            
            #line default
            #line hidden
, 223), false)
);

WriteLiteral("/>\r\n\r\n<div");

WriteLiteral(" id=\"care-builder-container\"");

WriteLiteral(">\r\n    <img");

WriteLiteral(" src=\"/Content/img/spinner.gif\"");

WriteLiteral(" class=\"spinner\"");

WriteLiteral(">\r\n</div>\r\n\r\n");

            
            #line 16 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/CareBuilderTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 17 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ScaleAnswerSetListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 18 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ScaleAnswersControlTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 19 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/SelectionAnswerSetListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 20 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/SelectionAnswerSetListItemBodyTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 21 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/QuestionElementListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 22 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/QuestionElementSelectionAnswersTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 23 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/QuestionElementScaleAnswersTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 24 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/AnswerSetTypeTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 25 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ContentTypeTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 26 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/SelectionAnswerSetFormTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 27 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/SelectionAnswerChoiceTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 28 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ScaleAnswerSetFormTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 29 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/SelectionAnswerSetFormTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 30 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/AddQuestionTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 31 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/SelectAnswerSetTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 32 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/SelectionAnswerSetTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 33 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/SelectionAnswerSetAnswersTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 34 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/OpenEndedAnswerSetTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 35 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/OpenEndedAnswerSetListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 36 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ChecklistAnswerSetTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 37 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ScaleAnswerSetTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 38 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/QuestionModalSelectionAnswerSetTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 39 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/QuestionModalSelectionAnswerChoiceTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 40 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/QuestionModalScaleAnswerSetTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 41 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/QuestionModalScaleAnswerChoiceTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 42 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/QuestionModalOpenEndedAnswerSetTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 43 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/TextMediaElementTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 44 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/TextMediaTemplateMediaButtons"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 45 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/TextMediaTemplateMediaPreview"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 46 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/TextMediaElementListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 47 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/TextMediaElementItemMediaTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 48 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/MediaElementFormTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 49 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/MediaFileTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 50 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/MediaFormMediaLibraryTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 51 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/MediaListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 52 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 53 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolTreeQuestionItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 54 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolTreeAnswerChoiceItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 55 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolTreeAnswerScaleItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 56 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolTreeAnswerOpenEndedTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 57 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolTreeMeasurementBranchTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 58 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolQuestionListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 59 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolMeasurementListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 60 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolAssessmentListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 61 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolTextMediaElementListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 62 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolViewTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 63 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProtocolSimulatorTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 64 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/MeasurementBranchModalTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 65 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/MeasurementThresholdTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 66 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProgramListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 67 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProgramViewTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 68 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProgramWeekTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 69 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProgramDayTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 70 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProgramProtocolListItemTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 71 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/ProgramProtocolTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 72 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/ProtocolsAndPrograms/RecurrenceModalTemplate"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 73 "..\..\Areas\Customer\Views\CareBuilder\Templates.cshtml"
Write(Html.Partial("Templates/AudioOptionsTemplate"));

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591