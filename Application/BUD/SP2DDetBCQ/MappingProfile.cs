using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetB>();
      CreateMap<Update.Command, SP2DDetB>();
      CreateMap<SP2DDetB, SP2DDetBDTO>()
        .ForMember(d => d.NoSP2D, opt => opt.MapFrom(s => s.SP2D.NoSP2D.Trim()))
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()));
    }
  }
}