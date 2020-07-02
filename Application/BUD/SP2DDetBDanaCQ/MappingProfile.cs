using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetBDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetBDana>();
      CreateMap<Update.Command, SP2DDetBDana>();
      CreateMap<SP2DDetBDana, SP2DDetBDanaDTO>()
        .ForMember(d => d.NmDana,
          opt => opt.MapFrom(s => s.JDana.NmDana.Trim()))
        .ForMember(d => d.KetDana, opt => opt.MapFrom(s => s.JDana.Ket.Trim()));
    }
  }
}
