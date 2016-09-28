using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    public partial class ConditionsAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            DropForeignKey("dbo.Branches", "SelectionAnswerChoiceId", "dbo.SelectionAnswerChoices");
            DropIndex("dbo.Branches", new[] { "SelectionAnswerChoiceId" });
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Operand = c.Int(nullable: false),
                        Operator = c.Int(),
                        Value = c.String(maxLength: 250),
                        BranchId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.MeasurementElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MeasurementType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.Branches", "Operator");
            DropColumn("dbo.Branches", "Value");
            DropColumn("dbo.Branches", "SelectionAnswerChoiceId");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            AddColumn("dbo.Branches", "SelectionAnswerChoiceId", c => c.Guid());
            AddColumn("dbo.Branches", "Value", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Branches", "Operator", c => c.Int());
            DropForeignKey("dbo.MeasurementElements", "Id", "dbo.Elements");
            DropForeignKey("dbo.Conditions", "BranchId", "dbo.Branches");
            DropIndex("dbo.MeasurementElements", new[] { "Id" });
            DropIndex("dbo.Conditions", new[] { "BranchId" });
            DropTable("dbo.MeasurementElements");
            DropTable("dbo.Conditions");
            CreateIndex("dbo.Branches", "SelectionAnswerChoiceId");
            AddForeignKey("dbo.Branches", "SelectionAnswerChoiceId", "dbo.SelectionAnswerChoices", "Id");
        }
    }
}