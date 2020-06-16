using AutoMapper;
using Domain;

namespace Application.PPKSKPD
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, PPK>();
      CreateMap<Update.Command, PPK>();
    }
  }
}
