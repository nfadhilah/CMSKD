using Domain.Auth;

namespace Application.Auth.User
{
  public class MappingProfile : AutoMapper.Profile
  {
    public MappingProfile()
    {
      CreateMap<AppUser, AppUserDto>();
      CreateMap<Register.Command, AppUser>()
        .ForMember(d => d.Password, opt => opt.Ignore());
    }
  }
}
