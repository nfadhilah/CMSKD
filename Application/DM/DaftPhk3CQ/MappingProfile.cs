using AutoMapper;
using Domain.DM;

namespace Application.DM.DaftPhk3CQ
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
