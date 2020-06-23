using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetRPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetRP>();
      CreateMap<Update.Command, SP2DDetRP>();
    }
  }
}
