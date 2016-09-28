namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipsUpdated : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HighLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            DropIndex("dbo.MidLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            DropIndex("dbo.LowLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            DropIndex("dbo.TextMediaElementStrings", new[] { "TextMediaElementId" });
            DropIndex("dbo.QuestionElementStrings", new[] { "QuestionElementId" });
            AlterColumn("dbo.HighLabelScaleAnswerSetStrings", "ScaleAnswerSetId", c => c.Guid());
            AlterColumn("dbo.LowLabelScaleAnswerSetStrings", "ScaleAnswerSetId", c => c.Guid());
            AlterColumn("dbo.MidLabelScaleAnswerSetStrings", "ScaleAnswerSetId", c => c.Guid());
            AlterColumn("dbo.QuestionElementStrings", "QuestionElementId", c => c.Guid());
            AlterColumn("dbo.TextMediaElementStrings", "TextMediaElementId", c => c.Guid());
            CreateIndex("dbo.HighLabelScaleAnswerSetStrings", "ScaleAnswerSetId");
            CreateIndex("dbo.MidLabelScaleAnswerSetStrings", "ScaleAnswerSetId");
            CreateIndex("dbo.LowLabelScaleAnswerSetStrings", "ScaleAnswerSetId");
            CreateIndex("dbo.TextMediaElementStrings", "TextMediaElementId");
            CreateIndex("dbo.QuestionElementStrings", "QuestionElementId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuestionElementStrings", new[] { "QuestionElementId" });
            DropIndex("dbo.TextMediaElementStrings", new[] { "TextMediaElementId" });
            DropIndex("dbo.LowLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            DropIndex("dbo.MidLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            DropIndex("dbo.HighLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            AlterColumn("dbo.TextMediaElementStrings", "TextMediaElementId", c => c.Guid(nullable: false));
            AlterColumn("dbo.QuestionElementStrings", "QuestionElementId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MidLabelScaleAnswerSetStrings", "ScaleAnswerSetId", c => c.Guid(nullable: false));
            AlterColumn("dbo.LowLabelScaleAnswerSetStrings", "ScaleAnswerSetId", c => c.Guid(nullable: false));
            AlterColumn("dbo.HighLabelScaleAnswerSetStrings", "ScaleAnswerSetId", c => c.Guid(nullable: false));
            CreateIndex("dbo.QuestionElementStrings", "QuestionElementId");
            CreateIndex("dbo.TextMediaElementStrings", "TextMediaElementId");
            CreateIndex("dbo.LowLabelScaleAnswerSetStrings", "ScaleAnswerSetId");
            CreateIndex("dbo.MidLabelScaleAnswerSetStrings", "ScaleAnswerSetId");
            CreateIndex("dbo.HighLabelScaleAnswerSetStrings", "ScaleAnswerSetId");
        }
    }
}
