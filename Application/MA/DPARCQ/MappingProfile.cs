using AutoMapper;
using Domain.MA;

namespace Application.MA.DPARCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPAR>();
      CreateMap<Update.Command, DPAR>();
    }
  }
}
