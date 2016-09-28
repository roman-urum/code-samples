using AutoMapper;
using MessagingHub.Data.Models;
using MessagingHub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessagingHub.Web
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.AddGlobalIgnore("Timestamp");


            Mapper.CreateMap<RegistrationPostRequestModel, Registration>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Secret, o => o.Ignore())
                .ForMember(d => d.Verified, o => o.Ignore())
                .ForMember(d => d.VerificationCode, o => o.Ignore())
                .ForMember(d => d.Disabled, o => o.Ignore())
                .ForMember(d => d.ApplicationId, o => o.Ignore())
                .ForMember(d => d.Application, o => o.Ignore())
                .ForMember(d => d.Tags, o => o.MapFrom(s => s.Tags.Select(t => new RegistrationTag { Value = t })))
            ;

            Mapper.CreateMap<Registration, RegistrationPostResponseModel>();


            Mapper.AssertConfigurationIsValid();
        }
    }
}