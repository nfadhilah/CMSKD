using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetBDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetBDana>();
      CreateMap<Update.Command, SPPDetBDana>();
    }
  }
}
