using AutoMapper;
using Domain.PM;

namespace Application.PM.PaketRUPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, PaketRUP>();
      CreateMap<Update.Command, PaketRUP>();
      CreateMap<PaketRUP, PaketRUPDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()))
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.Keg.NuKeg.Trim()))
        .ForMember(d => d.NmKeg,
          opt => opt.MapFrom(s => s.Keg.NmKegUnit.Trim()));
    }
  }
}