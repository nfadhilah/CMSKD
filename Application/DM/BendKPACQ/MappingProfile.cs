using AutoMapper;
using Domain.DM;

namespace Application.DM.BendKPACQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BendKPA>();
      CreateMap<Update.Command, BendKPA>();
    }
  }
}
