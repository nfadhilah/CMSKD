using AutoMapper;
using Domain;

namespace Application.JenisTransaksi
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
