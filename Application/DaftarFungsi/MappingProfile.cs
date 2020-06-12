using AutoMapper;
using Domain;

namespace Application.DaftarFungsi
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
