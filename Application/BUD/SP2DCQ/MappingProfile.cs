using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2D>();
      CreateMap<Update.Command, SP2D>();
      CreateMap<SP2D, SP2DDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()))
        .ForMember(d => d.NoSPD, opt => opt.MapFrom(s => s.SPD.NoSPD.Trim()))
        .ForMember(d => d.TglSPD, opt => opt.MapFrom(s => s.SPD.TglSPD))
        .ForMember(d => d.KeteranganSPD, opt => opt.MapFrom(s => s.SPD.Keterangan))
        .ForMember(d => d.KdUnit,
          opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()));
    }
  }
}
