using AutoMapper;
using Domain.DM;

namespace Application.DM.PegawaiCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Pegawai>();
      CreateMap<Update.Command, Pegawai>();
      CreateMap<Pegawai, PegawaiDTO>()
        .ForMember(d => d.KdUnit,
          opt => opt.MapFrom(s => s.DaftUnit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.DaftUnit.NmUnit.Trim()))
        .ForMember(d => d.KdGol, opt => opt.MapFrom(s => s.KdGol.Trim()))
        .ForMember(d => d.NmGol,
          opt => opt.MapFrom(s => s.Golongan.NmGol.Trim()))
        .ForMember(d => d.Pangkat,
          opt => opt.MapFrom(s => s.Golongan.Pangkat.Trim()));
    }
  }
}
