namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDbStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Value = c.String(maxLength: 256),
                        Disabled = c.Boolean(nullable: false),
                        ExpiresUtc = c.DateTime(),
                        PrincipalId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.PrincipalId, cascadeDelete: true)
                .Index(t => t.PrincipalId);
            
            CreateTable(
                "dbo.Principals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Disabled = c.Boolean(nullable: false),
                        ExpiresUtc = c.DateTime(),
                        LockedOutUntilUtc = c.DateTime(),
                        UpdatedUtc = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 1000),
                        Customer = c.Int(),
                        Disabled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Policies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                        Effect = c.Int(nullable: false),
                        Service = c.String(maxLength: 50),
                        Controller = c.String(maxLength: 50),
                        Action = c.Int(nullable: false),
                        Customer = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DevicesCertificates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        Certificate = c.String(),
                        Thumbprint = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupPolicies",
                c => new
                    {
                        Group_Id = c.Guid(nullable: false),
                        Policy_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Policy_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Policies", t => t.Policy_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Policy_Id);
            
            CreateTable(
                "dbo.PrincipalGroups",
                c => new
                    {
                        Principal_Id = c.Guid(nullable: false),
                        Group_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Principal_Id, t.Group_Id })
                .ForeignKey("dbo.Principals", t => t.Principal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.Principal_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.PrincipalPolicies",
                c => new
                    {
                        Principal_Id = c.Guid(nullable: false),
                        Policy_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Principal_Id, t.Policy_Id })
                .ForeignKey("dbo.Principals", t => t.Principal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Policies", t => t.Policy_Id, cascadeDelete: true)
                .Index(t => t.Principal_Id)
                .Index(t => t.Policy_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Credentials", "PrincipalId", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPolicies", "Policy_Id", "dbo.Policies");
            DropForeignKey("dbo.PrincipalPolicies", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.PrincipalGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.PrincipalGroups", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.GroupPolicies", "Policy_Id", "dbo.Policies");
            DropForeignKey("dbo.GroupPolicies", "Group_Id", "dbo.Groups");
            DropIndex("dbo.PrincipalPolicies", new[] { "Policy_Id" });
            DropIndex("dbo.PrincipalPolicies", new[] { "Principal_Id" });
            DropIndex("dbo.PrincipalGroups", new[] { "Group_Id" });
            DropIndex("dbo.PrincipalGroups", new[] { "Principal_Id" });
            DropIndex("dbo.GroupPolicies", new[] { "Policy_Id" });
            DropIndex("dbo.GroupPolicies", new[] { "Group_Id" });
            DropIndex("dbo.Credentials", new[] { "PrincipalId" });
            DropTable("dbo.PrincipalPolicies");
            DropTable("dbo.PrincipalGroups");
            DropTable("dbo.GroupPolicies");
            DropTable("dbo.DevicesCertificates");
            DropTable("dbo.Policies");
            DropTable("dbo.Groups");
            DropTable("dbo.Principals");
            DropTable("dbo.Credentials");
        }
    }
}
