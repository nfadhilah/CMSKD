using AutoMapper;
using Domain.DM;

namespace Application.DM.JUsahaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JUsaha>();
      CreateMap<Update.Command, JUsaha>();
    }
  }
}
