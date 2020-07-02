using AutoMapper;
using Domain.DM;

namespace Application.DM.JabTtdCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JabTtd>();
      CreateMap<Update.Command, JabTtd>();
      CreateMap<JabTtd, JabTTdDTO>()
        .ForMember(d => d.KdUnit,
          opt => opt.MapFrom(s => s.DaftUnit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.DaftUnit.NmUnit.Trim()))
        .ForMember(d => d.NIP, opt => opt.MapFrom(s => s.Pegawai.NIP.Trim()))
        .ForMember(d => d.Nama, opt => opt.MapFrom(s => s.Pegawai.Nama.Trim()))
        .ForMember(d => d.KdGol,
          opt => opt.MapFrom(s => s.Pegawai.KdGol.Trim()));
    }
  }
}
