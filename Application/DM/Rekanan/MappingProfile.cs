using AutoMapper;
using Domain.DM;

namespace Application.DM.Rekanan
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftPhk3>();
      CreateMap<Update.Command, DaftPhk3>();
    }
  }
}
