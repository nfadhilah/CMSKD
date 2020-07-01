using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetBDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetBDana>();
      CreateMap<Update.Command, SPPDetBDana>();
      CreateMap<SPPDetBDana, SPPDetBDanaDTO>()
        .ForMember(d => d.KdDana,
          opt => opt.MapFrom(s => s.JDana.KdDana.Trim()))
        .ForMember(d => d.NmDana,
          opt => opt.MapFrom(s => s.JDana.NmDana.Trim()))
        .ForMember(d => d.KetDana, opt => opt.MapFrom(s => s.JDana.Ket.Trim()));
    }
  }
}