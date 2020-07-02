using AutoMapper;
using Domain.MA;

namespace Application.MA.DPACQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPA>();
      CreateMap<Update.Command, DPA>();
      CreateMap<DPA, DPADTO>()
        .ForMember(d => d.KdUnit,
          opt => opt.MapFrom(s => s.DaftUnit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.DaftUnit.NmUnit.Trim()));
    }
  }
}
