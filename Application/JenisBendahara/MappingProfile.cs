using AutoMapper;
using Domain;

namespace Application.JenisBendahara
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBend>();
      CreateMap<Update.Command, JBend>();
    }
  }
}
