namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCertificateSubjectColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "CertificateSubject", c => c.String(maxLength: 100));
            DropColumn("dbo.Devices", "PublicKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "PublicKey", c => c.String(maxLength: 100));
            DropColumn("dbo.Devices", "CertificateSubject");
        }
    }
}
