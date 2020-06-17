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
    }
  }
}
