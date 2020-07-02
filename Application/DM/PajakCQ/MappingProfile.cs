using AutoMapper;
using Domain.DM;

namespace Application.DM.PajakCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Pajak>();
      CreateMap<Update.Command, Pajak>();
      CreateMap<Pajak, PajakDTO>()
        .ForMember(d => d.KdPajak, opt => opt.MapFrom(s => s.KdPajak.Trim()))
        .ForMember(d => d.NmPajak, opt => opt.MapFrom(s => s.NmPajak.Trim()))
        .ForMember(d => d.NmPajak, opt => opt.MapFrom(s => s.Uraian.Trim()));
    }
  }
}
