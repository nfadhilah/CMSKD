using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BeritaDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BeritaDetR>();
      CreateMap<Update.Command, BeritaDetR>();
      CreateMap<BeritaDetR, BeritaDetRDTO>()
        .ForMember(d => d.NoBerita, opt => opt.MapFrom(s => s.Berita.NoBerita))
        .ForMember(d => d.KdPer, opt => opt.MapFrom(s => s.Rekening.KdPer))
        .ForMember(d => d.NmPer, opt => opt.MapFrom(s => s.Rekening.NmPer));
    }
  }
}
