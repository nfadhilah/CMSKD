using AutoMapper;
using Domain.DM;

namespace Application.DM.PPKCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, PPK>();
      CreateMap<Update.Command, PPK>();
      CreateMap<PPK, PPKDTO>()
        .ForMember(d => d.Nama, opt => opt.MapFrom(s => s.Pegawai.Nama.Trim()))
        .ForMember(d => d.NIP, opt => opt.MapFrom(s => s.Pegawai.NIP.Trim()))
        .ForMember(d => d.Jabatan,
          opt => opt.MapFrom(s => s.Pegawai.Jabatan.Trim()))
        .ForMember(d => d.KdGol,
          opt => opt.MapFrom(s => s.Pegawai.KdGol.Trim()));
    }
  }
}
