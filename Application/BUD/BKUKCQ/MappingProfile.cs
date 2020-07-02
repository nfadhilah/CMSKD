using AutoMapper;
using Domain.BUD;

namespace Application.BUD.BKUKCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BKUK>();
      CreateMap<Update.Command, BKUK>();
      CreateMap<BKUK, BKUKDTO>()
        .ForMember(d => d.NoSP2D, opt => opt.MapFrom(s => s.SP2D.NoSP2D.Trim()))
        .ForMember(d => d.TglSP2D, opt => opt.MapFrom(s => s.SP2D.TglSP2D))
        .ForMember(d => d.UraianSP2D,
          opt => opt.MapFrom(s => s.SP2D.Keperluan.Trim()))
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()));
      ;
    }
  }
}