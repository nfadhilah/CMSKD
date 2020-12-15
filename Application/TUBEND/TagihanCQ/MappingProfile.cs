using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TagihanCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Tagihan>();
      CreateMap<Update.Command, Tagihan>();
      CreateMap<Tagihan, TagihanDTO>()
        .ForMember(d => d.NoKontrak, opt => opt.MapFrom(s => s.Kontrak.NoKontrak))
        .ForMember(d => d.NmPhk3, opt => opt.MapFrom(s => s.DaftPhk3.NmPhk3));
    }
  }
}
