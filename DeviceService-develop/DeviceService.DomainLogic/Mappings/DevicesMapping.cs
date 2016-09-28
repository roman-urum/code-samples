using AutoMapper;
using DeviceService.Domain.Entities;

namespace DeviceService.DomainLogic.Mappings
{
    /// <summary>
    /// DevicesMapping.
    /// </summary>
    public class DevicesMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        ///             Avoid calling the <see cref="T:AutoMapper.Mapper"/> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Mapper.CreateMap<Device, Device>()
                .ForMember(d => d.DeviceId, m => m.Ignore())
                .ForMember(d => d.DeviceType, m => m.Ignore())
                .ForMember(d => d.DeviceIdType, m => m.Ignore())
                .ForMember(d => d.Status, m => m.Ignore())
                .ForMember(d => d.ActivationCode, m => m.Ignore())
                .ForMember(d => d.Thumbprint, m => m.Ignore())
                .ForMember(d => d.Certificate, m => m.Ignore())
                .ForMember(d => d.IsDeleted, m => m.Ignore());
        }
    }
}