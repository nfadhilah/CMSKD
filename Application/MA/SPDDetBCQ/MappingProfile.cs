using AutoMapper;
using Domain.MA;

namespace Application.MA.SPDDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPDDetB>();
      CreateMap<Update.Command, SPDDetB>();
      CreateMap<SPDDetB, SPDDetBDTO>()
        .ForMember(d => d.NoSPD, opt => opt.MapFrom(s => s.SPD.NoSPD.Trim()))
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()));
    }
  }
}
