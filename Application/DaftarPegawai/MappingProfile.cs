using AutoMapper;
using Domain;

namespace Application.DaftarPegawai
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
