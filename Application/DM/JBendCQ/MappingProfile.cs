using AutoMapper;
using Domain.DM;

namespace Application.DM.JBendCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBend>();
      CreateMap<Update.Command, JBend>();
    }
  }
}
