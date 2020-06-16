using AutoMapper;
using Domain.DM;

namespace Application.DM.JenisTransaksi
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JTrans>();
      CreateMap<Update.Command, JTrans>();
    }
  }
}
