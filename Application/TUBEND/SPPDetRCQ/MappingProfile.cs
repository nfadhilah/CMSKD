using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetR>();
      CreateMap<Update.Command, SPPDetR>();
    }
  }
}
