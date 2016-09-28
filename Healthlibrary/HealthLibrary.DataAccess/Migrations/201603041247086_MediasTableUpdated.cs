using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// MediasTableUpdated.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class MediasTableUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            Sql("UPDATE Medias SET OriginalFileName = substring(OriginalFileName, 0, 100) where len(OriginalFileName) > 100");
            Sql("UPDATE Medias SET Name = substring(Name, 0, 100) where len(Name) > 100");

            AlterColumn("dbo.Medias", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Medias", "OriginalFileName", c => c.String(nullable: false, maxLength: 100));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Medias", "OriginalFileName", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Medias", "Name", c => c.String(nullable: false, maxLength: 200));
        }
    }
}