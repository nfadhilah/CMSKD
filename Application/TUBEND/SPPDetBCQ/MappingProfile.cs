using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetB>();
      CreateMap<Update.Command, SPPDetB>();
    }
  }
}
