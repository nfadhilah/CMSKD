using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TagihanDetCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, TagihanDet>();
      CreateMap<Update.Command, TagihanDet>();
      CreateMap<TagihanDet, TagihanDetDTO>().ForMember(d => d.KdPer, opt => opt.MapFrom(s => s.Rekening.KdPer))
        .ForMember(d => d.NmPer, opt => opt.MapFrom(s => s.Rekening.NmPer));
    }
  }
}
