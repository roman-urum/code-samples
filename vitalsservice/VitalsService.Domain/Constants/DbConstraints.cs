namespace VitalsService.Domain.Constants
{
    /// <summary>
    /// Container for constants related with database.
    /// </summary>
    public static class DbConstraints
    {
        public static class MaxLength
        {
            public const int VitalName = 100;

            public const int Unit = 50;

            public const int NoteTitle = 250;

            public const int NoteText = 1000;

            public const int ProtocolName = 1000;

            public const int HealthSessionElementText = 1000;

            public const int SelectionAnswerText = 1000;

            public const int InternalId = 100;

            public const int ExternalId = 100;

            public const int NotableName = 50;

            public const int CreatedBy = 100;

            public const int ContentType = 100;

            public const int FileName = 100;

            public const int DeviceUniqueIdentifier = 50;

            public const int DeviceModel = 100;

            public const int AlertSeverityName = 250;
        }
    }
}
