using System;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Domain.Entities.Protocol
{
    /// <summary>
    /// Condition.
    /// </summary>
    public class Condition : Entity
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

        /// <summary>
        /// Gets or sets the branch identifier.
        /// </summary>
        /// <value>
        /// The branch identifier.
        /// </value>
        public virtual Guid? BranchId { get; set; }

        /// <summary>
        /// Gets or sets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public virtual Branch Branch { get; set; }

        /// <summary>
        /// Gets or sets the alert identifier.
        /// </summary>
        /// <value>
        /// The alert identifier.
        /// </value>
        public virtual Guid? AlertId { get; set; }

        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        /// <value>
        /// The alert.
        /// </value>
        public virtual Alert Alert { get; set; }
    }
}