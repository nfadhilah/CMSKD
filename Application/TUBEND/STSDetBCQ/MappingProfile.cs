using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.STSDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, STSDetB>();
      CreateMap<Update.Command, STSDetB>();
      CreateMap<STSDetB, STSDetBDTO>()
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()));
    }
  }
}
