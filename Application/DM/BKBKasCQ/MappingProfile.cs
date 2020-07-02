using AutoMapper;
using Domain.DM;

namespace Application.DM.BKBKasCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BkBKas>();
      CreateMap<Update.Command, BkBKas>();
      CreateMap<BkBKas, BKBKasDTO>()
        .ForMember(d => d.KdUnit,
          opt => opt.MapFrom(s => s.DaftUnit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.DaftUnit.NmUnit.Trim()))
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.DaftRekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.DaftRekening.KdPer.Trim()));
    }
  }
}
