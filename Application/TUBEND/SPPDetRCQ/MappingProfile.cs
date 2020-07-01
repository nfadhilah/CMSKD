using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetR>();
      CreateMap<Update.Command, SPPDetR>();
      CreateMap<SPPDetR, SPPDetRDTO>()
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()))
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit,
          opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()))
        .ForMember(d => d.NoSPP,
          opt => opt.MapFrom(s => s.SPP.NoSPP.Trim()));
    }
  }
}
