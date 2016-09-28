using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// Initial.
    /// </summary>
    public partial class Initial : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Elements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProtocolElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Sort = c.Int(nullable: false),
                        ProtocolId = c.Guid(nullable: false),
                        ElementId = c.Guid(nullable: false),
                        NextProtocolElementId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.ElementId, cascadeDelete: true)
                .ForeignKey("dbo.ProtocolElements", t => t.NextProtocolElementId)
                .ForeignKey("dbo.Protocols", t => t.ProtocolId, cascadeDelete: true)
                .Index(t => t.ProtocolId)
                .Index(t => t.ElementId)
                .Index(t => t.NextProtocolElementId);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Operator = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProtocolElementId = c.Guid(nullable: false),
                        NextProtocolElementId = c.Guid(),
                        SelectionAnswerChoiceId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProtocolElements", t => t.NextProtocolElementId)
                .ForeignKey("dbo.ProtocolElements", t => t.ProtocolElementId, cascadeDelete: true)
                .ForeignKey("dbo.SelectionAnswerChoices", t => t.SelectionAnswerChoiceId)
                .Index(t => t.ProtocolElementId)
                .Index(t => t.NextProtocolElementId)
                .Index(t => t.SelectionAnswerChoiceId);
            
            CreateTable(
                "dbo.SelectionAnswerChoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InternalIdentifier = c.String(),
                        ExternalIdentifier = c.String(),
                        Description = c.String(),
                        IsOpenEnded = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        SelectionAnswerSetId = c.Guid(nullable: false),
                        DefaultStringId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SelectionAnswerSets", t => t.SelectionAnswerSetId)
                .ForeignKey("dbo.SelectionAnswerChoiceStrings", t => t.DefaultStringId)
                .Index(t => t.SelectionAnswerSetId)
                .Index(t => t.DefaultStringId);
            
            CreateTable(
                "dbo.LocalizedStrings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Language = c.String(nullable: false, maxLength: 5),
                        Value = c.String(nullable: false, maxLength: 1000),
                        Description = c.String(maxLength: 250),
                        Pronunciation = c.String(maxLength: 1000),
                        AudioFileMediaId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medias", t => t.AudioFileMediaId)
                .Index(t => t.AudioFileMediaId);
            
            CreateTable(
                "dbo.Medias",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        ContentLength = c.Long(nullable: false),
                        StorageKey = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnswerSets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProgramDayElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Day = c.Int(nullable: false),
                        Sort = c.Int(),
                        ProgramId = c.Guid(nullable: false),
                        RecurrenceId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programs", t => t.ProgramId)
                .ForeignKey("dbo.Recurrences", t => t.RecurrenceId)
                .Index(t => t.ProgramId)
                .Index(t => t.RecurrenceId);
            
            CreateTable(
                "dbo.ProgramElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Sort = c.Int(nullable: false),
                        ProgramId = c.Guid(nullable: false),
                        ProtocolId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.Protocols", t => t.ProtocolId)
                .Index(t => t.ProgramId)
                .Index(t => t.ProtocolId);
            
            CreateTable(
                "dbo.Protocols",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(),
                        FirstProtocolElementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProtocolElements", t => t.FirstProtocolElementId)
                .Index(t => t.FirstProtocolElementId);
            
            CreateTable(
                "dbo.Recurrences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDay = c.Int(nullable: false),
                        EndDay = c.Int(nullable: false),
                        EveryDays = c.Int(nullable: false),
                        ProgramId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .Index(t => t.ProgramId);
            
            CreateTable(
                "dbo.TextMediaElementsToMedias",
                c => new
                    {
                        TextMediaElementId = c.Guid(nullable: false),
                        MediaId = c.Guid(nullable: false),
                        Language = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => new { t.TextMediaElementId, t.MediaId })
                .ForeignKey("dbo.Medias", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.TextMediaElements", t => t.TextMediaElementId)
                .Index(t => t.TextMediaElementId)
                .Index(t => t.MediaId);
            
            CreateTable(
                "dbo.ElementsToTags",
                c => new
                    {
                        ElementId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ElementId, t.TagId })
                .ForeignKey("dbo.Tags", t => t.ElementId, cascadeDelete: true)
                .ForeignKey("dbo.Elements", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ElementId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.ProtocolsToTags",
                c => new
                    {
                        ProtocolId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProtocolId, t.TagId })
                .ForeignKey("dbo.Protocols", t => t.ProtocolId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ProtocolId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.ProgramDayElementsToProgramElements",
                c => new
                    {
                        ProgramDayElementId = c.Guid(nullable: false),
                        ProgramElementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgramDayElementId, t.ProgramElementId })
                .ForeignKey("dbo.ProgramDayElements", t => t.ProgramDayElementId, cascadeDelete: true)
                .ForeignKey("dbo.ProgramElements", t => t.ProgramElementId, cascadeDelete: true)
                .Index(t => t.ProgramDayElementId)
                .Index(t => t.ProgramElementId);
            
            CreateTable(
                "dbo.ProgramsToTags",
                c => new
                    {
                        ProgramId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgramId, t.TagId })
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ProgramId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.AnswerSetsToTags",
                c => new
                    {
                        AnswerSetId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnswerSetId, t.TagId })
                .ForeignKey("dbo.AnswerSets", t => t.AnswerSetId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.AnswerSetId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.MediasToTags",
                c => new
                    {
                        MediaId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaId, t.TagId })
                .ForeignKey("dbo.Medias", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.HighLabelScaleAnswerSetStrings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ScaleAnswerSetId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocalizedStrings", t => t.Id)
                .ForeignKey("dbo.ScaleAnswerSets", t => t.ScaleAnswerSetId)
                .Index(t => t.Id)
                .Index(t => t.ScaleAnswerSetId);
            
            CreateTable(
                "dbo.MidLabelScaleAnswerSetStrings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ScaleAnswerSetId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocalizedStrings", t => t.Id)
                .ForeignKey("dbo.ScaleAnswerSets", t => t.ScaleAnswerSetId)
                .Index(t => t.Id)
                .Index(t => t.ScaleAnswerSetId);
            
            CreateTable(
                "dbo.LowLabelScaleAnswerSetStrings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ScaleAnswerSetId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocalizedStrings", t => t.Id)
                .ForeignKey("dbo.ScaleAnswerSets", t => t.ScaleAnswerSetId)
                .Index(t => t.Id)
                .Index(t => t.ScaleAnswerSetId);
            
            CreateTable(
                "dbo.TextMediaElementStrings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TextMediaElementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocalizedStrings", t => t.Id)
                .ForeignKey("dbo.TextMediaElements", t => t.TextMediaElementId)
                .Index(t => t.Id)
                .Index(t => t.TextMediaElementId);
            
            CreateTable(
                "dbo.SelectionAnswerChoiceStrings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SelectionAnswerChoiceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocalizedStrings", t => t.Id)
                .ForeignKey("dbo.SelectionAnswerChoices", t => t.SelectionAnswerChoiceId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.SelectionAnswerChoiceId);
            
            CreateTable(
                "dbo.QuestionElementStrings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionElementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocalizedStrings", t => t.Id)
                .ForeignKey("dbo.QuestionElements", t => t.QuestionElementId)
                .Index(t => t.Id)
                .Index(t => t.QuestionElementId);
            
            CreateTable(
                "dbo.ScaleAnswerSets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LowValue = c.Int(nullable: false),
                        HighValue = c.Int(nullable: false),
                        DefaultLowLabelStringId = c.Guid(nullable: false),
                        DefaultMidLabelStringId = c.Guid(nullable: false),
                        DefaultHighLabelStringId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnswerSets", t => t.Id)
                .ForeignKey("dbo.LowLabelScaleAnswerSetStrings", t => t.DefaultLowLabelStringId)
                .ForeignKey("dbo.MidLabelScaleAnswerSetStrings", t => t.DefaultMidLabelStringId)
                .ForeignKey("dbo.HighLabelScaleAnswerSetStrings", t => t.DefaultHighLabelStringId)
                .Index(t => t.Id)
                .Index(t => t.DefaultLowLabelStringId)
                .Index(t => t.DefaultMidLabelStringId)
                .Index(t => t.DefaultHighLabelStringId);
            
            CreateTable(
                "dbo.SelectionAnswerSets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsMultipleChoice = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnswerSets", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QuestionElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DefaultStringId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.Id)
                .ForeignKey("dbo.AnswerSets", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.QuestionElementStrings", t => t.DefaultStringId)
                .Index(t => t.Id)
                .Index(t => t.DefaultStringId);
            
            CreateTable(
                "dbo.TextMediaElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        MediaType = c.Int(nullable: false),
                        DefaultStringId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.Id)
                .ForeignKey("dbo.TextMediaElementStrings", t => t.DefaultStringId)
                .Index(t => t.Id)
                .Index(t => t.DefaultStringId);
            
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.TextMediaElements", "DefaultStringId", "dbo.TextMediaElementStrings");
            DropForeignKey("dbo.TextMediaElements", "Id", "dbo.Elements");
            DropForeignKey("dbo.QuestionElements", "DefaultStringId", "dbo.QuestionElementStrings");
            DropForeignKey("dbo.QuestionElements", "Id", "dbo.AnswerSets");
            DropForeignKey("dbo.QuestionElements", "Id", "dbo.Elements");
            DropForeignKey("dbo.SelectionAnswerSets", "Id", "dbo.AnswerSets");
            DropForeignKey("dbo.ScaleAnswerSets", "DefaultHighLabelStringId", "dbo.HighLabelScaleAnswerSetStrings");
            DropForeignKey("dbo.ScaleAnswerSets", "DefaultMidLabelStringId", "dbo.MidLabelScaleAnswerSetStrings");
            DropForeignKey("dbo.ScaleAnswerSets", "DefaultLowLabelStringId", "dbo.LowLabelScaleAnswerSetStrings");
            DropForeignKey("dbo.ScaleAnswerSets", "Id", "dbo.AnswerSets");
            DropForeignKey("dbo.QuestionElementStrings", "QuestionElementId", "dbo.QuestionElements");
            DropForeignKey("dbo.QuestionElementStrings", "Id", "dbo.LocalizedStrings");
            DropForeignKey("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId", "dbo.SelectionAnswerChoices");
            DropForeignKey("dbo.SelectionAnswerChoiceStrings", "Id", "dbo.LocalizedStrings");
            DropForeignKey("dbo.TextMediaElementStrings", "TextMediaElementId", "dbo.TextMediaElements");
            DropForeignKey("dbo.TextMediaElementStrings", "Id", "dbo.LocalizedStrings");
            DropForeignKey("dbo.LowLabelScaleAnswerSetStrings", "ScaleAnswerSetId", "dbo.ScaleAnswerSets");
            DropForeignKey("dbo.LowLabelScaleAnswerSetStrings", "Id", "dbo.LocalizedStrings");
            DropForeignKey("dbo.MidLabelScaleAnswerSetStrings", "ScaleAnswerSetId", "dbo.ScaleAnswerSets");
            DropForeignKey("dbo.MidLabelScaleAnswerSetStrings", "Id", "dbo.LocalizedStrings");
            DropForeignKey("dbo.HighLabelScaleAnswerSetStrings", "ScaleAnswerSetId", "dbo.ScaleAnswerSets");
            DropForeignKey("dbo.HighLabelScaleAnswerSetStrings", "Id", "dbo.LocalizedStrings");
            DropForeignKey("dbo.ProtocolElements", "ProtocolId", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolElements", "NextProtocolElementId", "dbo.ProtocolElements");
            DropForeignKey("dbo.ProtocolElements", "ElementId", "dbo.Elements");
            DropForeignKey("dbo.Branches", "SelectionAnswerChoiceId", "dbo.SelectionAnswerChoices");
            DropForeignKey("dbo.SelectionAnswerChoices", "DefaultStringId", "dbo.SelectionAnswerChoiceStrings");
            DropForeignKey("dbo.MediasToTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.MediasToTags", "MediaId", "dbo.Medias");
            DropForeignKey("dbo.TextMediaElementsToMedias", "TextMediaElementId", "dbo.TextMediaElements");
            DropForeignKey("dbo.TextMediaElementsToMedias", "MediaId", "dbo.Medias");
            DropForeignKey("dbo.SelectionAnswerChoices", "SelectionAnswerSetId", "dbo.SelectionAnswerSets");
            DropForeignKey("dbo.AnswerSetsToTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.AnswerSetsToTags", "AnswerSetId", "dbo.AnswerSets");
            DropForeignKey("dbo.ProgramsToTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.ProgramsToTags", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.ProgramDayElements", "RecurrenceId", "dbo.Recurrences");
            DropForeignKey("dbo.Recurrences", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.ProgramDayElementsToProgramElements", "ProgramElementId", "dbo.ProgramElements");
            DropForeignKey("dbo.ProgramDayElementsToProgramElements", "ProgramDayElementId", "dbo.ProgramDayElements");
            DropForeignKey("dbo.ProgramElements", "ProtocolId", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolsToTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.ProtocolsToTags", "ProtocolId", "dbo.Protocols");
            DropForeignKey("dbo.Protocols", "FirstProtocolElementId", "dbo.ProtocolElements");
            DropForeignKey("dbo.ProgramElements", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.ProgramDayElements", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.ElementsToTags", "TagId", "dbo.Elements");
            DropForeignKey("dbo.ElementsToTags", "ElementId", "dbo.Tags");
            DropForeignKey("dbo.LocalizedStrings", "AudioFileMediaId", "dbo.Medias");
            DropForeignKey("dbo.Branches", "ProtocolElementId", "dbo.ProtocolElements");
            DropForeignKey("dbo.Branches", "NextProtocolElementId", "dbo.ProtocolElements");
            DropIndex("dbo.TextMediaElements", new[] { "DefaultStringId" });
            DropIndex("dbo.TextMediaElements", new[] { "Id" });
            DropIndex("dbo.QuestionElements", new[] { "DefaultStringId" });
            DropIndex("dbo.QuestionElements", new[] { "Id" });
            DropIndex("dbo.SelectionAnswerSets", new[] { "Id" });
            DropIndex("dbo.ScaleAnswerSets", new[] { "DefaultHighLabelStringId" });
            DropIndex("dbo.ScaleAnswerSets", new[] { "DefaultMidLabelStringId" });
            DropIndex("dbo.ScaleAnswerSets", new[] { "DefaultLowLabelStringId" });
            DropIndex("dbo.ScaleAnswerSets", new[] { "Id" });
            DropIndex("dbo.QuestionElementStrings", new[] { "QuestionElementId" });
            DropIndex("dbo.QuestionElementStrings", new[] { "Id" });
            DropIndex("dbo.SelectionAnswerChoiceStrings", new[] { "SelectionAnswerChoiceId" });
            DropIndex("dbo.SelectionAnswerChoiceStrings", new[] { "Id" });
            DropIndex("dbo.TextMediaElementStrings", new[] { "TextMediaElementId" });
            DropIndex("dbo.TextMediaElementStrings", new[] { "Id" });
            DropIndex("dbo.LowLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            DropIndex("dbo.LowLabelScaleAnswerSetStrings", new[] { "Id" });
            DropIndex("dbo.MidLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            DropIndex("dbo.MidLabelScaleAnswerSetStrings", new[] { "Id" });
            DropIndex("dbo.HighLabelScaleAnswerSetStrings", new[] { "ScaleAnswerSetId" });
            DropIndex("dbo.HighLabelScaleAnswerSetStrings", new[] { "Id" });
            DropIndex("dbo.MediasToTags", new[] { "TagId" });
            DropIndex("dbo.MediasToTags", new[] { "MediaId" });
            DropIndex("dbo.AnswerSetsToTags", new[] { "TagId" });
            DropIndex("dbo.AnswerSetsToTags", new[] { "AnswerSetId" });
            DropIndex("dbo.ProgramsToTags", new[] { "TagId" });
            DropIndex("dbo.ProgramsToTags", new[] { "ProgramId" });
            DropIndex("dbo.ProgramDayElementsToProgramElements", new[] { "ProgramElementId" });
            DropIndex("dbo.ProgramDayElementsToProgramElements", new[] { "ProgramDayElementId" });
            DropIndex("dbo.ProtocolsToTags", new[] { "TagId" });
            DropIndex("dbo.ProtocolsToTags", new[] { "ProtocolId" });
            DropIndex("dbo.ElementsToTags", new[] { "TagId" });
            DropIndex("dbo.ElementsToTags", new[] { "ElementId" });
            DropIndex("dbo.TextMediaElementsToMedias", new[] { "MediaId" });
            DropIndex("dbo.TextMediaElementsToMedias", new[] { "TextMediaElementId" });
            DropIndex("dbo.Recurrences", new[] { "ProgramId" });
            DropIndex("dbo.Protocols", new[] { "FirstProtocolElementId" });
            DropIndex("dbo.ProgramElements", new[] { "ProtocolId" });
            DropIndex("dbo.ProgramElements", new[] { "ProgramId" });
            DropIndex("dbo.ProgramDayElements", new[] { "RecurrenceId" });
            DropIndex("dbo.ProgramDayElements", new[] { "ProgramId" });
            DropIndex("dbo.LocalizedStrings", new[] { "AudioFileMediaId" });
            DropIndex("dbo.SelectionAnswerChoices", new[] { "DefaultStringId" });
            DropIndex("dbo.SelectionAnswerChoices", new[] { "SelectionAnswerSetId" });
            DropIndex("dbo.Branches", new[] { "SelectionAnswerChoiceId" });
            DropIndex("dbo.Branches", new[] { "NextProtocolElementId" });
            DropIndex("dbo.Branches", new[] { "ProtocolElementId" });
            DropIndex("dbo.ProtocolElements", new[] { "NextProtocolElementId" });
            DropIndex("dbo.ProtocolElements", new[] { "ElementId" });
            DropIndex("dbo.ProtocolElements", new[] { "ProtocolId" });
            DropTable("dbo.TextMediaElements");
            DropTable("dbo.QuestionElements");
            DropTable("dbo.SelectionAnswerSets");
            DropTable("dbo.ScaleAnswerSets");
            DropTable("dbo.QuestionElementStrings");
            DropTable("dbo.SelectionAnswerChoiceStrings");
            DropTable("dbo.TextMediaElementStrings");
            DropTable("dbo.LowLabelScaleAnswerSetStrings");
            DropTable("dbo.MidLabelScaleAnswerSetStrings");
            DropTable("dbo.HighLabelScaleAnswerSetStrings");
            DropTable("dbo.MediasToTags");
            DropTable("dbo.AnswerSetsToTags");
            DropTable("dbo.ProgramsToTags");
            DropTable("dbo.ProgramDayElementsToProgramElements");
            DropTable("dbo.ProtocolsToTags");
            DropTable("dbo.ElementsToTags");
            DropTable("dbo.TextMediaElementsToMedias");
            DropTable("dbo.Recurrences");
            DropTable("dbo.Protocols");
            DropTable("dbo.ProgramElements");
            DropTable("dbo.ProgramDayElements");
            DropTable("dbo.Programs");
            DropTable("dbo.Tags");
            DropTable("dbo.AnswerSets");
            DropTable("dbo.Medias");
            DropTable("dbo.LocalizedStrings");
            DropTable("dbo.SelectionAnswerChoices");
            DropTable("dbo.Branches");
            DropTable("dbo.ProtocolElements");
            DropTable("dbo.Elements");
        }
    }
}