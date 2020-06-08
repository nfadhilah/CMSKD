using AutoMapper;
using Domain;

namespace Application.JenisDana
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JDana>();
      CreateMap<Update.Command, JDana>();
    }
  }
}
