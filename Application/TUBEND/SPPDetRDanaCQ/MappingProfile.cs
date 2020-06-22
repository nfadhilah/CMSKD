using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetRDana>();
      CreateMap<Update.Command, SPPDetRDana>();
    }
  }
}
