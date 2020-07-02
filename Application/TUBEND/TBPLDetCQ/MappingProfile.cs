using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TBPLDetCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, TBPLDet>();
      CreateMap<Update.Command, TBPLDet>();
      CreateMap<TBPLDet, TBPLDetDTO>()
        .ForMember(d => d.NmJeTra,
          opt => opt.MapFrom(s => s.JTrnlKas.NmJeTra.Trim()))
        .ForMember(d => d.KdPersJeTra,
          opt => opt.MapFrom(s => s.JTrnlKas.KdPers.Trim()));
    }
  }
}
