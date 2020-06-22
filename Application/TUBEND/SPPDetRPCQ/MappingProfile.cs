using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetRPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetRP>();
      CreateMap<Update.Command, SPPDetRP>();
    }
  }
}
