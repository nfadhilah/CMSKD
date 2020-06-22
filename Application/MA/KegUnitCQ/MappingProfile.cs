using AutoMapper;
using Domain.MA;

namespace Application.MA.KegUnitCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, KegUnit>();
      CreateMap<Update.Command, KegUnit>();
    }
  }
}
