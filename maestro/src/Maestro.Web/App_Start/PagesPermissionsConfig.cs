using Maestro.Domain.Enums;
using Maestro.Web.Helpers;

namespace Maestro.Web
{
    /// <summary>
    /// Initializes map of customer permissions by pages.
    /// </summary>
    public static class PagesPermissionsConfig
    {
        public static void Initialize()
        {
            PagePermissionsHelper.RegisterPage(
                "General",
                "Settings",
                "Customer",
                CustomerUserRolePermissions.ViewCustomerSettings, 
                CustomerUserRolePermissions.ManageCustomerSettings,
                CustomerUserRolePermissions.ManageCustomerSites
            );

            PagePermissionsHelper.RegisterPage(
                "CustomerUsers", 
                "Settings", 
                "Customer",
                CustomerUserRolePermissions.CreateCustomerUsers, 
                CustomerUserRolePermissions.ViewCustomerUsers,
                CustomerUserRolePermissions.ManageCustomerUserDetails,
                CustomerUserRolePermissions.ManageCustomerUserPassword, 
                CustomerUserRolePermissions.ManageCustomerUserPermissions
            );

            PagePermissionsHelper.RegisterPage(
                "ManageThresholds",
                "Settings",
                "Customer",
                CustomerUserRolePermissions.ManageCustomerThresholds
            );

            PagePermissionsHelper.RegisterPage(
                "CareElements",
                "CareBuilder",
                "Customer",
                CustomerUserRolePermissions.BrowseHealthContent,
                CustomerUserRolePermissions.ManageCareElements,
                CustomerUserRolePermissions.ManageHealthProtocols, 
                CustomerUserRolePermissions.ManageHealthPrograms
            );

            PagePermissionsHelper.RegisterPage(
                "Index", 
                "Patients",
                "Site",
                CustomerUserRolePermissions.ViewAllPatients,
                CustomerUserRolePermissions.ViewOwnPatients
            );

            PagePermissionsHelper.RegisterPage(
                "Create",
                "Patients",
                "Site",
                CustomerUserRolePermissions.CreatePatients
            );

            PagePermissionsHelper.RegisterPage(
                "Index",
                "Dashboard",
                "Site",
                CustomerUserRolePermissions.ViewAllPatients,
                CustomerUserRolePermissions.ViewOwnPatients
            );
        }
    }
}