using AutoMapper;
using Domain.MA;

namespace Application.MA.DPADetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPADetB>();
      CreateMap<Update.Command, DPADetB>();
      CreateMap<DPADetB, DPADetBDTO>();
    }
  }
}
