using AutoMapper;
using Domain.MA;

namespace Application.MA.SPDDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPDDetR>();
      CreateMap<Update.Command, SPDDetR>();
    }
  }
}
