using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using CareInnovations.HealthHarmony.Maestro.TokenService.Common.Helpers;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Contexts;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities.Enums;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TokenServiceDbContext>
    {
        private const string AdminPrincipalUserName = "admin@careinnovations.com";
        private const string CrossServiceCommunicationPrincipalUserName = "CrossServiceCommunicationPrincipal";
        private const string DevicePrincipalUserName = "DevicePrincipal";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TokenServiceDbContext context)
        {
            #region Default groups generation

            var superAdminGroup = new Group()
            {
                Name = "Super Admin",
                Description = "Super Admin Group",
                Id = GroupGuids.SuperAdmin,
                Policies = SuperAdminPolicies(),
                Disabled = false
            };
            AddOrUpdateGroup(superAdminGroup, context);

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewCustomerSettings,
                    Name = "Customers Service View Settings Group",
                    Description = "Allows read only access to customers service",
                    Policies = GrantReadOnlyAccessToCustomerService(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageCustomerSettings,
                    Name = "Manage Customer Settings Group",
                    Description = "Allows full access to customer service",
                    Policies = GetCustomerServiceFullAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageCustomerSites,
                    Name = "Full Access To sites",
                    Description = "Allows full access to sites at customers service",
                    Policies = GetManageCustomerSitesAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewCustomerUsers,
                    Name = "View Customer Users Access",
                    Description = "Allows Read access to customer users",
                    Policies = GetViewCustomerUsersAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageCustomerUserDetails,
                    Name = "Manage customer Users Access",
                    Description = "Allows full access to users",
                    Policies = GetUsersReadWriteAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageCustomerUserPermissions,
                    Name = "Manage customer user permissions access",
                    Description = "Allows: View Users, Update Role, Grant/revoke access to the sites",
                    Policies = GetUsersReadWriteAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageCustomerUserPassword,
                    Name = "Manage customer users permissions access",
                    Description = "Allows: View Users, Resend Invitation, Reset Password",
                    Policies = GetUsersReadWriteAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.CreateCustomerUsers,
                    Name = "Create customer users access",
                    Description = "Allows: View Users, Create New User button, Edit User Information for New User, Update Role for New User, Grant/revoke access to the sites for new user",
                    Policies = GetUsersReadWriteAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageAlertSeverities,
                    Name = "Manage Alert Severities Access",
                    Description = "Manage Alert Severities Access",
                    Policies = GetManageAlertSeveritiesAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewNotables,
                    Name = "View Notables Access",
                    Description = "View Notables Access",
                    Policies = GetViewNotablesAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageNotables,
                    Name = "Manage Notables Access",
                    Description = "Manage Notables Access",
                    Policies = GetManageNotablesAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageNotes,
                    Name = "Manage Notes Access",
                    Description = "Manage Notes Access",
                    Policies = GetManageNotesAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageCustomerThresholds,
                    Name = "Manage Customer Thresholds",
                    Description = "Allows manage custoemer thresholds",
                    Policies = GetManageCustomerThresholdsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageCategoriesOfCare,
                    Name = "Manage Categories of Care",
                    Description = "Manage Categories of Care",
                    Policies = GetManageCategoriesOfCareAccess(),
                    IsDeleted = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.BrowseCustomers,
                    Name = "Browse customers",
                    Description = "	View List of Customers. View Details of any customer",
                    Policies = GetBrowserCustomersAccess(),
                    IsDeleted = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.BrowseHealthContent,
                    Name = "Browse health content",
                    Description = "Search care elements, health protocols and programs. View care elements, health protocols and programs details",
                    Policies = GetBrowserHealthContentAccess(),
                    IsDeleted = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageCareElements,
                    Name = "Manage care elements access",
                    Description = "Allows managing care elements",
                    Policies = GetManageCareElementsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageHealthProtocols,
                    Name = "Manage health protocols access",
                    Description = "Allows managing health protocls",
                    Policies = GetManageHealthProtocolsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManageHealthPrograms,
                    Name = "Manage health programs access",
                    Description = "Allows managing health programs",
                    Policies = GetManageHealthProgramsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewOwnPatients,
                    Name = "View Patients Access",
                    Description = "Allows view patients",
                    Policies = GetViewPatientsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewAllPatients,
                    Name = "View Patients Access",
                    Description = "Allows view patients",
                    Policies = GetViewPatientsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.IgnoreReadings,
                    Name = "Ignore reading",
                    Description = "Allows ignore reading",
                    Policies = GetIgnoreReadingAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.AcknowledgeAlerts,
                    Name = "Acknowledge alerts",
                    Description = "Allows acknowledging alerts",
                    Policies = GetAckowledgeAlertsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewPatientHealthHistory,
                    Name = "View patient health history",
                    Description = "View Patient’s Dashboard. View Patient’s Detailed Data. View Patient’s Calendar. View Patient’s Calendar Assignment History. View Patient’s Trends",
                    Policies = GetViewPatientHealthHistoryAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManagePatientCalendar,
                    Name = "Manage Patient’s Calendar",
                    Description = "Manage Patient’s Calendar",
                    Policies = GetManagePatientsCalendar(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManagePatientTrends,
                    Name = "Manage Patient’s trends",
                    Description = "Manage Patient’s trends",
                    Policies = GetManagePatientTrendsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.CreatePatients,
                    Name = "Create Patients",
                    Description = "Create Patients",
                    Policies = GetCreatePatientsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewPatientDemographics,
                    Name = "View Patient Demographics",
                    Description = "View Patient Demographics",
                    Policies = GetViewPatientDemographicsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManagePatientDemographics,
                    Name = "Manage Patient Demographics",
                    Description = "Manage Patient Demographics",
                    Policies = GetManagePatientDemographicsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManagePatientCareManagers,
                    Name = "Manage patient care managers",
                    Description = "Manage Patient Demographics",
                    Policies = GetManagePatientCareManagersAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewPatientMeasurementSettings,
                    Name = "View patient measurement settings",
                    Description = "Manage Patient Demographics",
                    Policies = GetViewPatientMeasurementSettingsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManagePatientPeripherals,
                    Name = "Manage patient peripherals",
                    Description = "Manage patient peripherals",
                    Policies = GetManagePatientPeripheralsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManagePatientThresholds,
                    Name = "Manage patient thresholds",
                    Description = "Manage patient thresholds",
                    Policies = GetManagePatientThresholdsAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ViewPatientDevices,
                    Name = "View patient devices",
                    Description = "View patient devices",
                    Policies = GetViewPatientDevicesAccess(),
                    Disabled = false
                },
                context
            );

            AddOrUpdateGroup(
                new Group()
                {
                    Id = GroupGuids.ManagePatientDevices,
                    Name = "Manage patient devices",
                    Description = "Manage patient devices",
                    Policies = GetManagePatientDevicesAccess(),
                    Disabled = false
                },
                context
            );

            #endregion

            #region Default principals generation

            #region Admin principal

            if (context.Principals.Count(p => p.Username.Equals(AdminPrincipalUserName)) == 0)
            {
                var adminPrincipal = new Principal
                {
                    Credentials = new List<Credential>
                    {
                        new Credential
                        {
                            Disabled = false,
                            ExpiresUtc = null,
                            Type = CredentialTypes.Password,
                            Value = HashGenerator.GetHash("Password1,")
                        },
                        new Credential
                        {
                            Disabled = false,
                            ExpiresUtc = null,
                            Type = CredentialTypes.ApiKey,
                            Value = "WUxj4FkuLB"
                        }
                    },
                    CustomerId = 1,
                    Disabled = false,
                    Groups = new List<Group>
                    {
                        superAdminGroup
                    },
                    Username = AdminPrincipalUserName,
                    UpdatedUtc = DateTime.UtcNow,
                    FirstName = "Maestro",
                    LastName = "Admin"
                };

                context.Principals.Add(adminPrincipal);
            }

            #endregion

            #region Cross service communication principal

            if (context.Principals.FirstOrDefault(p => p.Username.ToLower() == CrossServiceCommunicationPrincipalUserName) == null)
            {
                var crossServiceCommunicationPrincipal = new Principal
                {
                    Username = CrossServiceCommunicationPrincipalUserName,
                    CustomerId = 1,
                    UpdatedUtc = DateTime.UtcNow,
                    Disabled = false,
                    Groups = context.Groups.Where(g => g.Name.Equals(superAdminGroup.Name)).ToList(),
                    Credentials = new List<Credential>
                    {
                        new Credential
                        {
                            Disabled = false,
                            ExpiresUtc = null,
                            Type = CredentialTypes.ApiKey,
                            Value = "7QB7Eu6LhyiFbKU7q4rvb9tijAUBlD9HRvhSFDV5aJcuxVqxn3jG7J2KZMhLnSRD"
                        }
                    }
                };

                context.Principals.Add(crossServiceCommunicationPrincipal);
            }

            #endregion

            #region Device principal

            if (context.Principals.FirstOrDefault(p => p.Username.ToLower() == DevicePrincipalUserName) == null)
            {
                var devicePrincipal = new Principal
                {
                    Username = DevicePrincipalUserName,
                    UpdatedUtc = DateTime.UtcNow,
                    Disabled = false,
                    Policies = new List<Policy>()
                    {
                        new Policy()
                        {
                            Name = "Patients service: Get patient's details",
                            Service = MaestroServices.PatientsService,
                            Controller = "patients",
                            Action = Actions.Get,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Patients service: Get patient's adherences & Update their status",
                            Service = MaestroServices.PatientsService,
                            Controller = "adherence",
                            Action = Actions.Get | Actions.Put,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Patients service: Get patient's events",
                            Service = MaestroServices.PatientsService,
                            Controller = "calendar",
                            Action = Actions.Get,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Devices service: Get device details & Update decomissioning status of the device",
                            Service = MaestroServices.DevicesService,
                            Controller = "devices",
                            Action = Actions.Get | Actions.Post,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Devices service: Checkin device",
                            Service = MaestroServices.DevicesService,
                            Controller = "checkin",
                            Action = Actions.Put,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Vitals service: Get and Post patient's vitals",
                            Service = MaestroServices.VitalsService,
                            Controller = "vitals",
                            Action = Actions.Get | Actions.Post,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Vitals service: Get patient's thresholds",
                            Service = MaestroServices.VitalsService,
                            Controller = "thresholds",
                            Action = Actions.Get,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Vitals service: Post patient's health session",
                            Service = MaestroServices.VitalsService,
                            Controller = "healthsessions",
                            Action = Actions.Post,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Vitals service: Post & Put patient's assessment media",
                            Service = MaestroServices.VitalsService,
                            Controller = "assessmentmedia",
                            Action = Actions.Post | Actions.Put,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Vitals service: Get alert severities",
                            Service = MaestroServices.VitalsService,
                            Controller = "AlertSeverities",
                            Action = Actions.Get,
                            Effect = PolicyEffects.Allow
                        },
                        new Policy()
                        {
                            Name = "Health library: Get health protocol",
                            Service = MaestroServices.HealthLibrary,
                            Controller = "protocols",
                            Action = Actions.Get,
                            Effect = PolicyEffects.Allow
                        }
                    }
                };

                context.Principals.Add(devicePrincipal);
            }

            #endregion

            #endregion

            context.SaveChanges();
        }

        #region Policies

        private static List<Policy> GetViewCustomerUsersAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "User service: view customer users access",
                    Controller = "CustomerUsersApi",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.UsersService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetUsersReadWriteAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "User service: Users full access",
                    Controller = "CustomerUsersApi",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.UsersService,
                    Action = Actions.Any
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManageCareElementsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Health Library full access",
                    Controller = "*",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.HealthLibrary,
                    Action = Actions.Any
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }
        private static List<Policy> GetManageHealthProtocolsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Health protocols access",
                    Controller = "Protocols",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.HealthLibrary,
                    Action = Actions.Any
                },
                new Policy()
                {
                    Name = "Severities read access",
                    Controller = "AlertSeverities",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }
        private static List<Policy> GetManageHealthProgramsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Health programs access",
                    Controller = "Programs",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.HealthLibrary,
                    Action = Actions.Any
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManageAlertSeveritiesAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Severities full access",
                    Controller = "AlertSeverities",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Any
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetViewPatientsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View Patients Access",
                    Controller = "patients",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManageCustomerSitesAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Sites Full Access",
                    Action = Actions.Any,
                    Controller = "sites",
                    Service = MaestroServices.CustomersService,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "Organizations Full Access",
                    Action = Actions.Any,
                    Controller = "organizations",
                    Service = MaestroServices.CustomersService,
                    Effect = PolicyEffects.Allow
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GrantReadOnlyAccessToCustomerService()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Action = Actions.Get,
                    Name = "Customers service: Customers Read Access",
                    Controller = "customers",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService
                },
                new Policy()
                {
                    Action = Actions.Get,
                    Name = "Customers service: Sites Read Access",
                    Controller = "sites",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService
                },
                new Policy()
                {
                    Action = Actions.Get,
                    Name = "Customers service: Organizations Read Access",
                    Controller = "organizations",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService,
                },
                new Policy()
                {
                    Action = Actions.Get,
                    Name = "Customers service: CategoriesOfCare Read access",
                    Controller = "categoriesofcare",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService
                }
            };

            return policies;
        }

        private static List<Policy> GetCustomerServiceFullAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Customers service: Customers Full Access",
                    Action = Actions.Any,
                    Controller = "customers",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService
                },
                new Policy()
                {
                    Name = "Customers service: Sites full access",
                    Action = Actions.Any,
                    Controller = "sites",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService
                },
                new Policy()
                {
                    Name = "Customers service: Organizations full access",
                    Action = Actions.Any,
                    Controller = "organizations",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService
                },
                new Policy()
                {
                    Name = "Customers service: CategoriesOfCare full access",
                    Action = Actions.Any,
                    Controller = "categoriesofcare",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService
                },
                new Policy()
                {
                    Name = "Vitals service: Default Thresholds Full Access",
                    Action = Actions.Any,
                    Controller = "defaultthresholds",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService
                },
                new Policy()
                {
                    Name = "Vitals service: Thresholds Full Access",
                    Action = Actions.Any,
                    Controller = "thresholds",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService
                },
                new Policy()
                {
                    Name = "Vitals service: customer conditions full access",
                    Action = Actions.Any,
                    Controller = "conditions",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService
                },
                new Policy()
                {
                    Name = "Search care elements access",
                    Action = Actions.Any,
                    Controller = "Search",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.HealthLibrary
                }
            };

            return policies;
        }

        private static List<Policy> GetManageCustomerThresholdsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Vitals service: Customer Thresholds Full Access",
                    Action = Actions.Any,
                    Controller = "defaultthresholds",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> SuperAdminPolicies()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Action = Actions.Any,
                    Controller = "*",
                    Effect = PolicyEffects.Allow,
                    Name = "Super Admin Access",
                    Service = "*"
                }
            };

            return policies;
        }

        private ICollection<Policy> GetViewNotablesAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Suggested notables view access",
                    Service = MaestroServices.VitalsService,
                    Controller = "SuggestedNotables",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManageNotablesAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Suggested notables full access",
                    Service = MaestroServices.VitalsService,
                    Controller = "SuggestedNotables",
                    Action = Actions.Any,
                    Effect = PolicyEffects.Allow
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private ICollection<Policy> GetManageNotesAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Patient's notes full access",
                    Service = MaestroServices.VitalsService,
                    Controller = "Notes",
                    Action = Actions.Any,
                    Effect = PolicyEffects.Allow
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static IList<Policy> GetManageCategoriesOfCareAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "CategoriesOfCare full access",
                    Action = Actions.Any,
                    Controller = "categoriesofcare",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.CustomersService
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetBrowserCustomersAccess()
        {
            return GrantReadOnlyAccessToCustomerService();
        }

        private static List<Policy> GetBrowserHealthContentAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Search care elements access",
                    Controller = "Search",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.HealthLibrary,
                    Action = Actions.Any
                },
                new Policy()
                {
                    Name = "View text&media elements",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "TextMediaElements",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "View question elements",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "QuestionElements",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "View Medias",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "Medias",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "View open ended answersets",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "OpenEndedAnswerSets",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "View Programs",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "Programs",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "View Protocols",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "Protocols",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "View scale answer sets",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "ScaleAnswerSets",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "View selection answer sets",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "SelectionAnswerSets",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "Health library: View tags",
                    Service = MaestroServices.HealthLibrary,
                    Controller = "Tags",
                    Action = Actions.Get,
                    Effect = PolicyEffects.Allow
                },
                new Policy()
                {
                    Name = "Vitals service: View alert severities",
                    Controller = "AlertSeverities",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetIgnoreReadingAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Ignore reading",
                    Controller = "vitals",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Put
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }
        private static List<Policy> GetAckowledgeAlertsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Acknowledge alerts",
                    Controller = "Alerts",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Post
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetViewPatientHealthHistoryAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View vitals",
                    Controller = "Vitals",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                },
                new Policy()
                {
                    Name = "View Health sessions",
                    Controller = "HealthSessions",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                },
                new Policy()
                {
                    Name = "View thresholds",
                    Controller = "Thresholds",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                },
                new Policy()
                {
                    Name = "View Calendar",
                    Controller = "Calendar",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Any
                },
                new Policy()
                {
                    Name = "Search care elements access",
                    Controller = "Search",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.HealthLibrary,
                    Action = Actions.Any
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManagePatientsCalendar()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Manage patient's calendar",
                    Controller = "calendar",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Any
                },
                new Policy()
                {
                    Name = "Manage patient's adherences",
                    Controller = "adherence",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Any
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManagePatientTrendsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Manage patient's trends",
                    Controller = "Vitals",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetCreatePatientsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Create patients",
                    Controller = "Patients",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Post
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetViewPatientDemographicsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View patients",
                    Controller = "Patients",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Get
                },
                new Policy()
                {
                    Name = "View patient's identifiers",
                    Controller = "Identifiers",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManagePatientDemographicsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View/Edit patients",
                    Controller = "Patients",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Get | Actions.Put
                },
                new Policy()
                {
                    Name = "View patient's identifiers",
                    Controller = "Identifiers",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManagePatientCareManagersAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View/Edit patients",
                    Controller = "Patients",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.PatientsService,
                    Action = Actions.Get | Actions.Put
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetViewPatientMeasurementSettingsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View Devices",
                    Controller = "Devices",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.DevicesService,
                    Action = Actions.Get
                },
                new Policy()
                {
                    Name = "View Thresholds",
                    Controller = "Thresholds",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManagePatientPeripheralsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View Thresholds",
                    Controller = "Thresholds",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                },
                new Policy()
                {
                    Name = "Update Devices",
                    Controller = "Devices",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.DevicesService,
                    Action = Actions.Put
                },
                new Policy()
                {
                    Name = "Vitals service: patient conditions full access",
                    Controller = "patientconditions",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Any
                },
                new Policy()
                {
                    Name = "Vitals service: customer consitions full access",
                    Controller = "conditions",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Any
                },
                new Policy()
                {
                    Name = "View Default Thresholds",
                    Controller = "defaultthresholds",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetManagePatientThresholdsAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View Thresholds",
                    Controller = "Thresholds",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.VitalsService,
                    Action = Actions.Any
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }

        private static List<Policy> GetViewPatientDevicesAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "View Devices",
                    Controller = "Devices",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.DevicesService,
                    Action = Actions.Get
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }


        private static List<Policy> GetManagePatientDevicesAccess()
        {
            var policies = new List<Policy>()
            {
                new Policy()
                {
                    Name = "Manage Devices",
                    Controller = "Devices",
                    Effect = PolicyEffects.Allow,
                    Service = MaestroServices.DevicesService,
                    Action = Actions.Any
                }
            };

            policies.AddRange(GrantReadOnlyAccessToCustomerService());

            return policies;
        }



        #endregion Policies

        #region Helpers

        private void AddOrUpdateGroup(Group group, TokenServiceDbContext context)
        {
            var existingGroup = context.Groups.FirstOrDefault(g => g.Id == group.Id);

            if (existingGroup == null)
            {
                context.Groups.Add(group);

                return;
            }

            if (IsPoliciesChanged(existingGroup.Policies.ToList(), group.Policies.ToList()))
            {
                foreach (var policy in existingGroup.Policies.ToList())
                {
                    context.Policies.Remove(policy);
                }

                existingGroup.Policies = group.Policies;
            }

            existingGroup.Customer = group.Customer;
            existingGroup.Description = group.Description;
            existingGroup.Name = group.Name;
            existingGroup.Disabled = group.Disabled;
            existingGroup.IsDeleted = group.IsDeleted;
        }

        /// <summary>
        /// Compares 2 lists of policies.
        /// </summary>
        /// <param name="currentPolicies"></param>
        /// <param name="newPolicies"></param>
        /// <returns></returns>
        private bool IsPoliciesChanged(IList<Policy> currentPolicies, IList<Policy> newPolicies)
        {
            if (currentPolicies.Count != newPolicies.Count)
            {
                return true;
            }

            foreach (var newPolicy in newPolicies)
            {
                var currPolicy = currentPolicies.FirstOrDefault(cp => cp.Name == newPolicy.Name);

                if (currPolicy == null || IsPolicyChanged(currPolicy, newPolicy))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Compares 2 policies.
        /// </summary>
        /// <param name="existingPolicy"></param>
        /// <param name="newPolicy"></param>
        /// <returns></returns>
        private bool IsPolicyChanged(Policy existingPolicy, Policy newPolicy)
        {
            return existingPolicy.Action != newPolicy.Action ||
                   existingPolicy.Controller != newPolicy.Controller ||
                   existingPolicy.Customer != newPolicy.Customer ||
                   existingPolicy.Effect != newPolicy.Effect ||
                   existingPolicy.Service != newPolicy.Service ||
                   existingPolicy.Name != newPolicy.Name;
        }

        #endregion
    }
}
