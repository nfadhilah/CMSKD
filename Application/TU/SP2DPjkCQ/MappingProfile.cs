using AutoMapper;
using Domain.TU;

namespace Application.TU.SP2DPjkCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<SP2DPjk, SP2DPjkDTO>()
        .ForMember(d => d.UnitKey, opt => opt.MapFrom(s => s.UnitKey.Trim()))
        .ForMember(d => d.NoSP2D, opt => opt.MapFrom(s => s.NoSP2D.Trim()))
        .ForMember(d => d.KdPajak, opt => opt.MapFrom(s => s.JPajak.KdPajak.Trim()))
        .ForMember(d => d.PjkKey, opt => opt.MapFrom(s => s.JPajak.PjkKey.Trim()))
        .ForMember(d => d.NmPajak, opt => opt.MapFrom(s => s.JPajak.NmPajak.Trim()));
    }
  }
}
