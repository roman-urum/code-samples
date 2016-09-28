using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// CustomerUserSiteMapping.
    /// </summary>
    internal class CustomerUserSiteMapping : EntityTypeConfiguration<CustomerUserSite>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserSiteMapping"/> class.
        /// </summary>
        public CustomerUserSiteMapping()
        {
            // Table name
            this.ToTable("CustomerUserSites");

            // Primary Key
            this.HasKey(e => new { e.CustomerUserId, e.SiteId });

            // One-to-Many /CustomerUser - CustomerUserSites/
            this.HasRequired(cus => cus.CustomerUser)
                .WithMany(cu => cu.CustomerUserSites)
                .HasForeignKey(cus => cus.CustomerUserId)
                .WillCascadeOnDelete(true);
        }
    }
}