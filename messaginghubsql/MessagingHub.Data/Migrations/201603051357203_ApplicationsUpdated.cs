using System.Data.Entity.Migrations;

namespace MessagingHub.Data.Migrations
{
    /// <summary>
    /// ApplicationsUpdated.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class ApplicationsUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Applications", "AppleCertificateBase64", c => c.String(nullable: true));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Applications", "AppleCertificateBase64", c => c.String(nullable: true, maxLength: 8192));
        }
    }
}