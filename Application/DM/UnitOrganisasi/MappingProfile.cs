using AutoMapper;
using Domain.DM;

namespace Application.DM.UnitOrganisasi
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftUnit>();
      CreateMap<Update.Command, DaftUnit>();
    }
  }
}
