using AutoMapper;
using Domain.MA;

namespace Application.MA.SPDDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPDDetB>();
      CreateMap<Update.Command, SPDDetB>();
    }
  }
}
