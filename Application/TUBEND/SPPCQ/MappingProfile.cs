using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPP>();
      CreateMap<Update.Command, SPP>();
      CreateMap<CreateSPPLS.Command, SPP>();
      CreateMap<UpdateSPPLS.Command, SPP>();
      CreateMap<SPP, SPPDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()))
        .ForMember(d => d.NoSPD, opt => opt.MapFrom(s => s.SPD.NoSPD.Trim()))
        .ForMember(d => d.TglSPD, opt => opt.MapFrom(s => s.SPD.TglSPD))
        .ForMember(d => d.NmPhk3, opt => opt.MapFrom(s => s.Phk3.NmPhk3.Trim()))
        .ForMember(d => d.NoKontrak, opt => opt.MapFrom(s => s.Kontrak.NoKontrak.Trim()))
        .ForMember(d => d.NmInstPhk3,
          opt => opt.MapFrom(s => s.Phk3.NmInst.Trim()));
    }
  }
}