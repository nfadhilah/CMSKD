using AutoMapper;
using Domain.DM;

namespace Application.DM.JBKasCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBKas>();
      CreateMap<Update.Command, JBKas>();
    }
  }
}
