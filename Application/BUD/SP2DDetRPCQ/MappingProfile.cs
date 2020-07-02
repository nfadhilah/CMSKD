using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetRPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetRP>();
      CreateMap<Update.Command, SP2DDetRP>();
      CreateMap<SP2DDetRP, SP2DDetRPDTO>()
        .ForMember(d => d.KdPajak, opt => opt.MapFrom(s => s.Pajak.KdPajak.Trim()))
        .ForMember(d => d.NmPajak, opt => opt.MapFrom(s => s.Pajak.NmPajak.Trim()))
        .ForMember(d => d.UraianPajak, opt => opt.MapFrom(s => s.Pajak.Uraian.Trim()));
    }
  }
}
