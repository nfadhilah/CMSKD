using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.STSDetDCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, STSDetD>();
      CreateMap<Update.Command, STSDetD>();
      CreateMap<STSDetD, STSDetDDTO>()
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()));
    }
  }
}
