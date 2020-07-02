using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TBPLDetKegCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, TBPLDetKeg>();
      CreateMap<Update.Command, TBPLDetKeg>();
      CreateMap<TBPLDetKeg, TBPLDetKegDTO>()
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit,
          opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()))
        .ForMember(d => d.NmJeTra,
          opt => opt.MapFrom(s => s.JTrnlKas.NmJeTra.Trim()))
        .ForMember(d => d.KdPersJeTra,
          opt => opt.MapFrom(s => s.JTrnlKas.KdPers.Trim()));
    }
  }
}
