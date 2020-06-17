using AutoMapper;
using Domain.DM;

namespace Application.DM.DaftFungsiCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftFungsi>();
      CreateMap<Update.Command, DaftFungsi>();
    }
  }
}
