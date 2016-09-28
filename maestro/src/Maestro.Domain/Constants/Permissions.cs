using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.TokenService;
using Maestro.Domain.Enums;

namespace Maestro.Domain.Constants
{
    /// <summary>
    /// PermissionCategories.
    /// </summary>
    public static class PermissionCategories
    {
        public const string CustomerSettingsManagementPermissionsCategory = "Customer settings management permissions";
        public const string HealthContentManagementPermissionsCategory = "Health content management permissions";
        public const string PatientManagementPermissionsCategory = "Patient's management permissions";
    }

    /// <summary>
    /// PermissionInfo.
    /// </summary>
    public class PermissionInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the token service group identifier.
        /// </summary>
        /// <value>
        /// The token service group identifier.
        /// </value>
        public Guid TokenServiceGroupId { get; set; }

        public static readonly Dictionary<CustomerUserRolePermissions, PermissionInfo> Infos =
            new Dictionary<CustomerUserRolePermissions, PermissionInfo>()
            {
                {
                    CustomerUserRolePermissions.ViewCustomerSettings,
                    new PermissionInfo()
                    {
                        Name = "View customer settings",
                        Description = "Allows to view settings menu, access customer settings tab, view customer settings and view sites",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewCustomerSettings
                    }
                },
                {
                    CustomerUserRolePermissions.ManageCustomerSettings,
                    new PermissionInfo()
                    {
                        Name = "Manage customer settings",
                        Description = "Allows to view customer settings, manage customer name, logo, session settings and refresh Application cache",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageCustomerSettings
                    }
                },
                {
                    CustomerUserRolePermissions.ManageCustomerSites,
                    new PermissionInfo()
                    {
                        Name = "Manage sites",
                        Description = "Allows to view customer settings, add site, edit site details and enable/disable sites",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageCustomerSites
                    }
                },
                {
                    CustomerUserRolePermissions.ViewCustomerUsers,
                    new PermissionInfo()
                    {
                        Name = "View users",
                        Description = "Allows to view settings menu, access users tab, view list of users, search users and open users details",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewCustomerUsers
                    }
                },
                {
                    CustomerUserRolePermissions.ManageCustomerUserDetails,
                    new PermissionInfo()
                    {
                        Name = "Manage user details",
                        Description = "Allows to view users, enable/disable user and edit user details",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageCustomerUserDetails
                    }
                },
                {
                    CustomerUserRolePermissions.ManageCustomerUserPermissions,
                    new PermissionInfo()
                    {
                        Name = "Manage user permissions",
                        Description = "Allows to view users, update role, grant/revoke access to the sites",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageCustomerUserPermissions
                    }
                },
                {
                    CustomerUserRolePermissions.ManageCustomerUserPassword,
                    new PermissionInfo()
                    {
                        Name = "Manage user password",
                        Description = "Allows to view users, resend invitation, reset password",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageCustomerUserPassword
                    }
                },
                {
                    CustomerUserRolePermissions.CreateCustomerUsers,
                    new PermissionInfo()
                    {
                        Name = "Create users",
                        Description = "Allows to view Users, create new user button, edit user information for new user, update role for new user, grant/revoke access to the sites for new user",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.CreateCustomerUsers
                    }
                },
                {
                    CustomerUserRolePermissions.ManageAlertSeverities,
                    new PermissionInfo()
                    {
                        Name = "Manage alert severities",
                        Description = "Allows to manage customer's alert severities",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageAlertSeverities
                    }
                },
                {
                    CustomerUserRolePermissions.ViewNotables,
                    new PermissionInfo()
                    {
                        Name = "View notables",
                        Description = "Allows to view notables",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewNotables
                    }
                },
                {
                    CustomerUserRolePermissions.ManageNotables,
                    new PermissionInfo()
                    {
                        Name = "Manage notables",
                        Description = "Allows to manage notables",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageNotables
                    }
                },
                {
                    CustomerUserRolePermissions.ManageCustomerThresholds,
                    new PermissionInfo()
                    {
                        Name = "Manage customer thresholds",
                        Description = "Allows to manage customer's thresholds",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageCustomerThresholds
                    }
                },
                {
                    CustomerUserRolePermissions.ManageCategoriesOfCare,
                    new PermissionInfo()
                    {
                        Name = "Manage categories of care",
                        Description = "Allows to manage categories of care",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageCategoriesOfCare
                    }
                },
                {
                    CustomerUserRolePermissions.BrowseCustomers,
                    new PermissionInfo()
                    {
                        Name = "Browse customers",
                        Description = "Allows to view list of customers and view details of any customer",
                        Category = PermissionCategories.CustomerSettingsManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.BrowseCustomers
                    }
                },
                {
                    CustomerUserRolePermissions.BrowseHealthContent,
                    new PermissionInfo()
                    {
                        Name = "Browse health content",
                        Description = "Allows to view care builder item in top menu, search care elements, health protocols and programs and view care elements, health protocols and programs details",
                        Category = PermissionCategories.HealthContentManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.BrowseHealthContent
                    }
                },
                {
                    CustomerUserRolePermissions.ManageCareElements,
                    new PermissionInfo()
                    {
                        Name = "Manage care elements",
                        Description = "Allows to browse health content, add and edit multiple selection answer sets, scale answer sets, questions, text and media elements",
                        Category = PermissionCategories.HealthContentManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageCareElements
                    }
                },
                {
                    CustomerUserRolePermissions.ManageHealthProtocols,
                    new PermissionInfo()
                    {
                        Name = "Manage health protocols",
                        Description = "Allows to browse health content, add and edit health protocols",
                        Category = PermissionCategories.HealthContentManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageHealthProtocols
                    }
                },
                {
                    CustomerUserRolePermissions.ManageHealthPrograms,
                    new PermissionInfo()
                    {
                        Name = "Manage health programs",
                        Description = "Allows to browse health content, add and edit health programs",
                        Category = PermissionCategories.HealthContentManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageHealthPrograms
                    }
                },
                {
                    CustomerUserRolePermissions.ViewOwnPatients,
                    new PermissionInfo()
                    {
                        Name = "View own patients",
                        Description = "Allows to view patient item in top navigation, view list of assigned patients, search assigned patients, open assigned patient, view patient's summary header in patient's dashboard, view own patient's alerts in the dashboard",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewOwnPatients
                    }
                },
                {
                    CustomerUserRolePermissions.ViewAllPatients,
                    new PermissionInfo()
                    {
                        Name = "View all patients",
                        Description = "Allows to view patient item in top navigation, view list of all patients for selected site, search all patients in assigned site, open patient, view patient's summary header in patient's dashboard, view all patients alerts in the dashboard",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewAllPatients
                    }
                },
                {
                    CustomerUserRolePermissions.IgnoreReadings,
                    new PermissionInfo()
                    {
                        Name = "Ignore readings",
                        Description = "Allows to ignore reading from alert dashboard, ignore reading from patient's dashboard, ignore reading from detailed date",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.IgnoreReadings
                    }
                },
                {
                    CustomerUserRolePermissions.AcknowledgeAlerts,
                    new PermissionInfo()
                    {
                        Name = "Acknowledge alerts",
                        Description = "Allows to acknowledge individual alert in the dashboard, acknowledge all patient's alerts in the dashboard",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.AcknowledgeAlerts
                    }
                },
                {
                    CustomerUserRolePermissions.ViewPatientHealthHistory,
                    new PermissionInfo()
                    {
                        Name = "View patient health history",
                        Description = "Allows to view patient's dashboard, view patient's detailed data, view patient's calendar, view patient's calendar assignment history, view patient's trends",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewPatientHealthHistory
                    }
                },
                {
                    CustomerUserRolePermissions.ManagePatientCalendar,
                    new PermissionInfo()
                    {
                        Name = "Manage patient calendar",
                        Description = "Allows to view patient's calendar, view patient's calendar assignment history, search and schedule programs, reschedule, terminate and delete programs",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManagePatientCalendar
                    }
                },
                {
                    CustomerUserRolePermissions.ManagePatientTrends,
                    new PermissionInfo()
                    {
                        Name = "Manage patient trends",
                        Description = "Allows to view patient's trends, add, remove, reorder trends charts",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManagePatientTrends
                    }
                },
                {
                    CustomerUserRolePermissions.CreatePatients,
                    new PermissionInfo()
                    {
                        Name = "Create patients",
                        Description = "Allows to view patient item in top navigation, view add patient button, manage demographics, manage care managers, manage patient's peripherals, manage patient's thresholds, manage patient's devices",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.CreatePatients
                    }
                },
                {
                    CustomerUserRolePermissions.ViewPatientDemographics,
                    new PermissionInfo()
                    {
                        Name = "View patient demographics",
                        Description = "Allows to view patient's details, view demographics tab",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewPatientDemographics
                    }
                },
                {
                    CustomerUserRolePermissions.ManagePatientDemographics,
                    new PermissionInfo()
                    {
                        Name = "Manage patient demographics",
                        Description = "Allows to view demographics tab, edit patient's demographics",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManagePatientDemographics
                    }
                },
                {
                    CustomerUserRolePermissions.ManagePatientCareManagers,
                    new PermissionInfo()
                    {
                        Name = "Manage patient care managers",
                        Description = "Allows to edit patient's details, view care managers tab, add/remove buttons in care managers tab, add/remove care managers buttons in patient's header",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManagePatientCareManagers
                    }
                },
                {
                    CustomerUserRolePermissions.ViewPatientMeasurementSettings,
                    new PermissionInfo()
                    {
                        Name = "View patient measurement settings",
                        Description = "Allows to access measurement settings tab, switch between devices, view peripherals settings, view thresholds",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewPatientMeasurementSettings
                    }
                },
                {
                    CustomerUserRolePermissions.ManagePatientPeripherals,
                    new PermissionInfo()
                    {
                        Name = "Manage patient peripherals",
                        Description = "Allows to view patient's measurement settings, turn on-off peripherals (manual and automated)",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManagePatientPeripherals
                    }
                },
                {
                    CustomerUserRolePermissions.ManagePatientThresholds,
                    new PermissionInfo()
                    {
                        Name = "Manage patient thresholds",
                        Description = "Allows to view patient's measurement settings, edit patient's thresholds",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManagePatientThresholds
                    }
                },
                {
                    CustomerUserRolePermissions.ViewPatientDevices,
                    new PermissionInfo()
                    {
                        Name = "View patient devices",
                        Description = "Allows to access patient's device tab, view list of patient's devices",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ViewPatientDevices
                    }
                },
                {
                    CustomerUserRolePermissions.ManagePatientDevices,
                    new PermissionInfo()
                    {
                        Name = "Manage patient devices",
                        Description = "Allows to view patient's devices, add new device, decommission, delete existing device, change Pin code settings",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManagePatientDevices
                    }
                },
                {
                    CustomerUserRolePermissions.ManageNotes,
                    new PermissionInfo()
                    {
                        Name = "Manage notes",
                        Description = "Allows to manage notes",
                        Category = PermissionCategories.PatientManagementPermissionsCategory,
                        TokenServiceGroupId = TokenServiceGroupGuids.ManageNotes
                    }
                }
            };
    }
}