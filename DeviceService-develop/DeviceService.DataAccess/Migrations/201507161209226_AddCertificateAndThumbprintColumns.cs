namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCertificateAndThumbprintColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Certificate", c => c.String());
            AddColumn("dbo.Devices", "Thumbprint", c => c.String());
            DropColumn("dbo.Devices", "CertificateSubject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "CertificateSubject", c => c.String(maxLength: 100));
            DropColumn("dbo.Devices", "Thumbprint");
            DropColumn("dbo.Devices", "Certificate");
        }
    }
}
