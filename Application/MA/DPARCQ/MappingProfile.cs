using AutoMapper;
using Domain.MA;

namespace Application.MA.DPARCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPAR>();
      CreateMap<Update.Command, DPAR>();
      CreateMap<DPAR, DPARDTO>()
        .ForMember(d => d.NoDPA, opt => opt.MapFrom(s => s.DPA.NoDPA.Trim()))
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.Kegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit,
          opt => opt.MapFrom(s => s.Kegiatan.NmKegUnit.Trim()))
        .ForMember(d => d.KdPer,
          opt => opt.MapFrom(s => s.DaftRekening.KdPer.Trim()))
        .ForMember(d => d.NmPer,
          opt => opt.MapFrom(s => s.DaftRekening.NmPer.Trim()));
    }
  }
}
