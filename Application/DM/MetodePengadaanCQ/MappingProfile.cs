using AutoMapper;
using Domain.DM;

namespace Application.DM.MetodePengadaanCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, MetodePengadaan>();
      CreateMap<Update.Command, MetodePengadaan>();
    }
  }
}