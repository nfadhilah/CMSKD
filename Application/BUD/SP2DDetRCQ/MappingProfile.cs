using AutoMapper;
using Domain.BUD;

namespace Application.BUD.SP2DDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SP2DDetR>();
      CreateMap<Update.Command, SP2DDetR>();
      CreateMap<SP2DDetR, SP2DDetRDTO>()
        .ForMember(d => d.NoSP2D, opt => opt.MapFrom(s => s.SP2D.NoSP2D.Trim()))
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit,
          opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()))
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()));
    }
  }
}