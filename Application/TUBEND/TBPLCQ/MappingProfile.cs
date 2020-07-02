using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TBPLCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, TBPL>();
      CreateMap<Update.Command, TBPL>();
      CreateMap<TBPL, TBPLDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()));
    }
  }
}