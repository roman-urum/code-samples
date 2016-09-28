using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// CustomerIdIsRequired.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class CustomerIdIsRequired : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            Sql("UPDATE dbo.AnswerSets SET CustomerId = 1 WHERE CustomerId Is NULL");
            Sql("UPDATE dbo.Elements SET CustomerId = 1 WHERE CustomerId Is NULL");
            Sql("UPDATE dbo.Medias SET CustomerId = 1 WHERE CustomerId Is NULL");
            Sql("UPDATE dbo.Tags SET CustomerId = 1 WHERE CustomerId Is NULL");
            Sql("UPDATE dbo.Protocols SET CustomerId = 1 WHERE CustomerId Is NULL");
            Sql("UPDATE dbo.Programs SET CustomerId = 1 WHERE CustomerId Is NULL");

            DropIndex("dbo.AnswerSets", "IX_CUSTOMER_ID");
            DropIndex("dbo.Elements", "IX_CUSTOMER_ID");
            DropIndex("dbo.Medias", "IX_CUSTOMER_ID");
            DropIndex("dbo.Tags", "IX_CUSTOMER_ID");
            DropIndex("dbo.Protocols", "IX_CUSTOMER_ID");
            DropIndex("dbo.Programs", "IX_CUSTOMER_ID");
            AlterColumn("dbo.AnswerSets", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Elements", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Medias", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tags", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Protocols", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Programs", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.AnswerSets", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Elements", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Medias", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Tags", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Protocols", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Programs", "CustomerId", name: "IX_CUSTOMER_ID");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropIndex("dbo.Programs", "IX_CUSTOMER_ID");
            DropIndex("dbo.Protocols", "IX_CUSTOMER_ID");
            DropIndex("dbo.Tags", "IX_CUSTOMER_ID");
            DropIndex("dbo.Medias", "IX_CUSTOMER_ID");
            DropIndex("dbo.Elements", "IX_CUSTOMER_ID");
            DropIndex("dbo.AnswerSets", "IX_CUSTOMER_ID");
            AlterColumn("dbo.Programs", "CustomerId", c => c.Int());
            AlterColumn("dbo.Protocols", "CustomerId", c => c.Int());
            AlterColumn("dbo.Tags", "CustomerId", c => c.Int());
            AlterColumn("dbo.Medias", "CustomerId", c => c.Int());
            AlterColumn("dbo.Elements", "CustomerId", c => c.Int());
            AlterColumn("dbo.AnswerSets", "CustomerId", c => c.Int());
            CreateIndex("dbo.Programs", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Protocols", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Tags", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Medias", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Elements", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.AnswerSets", "CustomerId", name: "IX_CUSTOMER_ID");
        }
    }
}