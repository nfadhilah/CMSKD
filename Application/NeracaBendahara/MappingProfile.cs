using AutoMapper;
using Domain;

namespace Application.NeracaBendahara
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, NrcBend>();
      CreateMap<Update.Command, NrcBend>();
    }
  }
}
