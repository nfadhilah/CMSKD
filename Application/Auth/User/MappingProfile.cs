using Domain.Auth;

namespace Application.Auth.User
{
  public class MappingProfile : AutoMapper.Profile
  {
    public MappingProfile()
    {
      CreateMap<WebUser, WebUserDto>();
      CreateMap<Register.Command, WebUser>()
        .ForMember(d => d.Pwd, opt => opt.Ignore());
    }
  }
}
