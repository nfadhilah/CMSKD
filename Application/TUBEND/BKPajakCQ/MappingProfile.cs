using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BKPajakCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BkPajak>();
      CreateMap<Update.Command, BkPajak>();
      CreateMap<BkPajak, BkPajakDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()));
    }
  }
}