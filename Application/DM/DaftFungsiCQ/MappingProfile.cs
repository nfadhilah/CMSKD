using AutoMapper;
using Domain.DM;

namespace Application.DM.DaftFungsiCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftFungsi>();
      CreateMap<Update.Command, DaftFungsi>();
      CreateMap<DaftFungsi, DaftFungsiDTO>()
        .ForMember(d => d.KdFung, opt => opt.MapFrom(s => s.KdFung.Trim()))
        .ForMember(d => d.NmFung, opt => opt.MapFrom(s => s.NmFung.Trim()));
    }
  }
}
