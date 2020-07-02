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
      CreateMap<KegUnit, KegUnitDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()))
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.MKegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit,
          opt => opt.MapFrom(s => s.MKegiatan.NmKegUnit.Trim()))
        .ForMember(d => d.NuPrgrm,
          opt => opt.MapFrom(s => s.MPgrm.NuPrgrm.Trim()))
        .ForMember(d => d.NmPrgrm,
          opt => opt.MapFrom(s => s.MPgrm.NmPrgrm.Trim()))
        .ForMember(d => d.NIP, opt => opt.MapFrom(s => s.Pegawai.NIP.Trim()))
        .ForMember(d => d.Nama, opt => opt.MapFrom(s => s.Pegawai.Nama.Trim()));
    }
  }
}
