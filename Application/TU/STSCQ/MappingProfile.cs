using AutoMapper;
using Domain.TU;

namespace Application.TU.STSCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, STS>();
    }
  }
}
