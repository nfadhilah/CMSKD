using AutoMapper;
using Domain.TU;

namespace Application.TU.SP2DPjkCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<SP2DPjk, SP2DPjkDTO>();
    }
  }
}
