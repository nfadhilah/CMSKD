using AutoMapper;
using Domain.DM;

namespace Application.DM.BendCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Bend>();
      CreateMap<Update.Command, Bend>();
    }
  }
}
