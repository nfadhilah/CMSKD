using AutoMapper;
using Domain;

namespace Application.JenisSatuan
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JSatuan>();
      CreateMap<Update.Command, JSatuan>();
    }
  }
}
