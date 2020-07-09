using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BeritaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Berita>();
      CreateMap<Update.Command, Berita>();
      CreateMap<Berita, BeritaDTO>()
        .ForMember(s => s.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(s => s.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()))
        .ForMember(s => s.NuKeg, opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(s => s.IdPhk3, opt => opt.MapFrom(s => s.Phk3.IdPhk3))
        .ForMember(s => s.NmPhk3, opt => opt.MapFrom(s => s.Phk3.NmPhk3.Trim()))
        .ForMember(s => s.NmKegUnit,
          opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()))
        .ForMember(s => s.NoKontrak,
          opt => opt.MapFrom(s => s.Kontrak.NoKontrak));
    }
  }
}
