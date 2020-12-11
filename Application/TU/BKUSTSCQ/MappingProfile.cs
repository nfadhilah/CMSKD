using AutoMapper;
using Domain.TU;

namespace Application.TU.BKUSTSCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BKUSTS>();
    }
  }
}
