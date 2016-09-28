using Maestro.Web.Resources;

namespace Maestro.Web.Helpers
{
    /// <summary>
    /// BreadCrumbHelper.
    /// </summary>
    public class BreadCrumbHelper
    {
        public static string CustomerManageThresholdsBreadcrumbTmpl
        {
            get
            {
                return @"<span class='ci-crumb'>{0}</span>" +
                    "<span class='ci-crumb'>&gt;</span>" +
                    "<a class='ci-crumb' href='{1}'>" + GlobalStrings.Edit_Customer_ThresholdsTabTitle + "</a>";
            }
        }
        public static string CustomerManageThresholdsBreadcrumbTmplForSuperAdmin
        {
            get
            {
                return @"<a class='ci-crumb' href='{0}'>" +
                    GlobalStrings.Edit_Customer_SiteManagementBreadcrumbText +
                    "</a>" +
                    "<span class='ci-crumb'>&gt;</span>" +
                    "<span class='ci-crumb'>{1}</span>" +
                    "<span class='ci-crumb'>&gt;</span>" +
                    "<a class='ci-crumb' href='{2}'>" +
                    GlobalStrings.Edit_Customer_ThresholdsTabTitle +
                    "</a>";
            }
        }
    }
}