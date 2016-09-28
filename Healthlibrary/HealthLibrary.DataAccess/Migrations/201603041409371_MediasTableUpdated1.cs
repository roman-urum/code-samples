using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// MediasTableUpdated1.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class MediasTableUpdated1 : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            Sql("UPDATE Medias SET Description = substring(Description, 0, 1000) where len(Description) > 1000");
            AlterColumn("dbo.Medias", "Description", c => c.String(nullable: true, maxLength: 1000));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Medias", "Description", c => c.String(nullable: true));
        }
    }
}