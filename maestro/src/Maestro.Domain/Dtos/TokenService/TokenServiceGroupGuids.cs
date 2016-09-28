using System;

namespace Maestro.Domain.Dtos.TokenService
{
    /// <summary>
    /// TokenServiceGroupGuids.
    /// </summary>
    public class TokenServiceGroupGuids
    {
        public static Guid SuperAdmin { get { return new Guid("5d1d34d5-4fe7-4987-a333-62784377eb53"); } }

        public static Guid ViewCustomerSettings { get { return new Guid("63E80D18-4E91-465A-848D-30957E31CAF9"); } }
        public static Guid ManageCustomerSettings { get { return new Guid("4423FBC2-5C7F-47AF-92B0-E45D32A8A2E6"); } }
        public static Guid ManageCustomerSites { get { return new Guid("E5D39FA4-7BF1-4CAC-A8D2-EB47EDEC959F"); } }
        public static Guid ViewCustomerUsers { get { return new Guid("DFEC1829-40EF-4245-B1C5-6AF214EB1F31"); } }
        public static Guid ManageCustomerUserDetails { get { return new Guid("EDAB6B78-95AD-4C0C-A1EF-BA1DFD619162"); } }
        public static Guid ManageCustomerUserPermissions { get { return new Guid("FBF9E1C4-05B8-4656-8FCF-56EB053D8121"); } }
        public static Guid ManageCustomerUserPassword { get { return new Guid("98A8C271-A64F-4C5F-B70F-0D8D3F70968D"); } }
        public static Guid CreateCustomerUsers { get { return new Guid("3B580D3E-95D6-4E9A-9B76-AD34D02DB9EF"); } }
        public static Guid ManageAlertSeverities { get { return new Guid("057B9E2C-3084-4288-96A0-4439E07320DB"); } }
        public static Guid ViewNotables { get { return new Guid("677E1B85-393E-4B70-9538-DB915EBF0FED"); } }
        public static Guid ManageNotables { get { return new Guid("36AA4A6A-EC4B-43BD-AE43-352A7869852E"); } }
        public static Guid ManageCustomerThresholds { get { return new Guid("81570ECA-4E69-456B-AA88-C3D12A5A0B2A"); } }
        public static Guid ManageCategoriesOfCare { get { return new Guid("52AF340A-106F-4377-951A-1E9243EE3242"); } }
        public static Guid BrowseCustomers { get { return new Guid("75FB5330-C7FA-426C-BE8F-CB7392A633C0"); } }
        public static Guid BrowseHealthContent { get { return new Guid("8814EFB9-3F41-49EC-9931-FBA39247DD15"); } }
        public static Guid ManageCareElements { get { return new Guid("3CB7BCF5-50A3-4477-9FE2-F84369524D75"); } }
        public static Guid ManageHealthProtocols { get { return new Guid("0D0094FC-6E18-4C26-8859-AB27E2763F65"); } }
        public static Guid ManageHealthPrograms { get { return new Guid("21F531FC-E056-4CB7-895B-8FA48E84F1E0"); } }
        public static Guid ViewOwnPatients { get { return new Guid("A47D5317-AD9F-4382-B44E-857AD97E6002"); } }
        public static Guid ViewAllPatients { get { return new Guid("22845D9B-DD11-4728-8C13-1639BE1D224C"); } }
        public static Guid IgnoreReadings { get { return new Guid("7D363D84-0127-4DF5-9669-1AA0FF9F00EF"); } }
        public static Guid AcknowledgeAlerts { get { return new Guid("FA02329C-7F94-46A9-874C-A442FCDE2AE9"); } }
        public static Guid ViewPatientHealthHistory { get { return new Guid("6FF22BE7-D523-4A34-8CFB-4FCAA9F0ED01"); } }
        public static Guid ManagePatientCalendar { get { return new Guid("901AD6F2-8F4B-4D51-B032-56323BD2B806"); } }
        public static Guid ManagePatientTrends { get { return new Guid("62DB22A9-AB5D-4630-BAF0-18EE85E07CE3"); } }
        public static Guid CreatePatients { get { return new Guid("D4B48F97-7018-4FB8-AD8B-B501E112B103"); } }
        public static Guid ViewPatientDemographics { get { return new Guid("53BA91D4-E9F7-4D30-84F8-AD8B2659866D"); } }
        public static Guid ManagePatientDemographics { get { return new Guid("B66EB9A1-AEE9-4983-A9C0-5EEF3B9FA368"); } }
        public static Guid ManagePatientCareManagers { get { return new Guid("8F921D3E-9709-4A09-A4A0-C0C9DD54D27E"); } }
        public static Guid ViewPatientMeasurementSettings { get { return new Guid("380585C6-F7A7-4840-8FAA-C4AF86FCF581"); } }
        public static Guid ManagePatientPeripherals { get { return new Guid("B1F7E1A4-08A8-447F-B46E-BFF33EF70416"); } }
        public static Guid ManagePatientThresholds { get { return new Guid("DA2E8BB3-7427-47C1-BD24-D3BD89C7C0F8"); } }
        public static Guid ViewPatientDevices { get { return new Guid("182A0865-FBBE-467C-8B95-A9AD7CCAC180"); } }
        public static Guid ManagePatientDevices { get { return new Guid("366F8A51-0723-42A1-B750-EAED1FAF9776"); } }
        public static Guid ManageNotes { get { return new Guid("8BD588A4-8554-4D7A-8982-25BC1DD40F17"); } }
    }
}