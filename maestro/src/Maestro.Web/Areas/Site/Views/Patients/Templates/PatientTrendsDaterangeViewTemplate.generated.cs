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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/PatientTrendsDaterangeViewTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates_PatientTrendsDaterangeViewTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_PatientTrendsDaterangeViewTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"patientTrendsDaterangeViewTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(@">

    <div class=""form-group"">
        <label>Date Range:</label><br>
        <select class=""form-control input-sm js-daterange-predefined daterange-predefined"">
            <option value=""7"">Last 1 week</option>
            <option value=""14"">Last 2 weeks</option>
            <option value=""28"">Last 4 weeks</option>
            <option value=""84"">Last 12 weeks</option>
            <option value=""172"">Last 6 months</option>
            <option value=""355"">Last 12 months</option>
            <option value=""0"">Custom range</option>
        </select>
        <span class=""custom-daterange-quick-access"">
            <a href="""" class=""js-custom-daterange-enable"">Custom</a>
        </span>
        <span class=""custom-daterange"">
            <input type=""text"" class=""form-control input-sm js-daterange-start"" value="""">
            <span class=""input-group-addon"">to</span>
            <input type=""text"" class=""form-control input-sm js-daterange-end"" value="""">
            <button class=""btn btn-sm btn-default js-custom-daterange-save"">Update</button>
        </span>
    </div>
</script>");

        }
    }
}
#pragma warning restore 1591
