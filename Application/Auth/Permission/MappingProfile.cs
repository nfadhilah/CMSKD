using AutoMapper;

namespace Application.Auth.Permission
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Domain.Auth.Permission, PermissionDto>();
    }
  }
}
