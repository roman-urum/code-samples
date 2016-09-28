using System;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Domain.Entities
{
    /// <summary>
    /// ProgramScore.
    /// </summary>
    public class ProgramScore : Entity
    {
        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        public OperatorType Operator { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public virtual Guid ProgramId { get; set; }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public virtual Program Program { get; set; }

        /// <summary>
        /// Gets or sets the program element identifier.
        /// </summary>
        /// <value>
        /// The program element identifier.
        /// </value>
        public virtual Guid ProgramElementId { get; set; }

        /// <summary>
        /// Gets or sets the program element.
        /// </summary>
        /// <value>
        /// The program element.
        /// </value>
        public virtual ProgramElement ProgramElement { get; set; }

        /// <summary>
        /// Gets or sets the selection answer choice identifier.
        /// </summary>
        /// <value>
        /// The selection answer choice identifier.
        /// </value>
        public virtual Guid SelectionAnswerChoiceId { get; set; }

        /// <summary>
        /// Gets or sets the selection answer choice.
        /// </summary>
        /// <value>
        /// The selection answer choice.
        /// </value>
        public virtual SelectionAnswerChoice SelectionAnswerChoice { get; set; }
    }
}