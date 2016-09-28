using System.Linq;
using AutoMapper;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;
using Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers;
using Maestro.Web.Areas.Settings.Models.Admins;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Models.Api.Dtos.Entities;
using Maestro.Web.Models.Api.Dtos.RequestsResponses;
using Maestro.Web.Models.Emails;
using Maestro.Web.Models.Users;
using Maestro.Web.Security;

namespace Maestro.Web.Models.Mappings
{
    /// <summary>
    /// UserMappings.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class UserMappings : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<User, AdminListModel>()
                .ForMember(a => a.EmailAddress, m => m.MapFrom(u => u.Email))
                .ForMember(a => a.Role, m => m.MapFrom(u => u.Role.Name));

            CreateMap<UserViewModel, User>()
                .ForMember(d => d.Role, m => m.Ignore());

            CreateMap<User, UserViewModel>()
                .ForMember(d => d.Role, m => m.MapFrom(s => s.Role.Name));

            CreateMap<User, CreatePrincipalModel>()
                .ForMember(d => d.Username, m => m.MapFrom(s => s.Email));

            CreateMap<User, UpdatePrincipalModel>()
                .ForMember(d => d.Username, m => m.MapFrom(s => s.Email));

            CreateMap<CustomerUserViewModel, User>()
                .ForMember(u => u.RoleId, m => m.Ignore())
                .ForMember(u => u.Role, m => m.Ignore());

            CreateMap<CustomerUserViewModel, CustomerUser>()
                .ForMember(m => m.CustomerUserSites, m =>
                    m.MapFrom(s => s.Sites == null ? null : s.Sites.Select(us => new CustomerUserSite {SiteId = us})));

            CreateMap<User, CustomerUserListViewModel>()
                .ForMember(m => m.Sites, m => m.Ignore());

            CreateMap<CustomerUser, CustomerUserListViewModel>()
                .ForMember(m => m.Sites, m => m.MapFrom(s => s.CustomerUserSites.Select(us => us.SiteId)));

            CreateMap<CustomerUser, CustomerUserViewModel>()
                .ForMember(m => m.Sites, m => m.Ignore())
                .ForMember(m => m.CustomerUserRoleId, o => o.MapFrom(s => s.CustomerUserRoleId));

            CreateMap<ActivationLinkViewModel, ActivateAccountViewModel>();

            CreateMap<CustomerUserRoleToPermissionMapping, CustomerUserRolePermissionViewModel>()
                .ConstructUsing(s => new CustomerUserRolePermissionViewModel(s));

            CreateMap<CustomerUserRole, CustomerUserRoleViewModel>();

            CreateMap<CustomerUser, CustomerUserDto>()
                .ForMember(m => m.FirstName, o => o.MapFrom(cu => cu.FirstName))
                .ForMember(m => m.LastName, o => o.MapFrom(cu => cu.LastName))
                .ForMember(m => m.Email, o => o.MapFrom(cu => cu.Email))
                .ForMember(m => m.Phone, o => o.MapFrom(cu => cu.Phone))
                .ForMember(m => m.IsEnabled, o => o.MapFrom(cu => cu.IsEnabled));

            CreateMap<CreateCustomerUserRequestDto, CustomerUser>()
                .ForMember(m => m.CustomerUserSites,
                    m => m.MapFrom(s => s.Sites.Select(us => new CustomerUserSite {SiteId = us.Id})));

            CreateMap<UpdateCustomerUserRequestDto, CustomerUser>()
                .ForMember(m => m.CustomerUserSites,
                    m => m.MapFrom(s => s.Sites.Select(us => new CustomerUserSite { SiteId = us.Id })));

            CreateMap<CustomerUser, CreatePrincipalModel>()
                .ForMember(d => d.Username, m => m.MapFrom(s => s.Email));

            CreateMap<ActivationEmail, ResetPasswordEmail>();

            CreateMap<User, CareManagerViewModel>()
                .ForMember(m => m.UserId, o => o.MapFrom(s => s.Id));

            CreateMap<User, CareManagerWithPatientsDetailsViewModel>()
                .ForMember(m => m.UserId, o => o.MapFrom(s => s.Id))
                .ForMember(m => m.AssignedPatientStatuses, o => o.Ignore());

            CreateMap<UserAuthData, CareManagerViewModel>();

            CreateMap<LoginViewModel, GetTokenRequest>()
                .ForMember(d => d.Username, m => m.MapFrom(s => s.Email));
            
            CreateMap<ResetPasswordLinkViewModel, ResetPasswordAccountViewModel>();
        }
    }
}