using AutoMapper;
using Domain.DM;

namespace Application.DM.DaftUnitCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftUnit>();
      CreateMap<Update.Command, DaftUnit>();
      CreateMap<DaftUnit, DaftUnitDTO>();
    }
  }
}
