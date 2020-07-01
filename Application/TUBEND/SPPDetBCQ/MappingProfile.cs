using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetB>();
      CreateMap<Update.Command, SPPDetB>();
      CreateMap<SPPDetB, SPPDetBDTO>()
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()));
    }
  }
}
