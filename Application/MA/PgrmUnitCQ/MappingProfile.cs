using AutoMapper;
using Domain.MA;

namespace Application.MA.PgrmUnitCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, PgrmUnit>();
      CreateMap<Update.Command, PgrmUnit>();
      CreateMap<PgrmUnit, PgrmUnitDTO>()
        .ForMember(d => d.KdUnit,
          opt => opt.MapFrom(s => s.DaftUnit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.DaftUnit.NmUnit.Trim()))
        .ForMember(d => d.NuPrgrm,
          opt => opt.MapFrom(s => s.MPgrm.NuPrgrm.Trim()))
        .ForMember(d => d.NmPrgrm,
          opt => opt.MapFrom(s => s.MPgrm.NmPrgrm.Trim()));
    }
  }
}
