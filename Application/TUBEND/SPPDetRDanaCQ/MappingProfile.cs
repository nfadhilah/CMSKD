using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPPDetRDana>();
      CreateMap<Update.Command, SPPDetRDana>();
      CreateMap<SPPDetRDana, SPPDetRDanaDTO>()
        .ForMember(d => d.KdDana,
          opt => opt.MapFrom(s => s.JDana.KdDana.Trim()))
        .ForMember(d => d.NmDana,
          opt => opt.MapFrom(s => s.JDana.NmDana.Trim()))
        .ForMember(d => d.KetDana, opt => opt.MapFrom(s => s.JDana.Ket.Trim()));
    }
  }
}
