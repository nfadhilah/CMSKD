using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPK>();
      CreateMap<Update.Command, BPK>();
      CreateMap<BPK, BPKDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()))
        .ForMember(d => d.NmPhk3, opt => opt.MapFrom(s => s.Phk3.NmPhk3))
        .ForMember(d => d.NPWP, opt => opt.MapFrom(s => s.Phk3.NPWP))
        .ForMember(d => d.UraianBayar,
          opt => opt.MapFrom(s => s.JBayar.UraianBayar))
        .ForMember(d => d.NoBerita, opt => opt.MapFrom(s => s.Berita.NoBerita))
        .ForMember(d => d.TglBA, opt => opt.MapFrom(s => s.Berita.TglBA));
    }
  }
}
