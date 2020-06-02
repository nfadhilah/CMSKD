using AutoMapper;
using Domain;

namespace Application.Bendahara
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Bend>();
      CreateMap<Update.Command, Bend>();
    }
  }
}
