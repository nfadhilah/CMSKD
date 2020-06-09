using AutoMapper;
using Domain;

namespace Application.JenisAkun
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JnsAkun>();
      CreateMap<Update.Command, JnsAkun>();
    }
  }
}
