using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKDetRDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPKDetRDana>();
      CreateMap<Update.Command, BPKDetRDana>();
      CreateMap<BPKDetRDana, BPKDetRDanaDTO>()
        .ForMember(d => d.NmDana, opt => opt.MapFrom(s => s.JDana.NmDana))
        .ForMember(d => d.KetDana, opt => opt.MapFrom(s => s.JDana.Ket));
    }
  }
}