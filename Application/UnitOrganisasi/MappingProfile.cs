using AutoMapper;
using Domain;

namespace Application.UnitOrganisasi
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
