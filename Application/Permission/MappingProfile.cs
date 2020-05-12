using AutoMapper;

namespace Application.Permission
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Domain.Permission, PermissionDto>();
    }
  }
}
