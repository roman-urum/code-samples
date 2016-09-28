using Maestro.Domain.Enums;
using System;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// Class ChartSetting.
    /// </summary>
    public abstract class ChartSetting : Entity
    {
        /// <summary>
        /// Gets or sets the trend setting identifier.
        /// </summary>
        /// <value>The trend setting identifier.</value>
        public Guid TrendSettingId { get; set; }

        /// <summary>
        /// Gets or sets the trend setting.
        /// </summary>
        /// <value>The trend setting.</value>
        public virtual TrendSetting TrendSetting { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public ChartType Type { get; set; }
    }
}