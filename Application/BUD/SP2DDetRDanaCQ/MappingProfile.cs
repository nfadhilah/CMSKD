using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetRDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetRDana>();
      CreateMap<Update.Command, SP2DDetRDana>();
    }
  }
}
