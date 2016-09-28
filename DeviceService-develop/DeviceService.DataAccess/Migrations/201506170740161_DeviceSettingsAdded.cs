using System.Data.Entity.Migrations;

namespace DeviceService.DataAccess.Migrations
{
    /// <summary>
    /// DeviceSettingsAdded.
    /// </summary>
    public partial class DeviceSettingsAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Devices", "Settings_IsWeightAutomated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsWeightManual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsBloodPressureAutomated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsBloodPressureManual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsPulseOxAutomated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsPulseOxManual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsBloodGlucoseAutomated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsBloodGlucoseManual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsPeakFlowAutomated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsPeakFlowManual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsTemperatureAutomated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsTemperatureManual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsPinCodeRequired", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_PinCode", c => c.String());
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Devices", "Settings_PinCode");
            DropColumn("dbo.Devices", "Settings_IsPinCodeRequired");
            DropColumn("dbo.Devices", "Settings_IsTemperatureManual");
            DropColumn("dbo.Devices", "Settings_IsTemperatureAutomated");
            DropColumn("dbo.Devices", "Settings_IsPeakFlowManual");
            DropColumn("dbo.Devices", "Settings_IsPeakFlowAutomated");
            DropColumn("dbo.Devices", "Settings_IsBloodGlucoseManual");
            DropColumn("dbo.Devices", "Settings_IsBloodGlucoseAutomated");
            DropColumn("dbo.Devices", "Settings_IsPulseOxManual");
            DropColumn("dbo.Devices", "Settings_IsPulseOxAutomated");
            DropColumn("dbo.Devices", "Settings_IsBloodPressureManual");
            DropColumn("dbo.Devices", "Settings_IsBloodPressureAutomated");
            DropColumn("dbo.Devices", "Settings_IsWeightManual");
            DropColumn("dbo.Devices", "Settings_IsWeightAutomated");
        }
    }
}