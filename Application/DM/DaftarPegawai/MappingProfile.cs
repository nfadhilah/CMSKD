using AutoMapper;
using Domain.DM;

namespace Application.DM.DaftarPegawai
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Pegawai>();
      CreateMap<Update.Command, Pegawai>();
    }
  }
}
