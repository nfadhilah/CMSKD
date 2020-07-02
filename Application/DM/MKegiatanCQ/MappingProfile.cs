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
      CreateMap<MKegiatan, MKegiatanDTO>()
        .ForMember(d => d.NuPrgrm, opt => opt.MapFrom(s => s.Program.NuPrgrm.Trim()))
        .ForMember(d => d.NmPrgrm, opt => opt.MapFrom(s => s.Program.NmPrgrm.Trim()));
    }
  }
}
