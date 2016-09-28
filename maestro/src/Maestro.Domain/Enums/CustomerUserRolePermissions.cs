namespace Maestro.Domain.Enums
{
    /// <summary>
    /// CustomerUserRolePermissions.
    /// </summary>
    public enum CustomerUserRolePermissions
    {
        SuperAdmin = 1,

        // Customer Settings Management
        ViewCustomerSettings = 101,
        ManageCustomerSettings = 102,
        ManageCustomerSites = 103,
        ViewCustomerUsers = 104,
        ManageCustomerUserDetails = 105,
        ManageCustomerUserPermissions = 106,
        ManageCustomerUserPassword = 107,
        CreateCustomerUsers = 108,
        ManageAlertSeverities = 109,
        ManageNotables = 110,
        ManageCustomerThresholds = 111,
        ManageCategoriesOfCare = 112,
        BrowseCustomers = 113,

        // Health Content Management
        BrowseHealthContent = 201,
        ManageCareElements = 202,
        ManageHealthProtocols = 203,
        ManageHealthPrograms = 204,

        // Patient's Management
        ViewOwnPatients = 301,
        ViewAllPatients = 302,
        IgnoreReadings = 303,
        AcknowledgeAlerts = 304,
        ViewPatientHealthHistory = 305,
        ManagePatientCalendar = 306,
        ManagePatientTrends = 307,
        CreatePatients = 308,
        ViewPatientDemographics = 309,
        ManagePatientDemographics = 310,
        ManagePatientCareManagers = 311,
        ViewPatientMeasurementSettings = 312,
        ManagePatientPeripherals = 313,
        ManagePatientThresholds = 314,
        ViewPatientDevices = 315,
        ManagePatientDevices = 316,
        ViewNotables = 317,
        ManageNotes = 318
    }
}