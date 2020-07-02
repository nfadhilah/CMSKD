using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.STSDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, STSDetR>();
      CreateMap<Update.Command, STSDetR>();
      CreateMap<STSDetR, STSDetRDTO>()
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.Rekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.Rekening.NmPer.Trim()))
        .ForMember(d => d.NuKeg, opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit, opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()));
    }
  }
}
