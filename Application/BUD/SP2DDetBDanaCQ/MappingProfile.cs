using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetBDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetBDana>();
      CreateMap<Update.Command, SP2DDetBDana>();
    }
  }
}
