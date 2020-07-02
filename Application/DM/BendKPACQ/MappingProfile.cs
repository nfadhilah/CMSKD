using AutoMapper;
using Domain.DM;

namespace Application.DM.BendKPACQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BendKPA>();
      CreateMap<Update.Command, BendKPA>();
      CreateMap<BendKPA, BendKPADTO>()
        .ForMember(d => d.NIP, opt => opt.MapFrom(s => s.Pegawai.NIP.Trim()))
        .ForMember(d => d.Nama, opt => opt.MapFrom(s => s.Pegawai.Nama.Trim()));
    }
  }
}
