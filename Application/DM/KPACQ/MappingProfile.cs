using AutoMapper;
using Domain.DM;

namespace Application.DM.KPACQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, KPA>();
      CreateMap<Update.Command, KPA>();
      CreateMap<KPA, KPADTO>()
        .ForMember(d => d.NIP, opt => opt.MapFrom(s => s.Pegawai.NIP.Trim()))
        .ForMember(d => d.Nama, opt => opt.MapFrom(s => s.Pegawai.Nama.Trim()))
        .ForMember(d => d.KdGol,
          opt => opt.MapFrom(s => s.Pegawai.KdGol.Trim()));
    }
  }
}