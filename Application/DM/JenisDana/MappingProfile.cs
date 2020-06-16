using AutoMapper;
using Domain.DM;

namespace Application.DM.JenisDana
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
