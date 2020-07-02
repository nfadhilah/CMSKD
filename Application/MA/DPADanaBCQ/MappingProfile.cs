using AutoMapper;
using Domain.MA;

namespace Application.MA.DPADanaBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPADanaB>();
      CreateMap<Update.Command, DPADanaB>();
      CreateMap<DPADanaB, DPADanaBDTO>()
        .ForMember(d => d.KdDana,
          opt => opt.MapFrom(s => s.JDana.KdDana.Trim()))
        .ForMember(d => d.NmDana,
          opt => opt.MapFrom(s => s.JDana.NmDana.Trim()));
    }
  }
}
