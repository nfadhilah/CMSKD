using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetRPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetRP>();
      CreateMap<Update.Command, SPPDetRP>();
      CreateMap<SPPDetRP, SPPDetRPDTO>()
        .ForMember(d => d.NmPajak,
          opt => opt.MapFrom(s => s.Pajak.NmPajak.Trim()))
        .ForMember(d => d.KdPajak,
          opt => opt.MapFrom(s => s.Pajak.KdPajak.Trim()))
        .ForMember(d => d.UraianPajak,
          opt => opt.MapFrom(s => s.Pajak.Uraian.Trim()));
    }
  }
}
