using AutoMapper;
using Domain.MA;

namespace Application.MA.DPADetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPADetR>();
      CreateMap<Update.Command, DPADetR>();
    }
  }
}
