using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// CustomerIdIndexesAdded.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class CustomerIdIndexesAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
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
        }
    }
}