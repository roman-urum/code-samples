namespace MessagingHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendApplicationwithNotifcationPortprop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "NotificationPort", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "NotificationPort");
        }
    }
}
