using AutoMapper;
using Domain.DM;

namespace Application.DM.RekeningKas
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BkBKas>();
      CreateMap<Update.Command, BkBKas>();
    }
  }
}
