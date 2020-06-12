using AutoMapper;
using Domain;

namespace Application.Kegiatan
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, MKegiatan>();
      CreateMap<Update.Command, MKegiatan>();
    }
  }
}
