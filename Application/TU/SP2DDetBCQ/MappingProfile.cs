using AutoMapper;
using Domain.TU;

namespace Application.TU.SP2DDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<SP2DDetB, SP2DDetBDTO>()
        .ForMember(d => d.MtgKey, opt => opt.MapFrom(s => s.MtgKey.Trim()))
        .ForMember(d => d.UnitKey, opt => opt.MapFrom(s => s.UnitKey.Trim()))
        .ForMember(d => d.NoSP2D, opt => opt.MapFrom(s => s.NoSP2D.Trim()))
        .ForMember(d => d.KdDana, opt => opt.MapFrom(s => s.KdDana.Trim()))
        .ForMember(d => d.NmDana, opt => opt.MapFrom(s => s.JDana.NmDana.Trim()))
        .ForMember(d => d.NoJeTra, opt => opt.MapFrom(s => s.NoJeTra.Trim()))
        .ForMember(d => d.KdPer, opt => opt.MapFrom(s => s.MatangB.KdPer.Trim()))
        .ForMember(d => d.NmPer, opt => opt.MapFrom(s => s.MatangB .NmPer.Trim()));
    }
  }
}