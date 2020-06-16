using AutoMapper;
using Domain.DM;

namespace Application.DM.PPKSKPD
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
