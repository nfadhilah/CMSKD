using AutoMapper;
using Domain.MA;

namespace Application.MA.DPABlnRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPABlnR>();
      CreateMap<Update.Command, DPABlnR>();
    }
  }
}
