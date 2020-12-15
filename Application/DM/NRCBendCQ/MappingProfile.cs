using AutoMapper;
using Domain.DM;

namespace Application.DM.NRCBendCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, NrcBend>();
      CreateMap<Update.Command, NrcBend>();
      CreateMap<NrcBend, NrcBendDTO>()
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.DaftRekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.DaftRekening.NmPer.Trim()));
    }
  }
}
