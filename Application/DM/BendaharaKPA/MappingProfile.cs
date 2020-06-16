using AutoMapper;
using Domain.DM;

namespace Application.DM.BendaharaKPA
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
