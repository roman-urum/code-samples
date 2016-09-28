namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Scale_RemoveDefaultStrings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScaleAnswerSets", "DefaultLowLabelStringId", "dbo.LowLabelScaleAnswerSetStrings");
            DropForeignKey("dbo.ScaleAnswerSets", "DefaultMidLabelStringId", "dbo.MidLabelScaleAnswerSetStrings");
            DropForeignKey("dbo.ScaleAnswerSets", "DefaultHighLabelStringId", "dbo.HighLabelScaleAnswerSetStrings");
            DropIndex("dbo.ScaleAnswerSets", new[] { "DefaultLowLabelStringId" });
            DropIndex("dbo.ScaleAnswerSets", new[] { "DefaultMidLabelStringId" });
            DropIndex("dbo.ScaleAnswerSets", new[] { "DefaultHighLabelStringId" });
            DropColumn("dbo.ScaleAnswerSets", "DefaultLowLabelStringId");
            DropColumn("dbo.ScaleAnswerSets", "DefaultMidLabelStringId");
            DropColumn("dbo.ScaleAnswerSets", "DefaultHighLabelStringId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScaleAnswerSets", "DefaultHighLabelStringId", c => c.Guid(nullable: false));
            AddColumn("dbo.ScaleAnswerSets", "DefaultMidLabelStringId", c => c.Guid(nullable: false));
            AddColumn("dbo.ScaleAnswerSets", "DefaultLowLabelStringId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ScaleAnswerSets", "DefaultHighLabelStringId");
            CreateIndex("dbo.ScaleAnswerSets", "DefaultMidLabelStringId");
            CreateIndex("dbo.ScaleAnswerSets", "DefaultLowLabelStringId");
            AddForeignKey("dbo.ScaleAnswerSets", "DefaultHighLabelStringId", "dbo.HighLabelScaleAnswerSetStrings", "Id");
            AddForeignKey("dbo.ScaleAnswerSets", "DefaultMidLabelStringId", "dbo.MidLabelScaleAnswerSetStrings", "Id");
            AddForeignKey("dbo.ScaleAnswerSets", "DefaultLowLabelStringId", "dbo.LowLabelScaleAnswerSetStrings", "Id");
        }
    }
}
