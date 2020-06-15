using AutoMapper;
using Domain;

namespace Application.BendaharaKPA
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
