using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using CareInnovations.HealthHarmony.Maestro.TokenService.Models;

namespace CareInnovations.HealthHarmony.Maestro.TokenService
{
    /// <summary>
    /// AutoMapperConfig.
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// Registers the profiles.
        /// </summary>
        public static void RegisterProfiles()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<CreatePrincipalModel, Principal>()
                    .ForMember(d => d.UpdatedUtc, o => o.Ignore())
                    .ForMember(d => d.IsDeleted, o => o.Ignore())
                    .ForMember(d => d.Id, o => o.Ignore())
                    .ForMember(d => d.Groups, m => m.Ignore())
                    .ForMember(d => d.FailedCount, m => m.Ignore());
                config.CreateMap<UpdatePrincipalModel, Principal>()
                    .ForMember(d => d.UpdatedUtc, o => o.Ignore())
                    .ForMember(d => d.IsDeleted, o => o.Ignore())
                    .ForMember(d => d.Id, o => o.Ignore())
                    .ForMember(d => d.Groups, m => m.Ignore())
                    .ForMember(d => d.FailedCount, m => m.Ignore())
                    .ForMember(d => d.CustomerId, m => m.Ignore())
                    .ForMember(d => d.Credentials, m => m.Ignore());
                config.CreateMap<CreatePrincipalModel, Principal>()
                    .ForMember(d => d.UpdatedUtc, o => o.Ignore())
                    .ForMember(d => d.IsDeleted, o => o.Ignore())
                    .ForMember(d => d.Id, o => o.Ignore())
                    .ForMember(d => d.Groups, m => m.Ignore())
                    .ForMember(d => d.FailedCount, m => m.Ignore());
                config.CreateMap<CredentialModel, Credential>()
                    .ForMember(d => d.Id, o => o.Ignore())
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.PrincipalId, o => o.Ignore());
                config.CreateMap<GroupModel, Group>()
                    .ForMember(d => d.Id, m => m.Ignore())
                    .ForMember(d => d.IsDeleted, m => m.Ignore())
                    .ForMember(d => d.Principals, m => m.Ignore())
                    .ForMember(d => d.Customer, m => m.Ignore());
                config.CreateMap<CreateGroupModel, Group>()
                    .ForMember(d => d.Id, m => m.Ignore())
                    .ForMember(d => d.IsDeleted, m => m.Ignore())
                    .ForMember(d => d.Principals, m => m.Ignore());
                config.CreateMap<PolicyModel, Policy>()
                    .ForMember(d => d.Id, m => m.Ignore())
                    .ForMember(d => d.Principals, m => m.Ignore())
                    .ForMember(d => d.Groups, m => m.Ignore());
                config.CreateMap<CreateCertificateModel, DeviceCertificate>()
                    .ForMember(d => d.Id, m => m.Ignore());

                config.CreateMap<Principal, PrincipalResponseModel>()
                    .ForMember(d => d.Groups,
                        m => m.MapFrom(s => s.Groups == null ? new List<Guid>() : s.Groups.Select(g => g.Id)));
                config.CreateMap<Group, GroupResponseModel>();
                config.CreateMap<Policy, PolicyModel>();
                config.CreateMap<Policy, CachedPolicyModel>();
                config.CreateMap<Credential, CredentialModel>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
