using AutoMapper;
using Domain.Auth;

namespace Application.Auth.RoleMenu
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<WebOtor, WebOtorDto>()
        .ForMember(d => d.WebRoleNmRole, opt => opt.MapFrom(s => s.WebRole.Role));
    }
  }
}
