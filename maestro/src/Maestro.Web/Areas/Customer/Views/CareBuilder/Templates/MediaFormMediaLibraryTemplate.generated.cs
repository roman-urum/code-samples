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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Customer/Views/CareBuilder/Templates/MediaFormMediaLibraryTemplate.cshtml" +
        "")]
    public partial class _Areas_Customer_Views_CareBuilder_Templates_MediaFormMediaLibraryTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Customer_Views_CareBuilder_Templates_MediaFormMediaLibraryTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"mediaLibraryTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n            <div class" +
"=\"well clearfix\">\r\n                <div class=\"row\">\r\n                    <div c" +
"lass=\"col-sm-4\">\r\n                        <select id=\"media-search-type\" class=\"" +
"form-control\" name=\"type\">\r\n                            <option value=\"Image\">Im" +
"age</option>\r\n                            <option value=\"Video\">Video</option>\r\n" +
"                            <option value=\"Document\">Document</option>\r\n        " +
"                </select>\r\n                    </div>\r\n                    <div " +
"class=\"col-sm-4\">\r\n                        <div class=\"form-group\">\r\n           " +
"                 <div class=\"input-group\">\r\n                                <sel" +
"ect id=\"media-search-tags\" data-placeholder=\"Search by Tags\" data-loading-placeh" +
"older=\"Loading...\" class=\"form-control chosen-select searching-tags\" name=\"name\"" +
"></select>\r\n                                <span class=\"input-group-btn\">\r\n    " +
"                                <button class=\"btn btn-default js-search-clear\" " +
"type=\"button\">\r\n                                        <span class=\"glyphicon g" +
"lyphicon-remove\"></span>\r\n                                    </button>\r\n       " +
"                         </span>\r\n                            </div>\r\n\r\n        " +
"                </div>\r\n                    </div>\r\n                    <div cla" +
"ss=\"col-sm-4\">\r\n                        <div class=\"form-group\">\r\n              " +
"              <div class=\"input-group\">\r\n                                <input " +
"id=\"media-search-keyword\" class=\"form-control\" type=\"text\" placeholder=\"Type key" +
"word here\">\r\n                                <div class=\"input-group-addon\"><spa" +
"n class=\"glyphicon glyphicon-search\"></span></div>\r\n                            " +
"</div>\r\n                        </div>\r\n                    </div>\r\n            " +
"    </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\"" +
">\r\n        <div class=\"col-xs-12 media-search-result\"></div>\r\n    </div>\r\n</scri" +
"pt>");

        }
    }
}
#pragma warning restore 1591