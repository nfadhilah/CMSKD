using AutoMapper;
using Domain.BUD;

namespace Application.BUD.DPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DP>();
      CreateMap<Update.Command, DP>();
      CreateMap<DP, DPDTO>();
    }
  }
}
