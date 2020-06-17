using AutoMapper;
using Domain.DM;

namespace Application.DM.JDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JDana>();
      CreateMap<Update.Command, JDana>();
    }
  }
}
