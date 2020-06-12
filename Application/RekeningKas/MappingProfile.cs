using AutoMapper;
using Domain;

namespace Application.RekeningKas
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
