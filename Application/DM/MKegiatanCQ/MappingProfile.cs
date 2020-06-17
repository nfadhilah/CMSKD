using AutoMapper;
using Domain.DM;

namespace Application.DM.MKegiatanCQ
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
