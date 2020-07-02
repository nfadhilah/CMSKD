using AutoMapper;
using Domain.BUD;

namespace Application.BUD.DPDetCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPDet>();
      CreateMap<Update.Command, DPDet>();
      CreateMap<DPDet, DPDetDTO>()
        .ForMember(d => d.NoSP2D, opt => opt.MapFrom(s => s.SP2D.NoSP2D.Trim()))
        .ForMember(d => d.TglSP2D, opt => opt.MapFrom(s => s.SP2D.TglSP2D))
        .ForMember(d => d.UraianSP2D,
          opt => opt.MapFrom(s => s.SP2D.Keperluan.Trim()));
    }
  }
}
