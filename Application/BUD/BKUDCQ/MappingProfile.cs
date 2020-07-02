using AutoMapper;
using Domain.BUD;

namespace Application.BUD.BKUDCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BKUD>();
      CreateMap<Update.Command, BKUD>();
      CreateMap<BKUD, BKUDDTO>()
        .ForMember(d => d.NoSTS, opt => opt.MapFrom(s => s.STS.NoSTS.Trim()))
        .ForMember(d => d.TglSTS, opt => opt.MapFrom(s => s.STS.TglSTS))
        .ForMember(d => d.UraianSTS,
          opt => opt.MapFrom(s => s.STS.Uraian.Trim()))
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()));
    }
  }
}
