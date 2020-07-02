using AutoMapper;
using Domain.MA;

namespace Application.MA.SPDDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPDDetR>();
      CreateMap<Update.Command, SPDDetR>();
      CreateMap<SPDDetR, SPDDetRDTO>()
        .ForMember(d => d.NoSPD, opt => opt.MapFrom(s => s.SPD.NoSPD.Trim()))
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit,
          opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()))
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()));
    }
  }
}
