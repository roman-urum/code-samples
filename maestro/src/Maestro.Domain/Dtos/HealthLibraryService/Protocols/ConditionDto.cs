using Maestro.Domain.Enums;

namespace Maestro.Domain.Dtos.HealthLibraryService.Protocols
{
    /// <summary>
    /// ConditionDto.
    /// </summary>
    public class ConditionDto
    {
        /// <summary>
        /// Gets or sets the operand.
        /// </summary>
        /// <value>
        /// The operand.
        /// </value>
        public OperandType Operand { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        public OperatorType? Operator { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}