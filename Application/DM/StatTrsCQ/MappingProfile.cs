using AutoMapper;
using Domain.DM;

namespace Application.DM.StatTrsCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, StatTrs>();
      CreateMap<Update.Command, StatTrs>();
    }
  }
}
