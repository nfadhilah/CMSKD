using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetRDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetRDana>();
      CreateMap<Update.Command, SP2DDetRDana>();
      CreateMap<SP2DDetRDana, SP2DDetRDanaDTO>()
        .ForMember(d => d.NmDana,
          opt => opt.MapFrom(s => s.JDana.NmDana.Trim()))
        .ForMember(d => d.KetDana, opt => opt.MapFrom(s => s.JDana.Ket.Trim()));
    }
  }
}
