using System;
using AutoMapper;
using DeviceService.Common;
using DeviceService.Domain.Entities;
using DeviceService.Web.Api.Models.Dtos.Entities;
using DeviceService.Web.Api.Models.Dtos.RequestsResponses;

namespace DeviceService.Web.Api.Models.Mapping
{
    /// <summary>
    /// DevicesMappings.
    /// </summary>
    public class DevicesMapping : Profile
    {
        private readonly string messagingHubUrl;

        public DevicesMapping(IAppSettings appSettings)
        {
            messagingHubUrl = appSettings.MessagingHubUrl;
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        ///             Avoid calling the <see cref="T:AutoMapper.Mapper"/> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<DeviceDto, Device>();

            CreateMap<BaseDeviceRequestDto, Device>()
                .ForMember(d => d.Settings, m => m.MapFrom(r => r.Settings ?? new DeviceSettingsDto()))
                .ForMember(dbo => dbo.BirthDate, m => m.MapFrom(dto => DateTime.Parse(dto.BirthDate)))
                .ForMember(dbo => dbo.ActivationCode, m => m.Ignore());

            CreateMap<CreateDeviceRequestDto, Device>()
                .ForMember(d => d.Settings, m => m.MapFrom(r => r.Settings ?? new DeviceSettingsDto()))
                .ForMember(dbo => dbo.BirthDate, m => m.MapFrom(dto => DateTime.Parse(dto.BirthDate)))
                .ForMember(dbo => dbo.ActivationCode, m => m.Ignore());

            CreateMap<DeviceSettingsDto, DeviceSettings>();

            CreateMap<Device, CreateDeviceResponseDto>()
                .ForMember(dto => dto.BirthDate, m => m.MapFrom(dbo => dbo.BirthDate.HasValue ? dbo.BirthDate.Value.ToString("yyyy-MM-dd") : null));
            
            CreateMap<DeviceSettings, DeviceSettingsDto>()
                .ForMember(d => d.MessagingHubUrl, m => m.UseValue(messagingHubUrl));

            CreateMap<Device, DeviceDto>()
                .ForMember(dto => dto.BirthDate, m => m.MapFrom(dbo => dbo.BirthDate.HasValue ? dbo.BirthDate.Value.ToString("yyyy-MM-dd") : null));

            CreateMap<ActivationDto, Activation>()
                .ForMember(dbo => dbo.BirthDate, m => m.MapFrom(dto => DateTime.Parse(dto.BirthDate)));

            CreateMap<Device, ActivationResponseDto>();
        }
    }
}