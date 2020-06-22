using AutoMapper;
using Domain.MA;

namespace Application.MA.DPADanaBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPADanaB>();
      CreateMap<Update.Command, DPADanaB>();
    }
  }
}
