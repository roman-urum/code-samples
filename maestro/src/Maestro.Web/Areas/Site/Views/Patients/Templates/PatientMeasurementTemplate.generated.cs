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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Site/Views/Patients/Templates/PatientMeasurementTemplate.cshtml")]
    public partial class _Areas_Site_Views_Patients_Templates_PatientMeasurementTemplate_cshtml : Maestro.Web.BaseViewPage<dynamic>
    {
        public _Areas_Site_Views_Patients_Templates_PatientMeasurementTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"patientMeasurementTemplate\"");

WriteLiteral(" type=\"text/template\"");

WriteLiteral(">\r\n\t<tbody>\r\n\t\t<tr>\r\n\t\t\t<td>Weight Scale</td>\r\n\t\t\t<td colspan=\"2\">\r\n\t\t\t\t<div clas" +
"s=\"peripherals-capture\">Data capture type</div>\r\n\t\t\t\t<div data-toggle=\"buttons\" " +
"class=\"btn-group btn-group-peripherals-ci clearfix\">\r\n\t\t\t\t\t<label class=\"btn btn" +
"-default<%=(peripherals.weight == \'both\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input ty" +
"pe=\"radio\" value=\"both\" name=\"<%=id%>.peripherals.weight\">\r\n\t\t\t\t\t\tBoth Available" +
"\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripherals.weight == \'au" +
"to\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"auto\" name=\"<%=id%>" +
".peripherals.weight\">\r\n\t\t\t\t\t\tAutomatic Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"b" +
"tn btn-default<%=(peripherals.weight == \'manual\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<" +
"input type=\"radio\" value=\"manual\" name=\"<%=id%>.peripherals.weight\">\r\n\t\t\t\t\t\tManu" +
"al Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default btn-none-ci<%=(periph" +
"erals.weight == \'none\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"" +
"none\" name=\"<%=id%>.peripherals.weight\">\r\n\t\t\t\t\t\tNone\r\n\t\t\t\t\t</label>\r\n\t\t\t\t</div>\r" +
"\n\t\t\t</td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td>Blood Pressure Monitor</td>\r\n\t\t\t<td colspan=\"" +
"2\">\r\n\t\t\t\t<div class=\"peripherals-capture\">Data capture type</div>\r\n\t\t\t\t<div data" +
"-toggle=\"buttons\" class=\"btn-group btn-group-peripherals-ci clearfix\">\r\n\t\t\t\t\t<la" +
"bel class=\"btn btn-default<%=(peripherals.bloodPressure == \'both\' ? \' active\' : " +
"\'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"both\" name=\"<%=id%>.peripherals.blood" +
"Pressure\">\r\n\t\t\t\t\t\tBoth Available\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-defa" +
"ult<%=(peripherals.bloodPressure == \'auto\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input " +
"type=\"radio\" value=\"auto\" name=\"<%=id%>.peripherals.bloodPressure\">\r\n\t\t\t\t\t\tAutom" +
"atic Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripherals.bloo" +
"dPressure == \'manual\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"m" +
"anual\" name=\"<%=id%>.peripherals.bloodPressure\">\r\n\t\t\t\t\t\tManual Only\r\n\t\t\t\t\t</labe" +
"l>\r\n\t\t\t\t\t<label class=\"btn btn-default btn-none-ci<%=(peripherals.bloodPressure " +
"== \'none\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"none\" name=\"<" +
"%=id%>.peripherals.bloodPressure\">\r\n\t\t\t\t\t\tNone\r\n\t\t\t\t\t</label>\r\n\t\t\t\t</div>\r\n\t\t\t</" +
"td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td>Pulse Oximeter</td>\r\n\t\t\t<td colspan=\"2\">\r\n\t\t\t\t<div " +
"class=\"peripherals-capture\">Data capture type</div>\r\n\t\t\t\t<div data-toggle=\"butto" +
"ns\" class=\"btn-group btn-group-peripherals-ci clearfix\">\r\n\t\t\t\t\t<label class=\"btn" +
" btn-default<%=(peripherals.pulseOx == \'both\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<inp" +
"ut type=\"radio\" value=\"both\" name=\"<%=id%>.peripherals.pulseOx\">\r\n\t\t\t\t\t\tBoth Ava" +
"ilable\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripherals.pulseOx" +
" == \'auto\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"auto\" name=\"" +
"<%=id%>.peripherals.pulseOx\">\r\n\t\t\t\t\t\tAutomatic Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label " +
"class=\"btn btn-default<%=(peripherals.pulseOx == \'manual\' ? \' active\' : \'\') %>\">" +
"\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"manual\" name=\"<%=id%>.peripherals.pulseOx\">\r\n" +
"\t\t\t\t\t\tManual Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default btn-none-ci" +
"<%=(peripherals.pulseOx == \'none\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"rad" +
"io\" value=\"none\" name=\"<%=id%>.peripherals.pulseOx\">\r\n\t\t\t\t\t\tNone\r\n\t\t\t\t\t</label>\r" +
"\n\t\t\t\t</div>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td class=\"td-setting\">Glucometer</td" +
">\r\n\t\t\t<td class=\"td-setting-type\">\r\n\t\t\t\t<div class=\"peripherals-capture\">Data ca" +
"pture type</div>\r\n\t\t\t\t<div data-toggle=\"buttons\" class=\"btn-group btn-group-peri" +
"pherals-ci clearfix\">\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripherals.bloodGl" +
"ucose == \'both\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"both\" n" +
"ame=\"<%=id%>.peripherals.bloodGlucose\">\r\n\t\t\t\t\t\tBoth Available\r\n\t\t\t\t\t</label>\r\n\t\t" +
"\t\t\t<label class=\"btn btn-default<%=(peripherals.bloodGlucose == \'auto\' ? \' activ" +
"e\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"auto\" name=\"<%=id%>.peripherals." +
"bloodGlucose\">\r\n\t\t\t\t\t\tAutomatic Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-" +
"default<%=(peripherals.bloodGlucose == \'manual\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<i" +
"nput type=\"radio\" value=\"manual\" name=\"<%=id%>.peripherals.bloodGlucose\">\r\n\t\t\t\t\t" +
"\tManual Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default btn-none-ci<%=(p" +
"eripherals.bloodGlucose == \'none\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"rad" +
"io\" value=\"none\" name=\"<%=id%>.peripherals.bloodGlucose\">\r\n\t\t\t\t\t\tNone\r\n\t\t\t\t\t</la" +
"bel>\r\n\t\t\t\t</div>\r\n\t\t\t</td>\r\n\t\t\t<td>\r\n\t\t\t\t<div class=\"js-glucometer-device-type\">" +
"\r\n\t\t\t\t\t<div class=\"peripherals-capture\">Device type</div>\r\n\t\t\t\t\t<div class=\"radi" +
"o\">\r\n\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t<input type=\"radio\" value=\"1\" name=\"<%=id%>.settings." +
"bloodGlucosePeripheral\"/>\r\n\t\t\t\t\t\t\tiHealth Glucometer\r\n\t\t\t\t\t\t</label>\r\n\t\t\t\t\t</div" +
">\r\n\t\t\t\t\t<div class=\"radio\">\r\n\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t<input type=\"radio\" value=\"3\"" +
" name=\"<%=id%>.settings.bloodGlucosePeripheral\"/>\r\n\t\t\t\t\t\t\tGlooko Glucometer\r\n\t\t\t" +
"\t\t\t</label>\r\n\t\t\t\t    </div>\r\n\t\t\t\t</div>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td>Spiro" +
"meter</td>\r\n\t\t\t<td colspan=\"2\">\r\n\t\t\t\t<div class=\"peripherals-capture\">Data captu" +
"re type</div>\r\n\t\t\t\t<div data-toggle=\"buttons\" class=\"btn-group btn-group-periphe" +
"rals-ci clearfix\">\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripherals.peakFlow =" +
"= \'both\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"both\" name=\"<%" +
"=id%>.peripherals.peakFlow\">\r\n\t\t\t\t\t\tBoth Available\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label c" +
"lass=\"btn btn-default<%=(peripherals.peakFlow == \'auto\' ? \' active\' : \'\') %>\">\r\n" +
"\t\t\t\t\t\t<input type=\"radio\" value=\"auto\" name=\"<%=id%>.peripherals.peakFlow\">\r\n\t\t\t" +
"\t\t\tAutomatic Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripher" +
"als.peakFlow == \'manual\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value" +
"=\"manual\" name=\"<%=id%>.peripherals.peakFlow\">\r\n\t\t\t\t\t\tManual Only\r\n\t\t\t\t\t</label>" +
"\r\n\t\t\t\t\t<label class=\"btn btn-default btn-none-ci<%=(peripherals.peakFlow == \'non" +
"e\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"none\" name=\"<%=id%>." +
"peripherals.peakFlow\">\r\n\t\t\t\t\t\tNone\r\n\t\t\t\t\t</label>\r\n\t\t\t\t</div>\r\n\t\t\t</td>\r\n\t\t</tr>" +
"\r\n\t\t<tr>\r\n\t\t\t<td>Thermometer</td>\r\n\t\t\t<td colspan=\"2\">\r\n\t\t\t\t<div class=\"peripher" +
"als-capture\">Data capture type</div>\r\n\t\t\t\t<div data-toggle=\"buttons\" class=\"btn-" +
"group btn-group-peripherals-ci clearfix\">\r\n\t\t\t\t\t<label class=\"btn btn-default<%=" +
"(peripherals.temperature == \'both\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"ra" +
"dio\" value=\"both\" name=\"<%=id%>.peripherals.temperature\">\r\n\t\t\t\t\t\tBoth Available\r" +
"\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripherals.temperature ==" +
" \'auto\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"auto\" name=\"<%=" +
"id%>.peripherals.temperature\">\r\n\t\t\t\t\t\tAutomatic Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label" +
" class=\"btn btn-default<%=(peripherals.temperature == \'manual\' ? \' active\' : \'\')" +
" %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"manual\" name=\"<%=id%>.peripherals.temper" +
"ature\">\r\n\t\t\t\t\t\tManual Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default bt" +
"n-none-ci<%=(peripherals.temperature == \'none\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<in" +
"put type=\"radio\" value=\"none\" name=\"<%=id%>.peripherals.temperature\">\r\n\t\t\t\t\t\tNon" +
"e\r\n\t\t\t\t\t</label>\r\n\t\t\t\t</div>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td>Pedometer</td>\r\n" +
"\t\t\t<td colspan=\"2\">\r\n\t\t\t\t<div class=\"peripherals-capture\">Data capture type</div" +
">\r\n\t\t\t\t<div data-toggle=\"buttons\" class=\"btn-group btn-group-peripherals-ci clea" +
"rfix\">\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripherals.pedometer == \'both\' ? " +
"\' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"both\" name=\"<%=id%>.perip" +
"herals.pedometer\">\r\n\t\t\t\t\t\tBoth Available\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn " +
"btn-default<%=(peripherals.pedometer == \'auto\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<in" +
"put type=\"radio\" value=\"auto\" name=\"<%=id%>.peripherals.pedometer\">\r\n\t\t\t\t\t\tAutom" +
"atic Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t\t<label class=\"btn btn-default<%=(peripherals.pedo" +
"meter == \'manual\' ? \' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"manua" +
"l\" name=\"<%=id%>.peripherals.pedometer\">\r\n\t\t\t\t\t\tManual Only\r\n\t\t\t\t\t</label>\r\n\t\t\t\t" +
"\t<label class=\"btn btn-default btn-none-ci<%=(peripherals.pedometer == \'none\' ? " +
"\' active\' : \'\') %>\">\r\n\t\t\t\t\t\t<input type=\"radio\" value=\"none\" name=\"<%=id%>.perip" +
"herals.pedometer\">\r\n\t\t\t\t\t\tNone\r\n\t\t\t\t\t</label>\r\n\t\t\t\t</div>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n\t<" +
"tbody>\r\n</script>");

        }
    }
}
#pragma warning restore 1591