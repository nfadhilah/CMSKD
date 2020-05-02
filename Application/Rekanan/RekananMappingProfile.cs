using AutoMapper;
using Domain;

namespace Application.Rekanan
{
  public class RekananMappingProfile : Profile
  {
    public RekananMappingProfile()
    {
      CreateMap<Create.Command, DaftPhk3>();
      CreateMap<Update.Command, DaftPhk3>();
    }
  }
}
