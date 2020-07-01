using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BkPajakDetStrCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BkPajakDetStr>();
      CreateMap<Update.Command, BkPajakDetStr>();
      CreateMap<BkPajakDetStr, BkPajakDetStrDTO>()
        .ForMember(d => d.KdPajak, opt => opt.MapFrom(s => s.Pajak.KdPajak))
        .ForMember(d => d.NmPajak, opt => opt.MapFrom(s => s.Pajak.NmPajak))
        .ForMember(d => d.NoBkPajak,
          opt => opt.MapFrom(s => s.BkPajak.NoBkPajak))
        .ForMember(d => d.IdBilling,
          opt => opt.MapFrom(s => s.IdBilling.Trim()))
        .ForMember(d => d.NTPN, opt => opt.MapFrom(s => s.NTPN.Trim()))
        .ForMember(d => d.NTB, opt => opt.MapFrom(s => s.NTB.Trim()));
    }
  }
}
