using AutoMapper;
using Domain.MA;

namespace Application.MA.DPADanaRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPADanaR>();
      CreateMap<Update.Command, DPADanaR>();
      CreateMap<DPADanaR, DPADanaRDTO>()
        .ForMember(d => d.KdDana,
          opt => opt.MapFrom(s => s.JDana.KdDana.Trim()))
        .ForMember(d => d.NmDana,
          opt => opt.MapFrom(s => s.JDana.NmDana.Trim()));
    }
  }
}
