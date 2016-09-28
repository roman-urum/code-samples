using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;
using System.Data.Entity.Migrations;
using System.Linq;
using Maestro.Domain.Enums;
using Maestro.DataAccess.EF.Context;

namespace Maestro.DataAccess.EF.Migrations
{
    /// <summary>
    /// Configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<MaestroDbContext>
    {
        private const string AdminEmail = "admin@careinnovations.com";

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(MaestroDbContext context)
        {
            #region Super Admin

            var superAdminRole = new UserRole
            {
                Name = Roles.SuperAdmin
            };

            context.UserRoles.AddOrUpdate(
                ur => ur.Name,
                superAdminRole
            );

            context.UserRoles.AddOrUpdate(
                ur => ur.Name,
                new UserRole
                {
                    Name = Roles.CustomerUser
                }
            );

            var superAdminUser = context.Users.FirstOrDefault(u => u.Email == AdminEmail);

            if (superAdminUser == null)
            {
                context.Users.Add(
                    new User
                    {
                        Email = AdminEmail,
                        FirstName = "Maestro",
                        LastName = "Admin",
                        IsEmailVerified = true,
                        IsEnabled = true,
                        Phone = "1234567",
                        Role = superAdminRole
                    }
                );
            }

            #endregion

            #region Customer's User Roles

            AddOrUpdateDefaultCustomerRole(
                context,
                new CustomerUserRole()
                {
                    Name = CustomerUserRoles.CustomerAdmin
                },
                CustomerUserRolePermissions.ViewCustomerSettings,
                CustomerUserRolePermissions.ManageCustomerSettings,
                CustomerUserRolePermissions.ManageCustomerSites,
                CustomerUserRolePermissions.ViewCustomerUsers,
                CustomerUserRolePermissions.ManageCustomerUserDetails,
                CustomerUserRolePermissions.ManageCustomerUserPermissions,
                CustomerUserRolePermissions.ManageCustomerUserPassword,
                CustomerUserRolePermissions.CreateCustomerUsers,
                CustomerUserRolePermissions.ManageAlertSeverities,
                CustomerUserRolePermissions.ManageNotables,
                CustomerUserRolePermissions.ManageCategoriesOfCare,
                CustomerUserRolePermissions.BrowseCustomers
            );

            AddOrUpdateDefaultCustomerRole(
                context, 
                new CustomerUserRole()
                {
                    Name = CustomerUserRoles.ManageOwnPatients
                },
                CustomerUserRolePermissions.ViewOwnPatients,
                CustomerUserRolePermissions.IgnoreReadings,
                CustomerUserRolePermissions.AcknowledgeAlerts,
                CustomerUserRolePermissions.ViewPatientHealthHistory,
                CustomerUserRolePermissions.ManagePatientCalendar,
                CustomerUserRolePermissions.ManagePatientTrends,
                CustomerUserRolePermissions.CreatePatients,
                CustomerUserRolePermissions.ManagePatientDemographics,
                CustomerUserRolePermissions.ManagePatientCareManagers,
                CustomerUserRolePermissions.ManagePatientPeripherals,
                CustomerUserRolePermissions.ManagePatientThresholds,
                CustomerUserRolePermissions.ManagePatientDevices,
                CustomerUserRolePermissions.BrowseHealthContent,
                CustomerUserRolePermissions.ViewNotables,
                CustomerUserRolePermissions.ManageNotes
            );

            AddOrUpdateDefaultCustomerRole(
                context,
                new CustomerUserRole()
                {
                    Name = CustomerUserRoles.ManageAllPatients
                },
                CustomerUserRolePermissions.ViewAllPatients,
                CustomerUserRolePermissions.IgnoreReadings,
                CustomerUserRolePermissions.AcknowledgeAlerts,
                CustomerUserRolePermissions.ViewPatientHealthHistory,
                CustomerUserRolePermissions.ManagePatientCalendar,
                CustomerUserRolePermissions.ManagePatientTrends,
                CustomerUserRolePermissions.CreatePatients,
                CustomerUserRolePermissions.ManagePatientDemographics,
                CustomerUserRolePermissions.ManagePatientCareManagers,
                CustomerUserRolePermissions.ManagePatientPeripherals,
                CustomerUserRolePermissions.ManagePatientThresholds,
                CustomerUserRolePermissions.ManagePatientDevices,
                CustomerUserRolePermissions.BrowseHealthContent,
                CustomerUserRolePermissions.ViewNotables,
                CustomerUserRolePermissions.ManageNotes
            );

            AddOrUpdateDefaultCustomerRole(
                context,
                new CustomerUserRole()
                {
                    Name = CustomerUserRoles.HealthContentManager
                },
                CustomerUserRolePermissions.ManageCareElements,
                CustomerUserRolePermissions.ManageHealthProtocols,
                CustomerUserRolePermissions.ManageHealthPrograms,
                CustomerUserRolePermissions.ManageCustomerThresholds
            );

            #endregion

            context.SaveChanges();
        }

        private void AddOrUpdateDefaultCustomerRole(
            MaestroDbContext dbContext,
            CustomerUserRole customerUserRole,
            params CustomerUserRolePermissions[] customerUserRolePermissions
        )
        {
            var existedCustomerRole = dbContext
                .CustomerUserRoles
                .FirstOrDefault(r => r.Name == customerUserRole.Name && r.CustomerId == null);

            if (existedCustomerRole == null)
            {
                dbContext.CustomerUserRoles.Add(customerUserRole);

                foreach (var permission in customerUserRolePermissions)
                {
                    dbContext.CustomerUserRoleToPermissionMappings.Add(
                        new CustomerUserRoleToPermissionMapping()
                        {
                            CustomerUserRole = customerUserRole,
                            PermissionCode = permission
                        }
                    );
                }
            }
            else
            {
                foreach (var permission in customerUserRolePermissions)
                {
                    if (!dbContext.CustomerUserRoleToPermissionMappings
                        .Any(m => m.CustomerUserRoleId == existedCustomerRole.Id && m.PermissionCode == permission))
                    {
                        dbContext.CustomerUserRoleToPermissionMappings.Add(
                            new CustomerUserRoleToPermissionMapping()
                            {
                                CustomerUserRole = existedCustomerRole,
                                PermissionCode = permission
                            }
                        );
                    }
                }
            }
        }
    }
}