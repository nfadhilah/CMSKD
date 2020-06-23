using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetB>();
      CreateMap<Update.Command, SP2DDetB>();
    }
  }
}
