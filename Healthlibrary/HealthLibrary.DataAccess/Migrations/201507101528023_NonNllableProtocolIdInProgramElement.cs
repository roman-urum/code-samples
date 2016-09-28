namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NonNllableProtocolIdInProgramElement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProgramElements", "ProtocolId", "dbo.Protocols");
            DropIndex("dbo.ProgramElements", new[] { "ProtocolId" });
            AlterColumn("dbo.ProgramElements", "ProtocolId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProgramElements", "ProtocolId");
            AddForeignKey("dbo.ProgramElements", "ProtocolId", "dbo.Protocols", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProgramElements", "ProtocolId", "dbo.Protocols");
            DropIndex("dbo.ProgramElements", new[] { "ProtocolId" });
            AlterColumn("dbo.ProgramElements", "ProtocolId", c => c.Guid());
            CreateIndex("dbo.ProgramElements", "ProtocolId");
            AddForeignKey("dbo.ProgramElements", "ProtocolId", "dbo.Protocols", "Id");
        }
    }
}
