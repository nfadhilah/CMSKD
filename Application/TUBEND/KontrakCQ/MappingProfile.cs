using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.KontrakCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Kontrak>();
      CreateMap<Update.Command, Kontrak>();
      CreateMap<Kontrak, KontrakDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()))
        .ForMember(d => d.NuKeg, opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit, opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()))
        .ForMember(d => d.NmPhk3, opt => opt.MapFrom(s => s.Phk3.NmPhk3.Trim()))
        .ForMember(d => d.NmInstPhk3, opt => opt.MapFrom(s => s.Phk3.NmInst.Trim()));
    }
  }
}
