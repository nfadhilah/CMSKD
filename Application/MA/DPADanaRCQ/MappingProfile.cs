using AutoMapper;
using Domain.MA;

namespace Application.MA.DPADanaRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPADanaR>();
      CreateMap<Update.Command, DPADanaR>();
    }
  }
}
