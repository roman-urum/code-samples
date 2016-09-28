namespace CustomerService.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// CustomerId_IdentitySeed.
    /// </summary>
    public partial class CustomerId_IdentitySeed : DbMigration
    {
        public override void Up()
        {
            // Customers Ids must starts from 3000 because existing customers 
            // can be migrated from old database.
            Sql("DBCC CHECKIDENT ('Customers', RESEED, 3001)");
        }
        
        public override void Down()
        {
        }
    }
}
