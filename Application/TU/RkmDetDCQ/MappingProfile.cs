using AutoMapper;
using Domain.TU;

namespace Application.TU.RkmDetDCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, RkmDetD>();
    }
  }
}
