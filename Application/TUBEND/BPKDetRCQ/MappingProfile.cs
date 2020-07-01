using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPKDetR>();
      CreateMap<Update.Command, BPKDetR>();
      CreateMap<BPKDetR, BPKDetRDTO>()
        .ForMember(d => d.NoBPK, opt => opt.MapFrom(s => s.BPK.NoBPK))
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit,
          opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()))
        .ForMember(d => d.KdPer, opt => opt.MapFrom(s => s.Rekening.KdPer))
        .ForMember(d => d.NmPer, opt => opt.MapFrom(s => s.Rekening.NmPer));
    }
  }
}
