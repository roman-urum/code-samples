using System.ComponentModel;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// ProcessingType.
    /// </summary>
    public enum ProcessingType
    {
        [Description("Debugging")]
        Debugging = 0,

        [Description("Testing")]
        Testing = 1,

        [Description("Production")]
        Production = 2
    }
}