using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2D>();
      CreateMap<Update.Command, SP2D>();
    }
  }
}
