using AutoMapper;
using Domain.MA;

namespace Application.MA.SPDCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPD>();
      CreateMap<Update.Command, SPD>();
      CreateMap<SPD, SPDDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()));
    }
  }
}
