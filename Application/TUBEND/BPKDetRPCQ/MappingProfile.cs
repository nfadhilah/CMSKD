using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKDetRPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPKDetRP>();
      CreateMap<Update.Command, BPKDetRP>();
      CreateMap<BPKDetRP, BPKDetRPDTO>()
        .ForMember(d => d.KdPajak, opt => opt.MapFrom(s => s.Pajak.KdPajak.Trim()))
        .ForMember(d => d.NmPajak, opt => opt.MapFrom(s => s.Pajak.NmPajak.Trim()))
        .ForMember(d => d.UraianPajak, opt => opt.MapFrom(s => s.Pajak.Uraian.Trim()));
    }
  }
}
