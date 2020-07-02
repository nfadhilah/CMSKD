using AutoMapper;
using Domain.DM;

namespace Application.DM.BendCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Bend>();
      CreateMap<Update.Command, Bend>();
      CreateMap<Bend, BendDTO>()
        .ForMember(d => d.RekBend, opt => opt.MapFrom(s => s.RekBend.Trim()))
        .ForMember(d => d.NPWPBend, opt => opt.MapFrom(s => s.NPWPBend.Trim()))
        .ForMember(d => d.Nama, opt => opt.MapFrom(s => s.Peg.Nama.Trim()))
        .ForMember(d => d.NIP, opt => opt.MapFrom(s => s.Peg.NIP.Trim()))
        .ForMember(d => d.KdBank, opt => opt.MapFrom(s => s.Bank.KdBank.Trim()))
        .ForMember(d => d.NmBank,
          opt => opt.MapFrom(s => s.Bank.NmBank.Trim()))
        .ForMember(d => d.NmCabBank, opt => opt.MapFrom(s => s.NmCabBank.Trim()));
    }
  }
}