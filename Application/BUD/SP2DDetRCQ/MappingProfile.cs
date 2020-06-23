using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetR>();
      CreateMap<Update.Command, SP2DDetR>();
    }
  }
}
